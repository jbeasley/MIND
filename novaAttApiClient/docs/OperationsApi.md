# IO.NovaAttSwagger.Api.OperationsApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OperationsGet**](OperationsApi.md#operationsget) | **GET** /operations | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.


<a name="operationsget"></a>
# **OperationsGet**
> Operations OperationsGet ()

This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.

This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class OperationsGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new OperationsApi();

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                Operations result = apiInstance.OperationsGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OperationsApi.OperationsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**Operations**](Operations.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

