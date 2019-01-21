# IO.NovaAttSwagger.Api.DeleteApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataAttachmentAttachmentDelete**](DeleteApi.md#dataattachmentattachmentdelete) | **DELETE** /data/services/attachment:attachment | Attachment
[**DataAttachmentAttachmentPePePeNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name} | List of PE devices
[**DataAttachmentAttachmentPePePeNamePeNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenamepenamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/pe-name | Name of the PE device
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidattachmentbandwidthdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/attachment-bandwidth | Attachment Bandwidth values in Gigabits/Sec
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamecontractbandwidthdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/contract-bandwidth | Service Contract Bandwidth value in Mbits/Sec
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name} | List of Contract Bandwidth Pools for this Attachment Interface
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamenamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/name | Name of the Contract Bandwidth Poole
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name} | List of Service Classes supported by this contract bandwidth pool
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbandwidthdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/bandwidth | Sevice Class bandwidth
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthburstsizedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/burst-size | Burst size in bytes
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbwunitsdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/bw-units | Bandwidth units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth | Sevice Class bandwidth and units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-name | 
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnametrustreceivedcosanddscpdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/trust-received-cos-and-dscp | Trust the receieved COS and DSCP marking
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceiddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id} | List of tagged Attachment Interfaces
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfaceiddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-id | Attachment Interface ID
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacemtudelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-mtu | Attachment MTU
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacetypedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-type | Attachment Interface Type
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidcontractbandwidthpoolnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/contract-bandwidth-pool-name | The name of the Contract Bandwidth Pool
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlaniddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id} | List of Virtual Interfaces
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Delete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidenableipv4delete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/enable-ipv4 | Enables the Logical Attachment Interface for IPv4
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Delete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4delete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4 | Logical Attachment IPv4 configuration
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4addressdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-address | IPv4 address
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4prefixlengthdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-prefix-length | IPv4 prefix length
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4subnetmaskdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-subnet-mask | IPv4 dotted-decimal subnet mask
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvlaniddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/vlan-id | The vlan ID of the Vif
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvrfnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/vrf-name | The name of the VRF
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address} | VRF BGP peers
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressisbfdenableddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/is-bfd-enabled | The BGP peer is enabled for BFD
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressismultihopdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/is-multi-hop | The BGP peer is enabled for multi-hop
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressmaxpeerroutesdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/max-peer-routes | Maximum number of routes from the BGP peer
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerautonomoussystemdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-autonomous-system | The peer autonomous system number
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressdelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-ipv4-address | The IPv4 address of the CE used to establish a BGP session with            the PE
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerpassworddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-password | The BGP peer clear password
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamerdadministratorsubfielddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/rd-administrator-subfield | Defines the 2 byte administrator sub-field value of the route-distinguisher
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamerdassignednumbersubfielddelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/rd-assigned-number-subfield | Defines the 4 byte assigned-number sub-field value of the route-distinguisher
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameDelete**](DeleteApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamevrfnamedelete) | **DELETE** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/vrf-name | The name of the VRF


