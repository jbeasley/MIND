# IO.NovaVpnSwagger.Api.PutApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataVpnVpnInstanceInstanceNamePut**](PutApi.md#datavpnvpninstanceinstancenameput) | **PUT** /data/services/vpn:vpn/instance&#x3D;{instance-name} | List of VPN instances


<a name="datavpnvpninstanceinstancenameput"></a>
# **DataVpnVpnInstanceInstanceNamePut**
> void DataVpnVpnInstanceInstanceNamePut (string instanceName, DataVpnVpnInstanceInstanceName instance, bool? noOutOfSyncCheck = null)

List of VPN instances

List of VPN instances

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNamePutExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PutApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var instance = new DataVpnVpnInstanceInstanceName(); // DataVpnVpnInstanceInstanceName | List of VPN instances
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of VPN instances
                apiInstance.DataVpnVpnInstanceInstanceNamePut(instanceName, instance, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PutApi.DataVpnVpnInstanceInstanceNamePut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **instance** | [**DataVpnVpnInstanceInstanceName**](DataVpnVpnInstanceInstanceName.md)| List of VPN instances | 
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

