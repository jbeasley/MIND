# IO.NovaVpnSwagger.Api.DeleteApi

All URIs are relative to *http://192.168.56.1:8080/restconf*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataVpnVpnDelete**](DeleteApi.md#datavpnvpndelete) | **DELETE** /data/services/vpn:vpn | VPN service container
[**DataVpnVpnInstanceInstanceNameAddressFamilyDelete**](DeleteApi.md#datavpnvpninstanceinstancenameaddressfamilydelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/address-family | The address-family of the IP VPN (e.g. IPv4)
[**DataVpnVpnInstanceInstanceNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name} | List of VPN instances
[**DataVpnVpnInstanceInstanceNameIsExtranetDelete**](DeleteApi.md#datavpnvpninstanceinstancenameisextranetdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/is-extranet | Determines whether the VPN supports Extranet
[**DataVpnVpnInstanceInstanceNameNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamenamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/name | VPN service name
[**DataVpnVpnInstanceInstanceNameProtocolTypeDelete**](DeleteApi.md#datavpnvpninstanceinstancenameprotocoltypedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/protocol-type | The Protocol Type of the VPN (e.g. IP)
[**DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldDelete**](DeleteApi.md#datavpnvpninstanceinstancenameroutetargetaadministratorsubfielddelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A/administrator-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldDelete**](DeleteApi.md#datavpnvpninstanceinstancenameroutetargetaassignednumbersubfielddelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A/assigned-number-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetADelete**](DeleteApi.md#datavpnvpninstanceinstancenameroutetargetadelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-A | Route-target &#39;A&#39;. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
[**DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldDelete**](DeleteApi.md#datavpnvpninstanceinstancenameroutetargetbadministratorsubfielddelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B/administrator-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldDelete**](DeleteApi.md#datavpnvpninstanceinstancenameroutetargetbassignednumbersubfielddelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B/assigned-number-subfield | The 2 byte or 4 byte administrator sub-field value of the route-target
[**DataVpnVpnInstanceInstanceNameRouteTargetBDelete**](DeleteApi.md#datavpnvpninstanceinstancenameroutetargetbdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/route-target-B | Route-target &#39;B&#39;. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
[**DataVpnVpnInstanceInstanceNameTopologyTypeDelete**](DeleteApi.md#datavpnvpninstanceinstancenametopologytypedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/topology-type | The Topology Type of the IP VPN (e.g. any-to-any)
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name} | VRF membership of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamenamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/name | Name of the Attachment Set.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name} | List of PE devices which have one or more VRFs which are members of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepenamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/pe-name | Name of the PE device
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address} | List of BGP peers which require network-based outbound routing                policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicydelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberlocaliproutingpreferencedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/local-ip-routing-preference | The local IP routing preference for the community.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix} | List of IPv4 prefixes for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/less-than-or-equal-to-length | Include prefix lengths up to and including the specified length.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlocaliproutingpreferencedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/local-ip-routing-preference | The IP routing preference for the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/prefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities which are associated with the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-inbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicydelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy | IPv4 outbound routing policy.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberadvertisediproutingpreferencedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/advertised-ip-routing-preference | The advertised IP routing preference for the community.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/autonomous-system-number | Autonomous System Number component of the community
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number} | List of BGP communities for sets of Tenant Routes.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-community&#x3D;{tenant-community-autonomous-system-number},{tenant-community-number}/number | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixadvertisediproutingpreferencedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/advertised-ip-routing-preference | The advertised IP routing preference for the prefix.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix} | List of IPv4 prefixes for routes towards Tenant Networks.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/less-than-or-equal-to-length | Include prefix lengths up to and including the specified length.
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/ipv4-outbound-routing-policy/tenant-ipv4-prefix&#x3D;{tenant-ipv4-prefix-prefix}/prefix | An IPv4 prefix and length in CIDR form, x.x.x.x/n
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressdelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/bgp-peer&#x3D;{bgp-peer-peer-ipv4-address}/peer-ipv4-address | 
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name} | List of VRFs which are members of the VPN
[**DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameDelete**](DeleteApi.md#datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamevrfnamedelete) | **DELETE** /data/services/vpn:vpn/instance&#x3D;{instance-name}/vpn-attachment-set&#x3D;{vpn-attachment-set-name}/pe&#x3D;{pe-pe-name}/vrf&#x3D;{vrf-vrf-name}/vrf-name | VRF which is a member of the VPN


<a name="datavpnvpndelete"></a>
# **DataVpnVpnDelete**
> void DataVpnVpnDelete ()

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
    public class DataVpnVpnDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();

            try
            {
                // VPN service container
                apiInstance.DataVpnVpnDelete();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnDelete: " + e.Message );
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

<a name="datavpnvpninstanceinstancenameaddressfamilydelete"></a>
# **DataVpnVpnInstanceInstanceNameAddressFamilyDelete**
> void DataVpnVpnInstanceInstanceNameAddressFamilyDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameAddressFamilyDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The address-family of the IP VPN (e.g. IPv4)
                apiInstance.DataVpnVpnInstanceInstanceNameAddressFamilyDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameAddressFamilyDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamedelete"></a>
# **DataVpnVpnInstanceInstanceNameDelete**
> void DataVpnVpnInstanceInstanceNameDelete (string instanceName, bool? noOutOfSyncCheck = null)

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
    public class DataVpnVpnInstanceInstanceNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var noOutOfSyncCheck = true;  // bool? | Commit even if out of sync (optional) 

            try
            {
                // List of VPN instances
                apiInstance.DataVpnVpnInstanceInstanceNameDelete(instanceName, noOutOfSyncCheck);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 
 **noOutOfSyncCheck** | **bool?**| Commit even if out of sync | [optional] 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameisextranetdelete"></a>
# **DataVpnVpnInstanceInstanceNameIsExtranetDelete**
> void DataVpnVpnInstanceInstanceNameIsExtranetDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameIsExtranetDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // Determines whether the VPN supports Extranet
                apiInstance.DataVpnVpnInstanceInstanceNameIsExtranetDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameIsExtranetDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamenamedelete"></a>
# **DataVpnVpnInstanceInstanceNameNameDelete**
> void DataVpnVpnInstanceInstanceNameNameDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // VPN service name
                apiInstance.DataVpnVpnInstanceInstanceNameNameDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameNameDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameprotocoltypedelete"></a>
# **DataVpnVpnInstanceInstanceNameProtocolTypeDelete**
> void DataVpnVpnInstanceInstanceNameProtocolTypeDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameProtocolTypeDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The Protocol Type of the VPN (e.g. IP)
                apiInstance.DataVpnVpnInstanceInstanceNameProtocolTypeDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameProtocolTypeDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaadministratorsubfielddelete"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldDelete**
> void DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameRouteTargetAAdministratorSubfieldDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetaassignednumbersubfielddelete"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldDelete**
> void DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameRouteTargetAAssignedNumberSubfieldDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetadelete"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetADelete**
> void DataVpnVpnInstanceInstanceNameRouteTargetADelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetADeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // Route-target 'A'. If this VPN topology is Any-to-Any then this route-target is used for both import and export. If this VPN topology is Hub-and-Spoke then this route-target is used for export at hub VRFs and for import at spoke VRFs
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetADelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameRouteTargetADelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbadministratorsubfielddelete"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldDelete**
> void DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameRouteTargetBAdministratorSubfieldDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbassignednumbersubfielddelete"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldDelete**
> void DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The 2 byte or 4 byte administrator sub-field value of the route-target
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameRouteTargetBAssignedNumberSubfieldDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenameroutetargetbdelete"></a>
# **DataVpnVpnInstanceInstanceNameRouteTargetBDelete**
> void DataVpnVpnInstanceInstanceNameRouteTargetBDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameRouteTargetBDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // Route-target 'B'. If this VPN topology is Any-to-Any then this route-target is NOT used. If this VPN topology is Hub-and-Spoke then this route-target is used for import at hub VRFs and for export at spoke VRFs 
                apiInstance.DataVpnVpnInstanceInstanceNameRouteTargetBDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameRouteTargetBDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenametopologytypedelete"></a>
# **DataVpnVpnInstanceInstanceNameTopologyTypeDelete**
> void DataVpnVpnInstanceInstanceNameTopologyTypeDelete (string instanceName)

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
    public class DataVpnVpnInstanceInstanceNameTopologyTypeDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name

            try
            {
                // The Topology Type of the IP VPN (e.g. any-to-any)
                apiInstance.DataVpnVpnInstanceInstanceNameTopologyTypeDelete(instanceName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameTopologyTypeDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **instanceName** | **string**| VPN service name | 

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameDelete (string instanceName, string vpnAttachmentSetName)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.

            try
            {
                // VRF membership of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameDelete(instanceName, vpnAttachmentSetName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamenamedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameDelete (string instanceName, string vpnAttachmentSetName)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.

            try
            {
                // Name of the Attachment Set.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameDelete(instanceName, vpnAttachmentSetName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNameNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameDelete (string instanceName, string vpnAttachmentSetName, string pePeName)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device

            try
            {
                // List of PE devices which have one or more VRFs which are members of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameDelete(instanceName, vpnAttachmentSetName, pePeName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamepenamedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameDelete (string instanceName, string vpnAttachmentSetName, string pePeName)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device

            try
            {
                // Name of the PE device
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameDelete(instanceName, vpnAttachmentSetName, pePeName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNamePeNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 

            try
            {
                // List of BGP peers which require network-based outbound routing                policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicydelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 

            try
            {
                // IPv4 outbound routing policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // Autonomous System Number component of the community
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // List of BGP communities for routes towards Tenant Networks.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberlocaliproutingpreferencedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // The local IP routing preference for the community.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberLocalIpRoutingPreferenceDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // List of IPv4 prefixes for routes towards Tenant Networks.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // Include prefix lengths up to and including the specified length.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlocaliproutingpreferencedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // The IP routing preference for the prefix.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLocalIpRoutingPreferenceDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // An IPv4 prefix and length in CIDR form, x.x.x.x/n
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // Autonomous System Number component of the community
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // List of BGP communities which are associated with the prefix.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4inboundroutingpolicytenantipv4prefixtenantipv4prefixprefixtenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicydelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 

            try
            {
                // IPv4 outbound routing policy.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberadvertisediproutingpreferencedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // The advertised IP routing preference for the community.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAdvertisedIpRoutingPreferenceDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberautonomoussystemnumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // Autonomous System Number component of the community
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberAutonomousSystemNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                // List of BGP communities for sets of Tenant Routes.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantcommunitytenantcommunityautonomoussystemnumbertenantcommunitynumbernumberdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, int? tenantCommunityAutonomousSystemNumber, int? tenantCommunityNumber)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantCommunityAutonomousSystemNumber = 56;  // int? | Autonomous System Number component of the community
            var tenantCommunityNumber = 56;  // int? | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantCommunityAutonomousSystemNumber, tenantCommunityNumber);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumberDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixadvertisediproutingpreferencedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // The advertised IP routing preference for the prefix.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixAdvertisedIpRoutingPreferenceDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // List of IPv4 prefixes for routes towards Tenant Networks.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixlessthanorequaltolengthdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // Include prefix lengths up to and including the specified length.
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixLessThanOrEqualToLengthDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addressipv4outboundroutingpolicytenantipv4prefixtenantipv4prefixprefixprefixdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address, string tenantIpv4PrefixPrefix)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 
            var tenantIpv4PrefixPrefix = tenantIpv4PrefixPrefix_example;  // string | An IPv4 prefix and length in CIDR form, x.x.x.x/n

            try
            {
                // An IPv4 prefix and length in CIDR form, x.x.x.x/n
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address, tenantIpv4PrefixPrefix);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressIpv4OutboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixPrefixDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamebgppeerbgppeerpeeripv4addresspeeripv4addressdelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName, string bgpPeerPeerIpv4Address)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Model;

namespace Example
{
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN
            var bgpPeerPeerIpv4Address = bgpPeerPeerIpv4Address_example;  // string | 

            try
            {
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName, bgpPeerPeerIpv4Address);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameBgpPeerBgpPeerPeerIpv4AddressPeerIpv4AddressDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN

            try
            {
                // List of VRFs which are members of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datavpnvpninstanceinstancenamevpnattachmentsetvpnattachmentsetnamepepepenamevrfvrfvrfnamevrfnamedelete"></a>
# **DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameDelete**
> void DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameDelete (string instanceName, string vpnAttachmentSetName, string pePeName, string vrfVrfName)

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
    public class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameDeleteExample
    {
        public void main()
        {
            // Configure HTTP basic authorization: basicAuth
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new DeleteApi();
            var instanceName = instanceName_example;  // string | VPN service name
            var vpnAttachmentSetName = vpnAttachmentSetName_example;  // string | Name of the Attachment Set.
            var pePeName = pePeName_example;  // string | Name of the PE device
            var vrfVrfName = vrfVrfName_example;  // string | VRF which is a member of the VPN

            try
            {
                // VRF which is a member of the VPN
                apiInstance.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameDelete(instanceName, vpnAttachmentSetName, pePeName, vrfVrfName);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DeleteApi.DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetNamePePePeNameVrfVrfVrfNameVrfNameDelete: " + e.Message );
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

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/yang-data+json
 - **Accept**: application/yang-data+json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

