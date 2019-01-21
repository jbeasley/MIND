# IO.NovaVpnSwagger.Api.PatchApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataPatch**](PatchApi.md#datapatch) | **PATCH** /data | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
[**DataVpnVpnInstanceInstanceNameAddressFamilyPatch**](PatchApi.md#datavpnvpninstanceinstancenameaddressfamilypatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/address-family | The address-family of the IP VPN (e.g. IPv4)
[**DataVpnVpnInstanceInstanceNameIsExtranetPatch**](PatchApi.md#datavpnvpninstanceinstancenameisextranetpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/is-extranet | Determines whether the VPN supports Extranet
[**DataVpnVpnInstanceInstanceNameNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamenamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/name | VPN service name
[**DataVpnVpnInstanceInstanceNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name} | List of VPN instances
[**DataVpnVpnInstanceInstanceNameProtocolTypePatch**](PatchApi.md#datavpnvpninstanceinstancenameprotocoltypepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/protocol-type | The Protocol Type of the VPN (e.g. IP)
[**DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldPatch**](PatchApi.md#datavpnvpninstanceinstancenameroutetargetaadministratorsubfieldpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A/administrator-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldPatch**](PatchApi.md#datavpnvpninstanceinstancenameroutetargetaassignednumbersubfieldpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A/assigned-number-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetAPatch**](PatchApi.md#datavpnvpninstanceinstancenameroutetargetapatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A | Route-target &#39;A&#39;. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
[**DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldPatch**](PatchApi.md#datavpnvpninstanceinstancenameroutetargetbadministratorsubfieldpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B/administrator-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldPatch**](PatchApi.md#datavpnvpninstanceinstancenameroutetargetbassignednumbersubfieldpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B/assigned-number-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetBPatch**](PatchApi.md#datavpnvpninstanceinstancenameroutetargetbpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B | Route-target &#39;B&#39;. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
[**DataVpnVpnInstanceInstanceNameTopologyTypePatch**](PatchApi.md#datavpnvpninstanceinstancenametopologytypepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/topology-type | The Topology Type of the IP VPN (e.g. any-to-any)
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamenamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/name | Name of the Attachment Set.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name} | VRF membership of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name} | List of PE devices which have one or more VRFs which are members of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepenamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/pe-name | Name of the PE device
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicypatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferencePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberlocaliproutingpreferencepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/local-ip-routing-preference | The local IP routing preference for the community.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/less-than-or-equal-to-length | Include prefix lengths up to and including the specified length.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferencePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlocaliproutingpreferencepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/local-ip-routing-preference | The IP routing preference for the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix} | List of IPv4 prefixes for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/prefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities which are associated with the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicypatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferencePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberadvertisediproutingpreferencepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/advertised-ip-routing-preference | The advertised IP routing preference for the community.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities for sets of Tenant Routes.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferencePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixadvertisediproutingpreferencepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/advertised-ip-routing-preference | The advertised IP routing preference for the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/less-than-or-equal-to-length | Include prefix lengths up to and including the specified length.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix} | List of IPv4 prefixes for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixpatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/prefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address} | List of BGP peers which require network-based outbound routing                policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addresspatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-ipv4-address | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs which are members of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNamePatch**](PatchApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamevrfnamepatch) | **PATCH** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/vrf-name | VRF which is a member of the VPN
[**DataVpnVpnPatch**](PatchApi.md#datavpnvpnpatch) | **PATCH** /data/services/vpn:vpn | VPN service container


<a name="datapatch"></a>
# **DataPatch**
> void DataPatch (DataPutPatch data)

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
    public class DataPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var data = new DataPutPatch(); // DataPutPatch | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018

            try
            {
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
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
 **data** | [**DataPutPatch**](DataPutPatch.md)| This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018 | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameaddressfamilypatch"></a>
# **DataVpnVpnInstanceInstanceNameAddressFamilyPatch**
> void DataVpnVpnInstanceInstanceNameAddressFamilyPatch (string instanceName, DataVpnVpnInstanceInstanceNameAddressFamily addressFamily)

The address-family of the IP VPN (e.g. IPv4)

The address-family of the IP VPN (e.g. IPv4)

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameAddressFamilyPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var addressFamily = new DataVpnVpnInstanceInstanceNameAddressFamily(); // DataVpnVpnInstanceInstanceNameAddressFamily | The address-family of the IP VPN (e.g. IPv4)

            try
            {
                // The address-family of the IP VPN (e.g. IPv4)
                apiInstance.DataVpnVpnInstanceInstanceNameAddressFamilyPatch(instanceName, addressFamily);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameAddressFamilyPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **addressFamily** | [**DataVpnVpnInstanceInstanceNameAddressFamily**](DataVpnVpnInstanceInstanceNameAddressFamily.md)| The address-family of the IP VPN (e.g. IPv4) | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameisextranetpatch"></a>
# **DataVpnVpnInstanceInstanceNameIsExtranetPatch**
> void DataVpnVpnInstanceInstanceNameIsExtranetPatch (string instanceName, DataVpnVpnInstanceInstanceNameIsExtranet isExtranet)

Determines whether the VPN supports Extranet

Determines whether the VPN supports Extranet

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameIsExtranetPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var isExtranet = new DataVpnVpnInstanceInstanceNameIsExtranet(); // DataVpnVpnInstanceInstanceNameIsExtranet | Determines whether the VPN supports Extranet

            try
            {
                // Determines whether the VPN supports Extranet
                apiInstance.DataVpnVpnInstanceInstanceNameIsExtranetPatch(instanceName, isExtranet);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameIsExtranetPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **isExtranet** | [**DataVpnVpnInstanceInstanceNameIsExtranet**](DataVpnVpnInstanceInstanceNameIsExtranet.md)| Determines whether the VPN supports Extranet | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamenamepatch"></a>
# **DataVpnVpnInstanceInstanceNameNamePatch**
> void DataVpnVpnInstanceInstanceNameNamePatch (string instanceName, DataVpnVpnInstanceInstanceNameName name)

VPN service name

VPN service name

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var name = new DataVpnVpnInstanceInstanceNameName(); // DataVpnVpnInstanceInstanceNameName | VPN service name

            try
            {
                // VPN service name
                apiInstance.DataVpnVpnInstanceInstanceNameNamePatch(instanceName, name);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameNamePatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **name** | [**DataVpnVpnInstanceInstanceNameName**](DataVpnVpnInstanceInstanceNameName.md)| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamepatch"></a>
# **DataVpnVpnInstanceInstanceNamePatch**
> void DataVpnVpnInstanceInstanceNamePatch (string instanceName, DataVpnVpnInstanceInstanceName instance, bool? noOutOfSyncCheck = null)

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
    public class DataVpnVpnInstanceInstanceNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var instance = new DataVpnVpnInstanceInstanceName(); // DataVpnVpnInstanceInstanceName | List of VPN instances
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of VPN instances
                apiInstance.DataVpnVpnInstanceInstanceNamePatch(instanceName, instance, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNamePatch: " + e.Message );
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

<a name="datavpnvpninstanceinstancenameprotocoltypepatch"></a>
# **DataVpnVpnInstanceInstanceNameProtocolTypePatch**
> void DataVpnVpnInstanceInstanceNameProtocolTypePatch (string instanceName, DataVpnVpnInstanceInstanceNameProtocolType protocolType)

The Protocol Type of the VPN (e.g. IP)

The Protocol Type of the VPN (e.g. IP)

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameProtocolTypePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var protocolType = new DataVpnVpnInstanceInstanceNameProtocolType(); // DataVpnVpnInstanceInstanceNameProtocolType | The Protocol Type of the VPN (e.g. IP)

            try
            {
                // The Protocol Type of the VPN (e.g. IP)
                apiInstance.DataVpnVpnInstanceInstanceNameProtocolTypePatch(instanceName, protocolType);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameProtocolTypePatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **protocolType** | [**DataVpnVpnInstanceInstanceNameProtocolType**](DataVpnVpnInstanceInstanceNameProtocolType.md)| The Protocol Type of the VPN (e.g. IP) | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaadministratorsubfieldpatch"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldPatch**
> void DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldPatch (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield administratorSubfield)

The 2 byte or 4 byte administrator sub-field value of the route-target

The 2 byte or 4 byte administrator sub-field value of the route-target

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var administratorSubfield = new DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield(); // DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield | The 2 byte or 4 byte administrator sub-field value of the route-target

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldPatch(instanceName, administratorSubfield);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **administratorSubfield** | [**DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield.md)| The 2 byte or 4 byte administrator sub-field value of the route-target | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaassignednumbersubfieldpatch"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldPatch**
> void DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldPatch (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield assignedNumberSubfield)

The 2 byte or 4 byte administrator sub-field value of the route-target

The 2 byte or 4 byte administrator sub-field value of the route-target

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var assignedNumberSubfield = new DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield(); // DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield | The 2 byte or 4 byte administrator sub-field value of the route-target

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldPatch(instanceName, assignedNumberSubfield);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **assignedNumberSubfield** | [**DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield.md)| The 2 byte or 4 byte administrator sub-field value of the route-target | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetapatch"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAPatch**
> void DataVpnVpnInstanceInstanceNameRouteTargetAPatch (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetA routeTargetA)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetAPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var routeTargetA = new DataVpnVpnInstanceInstanceNameRouteTargetA(); // DataVpnVpnInstanceInstanceNameRouteTargetA | Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs

            try
            {
                // Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAPatch(instanceName, routeTargetA);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameRouteTargetAPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **routeTargetA** | [**DataVpnVpnInstanceInstanceNameRouteTargetA**](DataVpnVpnInstanceInstanceNameRouteTargetA.md)| Route-target &#39;A&#39;. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbadministratorsubfieldpatch"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldPatch**
> void DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldPatch (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield administratorSubfield)

The 2 byte or 4 byte administrator sub-field value of the route-target

The 2 byte or 4 byte administrator sub-field value of the route-target

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var administratorSubfield = new DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield(); // DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield | The 2 byte or 4 byte administrator sub-field value of the route-target

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldPatch(instanceName, administratorSubfield);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **administratorSubfield** | [**DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield.md)| The 2 byte or 4 byte administrator sub-field value of the route-target | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbassignednumbersubfieldpatch"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldPatch**
> void DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldPatch (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield assignedNumberSubfield)

The 2 byte or 4 byte administrator sub-field value of the route-target

The 2 byte or 4 byte administrator sub-field value of the route-target

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var assignedNumberSubfield = new DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield(); // DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield | The 2 byte or 4 byte administrator sub-field value of the route-target

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldPatch(instanceName, assignedNumberSubfield);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **assignedNumberSubfield** | [**DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield.md)| The 2 byte or 4 byte administrator sub-field value of the route-target | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbpatch"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBPatch**
> void DataVpnVpnInstanceInstanceNameRouteTargetBPatch (string instanceName, DataVpnVpnInstanceInstanceNameRouteTargetB routeTargetB)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var routeTargetB = new DataVpnVpnInstanceInstanceNameRouteTargetB(); // DataVpnVpnInstanceInstanceNameRouteTargetB | Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 

            try
            {
                // Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBPatch(instanceName, routeTargetB);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameRouteTargetBPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **routeTargetB** | [**DataVpnVpnInstanceInstanceNameRouteTargetB**](DataVpnVpnInstanceInstanceNameRouteTargetB.md)| Route-target &#39;B&#39;. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenametopologytypepatch"></a>
# **DataVpnVpnInstanceInstanceNameTopologyTypePatch**
> void DataVpnVpnInstanceInstanceNameTopologyTypePatch (string instanceName, DataVpnVpnInstanceInstanceNameTopologyType topologyType)

The Topology Type of the IP VPN (e.g. any-to-any)

The Topology Type of the IP VPN (e.g. any-to-any)

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameTopologyTypePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var topologyType = new DataVpnVpnInstanceInstanceNameTopologyType(); // DataVpnVpnInstanceInstanceNameTopologyType | The Topology Type of the IP VPN (e.g. any-to-any)

            try
            {
                // The Topology Type of the IP VPN (e.g. any-to-any)
                apiInstance.DataVpnVpnInstanceInstanceNameTopologyTypePatch(instanceName, topologyType);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameTopologyTypePatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **topologyType** | [**DataVpnVpnInstanceInstanceNameTopologyType**](DataVpnVpnInstanceInstanceNameTopologyType.md)| The Topology Type of the IP VPN (e.g. any-to-any) | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamenamepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNamePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNamePatch (string instanceName, string vpnAttachmentSetName, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName name)

Name of the Attachment Set.

Name of the Attachment Set.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var name = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName | Name of the Attachment Set.

            try
            {
                // Name of the Attachment Set.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNamePatch(instanceName, vpnAttachmentSetName, name);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNamePatch: " + e.Message );
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
 **name** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName.md)| Name of the Attachment Set. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePatch (string instanceName, string vpnAttachmentSetName, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName vpnAttachmentSet)

VRF membership of the VPN

VRF membership of the VPN

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var vpnAttachmentSet = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName | VRF membership of the VPN

            try
            {
                // VRF membership of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePatch(instanceName, vpnAttachmentSetName, vpnAttachmentSet);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePatch: " + e.Message );
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
 **vpnAttachmentSet** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName.md)| VRF membership of the VPN | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePatch (string instanceName, string vpnAttachmentSetName, string pePeName, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName pe)

List of PE devices which have one or more VRFs which are members of the VPN

List of PE devices which have one or more VRFs which are members of the VPN

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var pe = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName | List of PE devices which have one or more VRFs which are members of the VPN

            try
            {
                // List of PE devices which have one or more VRFs which are members of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePatch(instanceName, vpnAttachmentSetName, pePeName, pe);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePatch: " + e.Message );
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
 **pe** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName.md)| List of PE devices which have one or more VRFs which are members of the VPN | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepenamepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNamePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNamePatch (string instanceName, string vpnAttachmentSetName, string pePeName, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName peName)

Name of the PE device

Name of the PE device

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var peName = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName | Name of the PE device

            try
            {
                // Name of the PE device
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNamePatch(instanceName, vpnAttachmentSetName, pePeName, peName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNamePatch: " + e.Message );
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
 **peName** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName.md)| Name of the PE device | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicypatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataVpnIpv4InboundRoutingPolicy ipv4InboundRoutingPolicy)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var ipv4InboundRoutingPolicy = new DataVpnIpv4InboundRoutingPolicy(); // DataVpnIpv4InboundRoutingPolicy | IPv4 outbound routing policy.

            try
            {
                // IPv4 outbound routing policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, ipv4InboundRoutingPolicy);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyPatch: " + e.Message );
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
 **ipv4InboundRoutingPolicy** | [**DataVpnIpv4InboundRoutingPolicy**](DataVpnIpv4InboundRoutingPolicy.md)| IPv4 outbound routing policy. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber autonomousSystemNumber)

Autonomous System Number component of the community

Autonomous System Number component of the community

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var autonomousSystemNumber = new DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber(); // DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber | Autonomous System Number component of the community

            try
            {
                // Autonomous System Number component of the community
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, autonomousSystemNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **autonomousSystemNumber** | [**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber.md)| Autonomous System Number component of the community | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberlocaliproutingpreferencepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferencePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferencePatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference localIpRoutingPreference)

The local IP routing preference for the community.

The local IP routing preference for the community.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferencePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var localIpRoutingPreference = new DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference(); // DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference | The local IP routing preference for the community.

            try
            {
                // The local IP routing preference for the community.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferencePatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, localIpRoutingPreference);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferencePatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **localIpRoutingPreference** | [**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference.md)| The local IP routing preference for the community. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber number)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var number = new DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber(); // DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, number);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **number** | [**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber.md)|  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber tenantCommunity)

List of BGP communities for routes towards Tenant Networks.

List of BGP communities for routes towards Tenant Networks.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var tenantCommunity = new DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber(); // DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber | List of BGP communities for routes towards Tenant Networks.

            try
            {
                // List of BGP communities for routes towards Tenant Networks.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, tenantCommunity);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **tenantCommunity** | [**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber.md)| List of BGP communities for routes towards Tenant Networks. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength lessThanOrEqualToLength)

Include prefix lengths up to and including the specified length.

Include prefix lengths up to and including the specified length.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var lessThanOrEqualToLength = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength | Include prefix lengths up to and including the specified length.

            try
            {
                // Include prefix lengths up to and including the specified length.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, lessThanOrEqualToLength);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **lessThanOrEqualToLength** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength.md)| Include prefix lengths up to and including the specified length. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlocaliproutingpreferencepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferencePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferencePatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference localIpRoutingPreference)

The IP routing preference for the prefix.

The IP routing preference for the prefix.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferencePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var localIpRoutingPreference = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference | The IP routing preference for the prefix.

            try
            {
                // The IP routing preference for the prefix.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferencePatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, localIpRoutingPreference);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferencePatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **localIpRoutingPreference** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference.md)| The IP routing preference for the prefix. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix tenantIpv4Prefix)

List of IPv4 prefixes for routes towards Tenant Networks.

List of IPv4 prefixes for routes towards Tenant Networks.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantIpv4Prefix = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix | List of IPv4 prefixes for routes towards Tenant Networks.

            try
            {
                // List of IPv4 prefixes for routes towards Tenant Networks.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantIpv4Prefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **tenantIpv4Prefix** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix.md)| List of IPv4 prefixes for routes towards Tenant Networks. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix prefix)

An IPv4 prefix and length in CIDR form, x.x.x.x/n

An IPv4 prefix and length in CIDR form, x.x.x.x/n

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var prefix = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // An IPv4 prefix and length in CIDR form, x.x.x.x/n
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, prefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **prefix** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix.md)| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber autonomousSystemNumber)

Autonomous System Number component of the community

Autonomous System Number component of the community

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var autonomousSystemNumber = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber | Autonomous System Number component of the community

            try
            {
                // Autonomous System Number component of the community
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, autonomousSystemNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **autonomousSystemNumber** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber.md)| Autonomous System Number component of the community | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber number)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var number = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, number);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **number** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber.md)|  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber tenantCommunity)

List of BGP communities which are associated with the prefix.

List of BGP communities which are associated with the prefix.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var tenantCommunity = new DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber(); // DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber | List of BGP communities which are associated with the prefix.

            try
            {
                // List of BGP communities which are associated with the prefix.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, tenantCommunity);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **tenantCommunity** | [**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber.md)| List of BGP communities which are associated with the prefix. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicypatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataVpnIpv4OutboundRoutingPolicy ipv4OutboundRoutingPolicy)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var ipv4OutboundRoutingPolicy = new DataVpnIpv4OutboundRoutingPolicy(); // DataVpnIpv4OutboundRoutingPolicy | IPv4 outbound routing policy.

            try
            {
                // IPv4 outbound routing policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, ipv4OutboundRoutingPolicy);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyPatch: " + e.Message );
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
 **ipv4OutboundRoutingPolicy** | [**DataVpnIpv4OutboundRoutingPolicy**](DataVpnIpv4OutboundRoutingPolicy.md)| IPv4 outbound routing policy. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberadvertisediproutingpreferencepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferencePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferencePatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference advertisedIpRoutingPreference)

The advertised IP routing preference for the community.

The advertised IP routing preference for the community.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferencePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var advertisedIpRoutingPreference = new DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference(); // DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference | The advertised IP routing preference for the community.

            try
            {
                // The advertised IP routing preference for the community.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferencePatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, advertisedIpRoutingPreference);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferencePatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **advertisedIpRoutingPreference** | [**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference.md)| The advertised IP routing preference for the community. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber autonomousSystemNumber)

Autonomous System Number component of the community

Autonomous System Number component of the community

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var autonomousSystemNumber = new DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber(); // DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber | Autonomous System Number component of the community

            try
            {
                // Autonomous System Number component of the community
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, autonomousSystemNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberPatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **autonomousSystemNumber** | [**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber.md)| Autonomous System Number component of the community | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber number)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var number = new DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber(); // DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, number);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberPatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **number** | [**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber.md)|  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber tenantCommunity)

List of BGP communities for sets of Tenant Routes.

List of BGP communities for sets of Tenant Routes.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var tenantCommunity = new DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber(); // DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber | List of BGP communities for sets of Tenant Routes.

            try
            {
                // List of BGP communities for sets of Tenant Routes.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, tenantCommunity);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberPatch: " + e.Message );
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
 **tenantCommunityAutonomousSystemNumber** | **int?**| Autonomous System Number component of the community | 
 **tenantCommunityNumber** | **int?**|  | 
 **tenantCommunity** | [**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber.md)| List of BGP communities for sets of Tenant Routes. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixadvertisediproutingpreferencepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferencePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferencePatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference advertisedIpRoutingPreference)

The advertised IP routing preference for the prefix.

The advertised IP routing preference for the prefix.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferencePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var advertisedIpRoutingPreference = new DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference(); // DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference | The advertised IP routing preference for the prefix.

            try
            {
                // The advertised IP routing preference for the prefix.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferencePatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, advertisedIpRoutingPreference);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferencePatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **advertisedIpRoutingPreference** | [**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference.md)| The advertised IP routing preference for the prefix. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength lessThanOrEqualToLength)

Include prefix lengths up to and including the specified length.

Include prefix lengths up to and including the specified length.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var lessThanOrEqualToLength = new DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength(); // DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength | Include prefix lengths up to and including the specified length.

            try
            {
                // Include prefix lengths up to and including the specified length.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, lessThanOrEqualToLength);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **lessThanOrEqualToLength** | [**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength.md)| Include prefix lengths up to and including the specified length. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix tenantIpv4Prefix)

List of IPv4 prefixes for routes towards Tenant Networks.

List of IPv4 prefixes for routes towards Tenant Networks.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantIpv4Prefix = new DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix(); // DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix | List of IPv4 prefixes for routes towards Tenant Networks.

            try
            {
                // List of IPv4 prefixes for routes towards Tenant Networks.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantIpv4Prefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **tenantIpv4Prefix** | [**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix.md)| List of IPv4 prefixes for routes towards Tenant Networks. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixpatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix prefix)

An IPv4 prefix and length in CIDR form, x.x.x.x/n

An IPv4 prefix and length in CIDR form, x.x.x.x/n

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var prefix = new DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix(); // DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // An IPv4 prefix and length in CIDR form, x.x.x.x/n
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, prefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixPatch: " + e.Message );
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
 **tenantIpv4PrefixPrefix** | **string**| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 
 **prefix** | [**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix.md)| An IPv4 prefix and length in CIDR form, x.x.x.x/n | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address bgpPeer)

List of BGP peers which require network-based outbound routing                policy.

List of BGP peers which require network-based outbound routing                policy.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var bgpPeer = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address | List of BGP peers which require network-based outbound routing                policy.

            try
            {
                // List of BGP peers which require network-based outbound routing                policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, bgpPeer);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPatch: " + e.Message );
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
 **bgpPeer** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address.md)| List of BGP peers which require network-based outbound routing                policy. | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addresspatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, DataVpnPeerIpv4Address peerIpv4Address)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var peerIpv4Address = new DataVpnPeerIpv4Address(); // DataVpnPeerIpv4Address | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, peerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressPatch: " + e.Message );
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
 **peerIpv4Address** | [**DataVpnPeerIpv4Address**](DataVpnPeerIpv4Address.md)|  | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNamePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNamePatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName vrf)

List of VRFs which are members of the VPN

List of VRFs which are members of the VPN

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var vrf = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName | List of VRFs which are members of the VPN

            try
            {
                // List of VRFs which are members of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNamePatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, vrf);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNamePatch: " + e.Message );
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
 **vrf** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName.md)| List of VRFs which are members of the VPN | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamevrfnamepatch"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNamePatch**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNamePatch (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName vrfName)

VRF which is a member of the VPN

VRF which is a member of the VPN

### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNamePatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var vrfName = new DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName(); // DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName | VRF which is a member of the VPN

            try
            {
                // VRF which is a member of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNamePatch(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, vrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNamePatch: " + e.Message );
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
 **vrfName** | [**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName.md)| VRF which is a member of the VPN | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpnpatch"></a>
# **DataVpnVpnPatch**
> void DataVpnVpnPatch (DataVpnVpn vpn)

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
    public class DataVpnVpnPatchExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new PatchApi();
            var vpn = new DataVpnVpn(); // DataVpnVpn | VPN service container

            try
            {
                // VPN service container
                apiInstance.DataVpnVpnPatch(vpn);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PatchApi.DataVpnVpnPatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vpn** | [**DataVpnVpn**](DataVpnVpn.md)| VPN service container | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

