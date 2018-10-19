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
using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Hubs;
using SCM.Validators;
using SCM.Factories;
using Microsoft.AspNetCore.Mvc.Filters;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class BaseVifController : BaseViewController
    {
        public BaseVifController(IVifService vifService, 
            IAttachmentService attachmentService,
            IVifRoleService vifRoleService,
            IRoutingInstanceService vrfService,
            IVlanService vlanService,
            ILogicalInterfaceService logicalInterfaceService,
            IVifValidator vifValidator,
            IVlanValidator vlanValidator,
            IRoutingInstanceValidator routingInstanceValidator,
            IMapper mapper)
        {
            VifService = vifService;
            VifRoleService = vifRoleService;
            AttachmentService = attachmentService;
            RoutingInstanceService = vrfService;
            VlanService = vlanService;
            LogicalInterfaceService = logicalInterfaceService;
            Mapper = mapper;

            VifValidator = vifValidator;
            this.Validator = vifValidator;
            VlanValidator = vlanValidator;
            RoutingInstanceValidator = routingInstanceValidator;
        }
        
        protected IAttachmentService AttachmentService { get; } 
        protected IVifService VifService { get; }
        protected IVifRoleService VifRoleService { get; }
        protected IVlanService VlanService { get; }
        protected IRoutingInstanceService RoutingInstanceService { get; }
        protected ILogicalInterfaceService LogicalInterfaceService { get; }
        protected IMapper Mapper { get; }
        protected IVifValidator VifValidator { get; }
        protected IVlanValidator VlanValidator { get; }
        protected IRoutingInstanceValidator RoutingInstanceValidator { get; }

        /// <summary>
        /// Executed before each action method is executed.
        /// Sets each of the validation dictionary properties for this controller's validators
        /// to the current modelstate object. Overrides the base controller method which 
        /// only sets the validation dictionary for the VIF validator.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var dic = new ModelStateWrapper(context.ModelState);
            Validator.ValidationDictionary = dic;
            VlanValidator.ValidationDictionary = dic;
            RoutingInstanceValidator.ValidationDictionary = dic;
        }

        protected async Task<IActionResult> BaseGetByID(int id)
        {
            var item = await VifService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VifViewModel>(item));
        }

        protected async Task<IActionResult> BaseGetAllByAttachmentID(int id)
        {
            var attachment = await AttachmentService.GetByIDAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            var vifs = await VifService.GetAllByAttachmentIDAsync(id);

            ViewData["SuccessMessage"] = FormatAsHtmlList(vifs
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            ViewData["NetworkWarningMessage"] = FormatAsHtmlList(vifs
                .Where(x => x.RequiresSync && x.ShowRequiresSyncAlert)
                .Select(x => $"{x.Name} requires sync with the network.").ToList());

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            return View(Mapper.Map<List<VifViewModel>>(vifs));
        }

        protected async Task<IActionResult> BaseGetAllLogicalInterfacesByVifID(int id)
        {
            var vif = await VifService.GetByIDAsync(id);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            var logicalInterfaces = await LogicalInterfaceService.GetAllByRoutingInstanceIDAsync(vif.RoutingInstanceID.Value);

            return View(Mapper.Map<List<LogicalInterfaceViewModel>>(logicalInterfaces));
        }

        protected async Task<IActionResult> BaseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VifService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(item.AttachmentID);
            if (attachment == null)
            {
                return NotFound();
            }

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            return View(Mapper.Map<VifViewModel>(item));
        }

        protected async Task<IActionResult> BaseRoutingInstanceDetails(Vif vif, bool showWarningMessage = false)
        {
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"Device '{vif.Attachment.Device.Name}' requires synchronisation with the network as a result of this update. "
                      + $"Follow this <a href = '/InfrastructureDevice/GetAll'>link</a> to go to the Devices page.";
            }

            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Vif = vif;
            var vrf = await RoutingInstanceService.GetByIDAsync(vif.RoutingInstanceID.Value);

            return View(Mapper.Map<RoutingInstanceViewModel>(vrf));
        }

        protected async Task<IActionResult> BaseLogicalInterfaceDetails(Vif vif, int logicalInterfaceID)
        {
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Vif = vif;
            var logicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceID);

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        protected async Task<IActionResult> BaseCreate(Attachment attachment)
        {

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            PopulateContractBandwidthPoolsDropDownList(attachment, attachment.TenantID);
            await PopulateContractBandwidthsDropDownList();
            await PopulateVifrRolessDropDownList(attachment.AttachmentRoleID);

            return View();
        }

        protected async Task<IActionResult> BaseCreateLogicalInterface(Vif vif)
        {
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            PopulateLogicalInterfaceTypesDropDownList();

            return View();
        }


        protected async Task<ActionResult> BaseEdit(Vif vif)
        {
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            PopulateContractBandwidthPoolsDropDownList(attachment, vif.TenantID, vif.ContractBandwidthPoolID);
            await PopulateContractBandwidthsDropDownList();
            
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<VifUpdateViewModel>(vif));
        }

        protected async Task<ActionResult> BaseEdit(VifUpdateViewModel updateModel, Vif currentVif)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vifUpdate = Mapper.Map<VifUpdate>(updateModel);
                    await VifValidator.ValidateChangesAsync(vifUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await VifService.UpdateAsync(vifUpdate);
                        return RedirectToAction("GetAllByAttachmentID", new
                        {
                            id = currentVif.AttachmentID
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedContractBandwidthPoolID = (int)exceptionEntry.Property("ContractBandwidthPoolID").CurrentValue;
                if (currentVif.ContractBandwidthPoolID != proposedContractBandwidthPoolID)
                {
                    ModelState.AddModelError("ContractBandwidthPoolID", $"Current value: {currentVif.ContractBandwidthPool.Name}");
                }

                var proposedTrustReceivedCosDscp = (bool)exceptionEntry.Property("TrustReceivedCosDscp").CurrentValue;
                if (currentVif.ContractBandwidthPool.TrustReceivedCosDscp != proposedTrustReceivedCosDscp)
                {
                    ModelState.AddModelError("TrustReceivedCosDscp", $"Current value: {currentVif.ContractBandwidthPool.TrustReceivedCosDscp}");
                }

                var proposedRoutingInstanceID = (int)exceptionEntry.Property("RoutingInstanceID").CurrentValue;
                if (currentVif.RoutingInstanceID.Value != proposedRoutingInstanceID)
                {
                    ModelState.AddModelError("RoutingInstanceID", $"Current value: {currentVif.RoutingInstance.Name}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /** ex **/)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            finally
            {
                var attachment = await AttachmentService.GetByIDAsync(currentVif.AttachmentID);
                PopulateContractBandwidthPoolsDropDownList(attachment, updateModel.TenantID, updateModel.ContractBandwidthPoolID);
                await PopulateContractBandwidthsDropDownList(updateModel.ContractBandwidthID);
                ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            }

            return View(Mapper.Map<VifUpdateViewModel>(currentVif));
        }

        protected async Task<ActionResult> BaseEditVlan(Vlan vlan)
        {
            var attachment = await AttachmentService.GetByIDAsync(vlan.Vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vlan.Vif);

            return View(Mapper.Map<VlanViewModel>(vlan));
        }

        protected async Task<ActionResult> BaseEditVlan(int id, VlanUpdateViewModel updateModel)
        {
            var vlan = await VlanService.GetByIDAsync(updateModel.VlanID);
            if (vlan == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.Map(updateModel, vlan);
                    await VlanValidator.ValidateChangesAsync(vlan);

                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await VlanService.UpdateAsync(vlan);
                        return RedirectToAction("GetAllVlansByVifID", new
                        {
                            id = vlan.VifID,
                            showWarningMessage = false
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (vlan.Vif.IsLayer3)
                {
                    var proposedIpAddress = (string)exceptionEntry.Property("IpAddress").CurrentValue;
                    if (vlan.IpAddress != proposedIpAddress)
                    {
                        ModelState.AddModelError("IpAddress", $"Current value: {vlan.IpAddress}");
                    }

                    var proposedSubnetMask = (string)exceptionEntry.Property("SubnetMask").CurrentValue;
                    if (vlan.SubnetMask != proposedSubnetMask)
                    {
                        ModelState.AddModelError("SubnetMask", $"Current value: {vlan.SubnetMask}");
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

            var attachment = await AttachmentService.GetByIDAsync(vlan.Vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vlan.Vif);

            return View(Mapper.Map<VlanViewModel>(vlan));
        }

        protected async Task<ActionResult> BaseEditLogicalInterface(int logicalInterfaceID, int vifID)
        {
            var logicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceID);
            if (logicalInterface == null)
            {
                return NotFound();
            }

            var vif = await VifService.GetByIDAsync(vifID);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        protected async Task<ActionResult> BaseEditLogicalInterface(int vifID, LogicalInterface currentLogicalInterface, 
            LogicalInterfaceViewModel updateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var logicalInterfaceUpdate = Mapper.Map<LogicalInterface>(updateModel);
                    await LogicalInterfaceService.UpdateAsync(logicalInterfaceUpdate);
                    return RedirectToAction("GetAllLogicalInterfacesByVifID", new
                    {
                        vifID = vifID,
                        showWarningMessage = true
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpAddress = (string)exceptionEntry.Property("IpAddress").CurrentValue;
                if (currentLogicalInterface.IpAddress != proposedIpAddress)
                {
                    ModelState.AddModelError("IpAddress", $"Current value: {currentLogicalInterface.IpAddress}");
                }

                var proposedSubnetMask = (string)exceptionEntry.Property("SubnetMask").CurrentValue;
                if (currentLogicalInterface.SubnetMask != proposedSubnetMask)
                {
                    ModelState.AddModelError("SubnetMask", $"Current value: {currentLogicalInterface.SubnetMask}");
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

            finally
            {
                var vif = await VifService.GetByIDAsync(vifID);
                ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
                var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
                ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            }

            return View(Mapper.Map<LogicalInterfaceViewModel>(currentLogicalInterface));
        }

        protected async Task<IActionResult> BaseDelete(int id, int attachmentID, bool? concurrencyError = false)
        {
            var vif = await VifService.GetByIDAsync(id);
            if (vif == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByAttachmentID", new { id = attachmentID });
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

            var attachment = await AttachmentService.GetByIDAsync(attachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<VifViewModel>(vif));
        }

        protected async Task<IActionResult> BaseDelete(VifViewModel vifModel)
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

                    await VifService.DeleteAsync(Mapper.Map<Vif>(vifModel));
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

        protected async Task<IActionResult> BaseDeleteLogicalInterface(int id, VifNavigationViewModel nav, bool? concurrencyError = false)
        {
            var item = await LogicalInterfaceService.GetByIDAsync(id);
            if (item == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nav.RedirectAction, nav);
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

            var vif = await VifService.GetByIDAsync(nav.VifID.Value);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<LogicalInterfaceViewModel>(item));
        }

        protected async Task<IActionResult> BaseDeleteLogicalInterface(LogicalInterfaceViewModel logicalInterfaceModel,
            LogicalInterface currentLogicalInterface, VifNavigationViewModel nav)
        {
            try
            {
                await LogicalInterfaceService.DeleteAsync(Mapper.Map<LogicalInterface>(logicalInterfaceModel));
                return RedirectToAction(nav.RedirectAction, nav);
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)

                nav.ConcurrencyError = true;
                nav.LogicalInterfaceID = currentLogicalInterface.LogicalInterfaceID;
                return RedirectToAction("DeleteLogicalInterface", nav);
            }

            catch (Exception /** ex **/ )
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            return View(Mapper.Map<LogicalInterfaceViewModel>(currentLogicalInterface));
        }


        /// <summary>
        /// Populates Contract Bandwidth Pool drop-down lists.
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="attachmentID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PartialViewResult> ContractBandwidthPools(int attachmentID, int? tenantID = null)
        {
            var attachment = await AttachmentService.GetByIDAsync(attachmentID);

            var contractBandwidthPools = attachment.Vifs
                .Select(q => q.ContractBandwidthPool)
                .Where(q => q.TenantID == tenantID)
                .GroupBy(q => q.ContractBandwidthPoolID)
                .Select(group => group.First());

            return PartialView(Mapper.Map<List<ContractBandwidthPoolViewModel>>(contractBandwidthPools));
        }

        protected async Task<IActionResult> BaseGetAllVlansByVifID(Vif vif, bool? showWarningMessage = false)
        {
           
            if (showWarningMessage.GetValueOrDefault())
            {
                ViewData["NetworkWarningMessage"] = $"The VIF requires synchronisation with the network as a result of this update. "
                      + $"Follow this <a href = '/Vif/GetAllByAttachmentID/{vif.AttachmentID}'>link</a> to go to the VIFs page.";
            }

            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            var vlans = await VlanService.GetAllByVifIDAsync(vif.VifID);

            return View(Mapper.Map<List<VlanViewModel>>(vlans));
        }

        /// <summary>
        /// Helper to populate Contract Bandwidth Pools drop-down lists
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="tenantID"></param>
        /// <param name="selectedContractBandwidthPool"></param>
        protected void PopulateContractBandwidthPoolsDropDownList(Attachment attachment, int? tenantID = null, object selectedContractBandwidthPool = null)
        {
            var contractBandwidthPools = attachment.Vifs
                .Select(q => q.ContractBandwidthPool)
                .Where(q => q.TenantID == tenantID)
                .GroupBy(q => q.ContractBandwidthPoolID)
                .Select(group => group.First());

            ViewBag.ContractBandwidthPoolID = new SelectList(contractBandwidthPools,
                "ContractBandwidthPoolID", 
                "Name", 
                selectedContractBandwidthPool);
        }

        /// <summary>
        /// Helper to populate Contract Bandwidths drop-down lists
        /// </summary>
        /// <param name="selectedContractBandwidth"></param>
        /// <returns></returns>
        protected async Task PopulateContractBandwidthsDropDownList(object selectedContractBandwidth = null)
        {
            var contractBandwidths = await AttachmentService.UnitOfWork.ContractBandwidthRepository.GetAsync();
            ViewBag.ContractBandwidthID = new SelectList(contractBandwidths.OrderBy(b => b.BandwidthMbps), 
                "ContractBandwidthID", "BandwidthMbps", selectedContractBandwidth);
        }

        /// <summary>
        /// Helper to populate the VRFs drop-down list
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="tenantID"></param>
        /// <param name="selectedRoutingInstance"></param>
        /// <returns></returns>
        protected async Task PopulateRoutingInstancesDropDownList(int deviceID, int? tenantID = null, 
            bool? isTenantFacingVrf = null, bool? isInfrastructureVrf = null, object selectedRoutingInstance = null)
        {
            var vrfs = await RoutingInstanceService.GetAllByDeviceIDAsync(deviceID: deviceID, tenantID: tenantID, 
                isTenantFacingVrf: isTenantFacingVrf, isInfrastructureVrf: isInfrastructureVrf);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(vrfs), 
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }

        /// <summary>
        /// Helper to populate the VIF Roles drop-down list
        /// </summary>
        /// <returns></returns>
        protected async Task PopulateVifrRolessDropDownList(int attachmentRoleID, object selectedVifRole = null)
        {
            var vifRoles = await VifRoleService.GetAllByAttachmentRoleIDAsync(attachmentRoleID);
            ViewBag.VifRoleID = new SelectList(Mapper.Map<List<VifRoleViewModel>>(vifRoles), "VifRoleID", "Name", selectedVifRole);
        }

        /// <summary>
        /// Helper to populate the Logical Interface Tyes drop-down list
        /// </summary>
        /// <param name="selectedLogicalInterfaceType"></param>
        private void PopulateLogicalInterfaceTypesDropDownList(object selectedLogicalInterfaceType = null)
        {
            var logicalInterfaceTypesList = new List<SelectListItem>();
            foreach (var logicalInterfaceType in Enum.GetValues(typeof(LogicalInterfaceTypeEnum)))
            {
                logicalInterfaceTypesList.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(LogicalInterfaceTypeEnum),
                    logicalInterfaceType),
                    Value = logicalInterfaceType.ToString()
                });
            }

            ViewBag.LogicalInterfaceType = logicalInterfaceTypesList;
        }
    }
}