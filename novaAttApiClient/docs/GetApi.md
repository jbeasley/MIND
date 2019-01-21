# IO.NovaAttSwagger.Api.GetApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataAttachmentAttachmentGet**](GetApi.md#dataattachmentattachmentget) | **GET** /data/services/attachment:attachment | Attachment
[**DataAttachmentAttachmentPePePeNameGet**](GetApi.md#dataattachmentattachmentpepepenameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name} | List of PE devices
[**DataAttachmentAttachmentPePePeNamePeNameGet**](GetApi.md#dataattachmentattachmentpepepenamepenameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/pe-name | Name of the PE device
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidattachmentbandwidthget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/attachment-bandwidth | Attachment Bandwidth values in Gigabits/Sec
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamecontractbandwidthget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/contract-bandwidth | Service Contract Bandwidth value in Mbits/Sec
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name} | List of Contract Bandwidth Pools for this Attachment Interface
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamenameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/name | Name of the Contract Bandwidth Poole
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name} | List of Service Classes supported by this contract bandwidth pool
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbandwidthget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/bandwidth | Sevice Class bandwidth
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthburstsizeget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/burst-size | Burst size in bytes
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbwunitsget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth/bw-units | Bandwidth units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-bandwidth | Sevice Class bandwidth and units
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/service-classes&#x3D;{service-classes-sc-name}/sc-name | 
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnametrustreceivedcosanddscpget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/contract-bandwidth-pool&#x3D;{contract-bandwidth-pool-name}/trust-received-cos-and-dscp | Trust the receieved COS and DSCP marking
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id} | List of tagged Attachment Interfaces
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfaceidget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-id | Attachment Interface ID
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacemtuget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-mtu | Attachment MTU
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacetypeget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/interface-type | Attachment Interface Type
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidcontractbandwidthpoolnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/contract-bandwidth-pool-name | The name of the Contract Bandwidth Pool
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Get**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidenableipv4get) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/enable-ipv4 | Enables the Logical Attachment Interface for IPv4
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id} | List of Virtual Interfaces
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Get**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4get) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4 | Logical Attachment IPv4 configuration
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4addressget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-address | IPv4 address
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4prefixlengthget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-prefix-length | IPv4 prefix length
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4subnetmaskget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/ipv4/ipv4-subnet-mask | IPv4 dotted-decimal subnet mask
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvlanidget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/vlan-id | The vlan ID of the Vif
[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameGet**](GetApi.md#dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvrfnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/tagged-attachment-interface&#x3D;{tagged-attachment-interface-interface-type},{tagged-attachment-interface-interface-id}/vif&#x3D;{vif-vlan-id}/vrf-name | The name of the VRF
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address} | VRF BGP peers
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressisbfdenabledget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/is-bfd-enabled | The BGP peer is enabled for BFD
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressismultihopget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/is-multi-hop | The BGP peer is enabled for multi-hop
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressmaxpeerroutesget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/max-peer-routes | Maximum number of routes from the BGP peer
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerautonomoussystemget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-autonomous-system | The peer autonomous system number
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-ipv4-address | The IPv4 address of the CE used to establish a BGP session with            the PE
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerpasswordget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-password | The BGP peer clear password
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamerdadministratorsubfieldget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/rd-administrator-subfield | Defines the 2 byte administrator sub-field value of the route-distinguisher
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamerdassignednumbersubfieldget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/rd-assigned-number-subfield | Defines the 4 byte assigned-number sub-field value of the route-distinguisher
[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameGet**](GetApi.md#dataattachmentattachmentpepepenamevrfvrfvrfnamevrfnameget) | **GET** /data/services/attachment:attachment/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/vrf-name | The name of the VRF
[**DataGet**](GetApi.md#dataget) | **GET** /data | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
[**OperationsGet**](GetApi.md#operationsget) | **GET** /operations | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
[**RootGet**](GetApi.md#rootget) | **GET** / | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
[**YangLibraryVersionGet**](GetApi.md#yanglibraryversionget) | **GET** /yang-library-version | This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.


<a name="dataattachmentattachmentget"></a>
# **DataAttachmentAttachmentGet**
> DataAttachmentAttachment DataAttachmentAttachmentGet (string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Attachment
                DataAttachmentAttachment result = apiInstance.DataAttachmentAttachmentGet(content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachment**](DataAttachmentAttachment.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenameget"></a>
# **DataAttachmentAttachmentPePePeNameGet**
> DataAttachmentAttachmentPePePeName DataAttachmentAttachmentPePePeNameGet (string pePeName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of PE devices
                DataAttachmentAttachmentPePePeName result = apiInstance.DataAttachmentAttachmentPePePeNameGet(pePeName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeName**](DataAttachmentAttachmentPePePeName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamepenameget"></a>
# **DataAttachmentAttachmentPePePeNamePeNameGet**
> DataAttachmentAttachmentPePePeNamePeName DataAttachmentAttachmentPePePeNamePeNameGet (string pePeName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNamePeNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Name of the PE device
                DataAttachmentAttachmentPePePeNamePeName result = apiInstance.DataAttachmentAttachmentPePePeNamePeNameGet(pePeName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNamePeNameGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pePeName** | **string**| Name of the PE device | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNamePeName**](DataAttachmentAttachmentPePePeNamePeName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidattachmentbandwidthget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Attachment Bandwidth values in Gigabits/Sec
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdAttachmentBandwidth.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamecontractbandwidthget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Service Contract Bandwidth value in Mbits/Sec
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameContractBandwidth.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of Contract Bandwidth Pools for this Attachment Interface
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnamenameget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Name of the Contract Bandwidth Poole
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnameget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of Service Classes supported by this contract bandwidth pool
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbandwidthget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Sevice Class bandwidth
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBandwidth.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthburstsizeget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Burst size in bytes
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSizeGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBurstSize.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthbwunitsget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Bandwidth units
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnitsGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthBwUnits.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescbandwidthget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Sevice Class bandwidth and units
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScBandwidth.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnameserviceclassesserviceclassesscnamescnameget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string serviceClassesScName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using IO.NovaAttSwagger.Model;

namespace Example
{
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var serviceClassesScName = serviceClassesScName_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, serviceClassesScName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScNameScName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidcontractbandwidthpoolcontractbandwidthpoolnametrustreceivedcosanddscpget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpGet**
> DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string contractBandwidthPoolName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var contractBandwidthPoolName = contractBandwidthPoolName_example;  // string | Name of the Contract Bandwidth Poole
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Trust the receieved COS and DSCP marking
                DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, contractBandwidthPoolName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscpGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp**](DataAttachmentContractBandwidthPoolContractBandwidthPoolNameTrustReceivedCosAndDscp.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of tagged Attachment Interfaces
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceId.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfaceidget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Attachment Interface ID
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceIdGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceId.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacemtuget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Attachment MTU
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtuGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceMtu.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidinterfacetypeget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Attachment Interface Type
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceTypeGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdInterfaceType.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidcontractbandwidthpoolnameget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The name of the Contract Bandwidth Pool
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdContractBandwidthPoolName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidenableipv4get"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Get**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4 DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Get (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4GetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Enables the Logical Attachment Interface for IPv4
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4 result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Get(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4Get: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdEnableIpv4.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of Virtual Interfaces
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanId.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4get"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Get**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4 DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Get (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4GetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Logical Attachment IPv4 configuration
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4 result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Get(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Get: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4addressget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // IPv4 address
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4AddressGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4Address.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4prefixlengthget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // IPv4 prefix length
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLengthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4PrefixLength.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidipv4ipv4subnetmaskget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // IPv4 dotted-decimal subnet mask
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMaskGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdIpv4Ipv4SubnetMask.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvlanidget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The vlan ID of the Vif
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanIdGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVlanId.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenametaggedattachmentinterfacetaggedattachmentinterfaceinterfacetypetaggedattachmentinterfaceinterfaceidvifvifvlanidvrfnameget"></a>
# **DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameGet**
> DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameGet (string pePeName, string taggedAttachmentInterfaceInterfaceType, string taggedAttachmentInterfaceInterfaceId, int? vifVlanId, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var taggedAttachmentInterfaceInterfaceType = taggedAttachmentInterfaceInterfaceType_example;  // string | Attachment Interface Type
            var taggedAttachmentInterfaceInterfaceId = taggedAttachmentInterfaceInterfaceId_example;  // string | Attachment Interface ID
            var vifVlanId = 56;  // int? | The vlan ID of the Vif
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The name of the VRF
                DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName result = apiInstance.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameGet(pePeName, taggedAttachmentInterfaceInterfaceType, taggedAttachmentInterfaceInterfaceId, vifVlanId, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName**](DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdVrfName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // VRF BGP peers
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressisbfdenabledget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The BGP peer is enabled for BFD
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabledGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsBfdEnabled.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressismultihopget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The BGP peer is enabled for multi-hop
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHopGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIsMultiHop.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressmaxpeerroutesget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Maximum number of routes from the BGP peer
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutesGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressMaxPeerRoutes.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerautonomoussystemget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The peer autonomous system number
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystemGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerAutonomousSystem.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The IPv4 address of the CE used to establish a BGP session with            the PE
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4Address.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeerpasswordget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordGet (string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | The IPv4 address of the CE used to establish a BGP session with            the PE
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The BGP peer clear password
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordGet(pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPasswordGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerPassword.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnameget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfName DataAttachmentAttachmentPePePeNameVrfVrfVrfNameGet (string pePeName, string vrfVrfName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of VRFs
                DataAttachmentAttachmentPePePeNameVrfVrfVrfName result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameGet(pePeName, vrfVrfName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfName**](DataAttachmentAttachmentPePePeNameVrfVrfVrfName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamerdadministratorsubfieldget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldGet (string pePeName, string vrfVrfName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Defines the 2 byte administrator sub-field value of the route-distinguisher
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldGet(pePeName, vrfVrfName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfieldGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAdministratorSubfield.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamerdassignednumbersubfieldget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldGet (string pePeName, string vrfVrfName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Defines the 4 byte assigned-number sub-field value of the route-distinguisher
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldGet(pePeName, vrfVrfName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfieldGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameRdAssignedNumberSubfield.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataattachmentattachmentpepepenamevrfvrfvrfnamevrfnameget"></a>
# **DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameGet**
> DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameGet (string pePeName, string vrfVrfName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | The name of the VRF
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The name of the VRF
                DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName result = apiInstance.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameGet(pePeName, vrfVrfName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName**](DataAttachmentAttachmentPePePeNameVrfVrfVrfNameVrfName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="dataget"></a>
# **DataGet**
> Data DataGet ()

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
    public class DataGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                Data result = apiInstance.DataGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**Data**](Data.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

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

            var apiInstance = new GetApi();

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                Operations result = apiInstance.OperationsGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.OperationsGet: " + e.Message );
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

<a name="rootget"></a>
# **RootGet**
> Root RootGet ()

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
    public class RootGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                Root result = apiInstance.RootGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.RootGet: " + e.Message );
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

<a name="yanglibraryversionget"></a>
# **YangLibraryVersionGet**
> YangLibraryVersion YangLibraryVersionGet ()

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
    public class YangLibraryVersionGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();

            try
            {
                // This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
                YangLibraryVersion result = apiInstance.YangLibraryVersionGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.YangLibraryVersionGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**YangLibraryVersion**](YangLibraryVersion.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

