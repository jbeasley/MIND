# IO.NovaAttSwagger.Api.PatchApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataAttachmentAttachmentPatch**](PatchApi.md#dataattachmentattachmentpatch) | **PATCH** /data/services/attachment:attachment | Attachment
[**DataAttachmentAttachmentPePePeNamePatch**](PatchApi.md#dataattachmentattachmentpepepenamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name} | List of PE devices
[**DataAttachmentAttachmentPePePeNamePeNamePatch**](PatchApi.md#dataattachmentattachmentpepepenamepenamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/pe-name | Name of the PE device
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidattachmentbandwidthpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/attachment-bandwidth | Attachment Bandwidth values in Gigabits/Sec
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamecontractbandwidthpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/contract-bandwidth | Service Contract Bandwidth value in Mbits/Sec
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNamePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamenamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/name | Name of the Contract Bandwidth Poole
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name} | List of Contract Bandwidth Pools for this Attachment Interface
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNamePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name} | List of Service Classes supported by this contract bandwidth pool
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbandwidthpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/bandwidth | Sevice Class bandwidth
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthburstsizepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/burst-size | Burst size in bytes
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbwunitspatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/bw-units | Bandwidth units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth | Sevice Class bandwidth and units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNamePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-name | 
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnametrustreceivedcosanddscppatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/trust-received-cos-and-dscp | Trust the receieved COS and DSCP marking
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfaceidpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-id | Attachment Interface ID
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacemtupatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-mtu | Attachment MTU
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacetypepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-type | Attachment Interface Type
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id} | List of tagged Attachment Interfaces
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNamePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidcontractbandwidthpoolnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/contract-bandwidth-pool-name | The name of the Contract Bandwidth Pool
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Patch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidenableipv4patch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/enable-ipv4 | Enables the Logical Attachment Interface for IPv4
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4addresspatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-address | IPv4 address
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4prefixlengthpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-prefix-length | IPv4 prefix length
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4subnetmaskpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-subnet-mask | IPv4 dotted-decimal subnet mask
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Patch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4patch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4 | Logical Attachment IPv4 configuration
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id} | List of Virtual Interfaces
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdPatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvlanidpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/vlan-id | The vlan ID of the Vif
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNamePatch**](PatchApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvrfnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/vrf-name | The name of the VRF
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressisbfdenabledpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/is-bfd-enabled | The BGP peer is enabled for BFD
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressismultihoppatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/is-multi-hop | The BGP peer is enabled for multi-hop
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressmaxpeerroutespatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/max-peer-routes | Maximum number of routes from the BGP peer
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address} | VRF BGP peers
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerautonomoussystempatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-autonomous-system | The peer autonomous system number
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addresspatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-ipv4-address | The IPv4 address of the CE used to establish a BGP session with            the PE
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerpasswordpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-password | The BGP peer clear password
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamerdadministratorsubfieldpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/rd-administrator-subfield | Defines the 2 byte administrator sub-field value of the route-distinguisher
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldPatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamerdassignednumbersubfieldpatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/rd-assigned-number-subfield | Defines the 4 byte assigned-number sub-field value of the route-distinguisher
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNamePatch**](PatchApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamevrfnamepatch) | **PATCH** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/vrf-name | The name of the VRF
[**DataPatch**](PatchApi.md#datapatch) | **PATCH** /data | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.


<a name="dataattachmentattachmentpatch"></a>
# **DataAttachmentAttachmentPatch**
> void DataAttachmentAttachmentPatch (DataAttachmentAttachment attachment)

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
    public class DataAttachmentAttachmentPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var attachment = new DataAttachmentAttachment(); // DataAttachmentAttachment | Attachment

            try
            {
                // Attachment
                apiInstance.DataAttachmentAttachmentPatch(attachment);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **attachment** | [**DataAttachmentAttachment**](DataAttachmentAttachment.md)| Attachment | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamepatch"></a>
# **DataAttachmentAttachmentPePePeNamePatch**
> void DataAttachmentAttachmentPePePeNamePatch (string pePeName, DataAttachmentAttachmentPePePeName pe)

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
    public class DataAttachmentAttachmentPePePeNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var pe = new DataAttachmentAttachmentPePePeName(); // DataAttachmentAttachmentPePePeName | List of PE devices

            try
            {
                // List of PE devices
                apiInstance.DataAttachmentAttachmentPePePeNamePatch(pePeName, pe);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNamePatch: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamepenamepatch"></a>
# **DataAttachmentAttachmentPePePeNamePeNamePatch**
> void DataAttachmentAttachmentPePePeNamePeNamePatch (string pePeName, DataAttachmentAttachmentPePePeNamePeName peName)

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
    public class DataAttachmentAttachmentPePePeNamePeNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var peName = new DataAttachmentAttachmentPePePeNamePeName(); // DataAttachmentAttachmentPePePeNamePeName | Name of the PE device

            try
            {
                // Name of the PE device
                apiInstance.DataAttachmentAttachmentPePePeNamePeNamePatch(pePeName, peName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNamePeNamePatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **peName** | [**DataAttachmentAttachmentPePePeNamePeName**](DataAttachmentAttachmentPePePeNamePeName.md)| Name of the PE device | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidattachmentbandwidthpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth attachmentBandwidth)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var attachmentBandwidth = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth | Attachment Bandwidth values in Gigabits/Sec

            try
            {
                // Attachment Bandwidth values in Gigabits/Sec
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, attachmentBandwidth);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthPatch: " + e.Message );
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
 **attachmentBandwidth** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth.md)| Attachment Bandwidth values in Gigabits/Sec | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamecontractbandwidthpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth contractBandwidth)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var contractBandwidth = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth | Service Contract Bandwidth value in Mbits/Sec

            try
            {
                // Service Contract Bandwidth value in Mbits/Sec
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, contractBandwidth);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthPatch: " + e.Message );
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
 **contractBandwidth** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth.md)| Service Contract Bandwidth value in Mbits/Sec | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamenamepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNamePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNamePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName name)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var name = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName | Name of the Contract Bandwidth Poole

            try
            {
                // Name of the Contract Bandwidth Poole
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNamePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, name);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNamePatch: " + e.Message );
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
 **name** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName.md)| Name of the Contract Bandwidth Poole | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName contractBandwidthPool)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var contractBandwidthPool = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName | List of Contract Bandwidth Pools for this Attachment Interface

            try
            {
                // List of Contract Bandwidth Pools for this Attachment Interface
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, contractBandwidthPool);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePatch: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNamePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNamePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName serviceClasses)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var serviceClasses = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName | List of Service Classes supported by this contract bandwidth pool

            try
            {
                // List of Service Classes supported by this contract bandwidth pool
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNamePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, serviceClasses);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNamePatch: " + e.Message );
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
 **serviceClasses** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName.md)| List of Service Classes supported by this contract bandwidth pool | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbandwidthpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth bandwidth)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var bandwidth = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth | Sevice Class bandwidth

            try
            {
                // Sevice Class bandwidth
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, bandwidth);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthPatch: " + e.Message );
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
 **bandwidth** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth.md)| Sevice Class bandwidth | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthburstsizepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize burstSize)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var burstSize = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize | Burst size in bytes

            try
            {
                // Burst size in bytes
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, burstSize);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizePatch: " + e.Message );
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
 **burstSize** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize.md)| Burst size in bytes | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbwunitspatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits bwUnits)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var bwUnits = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits | Bandwidth units

            try
            {
                // Bandwidth units
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, bwUnits);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsPatch: " + e.Message );
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
 **bwUnits** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits.md)| Bandwidth units | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth scBandwidth)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var scBandwidth = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth | Sevice Class bandwidth and units

            try
            {
                // Sevice Class bandwidth and units
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, scBandwidth);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthPatch: " + e.Message );
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
 **scBandwidth** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth.md)| Sevice Class bandwidth and units | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNamePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNamePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName scName)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var scName = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName | 

            try
            {
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNamePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, scName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNamePatch: " + e.Message );
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
 **scName** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName.md)|  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnametrustreceivedcosanddscppatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp trustReceivedCosAndDscp)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var trustReceivedCosAndDscp = new DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp(); // DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp | Trust the receieved COS and DSCP marking

            try
            {
                // Trust the receieved COS and DSCP marking
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, trustReceivedCosAndDscp);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpPatch: " + e.Message );
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
 **trustReceivedCosAndDscp** | [**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp.md)| Trust the receieved COS and DSCP marking | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfaceidpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId interfaceId)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var interfaceId = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId | Attachment Interface ID

            try
            {
                // Attachment Interface ID
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, interfaceId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdPatch: " + e.Message );
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
 **interfaceId** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId.md)| Attachment Interface ID | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacemtupatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu interfaceMtu)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var interfaceMtu = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu | Attachment MTU

            try
            {
                // Attachment MTU
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, interfaceMtu);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuPatch: " + e.Message );
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
 **interfaceMtu** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu.md)| Attachment MTU | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacetypepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType interfaceType)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var interfaceType = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType | Attachment Interface Type

            try
            {
                // Attachment Interface Type
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, interfaceType);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypePatch: " + e.Message );
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
 **interfaceType** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType.md)| Attachment Interface Type | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId taggedAttachmentInterface)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var taggedAttachmentInterface = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId | List of tagged Attachment Interfaces

            try
            {
                // List of tagged Attachment Interfaces
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, taggedAttachmentInterface);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdPatch: " + e.Message );
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
 **taggedAttachmentInterface** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId.md)| List of tagged Attachment Interfaces | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidcontractbandwidthpoolnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNamePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNamePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName contractBandwidthPoolName)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var contractBandwidthPoolName = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName | The name of the Contract Bandwidth Pool

            try
            {
                // The name of the Contract Bandwidth Pool
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNamePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, contractBandwidthPoolName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNamePatch: " + e.Message );
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
 **contractBandwidthPoolName** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName.md)| The name of the Contract Bandwidth Pool | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidenableipv4patch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Patch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Patch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4 enableIpv4)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4PatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var enableIpv4 = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4 | Enables the Logical Attachment Interface for IPv4

            try
            {
                // Enables the Logical Attachment Interface for IPv4
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Patch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, enableIpv4);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Patch: " + e.Message );
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
 **enableIpv4** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4.md)| Enables the Logical Attachment Interface for IPv4 | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4addresspatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address ipv4Address)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var ipv4Address = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address | IPv4 address

            try
            {
                // IPv4 address
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, ipv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressPatch: " + e.Message );
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
 **ipv4Address** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address.md)| IPv4 address | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4prefixlengthpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength ipv4PrefixLength)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var ipv4PrefixLength = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength | IPv4 prefix length

            try
            {
                // IPv4 prefix length
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, ipv4PrefixLength);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthPatch: " + e.Message );
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
 **ipv4PrefixLength** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength.md)| IPv4 prefix length | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4subnetmaskpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask ipv4SubnetMask)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var ipv4SubnetMask = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask | IPv4 dotted-decimal subnet mask

            try
            {
                // IPv4 dotted-decimal subnet mask
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, ipv4SubnetMask);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskPatch: " + e.Message );
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
 **ipv4SubnetMask** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask.md)| IPv4 dotted-decimal subnet mask | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4patch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Patch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Patch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4 ipv4)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4PatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var ipv4 = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4 | Logical Attachment IPv4 configuration

            try
            {
                // Logical Attachment IPv4 configuration
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Patch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, ipv4);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Patch: " + e.Message );
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
 **ipv4** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4.md)| Logical Attachment IPv4 configuration | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId vif)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var vif = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId | List of Virtual Interfaces

            try
            {
                // List of Virtual Interfaces
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, vif);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPatch: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvlanidpatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdPatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdPatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId vlanId)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var vlanId = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId | The vlan ID of the Vif

            try
            {
                // The vlan ID of the Vif
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdPatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, vlanId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdPatch: " + e.Message );
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
 **vlanId** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId.md)| The vlan ID of the Vif | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvrfnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNamePatch**
