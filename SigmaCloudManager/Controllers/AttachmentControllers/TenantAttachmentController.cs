using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using SCM.Models;
using SCM.Factories;
using Mind.Services;

namespace SCM.Controllers
{
    public class TenantAttachmentController : BaseAttachmentController
    {
        public TenantAttachmentController(ITenantAttachmentService tenantAttachmentService,
            IAttachmentService attachmentService,
            ITenantService tenantService,
            IRegionService regionService,
            ISubRegionService subRegionService,
            ILocationService locationService,
            IPlaneService planeService,
            IRoutingInstanceService vrfService,
            IDeviceService deviceService,
            IPortService portService,
            IInterfaceService interfaceService,
            IMtuService mtuService,
            IPortRoleService portRoleService,
            IPortPoolService portPoolService,
            IContractBandwidthService contractBandwidthService,
            IAttachmentRoleService attachmentRoleService,
            ILogicalInterfaceService logicalInterfaceService,
            IAttachmentValidator attachmentValidator,
            IRoutingInstanceValidator routingInstanceValidator,
            IMapper mapper) : base(attachmentService,
                vrfService,
                regionService,
                subRegionService,
                locationService,
                deviceService,
                portService,
                interfaceService,
                mtuService,
                portRoleService,
                portPoolService,
                attachmentRoleService,
                logicalInterfaceService,
                attachmentValidator, 
                routingInstanceValidator,
                mapper)
        {
            TenantAttachmentService = tenantAttachmentService;
            TenantService = tenantService;
            PlaneService = planeService;
            ContractBandwidthService = contractBandwidthService;
        }

