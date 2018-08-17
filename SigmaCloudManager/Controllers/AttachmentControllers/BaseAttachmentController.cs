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
using Microsoft.AspNetCore.Mvc.Filters;

namespace SCM.Controllers
{
    public class BaseAttachmentController : BaseViewController
    {
        public BaseAttachmentController(IAttachmentService attachmentService, 
            IRoutingInstanceService vrfService,
            IRegionService regionService,
            ISubRegionService subRegionService,
            ILocationService locationService,
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
            IMapper mapper)
        {
            RegionService = regionService;
            SubRegionService = subRegionService;
            LocationService = locationService;
            AttachmentService = attachmentService;
            AttachmentRoleService = attachmentRoleService;
            RoutingInstanceService = vrfService;
            DeviceService = deviceService;
            PortService = portService;
            InterfaceService = interfaceService;
            MtuService = mtuService;
            PortRoleService = portRoleService;
            PortPoolService = portPoolService;
            LogicalInterfaceService = logicalInterfaceService;
            Mapper = mapper;

            AttachmentValidator = attachmentValidator;
            this.Validator = attachmentValidator;
            RoutingInstanceValidator = routingInstanceValidator;
        }

        protected IRegionService RegionService { get; }
        protected ISubRegionService SubRegionService { get; }
        protected ILocationService LocationService { get; }
        protected IAttachmentService AttachmentService { get; }
        protected IRoutingInstanceService RoutingInstanceService { get; }
        protected IDeviceService DeviceService { get; }
        protected IPortService PortService { get; }
        protected IInterfaceService InterfaceService { get; }
        protected IMtuService MtuService { get; }
        protected IPortRoleService PortRoleService { get; }
        protected IPortPoolService PortPoolService { get; }
        protected IAttachmentRoleService AttachmentRoleService { get; }
        protected ILogicalInterfaceService LogicalInterfaceService { get; }
        protected IAttachmentValidator AttachmentValidator { get; }
        protected IRoutingInstanceValidator RoutingInstanceValidator { get; }
        protected IMapper 
            
            
            Mapper { get; }

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
            RoutingInstanceValidator.ValidationDictionary = dic;
        }

        [HttpGet]
        public async Task<PartialViewResult> SubRegions(int id)
        {
            var subRegions = await AttachmentService.UnitOfWork.SubRegionRepository.GetAsync(q => q.RegionID == id);
            return PartialView(Mapper.Map<List<SubRegionViewModel>>(subRegions));
        }

        [HttpGet]
        public async Task<PartialViewResult> Locations(int id)
        {
            var locations = await AttachmentService.UnitOfWork.LocationRepository.GetAsync(q => q.SubRegionID == id);
            return PartialView(Mapper.Map<List<LocationViewModel>>(locations));
        }

        [HttpGet]
        public async Task<PartialViewResult> Devices(int locationID, int? planeID)
        {
            var devices = await DeviceService.GetAllByLocationIDAsync(locationID, planeID, deep: false);
            return PartialView(Mapper.Map<List<DeviceViewModel>>(devices));
        }

        [HttpGet]
        public async Task<PartialViewResult> Ports(int deviceID, int portID)
        {
            var port = await PortService.GetByIDAsync(portID);
            var allDevicePorts = await PortService.GetAllByDeviceIDAsync(deviceID);
            var result = allDevicePorts.Where(x => x.PortStatus.Name == "Free" || x.ID == port.ID);

            return PartialView(Mapper.Map<List<PortViewModel>>(result));
        }

