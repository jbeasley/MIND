# IO.NovaVpnSwagger.Api.RootApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**RootGet**](RootApi.md#rootget) | **GET** / | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018


<a name="rootget"></a>
# **RootGet**
> Root RootGet ()

This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018

This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class RootGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new RootApi();

            try
            {
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
                Root result = apiInstance.RootGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RootApi.RootGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**Root**](Root.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