        private ITenantAttachmentService TenantAttachmentService { get; }
        private ITenantService TenantService { get; }
        private IPlaneService PlaneService { get; }
        private IContractBandwidthService ContractBandwidthService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(AttachmentNavigationViewModel nav)
        {
            if (nav.TenantID == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(nav.TenantID.Value);
            if (tenant == null)
            {
                return NotFound();
            }

            var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(tenant.TenantID);

            ViewData["SuccessMessage"] = FormatAsHtmlList(attachments
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            ViewData["NetworkWarningMessage"] = FormatAsHtmlList(attachments
                .Where(x => x.RequiresSync && x.ShowRequiresSyncAlert)
                .Select(x => $"{x.Name} requires sync with the network.").ToList());

            // Display errors if any Attachment Ports are mis-configured

            foreach (var attachment in attachments)
            {
                await AttachmentValidator.ValidateAttachmentPortsConfiguredCorrectlyAsync(attachment);
            }

            ViewBag.Tenant = tenant;
            return View(Mapper.Map<List<AttachmentViewModel>>(attachments));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ClearCreatedAlerts(int id)
        {
            var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(id, created: true, showCreatedAlert: true);
            foreach (var attachment in attachments)
            {
                attachment.ShowCreatedAlert = false;
            }

            try
            {
                await TenantAttachmentService.UpdateAsync(attachments);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAllByTenantID", new { tenantID = id });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ClearRequiresSyncAlerts(int id)
        {
            var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(id, requiresSync: true, showRequiresSyncAlert: true);
            foreach (var attachment in attachments)
            {
                attachment.ShowRequiresSyncAlert = false;
            }

            try
            {
                await TenantAttachmentService.UpdateAsync(attachments);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAllByTenantID", new { tenantID = id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterfacesByAttachmentID(int? id, bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (showWarningMessage)
            {
                var attachment = await TenantAttachmentService.GetByIDAsync(id.Value);
                ViewData["NetworkWarningMessage"] = $"Attachment '{attachment.Name}' now requires synchronisation with the network. "
                            + $"Follow this <a href = '/TenantAttachment/GetAllByTenantID?tenantID={attachment.TenantID}'>link</a> "
                            + "to go to the Attachments page.";
            }

            return await base.BaseGetAllInterfacesByAttachmentID(id.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortsByInterfaceID(int? id, bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"Devices now require synchronisation with the network. "
                            + $"Follow this <a href = '/Device/GetAll'>link</a> to go to the Devices page.";
            }

            return await base.BaseGetAllPortsByInterfaceID(id.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogicalInterfacesByAttachmentID(int? attachmentID, bool showWarningMessage = false)
        {
            if (attachmentID == null)
            {
                return NotFound();
            }

            if (showWarningMessage)
            {
                var attachment = await TenantAttachmentService.GetByIDAsync(attachmentID.Value);
                ViewData["NetworkWarningMessage"] = $"Attachment '{attachment.Name}' now requires synchronisation with the network. "
                            + $"Follow this <a href = '/TenantAttachment/GetAllByTenantID?tenantID={attachment.TenantID}'>link</a> "
                            + "to go to the Attachments page.";
            }

            return await base.BaseGetAllLogicalInterfacesByAttachmentID(attachmentID.Value);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProviderDomainAttachment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            await PopulateRegionsDropDownList();
            await PopulatePlanesDropDownList();
            await PopulateContractBandwidthsDropDownList();
            var portRole = await PortRoleService.GetByPortRoleTypeAsync(Models.PortRoleTypeEnum.TenantFacing);
            await base.PopulatePortPoolsDropDownList(portRole.PortRoleID);

            return await base.BaseCreateAttachment();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProviderDomainAttachment([Bind("TenantID,IpAddress1,SubnetMask1,IpAddress2,SubnetMask2,"
            + "IpAddress3,SubnetMask3,IpAddress4,SubnetMask4,BandwidthID,RegionID,SubRegionID,LocationID,PlaneID,"
            + "IsLayer3,IsTagged,BundleRequired,MultiPortRequired,ContractBandwidthID,TrustReceivedCosDscp,"
            + "PortPoolID,AttachmentRoleID,Description,Notes")] ProviderDomainAttachmentRequestViewModel requestModel,
            AttachmentNavigationViewModel nav)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.Map<AttachmentRequest>(requestModel);
                    await AttachmentValidator.ValidateNewAsync(request);

                    if (AttachmentValidator.ValidationDictionary.IsValid)
                    {
                        var result = await TenantAttachmentService.AddAsync(request);
                        if (result.IsSuccess)
                        {
                            return RedirectToAction("GetAllByTenantID", nav);
                        }
                        else
                        {
                            result.GetMessageList().ForEach(message => ModelState.AddModelError(string.Empty, message));
                        }
                    }
                }
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            catch (FactoryFailureException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(nav.TenantID.Value);
            await PopulateRegionsDropDownList(requestModel.RegionID);
            await PopulateSubRegionsDropDownList(requestModel.RegionID, requestModel.SubRegionID);
            await PopulateLocationsDropDownList(requestModel.SubRegionID, requestModel.LocationID);
            await PopulatePlanesDropDownList(requestModel.PlaneID);
            await PopulateContractBandwidthsDropDownList();
            var portRole = await PortRoleService.GetByPortRoleTypeAsync(Models.PortRoleTypeEnum.TenantFacing);
            await base.PopulatePortPoolsDropDownList(portRole.PortRoleID, requestModel.PortPoolID);
            await base.PopulateAttachmentRolesDropDownList(requestModel.PortPoolID, selectedAttachmentRole: requestModel.AttachmentRoleID);
            await base.PopulateBandwidthsDropDownList();

            return View(requestModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTenantDomainAttachment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            await PopulateDevicesByTenantDropDownList(id.Value);
            await PopulateContractBandwidthsDropDownList();
            var portRole = await PortRoleService.GetByPortRoleTypeAsync(Models.PortRoleTypeEnum.TenantInfrastructure);
            await base.PopulatePortPoolsDropDownList(portRole.PortRoleID);

            return await base.BaseCreateAttachment();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTenantDomainAttachment([Bind("TenantID,DeviceID,IpAddress1,SubnetMask1,IpAddress2,SubnetMask2,"
            + "IpAddress3,SubnetMask3,IpAddress4,SubnetMask4,BandwidthID,IsLayer3,IsTagged,BundleRequired,MultiPortRequired,"
            + "ContractBandwidthID,TrustReceivedCosDscp,PortPoolID,AttachmentRoleID,Description,Notes")]
        TenantDomainAttachmentRequestViewModel requestModel,
           AttachmentNavigationViewModel nav)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.Map<AttachmentRequest>(requestModel);
                    await AttachmentValidator.ValidateNewAsync(request);

                    if (AttachmentValidator.ValidationDictionary.IsValid)
                    {
                        var result = await TenantAttachmentService.AddAsync(request);
                        if (result.IsSuccess)
                        {
                            return RedirectToAction("GetAllByTenantID", nav);
                        }
                        else
                        {
                            result.GetMessageList().ForEach(message => ModelState.AddModelError(string.Empty, message));
                        }
                    }
                }
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            catch (FactoryFailureException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(nav.TenantID.Value);
            await PopulateDevicesByTenantDropDownList(nav.TenantID.Value, requestModel.DeviceID);
            await PopulateContractBandwidthsDropDownList();
            var portRole = await PortRoleService.GetByPortRoleTypeAsync(Models.PortRoleTypeEnum.TenantInfrastructure);
            await base.PopulatePortPoolsDropDownList(portRole.PortRoleID, requestModel.PortPoolID);
            var device = await DeviceService.GetByIDAsync(requestModel.DeviceID);
            await PopulateAttachmentRolesDropDownList(requestModel.PortPoolID, device.DeviceRoleID, requestModel.AttachmentRoleID);
            await base.PopulateBandwidthsDropDownList();

            return View(requestModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLogicalInterface(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(id.Value);
            if (attachment == null)
            {
                return NotFound();
            }

            return base.BaseCreateLogicalInterface(attachment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLogicalInterface([Bind("RoutingInstanceID,IpAddress,SubnetMask,Description,LogicalInterfaceType")]
        LogicalInterfaceViewModel model, AttachmentNavigationViewModel nav)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.Map<LogicalInterface>(model);
                    await LogicalInterfaceService.AddAsync(request);
                    return RedirectToAction("GetAllLogicalInterfacesByAttachmentID", nav);
                }
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            catch (FactoryFailureException  ex )
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await TenantAttachmentService.GetByIDAsync(id.Value);
            if (attachment == null)
            {
                return NotFound();
            }

            if (attachment.ContractBandwidthPool != null)
            {
                await PopulateContractBandwidthsDropDownList(attachment.ContractBandwidthPool.ContractBandwidthID);
            }

            if (attachment.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(attachment.RoutingInstance.DeviceID, attachment.TenantID, 
                    isTenantFacingVrf: true, selectedRoutingInstance: attachment.RoutingInstanceID);
            }

            var tenant = await TenantService.GetByIDAsync(attachment.TenantID.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return await base.BaseEdit(attachment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("AttachmentID,TenantID,ContractBandwidthID,TrustReceivedCosDscp,"
            + "RoutingInstanceID,CreateNewRoutingInstance,MtuID,BundleMinLinks,BundleMaxLinks,IsTagged,AttachmentRoleID,Description,Notes,RowVersion")]
        AttachmentUpdateViewModel updateModel, AttachmentNavigationViewModel nav)
        {
            if (id != updateModel.AttachmentID)
            {
                return NotFound();
            }

            var currentAttachment = await TenantAttachmentService.GetByIDAsync(updateModel.AttachmentID);
            if (currentAttachment == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                nav.RedirectAction = "GetAllByTenantID";
                return await base.BaseEdit(updateModel, currentAttachment, nav);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (currentAttachment.ContractBandwidthPoolID != null)
                {
                    var proposedContractBandwidthPoolID = (int)exceptionEntry.Property("ContractBandwidthPoolID").CurrentValue;
                    if (currentAttachment.ContractBandwidthPoolID != proposedContractBandwidthPoolID)
                    {
                        ModelState.AddModelError("ContractBandwidthPoolID", $"Current value: {currentAttachment.ContractBandwidthPool.Name}");
                    }
                }
            }

            finally
            {
                if (currentAttachment.ContractBandwidthPool != null)
                {
                    await PopulateContractBandwidthsDropDownList(currentAttachment.ContractBandwidthPool.ContractBandwidthID);
                }

                if (currentAttachment.RoutingInstance != null)
                {
                    await base.PopulateRoutingInstancesDropDownList(deviceID: currentAttachment.RoutingInstance.DeviceID, tenantID: currentAttachment.TenantID,
                        isTenantFacingVrf: true, selectedRoutingInstance: currentAttachment.RoutingInstanceID);
                }

                var tenant = await TenantService.GetByIDAsync(currentAttachment.TenantID.Value);
                ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            }

            return View(Mapper.Map<AttachmentUpdateViewModel>(currentAttachment));
        }

        [HttpGet]
        public async Task<ActionResult> EditInterface(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await base.BaseEditInterface(id.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditInterface(int? id, [Bind("InterfaceID,AttachmentID,DeviceID,IpAddress,SubnetMask,RowVersion")] InterfaceViewModel updateModel)
        {
            if (id != updateModel.InterfaceID)
            {
                return NotFound();
            }

            var currentInterface = await InterfaceService.GetByIDAsync(updateModel.InterfaceID);
            if (currentInterface == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            return await base.BaseEditInterface(currentInterface, updateModel);
        }

        [HttpGet]
        public async Task<ActionResult> EditLogicalInterface(int? logicalInterfaceID, int? attachmentID)
        {
            if (logicalInterfaceID == null)
            {
                return NotFound();
            }

            if (attachmentID == null)
            {
                return NotFound();
            }

            return await base.BaseEditLogicalInterface(logicalInterfaceID.Value, attachmentID.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLogicalInterface(int? logicalInterfaceID, int? attachmentID, 
            [Bind("LogicalInterfaceID,RoutingInstanceID,IpAddress,SubnetMask,Description,RowVersion")] LogicalInterfaceViewModel updateModel)
        {
            if (logicalInterfaceID != updateModel.LogicalInterfaceID)
            {
                return NotFound();
            }

            if (attachmentID == null)
            {
                return NotFound();
            }

            var currentLogicalInterface = await LogicalInterfaceService.GetByIDAsync(updateModel.LogicalInterfaceID);
            if (currentLogicalInterface == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            return await base.BaseEditLogicalInterface(attachmentID.Value, currentLogicalInterface, updateModel);
        }

        [HttpGet]
        public async Task<ActionResult> EditProviderPort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await PortService.GetByIDAsync(id.Value);

            await PopulatePlanesDropDownList(port.Device.PlaneID);
            await base.PopulateDevicesByLocationDropDownList(port.Device.LocationID, port.Device.PlaneID, port.DeviceID);

            return await base.BaseEditPort(port);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProviderPort(int? id, [Bind("AttachmentID,PlaneID,DeviceID,InterfaceID,PortID,"
            + "CurrentPortID,LocationID,RowVersion")] AttachmentPortUpdateViewModel updateModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            await PopulatePlanesDropDownList(updateModel.PlaneID);
            await PopulateDevicesByLocationDropDownList(updateModel.LocationID, updateModel.PlaneID, updateModel.DeviceID);
            return await base.BaseEditPort(id.Value, updateModel);
        }

        public async Task<ActionResult> EditTenantPort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await PortService.GetByIDAsync(id.Value);
            await PopulatePortsDropDownList(port.DeviceID, port.PortPoolID, port.ID);

            return await base.BaseEditPort(port);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTenantPort(int? id, [Bind("AttachmentID,DeviceID,InterfaceID,PortID,"
            + "CurrentPortID,RowVersion")] AttachmentPortUpdateViewModel updateModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await base.BaseEditPort(id.Value, updateModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? attachmentID, AttachmentNavigationViewModel nav, bool? concurrencyError = false)
        {
            if (attachmentID == null)
            {
                return NotFound();
            }

            nav.RedirectAction = "GetAllByTenantID";
            return await base.BaseDeleteAttachment(attachmentID.Value, nav, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AttachmentViewModel attachmentModel, AttachmentNavigationViewModel nav)
        {
            var currentAttachment = await TenantAttachmentService.GetByIDAsync(attachmentModel.AttachmentID);
            if (currentAttachment == null)
            {
                return RedirectToAction("GetAllByTenantID", nav);
            }

            nav.RedirectAction = "GetAllByTenantID";
            ViewBag.Tenant = await TenantService.GetByIDAsync(currentAttachment.TenantID.Value);

            return await base.BaseDeleteAttachment(attachmentModel, currentAttachment, nav);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLogicalInterface(int? logicalInterfaceID, AttachmentNavigationViewModel nav, bool? concurrencyError = false)
        {
            if (logicalInterfaceID == null)
            {
                return NotFound();
            }

            nav.RedirectAction = "GetAllLogicalInterfacesByAttachmentID";
            return await base.BaseDeleteLogicalInterface(logicalInterfaceID.Value, nav, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogicalInterface(LogicalInterfaceViewModel logicalInterfaceModel, AttachmentNavigationViewModel nav)
        {
            var currentLogicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceModel.LogicalInterfaceID);
            if (currentLogicalInterface == null)
            {
                return RedirectToAction("GetAllLogicalInterfacesByAttachmentID", nav);
            }
            
            nav.RedirectAction = "GetAllLogicalInterfacesByAttachmentID";
            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return await base.BaseDeleteLogicalInterface(logicalInterfaceModel, currentLogicalInterface, nav);
        }

        [HttpGet]
        public async Task<IActionResult> RoutingInstanceDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(id.Value);
            if (attachment == null)
            {
                return NotFound();
            }

            return await base.BaseRoutingInstanceDetails(attachment);
        }

        [HttpGet]
        public async Task<IActionResult> LogicalInterfaceDetails(int? logicalInterfaceID, int? attachmentID)
        {
            if (logicalInterfaceID == null)
            {
                return NotFound();
            }

            if (attachmentID == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentID.Value);
            if (attachment == null)
            {
                return NotFound();
            }

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            return await base.BaseLogicalInterfaceDetails(logicalInterfaceID.Value);
        }

        [HttpGet]
        public async Task<ActionResult> EditRoutingInstance(int? attachmentID)
        {
            if (attachmentID == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentID.Value);
            if (attachment == null)
            {
                return NotFound();
            }

            if (attachment.RoutingInstance == null)
            {
                return NotFound();
            }

            var vrf = await RoutingInstanceService.GetByIDAsync(attachment.RoutingInstance.RoutingInstanceID);
            ViewBag.Attachment = attachment;

            return View(Mapper.Map<TenantAttachmentRoutingInstanceUpdateViewModel>(vrf));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRoutingInstance(int id, [Bind("RoutingInstanceID,AttachmentID,Name,AdministratorSubField,"
            + "AssignedNumberSubField,RowVersion")] TenantAttachmentRoutingInstanceUpdateViewModel updateModel)
        {
            if (id != updateModel.AttachmentID)
            {
                return NotFound();
            }

            var currentAttachment = await AttachmentService.GetByIDAsync(updateModel.AttachmentID);
            if (currentAttachment == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            var currentRoutingInstance = await RoutingInstanceService.GetByIDAsync(currentAttachment.RoutingInstanceID.Value);

            try
            {
                if (ModelState.IsValid)
                {
                    var vrfUpdate = Mapper.Map<RoutingInstanceUpdate>(updateModel);
                    await RoutingInstanceValidator.ValidateChangesAsync(vrfUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await RoutingInstanceService.UpdateAsync(Mapper.Map(vrfUpdate, currentRoutingInstance));
                        return RedirectToAction("RoutingInstanceDetails", new
                        {
                            id = currentAttachment.AttachmentID,
                            showWarningMessage = true
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (currentRoutingInstance.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {currentRoutingInstance.Name}");
                }

                if (currentRoutingInstance.AdministratorSubField != null)
                {
                    var proposedAdministratorSubField = (int)exceptionEntry.Property("AdministratorSubField").CurrentValue;
                    if (currentRoutingInstance.AdministratorSubField != proposedAdministratorSubField)
                    {
                        ModelState.AddModelError("AdministratorSubField", $"Current value: {currentRoutingInstance.AdministratorSubField}");
                    }
                }

                if (currentRoutingInstance.AssignedNumberSubField != null)
                {
                    var proposedAssignedNumberSubField = (int)exceptionEntry.Property("AssignedNumberSubField").CurrentValue;
                    if (currentRoutingInstance.AssignedNumberSubField != proposedAssignedNumberSubField)
                    {
                        ModelState.AddModelError("AssignedNumberSubField", $"Current value: {currentRoutingInstance.AssignedNumberSubField}");
                    }
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            ViewBag.Attachment = await AttachmentService.GetByIDAsync(currentAttachment.AttachmentID);

            return View(Mapper.Map<TenantAttachmentRoutingInstanceUpdateViewModel>(currentRoutingInstance));
        }

        private async Task PopulatePlanesDropDownList(object selectedPlane = null)
        {
            var planes = await PlaneService.GetAllAsync();
            ViewBag.PlaneID = new SelectList(Mapper.Map<List<PlaneViewModel>>(planes), "PlaneID", "Name", selectedPlane);
        }

        private async Task PopulateContractBandwidthsDropDownList(object selectedContractBandwidth = null)
        {
            var contractBandwidths = await ContractBandwidthService.GetAllAsync();
            ViewBag.ContractBandwidthID = new SelectList(Mapper.Map<List<ContractBandwidthViewModel>>(contractBandwidths)
                .OrderBy(b => b.BandwidthMbps),
                "ContractBandwidthID", "BandwidthMbps", selectedContractBandwidth);
        }
        private async Task PopulateDevicesByTenantDropDownList(int tenantID, object selectedDevice = null)
        {
            var devices = await DeviceService.GetAllByTenantIDAsync(tenantID);
            ViewBag.DeviceID = new SelectList(devices, "DeviceID", "Name", selectedDevice);
        }

    }
}