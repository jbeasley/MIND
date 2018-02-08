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

namespace SCM.Controllers
{
    public class InfrastructureAttachmentController : BaseAttachmentController
    {
        public InfrastructureAttachmentController(IInfrastructureAttachmentService infrastructureAttachmentService,
            IAttachmentService attachmentService,
            IRegionService regionService,
            ISubRegionService subRegionService,
            ILocationService locationService,
            IRoutingInstanceService vrfService,
            IDeviceService deviceService,
            IPortService portService,
            IInterfaceService interfaceService,
            IMtuService mtuService,
            IPortRoleService portRoleService,
            IPortPoolService portPoolService,
            IAttachmentRoleService attachmentRoleService,
            ILogicalInterfaceService logicalInterfaceService,
            IAttachmentValidator attachmentValidator,
            IRoutingInstanceValidator routingInstanceValidator,
            IMapper mapper) : base(attachmentService,
                vrfService, regionService,
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
            InfrastructureAttachmentService = infrastructureAttachmentService;
        }

        private IInfrastructureAttachmentService InfrastructureAttachmentService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll(string searchString)
        {
            var attachments = await InfrastructureAttachmentService.GetAllAsync(searchString);
            var messages = attachments.Where(q => q.Created).Select(x => $"{x.Name} has been created.").ToList();

            if (messages.Any())
            {
                ViewData["SuccessMessage"] = FormatAsHtmlList(messages);
            }

            // Display errors if any Attachment Ports are mis-configured

            foreach (var attachment in attachments)
            {
                await AttachmentValidator.ValidateAttachmentPortsConfiguredCorrectlyAsync(attachment);
            }

            return View(Mapper.Map<List<AttachmentViewModel>>(attachments));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterfacesByAttachmentID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await base.BaseGetAllInterfacesByAttachmentID(id.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortsByInterfaceID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await base.BaseGetAllPortsByInterfaceID(id.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var portRole = await PortRoleService.GetByPortRoleTypeAsync(Models.PortRoleType.ProviderInfrastructure);
            await base.PopulatePortPoolsDropDownList(portRole.PortRoleID);
            await base.PopulateRegionsDropDownList();

            return await base.BaseCreateAttachment();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceID,IpAddress1,SubnetMask1,IpAddress2,SubnetMask2,"
            + "IpAddress3,SubnetMask3,IpAddress4,SubnetMask4,BandwidthID,RegionID,SubRegionID,LocationID,"
            + "IsLayer3,IsTagged,BundleRequired,MultiPortRequired,"
            + "PortPoolID,AttachmentRoleID,Description,Notes")] InfrastructureAttachmentRequestViewModel requestModel,
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
                        var result = await InfrastructureAttachmentService.AddAsync(request);
                        if (result.IsSuccess)
                        {
                            return RedirectToAction("GetAll", nav);
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

            await PopulateRegionsDropDownList(requestModel.RegionID);
            await PopulateSubRegionsDropDownList(requestModel.RegionID, requestModel.SubRegionID);
            await PopulateLocationsDropDownList(requestModel.SubRegionID, requestModel.LocationID);
            var portRole = await PortRoleService.GetByPortRoleTypeAsync(Models.PortRoleType.ProviderInfrastructure);
            await base.PopulatePortPoolsDropDownList(portRole.PortRoleID, requestModel.PortPoolID);
            var device = await DeviceService.GetByIDAsync(requestModel.DeviceID);
            await PopulateAttachmentRolesDropDownList(requestModel.PortPoolID, device.DeviceRoleID, requestModel.AttachmentRoleID);
            await base.PopulateDevicesByLocationDropDownList(requestModel.LocationID, selectedDevice: requestModel.DeviceID);
            await base.PopulateBandwidthsDropDownList();

            return View(requestModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await InfrastructureAttachmentService.GetByIDAsync(id.Value);
            if (attachment == null)
            {
                return NotFound();
            }

            if (attachment.RoutingInstance != null)
            {
                await base.PopulateRoutingInstancesDropDownList(attachment.RoutingInstance.DeviceID, attachment.TenantID,
                    isInfrastructureVrf: true, selectedRoutingInstance: attachment.RoutingInstanceID);
            }

            return await base.BaseEdit(attachment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("AttachmentID,RoutingInstanceID,CreateNewRoutingInstance,MtuID,"
            + "BundleMinLinks,BundleMaxLinks,IsTagged,AttachmentRoleID,Description,Notes,RowVersion")] AttachmentUpdateViewModel updateModel,
            AttachmentNavigationViewModel nav)
        {
            if (id != updateModel.AttachmentID)
            {
                return NotFound();
            }

            var currentAttachment = await InfrastructureAttachmentService.GetByIDAsync(updateModel.AttachmentID);
            if (currentAttachment == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            nav.RedirectAction = "GetAll";

            try
            {
                return await base.BaseEdit(updateModel, currentAttachment, nav);
            }

            finally
            {
                if (currentAttachment.RoutingInstance != null)
                {
                    await base.PopulateRoutingInstancesDropDownList(deviceID: currentAttachment.RoutingInstance.DeviceID,
                        isInfrastructureVrf: true, selectedRoutingInstance: currentAttachment.RoutingInstanceID);
                }
            }
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
        public async Task<ActionResult> EditInterface(int? id, [Bind("InterfaceID,AttachmentID,DeviceID,IpAddress,SubnetMask,RowVersion")]
        InterfaceViewModel updateModel)
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

        public async Task<ActionResult> EditPort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await PortService.GetByIDAsync(id.Value);
            return await base.BaseEditPort(port);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPort(int? id, [Bind("AttachmentID,DeviceID,InterfaceID,PortID,"
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

            return await base.BaseDeleteAttachment(attachmentID.Value, nav, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AttachmentViewModel attachmentModel, AttachmentNavigationViewModel nav)
        {
            var currentAttachment = await AttachmentService.GetByIDAsync(attachmentModel.AttachmentID);
            if (currentAttachment == null)
            {
                return RedirectToAction("GetAll", nav);
            }

            nav.RedirectAction = "GetAll";
            return await base.BaseDeleteAttachment(attachmentModel, currentAttachment, nav);
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

            return View(Mapper.Map<InfrastructureAttachmentRoutingInstanceUpdateViewModel>(vrf));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRoutingInstance(int id, [Bind("RoutingInstanceID,AttachmentID,Name,AdministratorSubField,"
            + "AssignedNumberSubField,RowVersion")] InfrastructureAttachmentRoutingInstanceUpdateViewModel updateModel)
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
                        return RedirectToAction("Details", new
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

            return View(Mapper.Map<InfrastructureAttachmentRoutingInstanceUpdateViewModel>(currentRoutingInstance));
        }
    }
}