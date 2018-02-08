using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Api.Models;
using AutoMapper;
using SCM.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SCM.Data;
using SCM.Validators;
using SCM.Models.RequestModels;

namespace SCM.Api.Validators
{ 
    /// <summary>
    /// Validates API requests for VPNs
    /// </summary>
    public class VpnApiValidator : BaseApiValidator, IVpnApiValidator
    {
        public VpnApiValidator(
            ITenantService tenantService,
            IVpnTenancyTypeService vpnTenancyTypeService,
            IVpnTopologyTypeService vpnTopologyTypeService,
            IPlaneService planeService,
            IRegionService regionService,
            IMulticastVpnServiceTypeService multicastVpnServiceTypeService,
            IMulticastVpnDirectionTypeService multicastVpnDirectionTypeService,
            IVpnValidator vpnValidator,
            IRouteTargetValidator routeTargetValidator,
            IMapper mapper)
        {
            TenantService = tenantService;
            VpnTopologyTypeService = vpnTopologyTypeService;
            VpnTenancyTypeService = vpnTenancyTypeService;
            RegionService = regionService;
            PlaneService = planeService;
            MulticastVpnServiceTypeService = multicastVpnServiceTypeService;
            MulticastVpnDirectionTypeService = multicastVpnDirectionTypeService;
            Mapper = mapper;

            VpnValidator = vpnValidator;
            RouteTargetValidator = routeTargetValidator;
        }

        private ITenantService TenantService { get; }
        private IVpnTopologyTypeService VpnTopologyTypeService { get; }
        private IVpnTenancyTypeService VpnTenancyTypeService { get; }
        private IPlaneService PlaneService { get; }
        private IRegionService RegionService { get; }
        private IMulticastVpnServiceTypeService MulticastVpnServiceTypeService { get; }
        private IMulticastVpnDirectionTypeService MulticastVpnDirectionTypeService { get; }
        private IVpnValidator VpnValidator { get; } 
        private IRouteTargetValidator RouteTargetValidator { get; }
        private IMapper Mapper { get; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                VpnValidator.ValidationDictionary = value;
                RouteTargetValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new Vpn request
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnRequestApiModel apiRequest)
        {
            var tenant = await TenantService.GetByIDAsync(apiRequest.TenantID.Value);
            if (tenant == null)
            {
                ValidationDictionary.AddError("TenantID", "The Tenant was not found.");
            }

            var topologyType = await VpnTopologyTypeService.GetByIDAsync(apiRequest.VpnTopologyTypeID.Value);
            if (topologyType == null)
            {
                ValidationDictionary.AddError("VpnTopologyTypeID", "The Topology Type was not found.");
            }

            var tenancyType = await VpnTenancyTypeService.GetByIDAsync(apiRequest.VpnTenancyTypeID.Value);
            if (tenancyType == null)
            {
                ValidationDictionary.AddError("VpnTenancyTypeID", "The Tenancy Type was not found.");
            }

            if (apiRequest.PlaneID != null)
            {
                var plane = await PlaneService.GetByIDAsync(apiRequest.PlaneID.Value);
                if (plane == null)
                {
                    ValidationDictionary.AddError("PlaneID", "The Plane was not found.");
                }
            }

            if (apiRequest.RegionID != null)
            {
                var region = await RegionService.GetByIDAsync(apiRequest.RegionID.Value);
                if (region == null)
                {
                    ValidationDictionary.AddError("RegionID", "The Region was not found.");
                }
            }

            if (apiRequest.MulticastVpnServiceTypeID != null)
            {
                var multicastVpnServiceType = await MulticastVpnServiceTypeService.GetByIDAsync(apiRequest.MulticastVpnServiceTypeID.Value);
                if (multicastVpnServiceType == null)
                {
                    ValidationDictionary.AddError("MulticastVpnServiceTypeID", "The Multicast VPN Service Type was not found.");
                }
            }

            if (apiRequest.MulticastVpnDirectionTypeID != null)
            {
                var multicastVpnDirectionType = await MulticastVpnDirectionTypeService.GetByIDAsync(apiRequest.MulticastVpnDirectionTypeID.Value);
                if (multicastVpnDirectionType == null)
                {
                    ValidationDictionary.AddError("MulticastVpnDirectionTypeID", "The Multicast VPN Direction Type was not found.");
                }
            }

            if (ValidationDictionary.IsValid)
            {
                // Hand-off to VPN Validator

                await VpnValidator.ValidateNewAsync(Mapper.Map<VpnRequest>(apiRequest));
            }
        }

        /// <summary>
        /// Validate changes to a VPN.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(Vpn updatedVpn)
        {

            // Region can be changed so validate the update

            if (updatedVpn.RegionID != null)
            {
                var region = await RegionService.GetByIDAsync(updatedVpn.RegionID.Value);
                if (region == null)
                {
                    ValidationDictionary.AddError("RegionID", "The Region was not found.");
                }
            }

            // Vpn Tenancy Type can be changed so validate the update

            var tenancyType = await VpnTenancyTypeService.GetByIDAsync(updatedVpn.VpnTenancyTypeID);
            if (tenancyType == null)
            {
                ValidationDictionary.AddError("VpnTenancyTypeID", "The Tenancy Type was not found.");
            }

            // Multicast VPN Direction Type can be changed so validate the update

            if (updatedVpn.MulticastVpnDirectionTypeID != null)
            {
                var multicastVpnDirectionType = await MulticastVpnDirectionTypeService.GetByIDAsync(updatedVpn.MulticastVpnDirectionTypeID.Value);
                if (multicastVpnDirectionType == null)
                {
                    ValidationDictionary.AddError("MulticastVpnDirectionTypeID", "The Multicast VPN Direction Type was not found.");
                }
            }

            if (ValidationDictionary.IsValid)
            {
                // Hand-off to Vpn Validator

                await VpnValidator.ValidateChangesAsync(updatedVpn);
            }
        }

        /// <summary>
        /// Validate an existing VPN.
        /// </summary>
        /// <param name="vpn"></param>
        public void ValidateOkToSyncToNetwork(Vpn vpn)
        {
            // Validate Route Targets for the VPN

            RouteTargetValidator.ValidateExisting(vpn);

            // Validate if the VPN can be synchronised to the network

            VpnValidator.ValidateOkToSyncToNetwork(vpn);
        }
    }
}
