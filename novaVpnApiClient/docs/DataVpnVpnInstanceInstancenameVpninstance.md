# IO.NovaVpnSwagger.Model.DataVpnVpnInstanceInstancenameVpninstance
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | VPN service name (leaf) | [optional] 
**ProtocolType** | **string** | The Protocol Type of the VPN (e.g. IP) (leaf) | [optional] [default to ProtocolTypeEnum.IP]
**VpnAttachmentSet** | [**List&lt;DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnameVpnvpnattachmentset&gt;**](DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnameVpnvpnattachmentset.md) | VRF membership of the VPN (list) | [optional] 
**AddressFamily** | **string** | The address-family of the IP VPN (e.g. IPv4) (leaf) | [optional] [default to AddressFamilyEnum.IPv4]
**TopologyType** | **string** | The Topology Type of the IP VPN (e.g. any-to-any) (leaf) | [optional] [default to TopologyTypeEnum.AnyToAny]
**IsExtranet** | **string** | Determines whether the VPN supports Extranet (leaf) | [optional] 
**RouteTargetA** | [**DataVpnVpnInstanceInstancenameRoutetargetAVpnroutetargetA**](DataVpnVpnInstanceInstancenameRoutetargetAVpnroutetargetA.md) |  | [optional] 
**RouteTargetB** | [**DataVpnVpnInstanceInstancenameRoutetargetBVpnroutetargetB**](DataVpnVpnInstanceInstancenameRoutetargetBVpnroutetargetB.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

