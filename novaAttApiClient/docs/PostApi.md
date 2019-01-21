# IO.NovaAttSwagger.Api.PostApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost**](PostApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthpost) | **POST** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth | Sevice Class bandwidth and units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post**](PostApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4post) | **POST** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4 | Logical Attachment IPv4 configuration
[**DataAttachmentAttachmentPost**](PostApi.md#dataattachmentattachmentpost) | **POST** /data/services/attachment:attachment | Attachment
[**DataPost**](PostApi.md#datapost) | **POST** /data | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.


<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthpost"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost scBandwidth)

Sevice Class bandwidth and units

Sevice Class bandwidth and units

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var scBandwidth = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost | Sevice Class bandwidth and units

            try
            {
                // Sevice Class bandwidth and units
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, scBandwidth);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **taggedAttachmentInterfaceInterfaceType** | **string**| Attachment Interface Type | 
 **taggedAttachmentInterfaceInterfaceId** | **string**| Attachment Interface ID | 
 **contractBandwidthPoolName** | **string**| Name of the Contract Bandwidth Poole | 
 **serviceClassesScName** | **string**|  | 
 **scBandwidth** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPost.md)| Sevice Class bandwidth and units | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4post"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post ipv4)

Logical Attachment IPv4 configuration

Logical Attachment IPv4 configuration

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4PostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var ipv4 = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post | Logical Attachment IPv4 configuration

            try
            {
                // Logical Attachment IPv4 configuration
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, ipv4);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **taggedAttachmentInterfaceInterfaceType** | **string**| Attachment Interface Type | 
 **taggedAttachmentInterfaceInterfaceId** | **string**| Attachment Interface ID | 
 **vifVlanId** | **int?**| The vlan ID of the Vif | 
 **ipv4** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Post.md)| Logical Attachment IPv4 configuration | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpost"></a>
# **DataAttachmentAttachmentPost**
> void DataAttachmentAttachmentPost (DataAttachmentAttachmentPost attachment)

Attachment

Attachment

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var attachment = new DataAttachmentAttachmentPost(); // DataAttachmentAttachmentPost | Attachment

            try
            {
                // Attachment
                apiInstance.DataAttachmentAttachmentPost(attachment);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataAttachmentAttachmentPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **attachment** | [**DataAttachmentAttachmentPost**](DataAttachmentAttachmentPost.md)| Attachment | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datapost"></a>
# **DataPost**
> void DataPost (DataPost data)

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
    public class DataPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var data = new DataPost(); // DataPost | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                apiInstance.DataPost(data);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **data** | [**DataPost**](DataPost.md)| This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

