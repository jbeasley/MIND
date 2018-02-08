using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Net;

namespace SCM.Services
{
    /// <summary>
    /// Service to handle sync requests to the network service orchestrator (NSO)
    /// </summary>
    public class NetworkSyncService : INetworkSyncService
    {

        /// <summary>
        /// URI for access to the NSO server. This is for testing only. Do NOT do this in production.
        /// Instead, store the URL in the appsettings.json file.
        /// </summary>
        private string NetworkBaseUri { get; } = "http://10.65.127.30:8088/api/running/services";

        /// <summary>
        /// Synchronise to the network server. Data is sent to the server using the 
        /// HTTP PUT method.
        /// </summary>
        /// <param name="item">
        /// Object data to send to the server.
        /// </param>
        /// <param name="resource">
        /// The server-side resource location
        /// </param>
        /// <returns></returns>
        public async Task<NetworkSyncServiceResult> SyncNetworkAsync(Object item, string resource)
        {
            return await SyncNetworkAsync(item, resource, HttpMethod.Put);
        }

        /// <summary>
        /// Synchronise to the network server. Data is sent to the server using
        /// a specified HTTP method.
        /// </summary>
        /// <param name="item">
        /// Object data to send to the server.
        /// </param>
        /// <param name="resource">
        /// The server-side resource location
        /// </param>
        /// <param name="method">
        /// The HTTP method (e.g. POST, PATCH ..)
        /// </param>
        /// <returns></returns>
        public async Task<NetworkSyncServiceResult> SyncNetworkAsync(Object item, string resource, HttpMethod method)
        { 
            // Serialize payload object to XML and construct HTTP request
            var xmlStr = ObjectToXmlString(item);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(NetworkBaseUri + resource),
                Method = method,
                Content = new StringContent(xmlStr, Encoding.UTF8, "application/vnd.yang.data+xml")
            };

            // Send the request
            var response = await GetNetworkHttpResponse(request);

            // Return object payload item to sender for any further processing
            response.Item = item;

            return response;
        }

        /// <summary>
        /// Check sync of a resource against a supplied data object/
        /// </summary>
        /// <param name="item">
        /// The data object to verify network sync against
        /// </param>
        /// <param name="resource">
        /// The server-side resource location
        /// </param>
        /// <returns></returns>
        public async Task<NetworkSyncServiceResult> CheckNetworkSyncAsync(Object item, string resource)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(NetworkBaseUri + resource + "?deep"),
                Method = HttpMethod.Get
            };

            NetworkSyncServiceResult response;
            try
            {
                response = await GetNetworkHttpResponse(request);

                // Return item to sender for any further processing

                response.Item = item;

                // Parse string response to an object then to xml to perform a deep-equals check.
                // This process normalises the data. The resulting xml trees must also be sorted 
                // in order to check equality using deep-equals.

                var objectItem = XmlStringToObject(response.Content, item.GetType());

                // Perform comparison

                var xmlTree = XElement.Parse(ObjectToXmlString(objectItem));
                var sortedXmlTree = Sort(xmlTree);
                var xmlTreeToCompare = XElement.Parse(ObjectToXmlString(item));
                var sortedXmlTreeToCompare = Sort(xmlTreeToCompare);

                response.IsSuccess = XNode.DeepEquals(sortedXmlTree, sortedXmlTreeToCompare);
            }

            catch (NetworkServiceFailureException ex)
            {
                // Ignore 'NotFound' response from network server.
                // This allows an operation to check sync for a collection of objects 
                // to continue processing

                response = ex.NetworkServiceResult;
                if (response.HttpStatusCode != HttpStatusCode.NotFound)
                {
                  throw;
                }
            }

            return response;
        }

        /// <summary>
        /// Delete a resource from the network.
        /// </summary>
        /// <param name="resource">
        /// The server-side resource location.
        /// </param>
        /// <returns></returns>
        public async Task<NetworkSyncServiceResult> DeleteFromNetworkAsync(string resource)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(NetworkBaseUri + resource),
                Method = HttpMethod.Delete
            };

            NetworkSyncServiceResult response;
            try
            {
                response = await GetNetworkHttpResponse(request);
            }

            catch (NetworkServiceFailureException ex)
            {
                // If the exception was caused by a NotFound response from the network server
                // then we can assume  that the resource has never been synced to the server.
                // In this case treat the delete operation as successful.
  
                response = ex.NetworkServiceResult;
                if (response.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    response.IsSuccess = true;
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        /// <summary>
        /// Helper to serilalize an object to an XML string. This process prepares 
        /// a payload object for sending to the network server.
        /// </summary>
        /// <param name="objectItem"></param>
        /// <returns></returns>
        private string ObjectToXmlString(Object objectItem)
        {
            XmlSerializer serializer = new XmlSerializer(objectItem.GetType());
            using (MemoryStream memStream = new MemoryStream())
            {

                var writer = new StreamWriter(memStream);
                serializer.Serialize(writer, objectItem);

                return Encoding.ASCII.GetString(memStream.ToArray());
            }
        }

        /// <summary>
        /// Helper to deserialize an XML string into an object.
        /// </summary>
        /// <param name="stringItem"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private Object XmlStringToObject(string stringItem, Type objectType)
        {
            XmlSerializer serializer = new XmlSerializer(objectType);
            using (TextReader reader = new StringReader(stringItem))
            {
               return serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Helper to send an HTTP request and process the response from the network server.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        private async Task<NetworkSyncServiceResult> GetNetworkHttpResponse(HttpRequestMessage httpRequest)
        {
            var result = new NetworkSyncServiceResult();
            var client = new HttpClient();

            // Authorisation header here is statically set. Do NOT do this in production!
            // Code published to repositories such as GIT will allow the auth settings to be visible. 
            // Authentication settings must be sourced from a secure location.

            client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46YWRtaW4=");

            var response = await client.SendAsync(httpRequest);
            result.Content = await response.Content.ReadAsStringAsync();
            result.HttpStatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                return result;
            }
            else
            {
                throw new NetworkServiceFailureException($"The network server returned an error " +
                    $"({response.StatusCode}, {response.ReasonPhrase}).", result);
            }
        }

        /// <summary>
        /// Recursively sorts the children of an XML element
        /// using the child element name followed by value as sort parameters.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static XElement Sort(XElement element)
        {
            XElement newElement = new XElement(element.Name,
                    from child in element.Elements()
                    orderby child.Name.ToString()
                    orderby child.Value.ToString()
                    select Sort(child));

            if (element.HasAttributes)
            {
                foreach (XAttribute attrib in element.Attributes())
                {
                    newElement.SetAttributeValue(attrib.Name, attrib.Value);
                }
            }

            if (!element.HasElements)
            {
                newElement.SetValue(element.Value);
            }

            return newElement;
        }
    }
}