        [HttpGet]
        public async Task<PartialViewResult> AttachmentRoles(int portPoolID, int? deviceID = null)
        {
            int? deviceRoleID = null;
            if (deviceID != null)
            {
                var device = await DeviceService.GetByIDAsync(deviceID.Value);
                deviceRoleID = device.DeviceRoleID;
            }

            var attachmentRoles = await AttachmentRoleService.GetAllByPortPoolIDAsync(portPoolID, deviceRoleID);
            return PartialView(Mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles));
        }

        protected async Task<IActionResult> BaseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await AttachmentService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<AttachmentViewModel>(item));
        }

        protected async Task<IActionResult> BaseRoutingInstanceDetails(Attachment attachment)
        {
            if (attachment.RoutingInstance == null)
            {
                return NotFound();
            }

            var vrf = await RoutingInstanceService.GetByIDAsync(attachment.RoutingInstance.RoutingInstanceID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<RoutingInstanceViewModel>(vrf));
        }

        protected async Task<IActionResult> BaseLogicalInterfaceDetails(int logicalInterfaceID)
        {
            var logicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceID);
            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        protected async Task<IActionResult> BaseGetAllInterfacesByAttachmentID(int id)
        {
            var attachment = await AttachmentService.GetByIDAsync(id);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            var ifaces = await InterfaceService.GetAllByAttachmentIDAsync(id);

            return View(Mapper.Map<List<InterfaceViewModel>>(ifaces));
        }

        protected async Task<IActionResult> BaseGetAllLogicalInterfacesByAttachmentID(int id)
        {
            var attachment = await AttachmentService.GetByIDAsync(id);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            var logicalInterfaces = await LogicalInterfaceService.GetAllByRoutingInstanceIDAsync(attachment.RoutingInstanceID.Value);

            return View(Mapper.Map<List<LogicalInterfaceViewModel>>(logicalInterfaces));
        }

        protected async Task<IActionResult> BaseGetAllPortsByInterfaceID(int id)
        {
            var attachmentInterface = await InterfaceService.GetByIDAsync(id);
            if (attachmentInterface == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentInterface.AttachmentID);
            ViewBag.Interface = Mapper.Map<InterfaceViewModel>(attachmentInterface);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            var ports = await PortService.GetAllByInterfaceIDAsync(id);

            return View(Mapper.Map<List<PortViewModel>>(ports));
        }

        protected async Task<IActionResult> BaseCreateAttachment()
        {
            await PopulateBandwidthsDropDownList();
            return View();
        }

        protected IActionResult BaseCreateLogicalInterface(Attachment attachment)
        {
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            PopulateLogicalInterfaceTypesDropDownList();

            return View();
        }

        protected async Task<ActionResult> BaseEdit(Attachment attachment)
        {
            await PopulateAttachmentRolesDropDownList(attachment.AttachmentRole.PortPoolID, 
                attachment.Device.DeviceRoleID, attachment.AttachmentRoleID);
            await PopulateMtusDropDownList(attachment.Device, attachment.MtuID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<AttachmentUpdateViewModel>(attachment));
        }

        protected async Task<ActionResult> BaseEdit(AttachmentUpdateViewModel updateModel, Attachment currentAttachment, AttachmentNavigationViewModel nav)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var attachmentUpdate = Mapper.Map<AttachmentUpdate>(updateModel);
                    await AttachmentValidator.ValidateChangesAsync(attachmentUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        nav.ShowWarningMessage = true;
                        await AttachmentService.UpdateAttachmentAsync(attachmentUpdate);
                        return RedirectToAction(nav.RedirectAction, nav);
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedMtuID = (int)exceptionEntry.Property("MtuID").CurrentValue;
                if (currentAttachment.MtuID != proposedMtuID)
                {
                    ModelState.AddModelError("MtuID", $"Current value: {currentAttachment.Mtu.MtuValue}");
                }

                if (currentAttachment.RoutingInstanceID != null)
                {
                    var proposedRoutingInstanceID = (int)exceptionEntry.Property("RoutingInstanceID").CurrentValue;
                    if (currentAttachment.RoutingInstanceID.Value != proposedRoutingInstanceID)
                    {
                        ModelState.AddModelError("RoutingInstanceID", $"Current value: {currentAttachment.RoutingInstance.Name}");
                    }
                }

                if (currentAttachment.IsBundle)
                {
                    var proposedBundleMinLinks = (int?)exceptionEntry.Property("BundleMinLinks").CurrentValue;
                    if (currentAttachment.BundleMinLinks!= proposedBundleMinLinks)
                    {
                        ModelState.AddModelError("BundleMinLinks", $"Current value: {currentAttachment.BundleMinLinks}");
                    }

                    var proposedBundleMaxLinks = (int?)exceptionEntry.Property("BundleMaxLinks").CurrentValue;
                    if (currentAttachment.BundleMaxLinks != proposedBundleMaxLinks)
                    {
                        ModelState.AddModelError("BundleMaxLinks", $"Current value: {currentAttachment.BundleMaxLinks}");
                    }
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");

                // Re-throw to be caught by derived class

                throw ex;
            }

            catch (DbUpdateException  /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            finally
            {
                await PopulateAttachmentRolesDropDownList(currentAttachment.AttachmentRole.PortPoolID, 
                    currentAttachment.Device.DeviceRoleID, currentAttachment.AttachmentRoleID);
                await PopulateMtusDropDownList(currentAttachment.Device, updateModel.MtuID);
            }

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(currentAttachment);

            return View(Mapper.Map<AttachmentUpdateViewModel>(currentAttachment));
        }

        protected async Task<ActionResult> BaseEditInterface(int id)
        {
            var attachmentInterface = await InterfaceService.GetByIDAsync(id);
            if (attachmentInterface == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentInterface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<InterfaceViewModel>(attachmentInterface));
        }

        protected async Task<ActionResult> BaseEditInterface(Interface currentInterface, InterfaceViewModel updateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var interfaceUpdate = Mapper.Map<Interface>(updateModel);
                    await AttachmentValidator.ValidateChangesAsync(interfaceUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await InterfaceService.UpdateAsync(interfaceUpdate);
                        return RedirectToAction("GetAllInterfacesByAttachmentID", new
                        {
                            id = currentInterface.AttachmentID,
                            showWarningMessage = true
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpAddress = (string)exceptionEntry.Property("IpAddress").CurrentValue;
                if (currentInterface.IpAddress != proposedIpAddress)
                {
                    ModelState.AddModelError("IpAddress", $"Current value: {currentInterface.IpAddress}");
                }

                var proposedSubnetMask = (string)exceptionEntry.Property("SubnetMask").CurrentValue;
                if (currentInterface.SubnetMask != proposedSubnetMask)
                {
                    ModelState.AddModelError("SubnetMask", $"Current value: {currentInterface.SubnetMask}");
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
                var attachment = await AttachmentService.GetByIDAsync(currentInterface.AttachmentID);
                ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            }

            return View(Mapper.Map<InterfaceViewModel>(currentInterface));
        }

        protected async Task<ActionResult> BaseEditLogicalInterface(int logicalInterfaceID, int attachmentID)
        {
            var logicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceID);
            if (logicalInterface == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        protected async Task<ActionResult> BaseEditLogicalInterface(int attachmentID, LogicalInterface currentLogicalInterface, LogicalInterfaceViewModel updateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var logicalInterfaceUpdate = Mapper.Map<LogicalInterface>(updateModel);
                    await LogicalInterfaceService.UpdateAsync(logicalInterfaceUpdate);
                    return RedirectToAction("GetAllLogicalInterfacesByAttachmentID", new
                    {
                        attachmentID = attachmentID,
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
                var attachment = await AttachmentService.GetByIDAsync(attachmentID);
                ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            }

            return View(Mapper.Map<LogicalInterfaceViewModel>(currentLogicalInterface));
        }

        protected async Task<ActionResult> BaseEditPort(Port currentPort)
        {
            if (currentPort.InterfaceID == null)
            {
                return NotFound();
            }

            var attachmentInterface = await InterfaceService.GetByIDAsync(currentPort.InterfaceID.Value);
            var attachment = await AttachmentService.GetByIDAsync(attachmentInterface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Interface = Mapper.Map<InterfaceViewModel>(attachmentInterface);
            var device = attachmentInterface.Device;
            await PopulatePortsDropDownList(device.DeviceID, currentPort.PortPoolID, currentPort.ID);

            return View(Mapper.Map<AttachmentPortUpdateViewModel>(currentPort));
        }

        protected async Task<ActionResult> BaseEditPort(int id, AttachmentPortUpdateViewModel updateModel)
        {
            if (id != updateModel.CurrentPortID)
            {
                return NotFound();
            }

            var currentPort = await PortService.GetByIDAsync(updateModel.CurrentPortID);
            if (currentPort == null)
            {
                return NotFound();
            }

            var currentInterface = await InterfaceService.GetByIDAsync(updateModel.InterfaceID);
            if (currentInterface == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var portUpdate = Mapper.Map<AttachmentPortUpdate>(updateModel);
                    await AttachmentValidator.ValidateChangesAsync(portUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await AttachmentService.UpdateAttachmentPortAsync(portUpdate);
                        return RedirectToAction("GetAllPortsByInterfaceID", new
                        {
                            id = currentInterface.InterfaceID,
                            showWarningMessage = true
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedDeviceID = (int)exceptionEntry.Property("DeviceID").CurrentValue;
                if (currentPort.Device.DeviceID != proposedDeviceID)
                {
                    ModelState.AddModelError("DeviceID", $"Current value: {currentPort.Device.Name}");
                }

                var proposedPortID = (int)exceptionEntry.Property("PortID").CurrentValue;
                if (currentPort.ID != proposedPortID)
                {
                    ModelState.AddModelError("PortID", $"Current value: {currentPort.FullName}");
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

            var attachment = await AttachmentService.GetByIDAsync(currentInterface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Interface = Mapper.Map<InterfaceViewModel>(currentInterface);
            var device = currentInterface.Device;
            await PopulatePortsDropDownList(updateModel.DeviceID, currentPort.PortPoolID, updateModel.PortID);

            return View(Mapper.Map<AttachmentPortUpdateViewModel>(currentPort));
        }
    
        protected async Task<IActionResult> BaseDeleteAttachment(int id, AttachmentNavigationViewModel nav, bool? concurrencyError = false)
        {
            var item = await AttachmentService.GetByIDAsync(id);
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

            return View(Mapper.Map<AttachmentViewModel>(item));
        }

        protected async Task<IActionResult> BaseDeleteAttachment(AttachmentViewModel attachmentModel, 
            Attachment currentAttachment, AttachmentNavigationViewModel nav)
        {
            try
            {
                AttachmentValidator.ValidationDictionary.Clear();
                AttachmentValidator.ValidateDelete(currentAttachment);
                if (AttachmentValidator.ValidationDictionary.IsValid)
                {
                    // Copy over RowVersion for concurrency checking to work
                    currentAttachment.RowVersion = attachmentModel.RowVersion;

                    await AttachmentService.DeleteAsync(currentAttachment);
                    return RedirectToAction(nav.RedirectAction, nav);
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                nav.ConcurrencyError = true;
                nav.AttachmentID = attachmentModel.AttachmentID;

                return RedirectToAction("Delete", nav);
            }

            catch (Exception /** ex **/  )
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "Try again later or if the problem persists contact your system administrator.";
            }

            // Must re-fetch the current attachment if we get to here because the delete 
            // process modifies the in-memory attachment in such a way that the Name property is no
            // longer calculable

            currentAttachment = await AttachmentService.GetByIDAsync(currentAttachment.AttachmentID);
            return View(Mapper.Map<AttachmentViewModel>(currentAttachment));
        }

        protected async Task<IActionResult> BaseDeleteLogicalInterface(int id, AttachmentNavigationViewModel nav, bool? concurrencyError = false)
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

            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            
            return View(Mapper.Map<LogicalInterfaceViewModel>(item));
        }

        protected async Task<IActionResult> BaseDeleteLogicalInterface(LogicalInterfaceViewModel logicalInterfaceModel, 
            LogicalInterface currentLogicalInterface, AttachmentNavigationViewModel nav)
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

        protected async Task PopulatePortPoolsDropDownList(int portRoleID, object selectedPortPool = null)
        {
            var portPools = await PortPoolService.GetAllByPortRoleIDAsync(portRoleID);
            ViewBag.PortPoolID = new SelectList(Mapper.Map<List<PortPoolViewModel>>(portPools), "PortPoolID", "Name", selectedPortPool);
        }

        protected async Task PopulateAttachmentRolesDropDownList(int portPoolID, int? deviceRoleID = null, object selectedAttachmentRole = null)
        {
            var attachmentRoles = await AttachmentRoleService.GetAllByPortPoolIDAsync(portPoolID, deviceRoleID);
            ViewBag.AttachmentRoleID = new SelectList(Mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles),
                "AttachmentRoleID", "Name", selectedAttachmentRole);
        }

        protected async Task PopulateBandwidthsDropDownList(object selectedBandwidth = null)
        {
            var bandwidths = await AttachmentService.UnitOfWork.AttachmentBandwidthRepository.GetAsync();
            ViewBag.BandwidthID = new SelectList(Mapper.Map<List<AttachmentBandwidthViewModel>>(bandwidths.OrderBy(b => b.BandwidthGbps)), 
                "AttachmentBandwidthID", "BandwidthGbps", selectedBandwidth);
        }

        protected async Task PopulateDevicesByLocationDropDownList(int locationID, int? planeID = null, object selectedDevice = null)
        {
            var devices = await DeviceService.GetAllByLocationIDAsync(locationID, planeID);
            ViewBag.DeviceID = new SelectList(Mapper.Map<List<DeviceViewModel>>(devices), "DeviceID", "Name", selectedDevice);
        }

        protected async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await RegionService.GetAllAsync();
            ViewBag.RegionID = new SelectList(Mapper.Map<List<RegionViewModel>>(regions), "RegionID", "Name", selectedRegion);
        }

        protected async Task PopulateSubRegionsDropDownList(int regionID, object selectedSubRegion = null)
        {
            var subRegions = await SubRegionService.GetAllByRegionIDAsync(regionID);
            ViewBag.SubRegionID = new SelectList(Mapper.Map<List<SubRegionViewModel>>(subRegions), "SubRegionID", "Name", selectedSubRegion);
        }

        protected async Task PopulateLocationsDropDownList(int subRegionID, object selectedLocation = null)
        {
            var locations = await LocationService.GetAllBySubRegionIDAsync(subRegionID);
            ViewBag.LocationID = new SelectList(Mapper.Map<List<LocationViewModel>>(locations), "LocationID", "SiteName", selectedLocation);
        }

        protected async Task PopulatePortsDropDownList(int deviceID, int? portPoolID, int currentPortID)
        {
            var ports = await PortService.GetAllByDeviceIDAsync(deviceID, portPoolID);
            var result = ports.Where(x => x.PortStatus.PortStatusType == Models.PortStatusType.Free || x.ID == currentPortID);
            ViewBag.PortID = new SelectList(Mapper.Map<List<PortViewModel>>(result), "ID", "FullName", currentPortID);
        }

        protected async Task PopulateRoutingInstancesDropDownList(int deviceID, int? tenantID = null,
            bool? isInfrastructureVrf = null, bool? isTenantFacingVrf = null, object selectedRoutingInstance = null)
        {
            var routingInstances = await RoutingInstanceService.GetAllByDeviceIDAsync(deviceID: deviceID, tenantID: tenantID, 
                isTenantFacingVrf: isTenantFacingVrf, isInfrastructureVrf: isInfrastructureVrf);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }

        private async Task PopulateMtusDropDownList(Device device, object selectedMtu = null)
        {
            var mtus = await MtuService.GetAllAsync();
            mtus = mtus.Where(x => x.ValueIncludesLayer2Overhead == device.UseLayer2InterfaceMtu);
            ViewBag.MtuID = new SelectList(Mapper.Map<List<MtuViewModel>>(mtus), "MtuID", "MtuValue", selectedMtu);
        }

        /// <summary>
        /// Helper to populate the Logical Interface Tyes drop-down list
        /// </summary>
        /// <param name="selectedLogicalInterfaceType"></param>
        private void PopulateLogicalInterfaceTypesDropDownList(object selectedLogicalInterfaceType = null)
        {
            var logicalInterfaceTypesList = new List<SelectListItem>();
            foreach (var logicalInterfaceType in Enum.GetValues(typeof(Models.ViewModels.LogicalInterfaceType)))
            {
                logicalInterfaceTypesList.Add(new SelectListItem { Text = Enum.GetName(typeof(Models.ViewModels.LogicalInterfaceType), 
                    logicalInterfaceType), Value = logicalInterfaceType.ToString() });
            }

            ViewBag.LogicalInterfaceType = logicalInterfaceTypesList;
        }
    }
}