> void DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNamePatch (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName vrfName)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var vrfName = new DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName(); // DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName | The name of the VRF

            try
            {
                // The name of the VRF
                apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNamePatch(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, vrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNamePatch: " + e.Message );
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
 **vrfName** | [**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName.md)| The name of the VRF | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressisbfdenabledpatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled isBfdEnabled)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var isBfdEnabled = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled | The BGP peer is enabled for BFD

            try
            {
                // The BGP peer is enabled for BFD
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, isBfdEnabled);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledPatch: " + e.Message );
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
 **isBfdEnabled** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled.md)| The BGP peer is enabled for BFD | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressismultihoppatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop isMultiHop)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var isMultiHop = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop | The BGP peer is enabled for multi-hop

            try
            {
                // The BGP peer is enabled for multi-hop
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, isMultiHop);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopPatch: " + e.Message );
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
 **isMultiHop** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop.md)| The BGP peer is enabled for multi-hop | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressmaxpeerroutespatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes maxPeerRoutes)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var maxPeerRoutes = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes | Maximum number of routes from the BGP peer

            try
            {
                // Maximum number of routes from the BGP peer
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, maxPeerRoutes);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesPatch: " + e.Message );
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
 **maxPeerRoutes** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes.md)| Maximum number of routes from the BGP peer | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address bgpPeer)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var bgpPeer = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address | VRF BGP peers

            try
            {
                // VRF BGP peers
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, bgpPeer);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch: " + e.Message );
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
 **bgpPeer** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address.md)| VRF BGP peers | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerautonomoussystempatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem peerAutonomousSystem)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var peerAutonomousSystem = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem | The peer autonomous system number

            try
            {
                // The peer autonomous system number
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, peerAutonomousSystem);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemPatch: " + e.Message );
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
 **peerAutonomousSystem** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem.md)| The peer autonomous system number | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addresspatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address peerIpv4Address)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var peerIpv4Address = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address | The IPv4 address of the CE used to establish a BGP session with            the PE

            try
            {
                // The IPv4 address of the CE used to establish a BGP session with            the PE
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, peerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch: " + e.Message );
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
 **peerIpv4Address** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address.md)| The IPv4 address of the CE used to establish a BGP session with            the PE | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerpasswordpatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordPatch (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword peerPassword)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var peerPassword = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword | The BGP peer clear password

            try
            {
                // The BGP peer clear password
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordPatch(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, peerPassword);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordPatch: " + e.Message );
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
 **peerPassword** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword.md)| The BGP peer clear password | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePatch (string pePeName, string vrfVrfName, DataAttachmentAttachmentPePePeNameVrfVrfVrfName vrf)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var vrf = new DataAttachmentAttachmentPePePeNameVrfVrfVrfName(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfName | List of VRFs

            try
            {
                // List of VRFs
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePatch(pePeName, vrfVrfName, vrf);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePatch: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamerdadministratorsubfieldpatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldPatch (string pePeName, string vrfVrfName, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield rdAdministratorSubfield)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var rdAdministratorSubfield = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield | Defines the 2 byte administrator sub-field value of the route-distinguisher

            try
            {
                // Defines the 2 byte administrator sub-field value of the route-distinguisher
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldPatch(pePeName, vrfVrfName, rdAdministratorSubfield);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldPatch: " + e.Message );
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
 **rdAdministratorSubfield** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield.md)| Defines the 2 byte administrator sub-field value of the route-distinguisher | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamerdassignednumbersubfieldpatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldPatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldPatch (string pePeName, string vrfVrfName, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield rdAssignedNumberSubfield)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var rdAssignedNumberSubfield = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield | Defines the 4 byte assigned-number sub-field value of the route-distinguisher

            try
            {
                // Defines the 4 byte assigned-number sub-field value of the route-distinguisher
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldPatch(pePeName, vrfVrfName, rdAssignedNumberSubfield);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldPatch: " + e.Message );
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
 **rdAssignedNumberSubfield** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield.md)| Defines the 4 byte assigned-number sub-field value of the route-distinguisher | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamevrfnamepatch"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNamePatch**
> void DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNamePatch (string pePeName, string vrfVrfName, DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName vrfName)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var vrfName = new DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName(); // DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName | The name of the VRF

            try
            {
                // The name of the VRF
                apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNamePatch(pePeName, vrfVrfName, vrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNamePatch: " + e.Message );
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
 **vrfName** | [**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName.md)| The name of the VRF | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datapatch"></a>
# **DataPatch**
> void DataPatch (DataPutPatch data)

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
    public class DataPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var data = new DataPutPatch(); // DataPutPatch | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                apiInstance.DataPatch(data);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **data** | [**DataPutPatch**](DataPutPatch.md)| This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

