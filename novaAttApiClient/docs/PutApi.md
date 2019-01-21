# IO.NovaAttSwagger.Api.PutApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataAttachmentAttachmentPePePeNamePut**](PutApi.md#dataattachmentattachmentpepepenameput) | **PUT** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name} | List of PE devices
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePut**](PutApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameput) | **PUT** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name} | List of Contract Bandwidth Pools for this Attachment Interface
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPut**](PutApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidput) | **PUT** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id} | List of Virtual Interfaces
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePut**](PutApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnameput) | **PUT** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs


<a name="dataattachmentattachmentpepepenameput"></a>
# **DataAttachmentAttachmentPePePeNamePut**
> void DataAttachmentAttachmentPePePeNamePut (string pePeName, DataAttachmentAttachmentPePePeName pe, bool? noOutOfSyncCheck = null)

List of PE devices

List of PE devices

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNamePutExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PutApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var pe = new DataAttachmentAttachmentPePePeName(); // DataAttachmentAttachmentPePePeName | List of PE devices
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of PE devices
                apiInstance.DataAttachmentAttachmentPePePeNamePut(pePeName, pe, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PutApi.DataAttachmentAttachmentPePePeNamePut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **pe** | [**DataAttachmentAttachmentPePePeName**](DataAttachmentAttachmentPePePeName.md)| List of PE devices | 
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameput"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePut**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePut (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName contractBandwidthPool, bool? noOutOfSyncCheck = null)

List of Contract Bandwidth Pools for this Attachment Interface

List of Contract Bandwidth Pools for this Attachment Interface

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePutExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PutApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var contractBandwidthPool = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName | List of Contract Bandwidth Pools for this Attachment Interface
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of Contract Bandwidth Pools for this Attachment Interface
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePut(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, contractBandwidthPool, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PutApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePut: " + e.Message );
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
 **contractBandwidthPool** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName.md)| List of Contract Bandwidth Pools for this Attachment Interface | 
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidput"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPut**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPut (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId vif, bool? noOutOfSyncCheck = null)

List of Virtual Interfaces

List of Virtual Interfaces

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPutExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PutApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var vif = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId | List of Virtual Interfaces
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of Virtual Interfaces
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPut(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, vif, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PutApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPut: " + e.Message );
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
 **vif** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId.md)| List of Virtual Interfaces | 
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnameput"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePut**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePut (string pePeName, string vrfVrfName, DataAttachmentAttachmentPePePeNameVrfVrfVrfName vrf, bool? noOutOfSyncCheck = null)

List of VRFs

List of VRFs

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePutExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PutApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var vrf = new DataAttachmentAttachmentPePePeNameVrfVrfVrfName(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfName | List of VRFs
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of VRFs
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePut(pePeName, vrfVrfName, vrf, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PutApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **vrfVrfName** | **string**| The name of the VRF | 
 **vrf** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfName**](DataAttachmentAttachmentPePePeNameVrfVrfVrfName.md)| List of VRFs | 
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

