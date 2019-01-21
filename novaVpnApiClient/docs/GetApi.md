# IO.NovaVpnSwagger.Api.GetApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataGet**](GetApi.md#dataget) | **GET** /data | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
[**DataVpnVpnGet**](GetApi.md#datavpnvpnget) | **GET** /data/services/vpn:vpn | VPN service container
[**DataVpnVpnInstanceInstanceNameAddressFamilyGet**](GetApi.md#datavpnvpninstanceinstancenameaddressfamilyget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/address-family | The address-family of the IP VPN (e.g. IPv4)
[**DataVpnVpnInstanceInstanceNameGet**](GetApi.md#datavpnvpninstanceinstancenameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name} | List of VPN instances
[**DataVpnVpnInstanceInstanceNameIsExtranetGet**](GetApi.md#datavpnvpninstanceinstancenameisextranetget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/is-extranet | Determines whether the VPN supports Extranet
[**DataVpnVpnInstanceInstanceNameNameGet**](GetApi.md#datavpnvpninstanceinstancenamenameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/name | VPN service name
[**DataVpnVpnInstanceInstanceNameProtocolTypeGet**](GetApi.md#datavpnvpninstanceinstancenameprotocoltypeget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/protocol-type | The Protocol Type of the VPN (e.g. IP)
[**DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldGet**](GetApi.md#datavpnvpninstanceinstancenameroutetargetaadministratorsubfieldget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A/administrator-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldGet**](GetApi.md#datavpnvpninstanceinstancenameroutetargetaassignednumbersubfieldget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A/assigned-number-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetAGet**](GetApi.md#datavpnvpninstanceinstancenameroutetargetaget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A | Route-target &#39;A&#39;. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
[**DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldGet**](GetApi.md#datavpnvpninstanceinstancenameroutetargetbadministratorsubfieldget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B/administrator-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldGet**](GetApi.md#datavpnvpninstanceinstancenameroutetargetbassignednumbersubfieldget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B/assigned-number-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetBGet**](GetApi.md#datavpnvpninstanceinstancenameroutetargetbget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B | Route-target &#39;B&#39;. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
[**DataVpnVpnInstanceInstanceNameTopologyTypeGet**](GetApi.md#datavpnvpninstanceinstancenametopologytypeget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/topology-type | The Topology Type of the IP VPN (e.g. any-to-any)
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name} | VRF membership of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamenameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/name | Name of the Attachment Set.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name} | List of PE devices which have one or more VRFs which are members of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepenameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/pe-name | Name of the PE device
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address} | List of BGP peers which require network-based outbound routing                policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicyget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberlocaliproutingpreferenceget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/local-ip-routing-preference | The local IP routing preference for the community.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix} | List of IPv4 prefixes for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/less-than-or-equal-to-length | Include prefix lengths up to and including the specified length.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlocaliproutingpreferenceget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/local-ip-routing-preference | The IP routing preference for the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/prefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities which are associated with the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicyget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberadvertisediproutingpreferenceget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/advertised-ip-routing-preference | The advertised IP routing preference for the community.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities for sets of Tenant Routes.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixadvertisediproutingpreferenceget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/advertised-ip-routing-preference | The advertised IP routing preference for the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix} | List of IPv4 prefixes for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/less-than-or-equal-to-length | Include prefix lengths up to and including the specified length.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/prefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-ipv4-address | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs which are members of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameGet**](GetApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamevrfnameget) | **GET** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/vrf-name | VRF which is a member of the VPN
[**OperationsGet**](GetApi.md#operationsget) | **GET** /operations | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
[**RootGet**](GetApi.md#rootget) | **GET** / | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
[**YangLibraryVersionGet**](GetApi.md#yanglibraryversionget) | **GET** /yang-library-version | This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018


<a name="dataget"></a>
# **DataGet**
> Data DataGet ()

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
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
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

<a name="datavpnvpnget"></a>
# **DataVpnVpnGet**
> DataVpnVpn DataVpnVpnGet (string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnGetExample
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
                // VPN service container
                DataVpnVpn result = apiInstance.DataVpnVpnGet(content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnGet: " + e.Message );
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

[**DataVpnVpn**](DataVpnVpn.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameaddressfamilyget"></a>
# **DataVpnVpnInstanceInstanceNameAddressFamilyGet**
> DataVpnVpnInstanceInstanceNameAddressFamily DataVpnVpnInstanceInstanceNameAddressFamilyGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameAddressFamilyGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The address-family of the IP VPN (e.g. IPv4)
                DataVpnVpnInstanceInstanceNameAddressFamily result = apiInstance.DataVpnVpnInstanceInstanceNameAddressFamilyGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameAddressFamilyGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameAddressFamily**](DataVpnVpnInstanceInstanceNameAddressFamily.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameget"></a>
# **DataVpnVpnInstanceInstanceNameGet**
> DataVpnVpnInstanceInstanceName DataVpnVpnInstanceInstanceNameGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of VPN instances
                DataVpnVpnInstanceInstanceName result = apiInstance.DataVpnVpnInstanceInstanceNameGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceName**](DataVpnVpnInstanceInstanceName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameisextranetget"></a>
# **DataVpnVpnInstanceInstanceNameIsExtranetGet**
> DataVpnVpnInstanceInstanceNameIsExtranet DataVpnVpnInstanceInstanceNameIsExtranetGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameIsExtranetGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Determines whether the VPN supports Extranet
                DataVpnVpnInstanceInstanceNameIsExtranet result = apiInstance.DataVpnVpnInstanceInstanceNameIsExtranetGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameIsExtranetGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameIsExtranet**](DataVpnVpnInstanceInstanceNameIsExtranet.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamenameget"></a>
# **DataVpnVpnInstanceInstanceNameNameGet**
> DataVpnVpnInstanceInstanceNameName DataVpnVpnInstanceInstanceNameNameGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // VPN service name
                DataVpnVpnInstanceInstanceNameName result = apiInstance.DataVpnVpnInstanceInstanceNameNameGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameNameGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameName**](DataVpnVpnInstanceInstanceNameName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameprotocoltypeget"></a>
# **DataVpnVpnInstanceInstanceNameProtocolTypeGet**
> DataVpnVpnInstanceInstanceNameProtocolType DataVpnVpnInstanceInstanceNameProtocolTypeGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameProtocolTypeGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The Protocol Type of the VPN (e.g. IP)
                DataVpnVpnInstanceInstanceNameProtocolType result = apiInstance.DataVpnVpnInstanceInstanceNameProtocolTypeGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameProtocolTypeGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameProtocolType**](DataVpnVpnInstanceInstanceNameProtocolType.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaadministratorsubfieldget"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldGet**
> DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield result = apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfield.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaassignednumbersubfieldget"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldGet**
> DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield result = apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfield.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaget"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAGet**
> DataVpnVpnInstanceInstanceNameRouteTargetA DataVpnVpnInstanceInstanceNameRouteTargetAGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetAGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
                DataVpnVpnInstanceInstanceNameRouteTargetA result = apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameRouteTargetAGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameRouteTargetA**](DataVpnVpnInstanceInstanceNameRouteTargetA.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbadministratorsubfieldget"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldGet**
> DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield result = apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfield.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbassignednumbersubfieldget"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldGet**
> DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield result = apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield**](DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfield.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbget"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBGet**
> DataVpnVpnInstanceInstanceNameRouteTargetB DataVpnVpnInstanceInstanceNameRouteTargetBGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
                DataVpnVpnInstanceInstanceNameRouteTargetB result = apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameRouteTargetBGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameRouteTargetB**](DataVpnVpnInstanceInstanceNameRouteTargetB.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenametopologytypeget"></a>
# **DataVpnVpnInstanceInstanceNameTopologyTypeGet**
> DataVpnVpnInstanceInstanceNameTopologyType DataVpnVpnInstanceInstanceNameTopologyTypeGet (string instanceName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameTopologyTypeGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The Topology Type of the IP VPN (e.g. any-to-any)
                DataVpnVpnInstanceInstanceNameTopologyType result = apiInstance.DataVpnVpnInstanceInstanceNameTopologyTypeGet(instanceName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameTopologyTypeGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameTopologyType**](DataVpnVpnInstanceInstanceNameTopologyType.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnameget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameGet (string instanceName, string vpnAttachmentSetName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // VRF membership of the VPN
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameGet(instanceName, vpnAttachmentSetName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamenameget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameGet (string instanceName, string vpnAttachmentSetName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Name of the Attachment Set.
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameGet(instanceName, vpnAttachmentSetName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenameget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameGet (string instanceName, string vpnAttachmentSetName, string pePeName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of PE devices which have one or more VRFs which are members of the VPN
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameGet(instanceName, vpnAttachmentSetName, pePeName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepenameget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameGet (string instanceName, string vpnAttachmentSetName, string pePeName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Name of the PE device
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameGet(instanceName, vpnAttachmentSetName, pePeName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of BGP peers which require network-based outbound routing                policy.
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4Address.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicyget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyGet**
> DataVpnIpv4InboundRoutingPolicy DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // IPv4 outbound routing policy.
                DataVpnIpv4InboundRoutingPolicy result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicy**](DataVpnIpv4InboundRoutingPolicy.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet**
> DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Autonomous System Number component of the community
                DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet**
> DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of BGP communities for routes towards Tenant Networks.
                DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberlocaliproutingpreferenceget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceGet**
> DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The local IP routing preference for the community.
                DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreference.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet**
> DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber**](DataVpnIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of IPv4 prefixes for routes towards Tenant Networks.
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Include prefix lengths up to and including the specified length.
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlocaliproutingpreferenceget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The IP routing preference for the prefix.
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreference.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // An IPv4 prefix and length in CIDR form, x.x.x.x/n
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Autonomous System Number component of the community
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of BGP communities which are associated with the prefix.
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet**
> DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber**](DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicyget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyGet**
> DataVpnIpv4OutboundRoutingPolicy DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // IPv4 outbound routing policy.
                DataVpnIpv4OutboundRoutingPolicy result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicy**](DataVpnIpv4OutboundRoutingPolicy.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberadvertisediproutingpreferenceget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceGet**
> DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The advertised IP routing preference for the community.
                DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreference.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet**
> DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Autonomous System Number component of the community
                DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet**
> DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of BGP communities for sets of Tenant Routes.
                DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet**
> DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber**](DataVpnIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixadvertisediproutingpreferenceget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceGet**
> DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // The advertised IP routing preference for the prefix.
                DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreference.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet**
> DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of IPv4 prefixes for routes towards Tenant Networks.
                DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefix.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet**
> DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // Include prefix lengths up to and including the specified length.
                DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLength.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet**
> DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // An IPv4 prefix and length in CIDR form, x.x.x.x/n
                DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix**](DataVpnIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefix.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet**
> DataVpnPeerIpv4Address DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                DataVpnPeerIpv4Address result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnPeerIpv4Address**](DataVpnPeerIpv4Address.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnameget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // List of VRFs which are members of the VPN
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamevrfnameget"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameGet**
> DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameGet (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string content = null, int? depth = null, string fields = null, string filter = null, string withDefaults = null)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameGetExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new GetApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var content = content_example;  // string | controlling descendant nodes in response (optional)  (default to config)
            var depth = 56;  // int? | limit the depth of nodes in response (optional) 
            var fields = fields_example;  // string | optionally identify specific data nodes in response (optional) 
            var filter = filter_example;  // string | xpath expression to filter data nodes in response (optional) 
            var withDefaults = withDefaults_example;  // string | controlling default values in response (optional)  (default to report-all)

            try
            {
                // VRF which is a member of the VPN
                DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName result = apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameGet(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, content, depth, fields, filter, withDefaults);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GetApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameGet: " + e.Message );
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
 **content** | **string**| controlling descendant nodes in response | [optional] [default to config]
 **depth** | **int?**| limit the depth of nodes in response | [optional] 
 **fields** | **string**| optionally identify specific data nodes in response | [optional] 
 **filter** | **string**| xpath expression to filter data nodes in response | [optional] 
 **withDefaults** | **string**| controlling default values in response | [optional] [default to report-all]

### Return type

[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName**](DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfName.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="operationsget"></a>
# **OperationsGet**
> Operations OperationsGet ()

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
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
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

            var apiInstance = new GetApi();

            try
            {
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
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
                // This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
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