<a name="dataattachmentattachmentdelete"></a>
# **DataAttachmentAttachmentDelete**
> void DataAttachmentAttachmentDelete ()

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
    public class DataAttachmentAttachmentDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();

            try
            {
                // Attachment
                apiInstance.DataAttachmentAttachmentDelete();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamedelete"></a>
# **DataAttachmentAttachmentPePePeNameDelete**
> void DataAttachmentAttachmentPePePeNameDelete (string pePeName)

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
    public class DataAttachmentAttachmentPePePeNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device

            try
            {
                // List of PE devices
                apiInstance.DataAttachmentAttachmentPePePeNameDelete(pePeName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamepenamedelete"></a>
# **DataAttachmentAttachmentPePePeNamePeNameDelete**
> void DataAttachmentAttachmentPePePeNamePeNameDelete (string pePeName)

Name of the PE device

Name of the PE device

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNamePeNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device

            try
            {
                // Name of the PE device
                apiInstance.DataAttachmentAttachmentPePePeNamePeNameDelete(pePeName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNamePeNameDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidattachmentbandwidthdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId)

Attachment Bandwidth values in Gigabits/Sec

Attachment Bandwidth values in Gigabits/Sec

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID

            try
            {
                // Attachment Bandwidth values in Gigabits/Sec
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamecontractbandwidthdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName)

Service Contract Bandwidth value in Mbits/Sec

Service Contract Bandwidth value in Mbits/Sec

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole

            try
            {
                // Service Contract Bandwidth value in Mbits/Sec
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, bool? noOutOfSyncCheck = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of Contract Bandwidth Pools for this Attachment Interface
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDelete: " + e.Message );
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
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamenamedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName)

Name of the Contract Bandwidth Poole

Name of the Contract Bandwidth Poole

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole

            try
            {
                // Name of the Contract Bandwidth Poole
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName)

List of Service Classes supported by this contract bandwidth pool

List of Service Classes supported by this contract bandwidth pool

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 

            try
            {
                // List of Service Classes supported by this contract bandwidth pool
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbandwidthdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName)

Sevice Class bandwidth

Sevice Class bandwidth

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 

            try
            {
                // Sevice Class bandwidth
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthburstsizedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName)

Burst size in bytes

Burst size in bytes

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 

            try
            {
                // Burst size in bytes
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbwunitsdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName)

Bandwidth units

Bandwidth units

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 

            try
            {
                // Bandwidth units
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 

            try
            {
                // Sevice Class bandwidth and units
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 

            try
            {
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnametrustreceivedcosanddscpdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName)

Trust the receieved COS and DSCP marking

Trust the receieved COS and DSCP marking

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole

            try
            {
                // Trust the receieved COS and DSCP marking
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceiddelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, bool? noOutOfSyncCheck = null)

List of tagged Attachment Interfaces

List of tagged Attachment Interfaces

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of tagged Attachment Interfaces
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdDelete: " + e.Message );
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
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfaceiddelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId)

Attachment Interface ID

Attachment Interface ID

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID

            try
            {
                // Attachment Interface ID
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacemtudelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId)

Attachment MTU

Attachment MTU

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID

            try
            {
                // Attachment MTU
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacetypedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId)

Attachment Interface Type

Attachment Interface Type

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID

            try
            {
                // Attachment Interface Type
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidcontractbandwidthpoolnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

The name of the Contract Bandwidth Pool

The name of the Contract Bandwidth Pool

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // The name of the Contract Bandwidth Pool
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlaniddelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, bool? noOutOfSyncCheck = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of Virtual Interfaces
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDelete: " + e.Message );
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
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidenableipv4delete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Delete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Delete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

Enables the Logical Attachment Interface for IPv4

Enables the Logical Attachment Interface for IPv4

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4DeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // Enables the Logical Attachment Interface for IPv4
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Delete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Delete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4delete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Delete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Delete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4DeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // Logical Attachment IPv4 configuration
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Delete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Delete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4addressdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

IPv4 address

IPv4 address

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // IPv4 address
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4prefixlengthdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

IPv4 prefix length

IPv4 prefix length

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // IPv4 prefix length
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4subnetmaskdelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

IPv4 dotted-decimal subnet mask

IPv4 dotted-decimal subnet mask

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // IPv4 dotted-decimal subnet mask
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvlaniddelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

The vlan ID of the Vif

The vlan ID of the Vif

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // The vlan ID of the Vif
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvrfnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameDelete**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameDelete (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId)

The name of the VRF

The name of the VRF

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif

            try
            {
                // The name of the VRF
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameDelete(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressdelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

VRF BGP peers

VRF BGP peers

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // VRF BGP peers
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressisbfdenableddelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

The BGP peer is enabled for BFD

The BGP peer is enabled for BFD

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // The BGP peer is enabled for BFD
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressismultihopdelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

The BGP peer is enabled for multi-hop

The BGP peer is enabled for multi-hop

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // The BGP peer is enabled for multi-hop
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressmaxpeerroutesdelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

Maximum number of routes from the BGP peer

Maximum number of routes from the BGP peer

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // Maximum number of routes from the BGP peer
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerautonomoussystemdelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

The peer autonomous system number

The peer autonomous system number

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // The peer autonomous system number
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressdelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

The IPv4 address of the CE used to establish a BGP session with            the PE

The IPv4 address of the CE used to establish a BGP session with            the PE

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // The IPv4 address of the CE used to establish a BGP session with            the PE
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerpassworddelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordDelete (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

The BGP peer clear password

The BGP peer clear password

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // The BGP peer clear password
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordDelete(pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordDelete: " + e.Message );
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
 **bgpPeerPeerIpv4Address** | **string**| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDelete (string pePeName, string vrfVrfName, bool? noOutOfSyncCheck = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of VRFs
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDelete(pePeName, vrfVrfName, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDelete: " + e.Message );
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
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamerdadministratorsubfielddelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldDelete (string pePeName, string vrfVrfName)

Defines the 2 byte administrator sub-field value of the route-distinguisher

Defines the 2 byte administrator sub-field value of the route-distinguisher

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF

            try
            {
                // Defines the 2 byte administrator sub-field value of the route-distinguisher
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldDelete(pePeName, vrfVrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamerdassignednumbersubfielddelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldDelete (string pePeName, string vrfVrfName)

Defines the 4 byte assigned-number sub-field value of the route-distinguisher

Defines the 4 byte assigned-number sub-field value of the route-distinguisher

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF

            try
            {
                // Defines the 4 byte assigned-number sub-field value of the route-distinguisher
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldDelete(pePeName, vrfVrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamevrfnamedelete"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameDelete**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameDelete (string pePeName, string vrfVrfName)

The name of the VRF

The name of the VRF

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF

            try
            {
                // The name of the VRF
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameDelete(pePeName, vrfVrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

