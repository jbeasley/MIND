using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using AutoMapper;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using SCM.Factories;
using SCM.Models.RequestModels;

namespace SCM.Controllers
{
    public class InfrastructureVifController : BaseVifController
    {
        public InfrastructureVifController(IVifService vifService,
            IAttachmentService attachmentService,
            IVifRoleService vifRoleService,
            IRoutingInstanceService vrfService,
            IVlanService vlanService,
            ILogicalInterfaceService logicalInterfaceService,
            IVifValidator vifValidator,
            IVlanValidator vlanValidator,
            IRoutingInstanceValidator routingInstanceValidator,
            IMapper mapper) : base(vifService, 
                attachmentService, 
                vifRoleService, 
                vrfService, 
                vlanService, 
                logicalInterfaceService,
                vifValidator, 
                vlanValidator,
                routingInstanceValidator,
                mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await base.BaseGetAllByAttachmentID(id.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
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

            return await base.BaseCreate(attachment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentID,IpAddress1,SubnetMask1,"
            + "IpAddress2,SubnetMask2,IpAddress3,SubnetMask3,"
            + "IpAddress4,SubnetMask4,IsLayer3,AutoAllocateVlanTag,"
            + "RequestedVlanTag,TenantID,ContractBandwidthPoolID,"
            + "ContractBandwidthID,TrustReceivedCosDscp,VifRoleID")] InfrastructureVifRequestViewModel requestModel)
        {

            var attachment = await AttachmentService.GetByIDAsync(requestModel.AttachmentID);
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<VifRequest>(requestModel);

                // Add AttachmentIsMultiPort property here to control program flow in the service logic

                request.AttachmentIsMultiPort = attachment.IsMultiPort;

                await VifValidator.ValidateNewAsync(request);
                if (VifValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await VifService.AddAsync(request);
                        return RedirectToAction("GetAllByAttachmentID", new { id = request.AttachmentID });
                    }

                    catch (DbUpdateException /** ex **/ )
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }

                    catch (ServiceException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }

                    catch (FactoryFailureException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            base.PopulateContractBandwidthPoolsDropDownList(attachment: attachment, selectedContractBandwidthPool: requestModel.ContractBandwidthPoolID);
            await base.PopulateContractBandwidthsDropDownList(requestModel.ContractBandwidthID);
            await base.PopulateVifrRolessDropDownList(attachment.AttachmentRoleID, requestModel.VifRoleID);

            return View(requestModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vif = await VifService.GetByIDAsync(id.Value);
            if (vif == null)
            {
                return NotFound();
            }

            await base.PopulateRoutingInstancesDropDownList(deviceID: vif.Attachment.DeviceID,
             isInfrastructureVrf: true, selectedRoutingInstance: vif.RoutingInstanceID);

            return await base.BaseEdit(vif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VifID,TenantID,AttachmentID,IsLayer3,ContractBandwidthID,ContractBandwidthPoolID," +
            "IpAddress1,SubnetMask1,TrustReceivedCosDscp,RoutingInstanceID,CreateNewRoutingInstance,RowVersion")] VifUpdateViewModel updateModel)
        {
            if (id != updateModel.VifID)
            {
                return NotFound();
            }

            var currentVif = await VifService.GetByIDAsync(updateModel.VifID);
            if (currentVif == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                return await base.BaseEdit(updateModel, currentVif);
            }

            finally
            {
                await PopulateRoutingInstancesDropDownList(deviceID: currentVif.Attachment.DeviceID,
                    isInfrastructureVrf: true, selectedRoutingInstance: updateModel.RoutingInstanceID);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentID, bool? concurrencyError = false)
        {
            if (id == null || attachmentID == null)
            {
                return NotFound();
            }

            return await base.BaseDelete(id.Value, attachmentID.Value, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VifViewModel vifModel)
        {
            return await base.BaseDelete(vifModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVlansByVifID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vif = await VifService.GetByIDAsync(id.Value);
            if (vif == null)
            {
                return NotFound();
            }

            return await base.BaseGetAllVlansByVifID(vif, showWarningMessage);
        }

        [HttpGet]
        public async Task<ActionResult> EditVlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vlan = await VlanService.GetByIDAsync(id.Value);
            if (vlan == null)
            {
                return NotFound();
            }

            return await base.BaseEditVlan(vlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVlan(int id, [Bind("VlanID,IpAddress,SubnetMask,RowVersion")] VlanUpdateViewModel updateModel)
        {
            if (id != updateModel.VlanID)
            {
                return NotFound();
            }

            return await base.BaseEditVlan(id, updateModel);
        }

        [HttpGet]
        public async Task<IActionResult> RoutingInstanceDetails(int? id, bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vif = await VifService.GetByIDAsync(id.Value);
            if (vif == null)
            {
                return NotFound();
            }

            return await base.BaseRoutingInstanceDetails(vif, showWarningMessage);
        }

        [HttpGet]
        public async Task<ActionResult> EditRoutingInstance(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vif = await VifService.GetByIDAsync(id.Value);
            if (vif == null)
            {
                return NotFound();
            }

            var vrf = await RoutingInstanceService.GetByIDAsync(vif.RoutingInstance.RoutingInstanceID);
            ViewBag.Vif = vif;
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<InfrastructureVifRoutingInstanceUpdateViewModel>(vrf));
        }

        [HttpPost]
        public async Task<ActionResult> EditRoutingInstance(int id, [Bind("RoutingInstanceID,VifID,Name,AdministratorSubField,"
            + "AssignedNumberSubField,RowVersion")] InfrastructureVifRoutingInstanceUpdateViewModel updateModel)
        {
            if (id != updateModel.VifID)
            {
                return NotFound();
            }

            var currentVif = await VifService.GetByIDAsync(updateModel.VifID);
            if (currentVif == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            var currentRoutingInstance = await RoutingInstanceService.GetByIDAsync(currentVif.RoutingInstanceID.Value);

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
                            id = currentVif.VifID,
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

            var attachment = await AttachmentService.GetByIDAsync(currentVif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Vif = await VifService.GetByIDAsync(currentVif.VifID);

            return View(Mapper.Map<InfrastructureVifRoutingInstanceUpdateViewModel>(currentRoutingInstance));
        }
    }
}