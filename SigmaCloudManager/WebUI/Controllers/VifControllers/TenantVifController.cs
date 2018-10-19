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
using SCM.Models.RequestModels;
using SCM.Factories;
using SCM.Models;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class TenantVifController : BaseVifController
    {
        public TenantVifController(IVifService vifService,
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
        public async Task<IActionResult> GetAllLogicalInterfacesByVifID(int? vifID)
        {
            if (vifID == null)
            {
                return NotFound();
            }

            return await base.BaseGetAllLogicalInterfacesByVifID(vifID.Value);
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ClearCreatedAlerts(int id)
        {
            var vifs = await VifService.GetAllByAttachmentIDAsync(id, created: true, showCreatedAlert: true);
            foreach (var vif in vifs)
            {
                vif.ShowCreatedAlert = false;
            }

            try
            {
                await VifService.UpdateAsync(vifs);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAllByAttachmentID", new { id });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ClearRequiresSyncAlerts(int id)
        {
            var vifs = await VifService.GetAllByAttachmentIDAsync(id, requiresSync: true, showRequiresSyncAlert: true);
            foreach (var vif in vifs)
            {
                vif.ShowRequiresSyncAlert = false;
            }

            try
            {
                await VifService.UpdateAsync(vifs);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAllByAttachmentID", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> LogicalInterfaceDetails(int? logicalInterfaceID, int? vifID)
        {
            if (logicalInterfaceID == null)
            {
                return NotFound();
            }

            if (vifID == null)
            {
                return NotFound();
            }

            var vif = await VifService.GetByIDAsync(vifID.Value);
            if (vif == null)
            {
                return NotFound();
            }

            return await base.BaseLogicalInterfaceDetails(vif, logicalInterfaceID.Value);
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

            await PopulateTenantsDropDownList(attachment.TenantID);
            return await base.BaseCreate(attachment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentID,IpAddress1,SubnetMask1,"
            + "IpAddress2,SubnetMask2,IpAddress3,SubnetMask3,"
            + "IpAddress4,SubnetMask4,IsLayer3,AutoAllocateVlanTag,"
            + "RequestedVlanTag,TenantID,ContractBandwidthPoolID,"
            + "ContractBandwidthID,TrustReceivedCosDscp,VifRoleID")] TenantVifRequestViewModel requestModel)
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
            await PopulateTenantsDropDownList(attachment.TenantID);

            return View(requestModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLogicalInterface(int? id)
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

            return await base.BaseCreateLogicalInterface(vif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLogicalInterface([Bind("RoutingInstanceID,IpAddress,SubnetMask,Description,LogicalInterfaceType")]
        LogicalInterfaceViewModel model, VifNavigationViewModel nav)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.Map<LogicalInterface>(model);
                    await LogicalInterfaceService.AddAsync(request);
                    return RedirectToAction("GetAllLogicalInterfacesByVifID", nav);
                }
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var vif = await VifService.GetByIDAsync(nav.VifID.Value);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
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

            var vif = await VifService.GetByIDAsync(id.Value);
            if (vif == null)
            {
                return NotFound();
            }

            await base.PopulateRoutingInstancesDropDownList(deviceID: vif.Attachment.DeviceID, tenantID: vif.RoutingInstance.TenantID, 
                isTenantFacingVrf: true, selectedRoutingInstance: vif.RoutingInstanceID);

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
                await PopulateRoutingInstancesDropDownList(deviceID: currentVif.Attachment.DeviceID, tenantID: currentVif.RoutingInstance.TenantID, 
                    isTenantFacingVrf: true, selectedRoutingInstance: updateModel.RoutingInstanceID);
            }
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
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<TenantVifRoutingInstanceUpdateViewModel>(vrf));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRoutingInstance(int id, [Bind("RoutingInstanceID,VifID,Name,AdministratorSubField,"
            + "AssignedNumberSubField,RowVersion")] TenantVifRoutingInstanceUpdateViewModel updateModel)
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

                var proposedAdministratorSubField = (int)exceptionEntry.Property("AdministratorSubField").CurrentValue;
                if (currentRoutingInstance.AdministratorSubField != proposedAdministratorSubField)
                {
                    ModelState.AddModelError("AdministratorSubField", $"Current value: {currentRoutingInstance.AdministratorSubField}");
                }

                var proposedAssignedNumberSubField = (int)exceptionEntry.Property("AssignedNumberSubField").CurrentValue;
                if (currentRoutingInstance.AssignedNumberSubField != proposedAssignedNumberSubField)
                {
                    ModelState.AddModelError("AssignedNumberSubField", $"Current value: {currentRoutingInstance.AssignedNumberSubField}");
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

            return View(Mapper.Map<TenantVifRoutingInstanceUpdateViewModel>(currentRoutingInstance));
        }

        [HttpGet]
        public async Task<ActionResult> EditLogicalInterface(int? logicalInterfaceID, int? vifID)
        {
            if (logicalInterfaceID == null)
            {
                return NotFound();
            }

            if (vifID == null)
            {
                return NotFound();
            }

            return await base.BaseEditLogicalInterface(logicalInterfaceID.Value, vifID.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLogicalInterface(int? logicalInterfaceID, int? vifID,
            [Bind("LogicalInterfaceID,RoutingInstanceID,IpAddress,SubnetMask,Description,RowVersion")] LogicalInterfaceViewModel updateModel)
        {
            if (logicalInterfaceID != updateModel.LogicalInterfaceId)
            {
                return NotFound();
            }

            if (vifID == null)
            {
                return NotFound();
            }

            var currentLogicalInterface = await LogicalInterfaceService.GetByIDAsync(updateModel.LogicalInterfaceId.Value);
            if (currentLogicalInterface == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            return await base.BaseEditLogicalInterface(vifID.Value, currentLogicalInterface, updateModel);
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

            var vif = await VifService.GetByIDAsync(vifModel.VifID);
            if (vif == null)
            {
                return RedirectToAction("GetAllByAttachmentID", new
                {
                    id = vifModel.AttachmentID
                });
            }

            try
            {
                VifValidator.ValidationDictionary.Clear();
                VifValidator.ValidateDelete(vif);
                if (VifValidator.ValidationDictionary.IsValid)
                {
                    // Copy over RowVersion to allow for concurrency checks to work
                    vif.RowVersion = vifModel.RowVersion;

                    await VifService.DeleteAsync(vif);
                    return RedirectToAction("GetAllByAttachmentID", new
                    {
                        id = vifModel.AttachmentID
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vifModel.VifID,
                    attachmentID = vifModel.AttachmentID
                });
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            var attachment = await AttachmentService.GetByIDAsync(vifModel.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<VifViewModel>(vif));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLogicalInterface(int? logicalInterfaceID, VifNavigationViewModel nav, bool? concurrencyError = false)
        {
            if (logicalInterfaceID == null)
            {
                return NotFound();
            }

            nav.RedirectAction = "GetAllLogicalInterfacesByVifID";
            return await base.BaseDeleteLogicalInterface(logicalInterfaceID.Value, nav, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogicalInterface(LogicalInterfaceViewModel logicalInterfaceModel, VifNavigationViewModel nav)
        {
            var currentLogicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceModel.LogicalInterfaceId.Value);
            if (currentLogicalInterface == null)
            {
                return RedirectToAction("GetAllLogicalInterfacesByVifID", nav);
            }

            nav.RedirectAction = "GetAllLogicalInterfacesByVifID";
            var vif = await VifService.GetByIDAsync(nav.VifID.Value);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return await base.BaseDeleteLogicalInterface(logicalInterfaceModel, currentLogicalInterface, nav);
        }

        /// <summary>
        /// Helper to populate Tenants drop-down lists
        /// </summary>
        /// <param name="selectedTenant"></param>
        /// <returns></returns>
        private async Task PopulateTenantsDropDownList(object selectedTenant = null)
        {
            var tenants = await VifService.UnitOfWork.TenantRepository.GetAsync();
            ViewBag.TenantID = new SelectList(tenants, "TenantID", "Name", selectedTenant);
        }
    }
}