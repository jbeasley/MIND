using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using SCM.Factories;
using SCM.Models.RequestModels;
using Mind.Services;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class VpnController : BaseVpnController
    {
        public VpnController(IVpnService vpnService,
            IVpnProtocolTypeService vpnProtocolTypeService,
            IVpnTopologyTypeService vpnTopologyTypeService,
            IAddressFamilyService addressFamilyService,
            ITenantService tenantService,
            IPlaneService planeService,
            IRegionService regionService,
            IVpnTenancyTypeService vpnTenancyTypeService,
            IAttachmentSetService attachmentSetService,
            IMulticastVpnServiceTypeService multicastVpnServiceTypeService,
            IMulticastVpnDirectionTypeService multicastVpnDirectionTypeService,
            IRouteTargetRangeService routeTargetRangeService,
            IMapper mapper, 
            IVpnValidator vpnValidator) : base(vpnService, attachmentSetService, mapper)
        {
            VpnProtocolTypeService = vpnProtocolTypeService;
            VpnTopologyTypeService = vpnTopologyTypeService;
            AddressFamilyService = addressFamilyService;
            TenantService = tenantService;
            PlaneService = planeService;
            RegionService = regionService;
            VpnTenancyTypeService = vpnTenancyTypeService;
            MulticastVpnServiceTypeService = multicastVpnServiceTypeService;
            MulticastVpnDirectionTypeService = multicastVpnDirectionTypeService;
            RouteTargetRangeService = routeTargetRangeService;
            this.Validator = vpnValidator;
        }

        private IVpnProtocolTypeService VpnProtocolTypeService { get; }
        private IVpnTopologyTypeService VpnTopologyTypeService { get; }
        private ITenantService TenantService { get; }
        private IAddressFamilyService AddressFamilyService { get; }
        private IVpnTenancyTypeService VpnTenancyTypeService { get; }
        private IRegionService RegionService { get; }
        private IPlaneService PlaneService { get; }
        private IMulticastVpnServiceTypeService MulticastVpnServiceTypeService { get; }
        private IMulticastVpnDirectionTypeService MulticastVpnDirectionTypeService { get; }
        private IRouteTargetRangeService RouteTargetRangeService { get; }
        private IVpnValidator VpnValidator { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll(string searchString, string sortKey)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortKey) ? "Name_Desc" : "";
            ViewBag.TenantSort = sortKey == "Tenant" ? "Tenant_Desc" : "Tenant";
            ViewBag.TenancyTypeSort = sortKey == "TenancyType"  ? "TenancyType_Desc" : "TenancyType";
            ViewBag.PlaneSort = sortKey == "Plane" ? "Plane_Desc" : "Plane";
            ViewBag.RegionSort = sortKey == "Region" ? "Region_Desc" : "Region";

            var vpns = await VpnService.GetAllAsync(searchString: searchString, sortKey:sortKey);

            ViewData["SuccessMessage"] = FormatAsHtmlList(vpns
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            ViewData["NetworkWarningMessage"] = FormatAsHtmlList(vpns
                .Where(x => x.RequiresSync && x.ShowRequiresSyncAlert)
                .Select(x => $"{x.Name} requires sync with the network.").ToList());

            return View(Mapper.Map<List<VpnViewModel>>(vpns));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCreatedAlerts()
        {
            var vpns = await VpnService.GetAllAsync(created: true, showCreatedAlert: true, asTrackable: true);
            foreach (var vpn in vpns)
            {
                vpn.ShowCreatedAlert = false;
            }

            try
            {
                await VpnService.UnitOfWork.SaveAsync();   
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> CreateStep1()
        {
            await PopulateProtocolTypesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStep2([Bind("VpnProtocolTypeID,IsMulticastVpn")] VpnRequestViewModel vpnRequestModel)
        {
            await PopulatePlanesDropDownList();
            await PopulateTenantsDropDownList();
            await PopulateRegionsDropDownList();
            await PopulateAddressFamiliesDropDownList();
            await PopulateRouteTargetRangesDropDownList();
            if (vpnRequestModel.IsMulticastVpn)
            {
                await PopulateMulticastVpnServiceTypesDropDownList();
                await PopulateMulticastVpnDirectionTypesDropDownList();
            }
            await PopulateTopologyTypesDropDownListByProtocolType(vpnRequestModel.VpnProtocolTypeID);
            await PopulateTenancyTypesDropDownList();
            var vpnProtocolType = await VpnProtocolTypeService.GetByIDAsync(vpnRequestModel.VpnProtocolTypeID);
            ViewBag.VpnProtocolType = Mapper.Map<VpnProtocolTypeViewModel>(vpnProtocolType);

            ModelState.Clear();
            return View(vpnRequestModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,PlaneID,RegionID,VpnTenancyTypeID," +
            "VpnTopologyTypeID,VpnProtocolTypeID,AddressFamilyID,TenantID,IsNovaVpn,IsExtranet,IsMulticastVpn,MulticastVpnServiceTypeID," +
            "MulticastVpnDirectionTypeID,RouteTargetRangeID")] VpnRequestViewModel vpnRequestModel)
        {
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<VpnRequest>(vpnRequestModel);
                try
                {
                    await VpnService.AddAsync(vpnRequestModel.TenantID, Mapper.Map<Mind.Models.RequestModels.VpnRequest>(vpnRequestModel));
                    return RedirectToAction("GetAll");
                }

                catch (DbUpdateException /** ex **/ )
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }

                catch (FactoryFailureException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var protocolType = await VpnProtocolTypeService.GetByVpnTopologyTypeIDAsync(vpnRequestModel.VpnTopologyTypeID);
            ViewBag.VpnProtocolType = Mapper.Map<VpnProtocolTypeViewModel>(protocolType);
            await PopulatePlanesDropDownList(vpnRequestModel.PlaneID);
            await PopulateTenantsDropDownList(vpnRequestModel.TenantID);
            await PopulateRegionsDropDownList(vpnRequestModel.RegionID);
            await PopulateTopologyTypesDropDownList(vpnRequestModel.VpnTopologyTypeID, vpnRequestModel.VpnTopologyTypeID);
            await PopulateAddressFamiliesDropDownList(vpnRequestModel.AddressFamilyID);
            await PopulateTenancyTypesDropDownList(vpnRequestModel.VpnTenancyTypeID);
            await PopulateRouteTargetRangesDropDownList(vpnRequestModel.RouteTargetRangeID);
            if (vpnRequestModel.IsMulticastVpn)
            {
                await PopulateMulticastVpnServiceTypesDropDownList(vpnRequestModel.MulticastVpnServiceTypeID);
                await PopulateMulticastVpnDirectionTypesDropDownList(vpnRequestModel.MulticastVpnDirectionTypeID);
            }

            return View("CreateStep2", vpnRequestModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpn = await VpnService.GetByIDAsync(id.Value);
            if (vpn == null)
            {
                return NotFound();
            }

            await PopulateRegionsDropDownList(vpn.RegionID);
            await PopulateTenancyTypesDropDownList(vpn.VpnTenancyTypeID);
            if (vpn.IsMulticastVpn)
            {
                await PopulateMulticastVpnDirectionTypesDropDownList(vpn.MulticastVpnDirectionTypeID);
            }

            return View(Mapper.Map<VpnUpdateViewModel>(vpn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnID,Name,Description,RegionID,VpnTenancyTypeID,RegionID,"
            + "IsExtranet,IsMulticastVpn,MulticastVpnDirectionTypeID,RowVersion")] VpnUpdateViewModel vpnModel)
        {
            if (id != vpnModel.VpnID)
            {
                return NotFound();
            }

            var updateVpn = await VpnService.GetByIDAsync(id);
            if (updateVpn == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                await VpnService.UpdateAsync(updateVpn.VpnID, Mapper.Map<Mind.Models.RequestModels.VpnUpdate>(updateVpn));
                return RedirectToAction("GetAll");
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var currentVpn = await VpnService.GetByIDAsync(vpnModel.VpnID);
                var exceptionEntry = ex.Entries.Single();

                var proposedDescription = (string)exceptionEntry.Property("Description").CurrentValue;
                if (currentVpn.Description != proposedDescription)
                {
                    ModelState.AddModelError("Description", $"Current value: {currentVpn.Description}");
                }

                var proposedRegionID = (int?)exceptionEntry.Property("RegionID").CurrentValue;
                if (currentVpn.RegionID != proposedRegionID)
                {
                    var currentRegionName = currentVpn.Region != null ? currentVpn.Region.Name : string.Empty;
                    ModelState.AddModelError("RegionID", $"Current value: {currentRegionName}");
                }

                var proposedMulticastVpnDirectionTypeID = (int?)exceptionEntry.Property("MulticastVpnDirectionTypeID").CurrentValue;
                if (currentVpn.MulticastVpnDirectionTypeID != proposedMulticastVpnDirectionTypeID)
                {
                    var currentMulticastVpnDirectionType = currentVpn.MulticastVpnDirectionType != null ? currentVpn.MulticastVpnDirectionType.Name : string.Empty;
                    ModelState.AddModelError("MulticastVpnDirectionTypeID", $"Current value: {currentMulticastVpnDirectionType}");
                }
           
                var proposedTenancyTypeID = (int)exceptionEntry.Property("VpnTenancyTypeID").CurrentValue;
                if (currentVpn.VpnTenancyTypeID != proposedTenancyTypeID)
                {
                    ModelState.AddModelError("VpnTenancyTypeID", $"Current value: {currentVpn.VpnTenancyType.TenancyType}");
                }

                var proposedIsExtranet = (bool)exceptionEntry.Property("IsExtranet").CurrentValue;
                if (currentVpn.IsExtranet != proposedIsExtranet)
                {
                    ModelState.AddModelError("IsExtranet", $"Current value: {currentVpn.IsExtranet}");
                }


                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                updateVpn.RowVersion = currentVpn.RowVersion;
                ModelState.Remove("RowVersion");
            }

            await PopulateRegionsDropDownList(updateVpn.RegionID);
            await PopulateTenancyTypesDropDownList(updateVpn.VpnTenancyTypeID);
            if (updateVpn.IsMulticastVpn)
            {
                await PopulateMulticastVpnDirectionTypesDropDownList(updateVpn.MulticastVpnDirectionTypeID);
            }

            return View(Mapper.Map<VpnUpdateViewModel>(updateVpn));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpn = await VpnService.GetByIDAsync(id.Value);
            if (vpn == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAll");
                }

                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was cancelled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(Mapper.Map<VpnViewModel>(vpn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnViewModel vpnModel)
        {
            var vpn = await VpnService.GetByIDAsync(vpnModel.VpnID);
            if (vpn == null)
            {
                return RedirectToAction("GetAll");
            }

            try
            {
                await VpnService.DeleteAsync(vpnModel.VpnID);
                return RedirectToAction("GetAll");
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnModel.VpnID
                });
            }

            catch (Exception /** ex **/) 
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            return View(Mapper.Map<VpnViewModel>(vpn));
        }
    
        /// <summary>
        /// Helper to populate a list of Planes
        /// </summary>
        /// <param name="selectedPlane"></param>
        /// <returns></returns>
        private async Task PopulatePlanesDropDownList(object selectedPlane = null)
        {
            var planes = await PlaneService.GetAllAsync();
            ViewBag.PlaneID = new SelectList(Mapper.Map<List<PlaneViewModel>>(planes), "PlaneID", "Name", selectedPlane);
        }

        /// <summary>
        /// Helper to populate a list of Regions
        /// </summary>
        /// <param name="selectedRegion"></param>
        /// <returns></returns>
        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await RegionService.GetAllAsync();
            ViewBag.RegionID = new SelectList(Mapper.Map<List<Region>>(regions), "RegionID", "Name", selectedRegion);
        }

        /// <summary>
        /// Helper to populate a list of VPN Tenancy Types
        /// </summary>
        /// <param name="selectedTenancyType"></param>
        /// <returns></returns>
        private async Task PopulateTenancyTypesDropDownList(object selectedTenancyType = null)
        {
            var tenancyTypes = await VpnTenancyTypeService.GetAllAsync();
            ViewBag.VpnTenancyTypeID = new SelectList(Mapper.Map<List<VpnTenancyType>>(tenancyTypes), "VpnTenancyTypeID", 
                "TenancyType", selectedTenancyType);
        }

        /// <summary>
        /// Helper to populate a list of VPN Topology Types for a given
        /// VPN Protocol Type.
        /// </summary>
        /// <param name="protocolTypeID"></param>
        /// <param name="selectedTopologyType"></param>
        /// <returns></returns>
        private async Task PopulateTopologyTypesDropDownListByProtocolType(int protocolTypeID, object selectedTopologyType = null)
        {
            var topologyTypes = await VpnTopologyTypeService.GetByVpnProtocolTypeIDAsync(protocolTypeID);
            ViewBag.VpnTopologyTypeID = new SelectList(Mapper.Map<List<VpnTopologyType>>(topologyTypes),
                "VpnTopologyTypeID", "Name", selectedTopologyType);
        }

        /// <summary>
        /// Helper to populate a list of VPN Topology Types
        /// </summary>
        /// <param name="topologyTypeID"></param>
        /// <param name="selectedTopologyType"></param>
        /// <returns></returns>
        private async Task PopulateTopologyTypesDropDownList(int topologyTypeID, object selectedTopologyType = null)
        {
            var topologyType = await VpnTopologyTypeService.GetByIDAsync(topologyTypeID);
            await PopulateTopologyTypesDropDownListByProtocolType(topologyType.VpnProtocolTypeID);
        }

        /// <summary>
        /// Helper to populate a list of Tenants
        /// </summary>
        /// <param name="selectedTenant"></param>
        /// <returns></returns>
        private async Task PopulateTenantsDropDownList(object selectedTenant = null)
        {
            var tenants = await TenantService.GetAllAsync();
            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants), "TenantID", "Name", selectedTenant);
        }
        
        /// <summary>
        /// Helper to populate a list of VPN Protocol Types
        /// </summary>
        /// <param name="selectedProtocolType"></param>
        /// <returns></returns>
        private async Task PopulateProtocolTypesDropDownList(object selectedProtocolType = null)
        {
            var protocolTypes = await VpnProtocolTypeService.GetAllAsync();
            ViewBag.VpnProtocolTypeID = new SelectList(Mapper.Map<List<VpnProtocolType>>(protocolTypes), 
                "VpnProtocolTypeID", "Name", selectedProtocolType);
        }

        /// <summary>
        /// Helper to populate a list of Address Families
        /// </summary>
        /// <param name="selectedAddressFamily"></param>
        /// <returns></returns>
        private async Task PopulateAddressFamiliesDropDownList(object selectedAddressFamily = null)
        {
            var addressFamilies = await AddressFamilyService.GetAllAsync();
            ViewBag.AddressFamilyID = new SelectList(Mapper.Map<List<AddressFamilyViewModel>>(addressFamilies), 
                "AddressFamilyID", "Name", selectedAddressFamily);
        }

        /// <summary>
        /// Helper to populate a list of Multicast VPN Service Types
        /// </summary>
        /// <param name="selectedMulticastVpnServiceType"></param>
        /// <returns></returns>
        private async Task PopulateMulticastVpnServiceTypesDropDownList(object selectedMulticastVpnServiceType = null)
        {
            var multicastVpnServiceTypes = await MulticastVpnServiceTypeService.GetAllAsync();
            ViewBag.MulticastVpnServiceTypeID = new SelectList(Mapper.Map<List<MulticastVpnServiceTypeViewModel>>(multicastVpnServiceTypes), 
                "MulticastVpnServiceTypeID", "Name", selectedMulticastVpnServiceType);
        }

        /// <summary>
        /// Helper to populate a list of Multicast VPN Direction Types
        /// </summary>
        /// <param name="selectedMulticastVpnDirectionType"></param>
        /// <returns></returns>
        private async Task PopulateMulticastVpnDirectionTypesDropDownList(object selectedMulticastVpnDirectionType = null)
        {
            var multicastVpnDirectionTypes = await MulticastVpnDirectionTypeService.GetAllAsync();
            ViewBag.MulticastVpnDirectionTypeID = new SelectList(Mapper.Map<List<MulticastVpnDirectionType>>(multicastVpnDirectionTypes), 
                "MulticastVpnDirectionTypeID","Name", selectedMulticastVpnDirectionType);
        }

        /// <summary>
        /// Helper to populate a list of Route Target Ranges
        /// </summary>
        /// <param name="selectedRouteTargetRange"></param>
        /// <returns></returns>
        private async Task PopulateRouteTargetRangesDropDownList(object selectedRouteTargetRange = null)
        {
            var routeTargetRanges = await RouteTargetRangeService.GetAllAsync();
            ViewBag.RouteTargetRangeID = new SelectList(Mapper.Map<List<RouteTargetRangeViewModel>>(routeTargetRanges),
                "RouteTargetRangeID", "Name", selectedRouteTargetRange);
        }
    }
}
