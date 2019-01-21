# IO.NovaVpnSwagger.Api.PostApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataPost**](PostApi.md#datapost) | **POST** /data | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
[**DataVpnVpnInstanceInstanceNameRouteTargetAPost**](PostApi.md#datavpnvpninstanceinstancenameroutetargetapost) | **POST** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A | Route-target &#39;A&#39;. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
[**DataVpnVpnInstanceInstanceNameRouteTargetBPost**](PostApi.md#datavpnvpninstanceinstancenameroutetargetbpost) | **POST** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B | Route-target &#39;B&#39;. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPost**](PostApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicypost) | **POST** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPost**](PostApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicypost) | **POST** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnPost**](PostApi.md#datavpnvpnpost) | **POST** /data/services/vpn:vpn | VPN service container


<a name="datapost"></a>
# **DataPost**
> void DataPost (DataPost data)

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
    public class DataPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var data = new DataPost(); // DataPost | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018

            try
            {
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
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
 **data** | [**DataPost**](DataPost.md)| This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018 | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetapost"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAPost**
> void DataVpnVpnInstanceInstanceNameRouteTargetAPost (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetAPost routeTargetA)

Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs

Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameRouteTargetAPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var routeTargetA = new DataVpnVpnInstanceInstanceNameRouteTargetAPost(); // DataVpnVpnInstanceInstanceNameRouteTargetAPost | Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs

            try
            {
                // Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAPost(instanceName, routeTargetA);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataVpnVpnInstanceInstanceNameRouteTargetAPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **routeTargetA** | [**DataVpnVpnInstanceInstanceNameRouteTargetAPost**](DataVpnVpnInstanceInstanceNameRouteTargetAPost.md)| Route-target &#39;A&#39;. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbpost"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBPost**
> void DataVpnVpnInstanceInstanceNameRouteTargetBPost (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetBPost routeTargetB)

Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 

Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameRouteTargetBPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var routeTargetB = new DataVpnVpnInstanceInstanceNameRouteTargetBPost(); // DataVpnVpnInstanceInstanceNameRouteTargetBPost | Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 

            try
            {
                // Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBPost(instanceName, routeTargetB);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataVpnVpnInstanceInstanceNameRouteTargetBPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **routeTargetB** | [**DataVpnVpnInstanceInstanceNameRouteTargetBPost**](DataVpnVpnInstanceInstanceNameRouteTargetBPost.md)| Route-target &#39;B&#39;. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicypost"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPost**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPost (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataVpnIpv4InboundRoutingPolicyPost ipv4InboundRoutingPolicy)

IPv4 outbound routing policy.

IPv4 outbound routing policy.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var ipv4InboundRoutingPolicy = new DataVpnIpv4InboundRoutingPolicyPost(); // DataVpnIpv4InboundRoutingPolicyPost | IPv4 outbound routing policy.

            try
            {
                // IPv4 outbound routing policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPost(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, ipv4InboundRoutingPolicy);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **vpnAttachmentSetName** | **string**| Name of the Attachment Set. | 
 **pePeName** | **string**| Name of the PE device | 
 **vrfVrfName** | **string**| VRF which is a member of the VPN | 
 **bgpPeerPeerIpv4Address** | **string**|  | 
 **ipv4InboundRoutingPolicy** | [**DataVpnIpv4InboundRoutingPolicyPost**](DataVpnIpv4InboundRoutingPolicyPost.md)| IPv4 outbound routing policy. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicypost"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPost**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPost (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataVpnIpv4OutboundRoutingPolicyPost ipv4OutboundRoutingPolicy)

IPv4 outbound routing policy.

IPv4 outbound routing policy.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var ipv4OutboundRoutingPolicy = new DataVpnIpv4OutboundRoutingPolicyPost(); // DataVpnIpv4OutboundRoutingPolicyPost | IPv4 outbound routing policy.

            try
            {
                // IPv4 outbound routing policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPost(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, ipv4OutboundRoutingPolicy);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **vpnAttachmentSetName** | **string**| Name of the Attachment Set. | 
 **pePeName** | **string**| Name of the PE device | 
 **vrfVrfName** | **string**| VRF which is a member of the VPN | 
 **bgpPeerPeerIpv4Address** | **string**|  | 
 **ipv4OutboundRoutingPolicy** | [**DataVpnIpv4OutboundRoutingPolicyPost**](DataVpnIpv4OutboundRoutingPolicyPost.md)| IPv4 outbound routing policy. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpnpost"></a>
# **DataVpnVpnPost**
> void DataVpnVpnPost (DataVpnVpnPost vpn)

VPN service container

VPN service container

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnPostExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PostApi();
            var vpn = new DataVpnVpnPost(); // DataVpnVpnPost | VPN service container

            try
            {
                // VPN service container
                apiInstance.DataVpnVpnPost(vpn);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PostApi.DataVpnVpnPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vpn** | [**DataVpnVpnPost**](DataVpnVpnPost.md)| VPN service container | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

