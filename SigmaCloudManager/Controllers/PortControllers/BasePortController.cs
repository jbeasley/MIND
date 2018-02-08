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

namespace SCM.Controllers
{
    public class BasePortController : BaseViewController
    {
        public BasePortController(IPortService portService,
            IPortConnectorService portConnectorService,
            IPortRoleService portRoleService,
            IPortPoolService portPoolService,
            IPortStatusService portStatusService,
            IPortSfpService portSfpService,
            IPortBandwidthService portBandwidthService,
            ITenantService tenantService,
            IDeviceService deviceService,
            IPortValidator portValidator, IMapper mapper)
        {
            PortService = portService;
            PortConnectorService = portConnectorService;
            PortRoleService = portRoleService;
            PortPoolService = portPoolService;
            PortSfpService = portSfpService;
            PortStatusService = portStatusService;
            PortBandwidthService = portBandwidthService;
            TenantService = tenantService;
            DeviceService = deviceService;
            Mapper = mapper;
            PortValidator = portValidator;
            this.Validator = portValidator;
        }

        protected IPortService PortService { get; set; }
        protected IPortConnectorService PortConnectorService { get; set; }
        protected IPortSfpService PortSfpService { get; set; }
        protected IPortStatusService PortStatusService { get; set; }
        protected IPortRoleService PortRoleService { get; set; }
        protected IPortPoolService PortPoolService { get; set; }
        protected IDeviceService DeviceService { get; set; }
        protected IPortBandwidthService PortBandwidthService { get; set; }
        protected ITenantService TenantService { get; set; }
        protected IMapper Mapper { get; set; }
        protected IPortValidator PortValidator { get; set; }

        [HttpGet]
        public async Task<PartialViewResult> PortPools(int portRoleID)
        {
            var portPools = await PortPoolService.GetAllByPortRoleIDAsync(portRoleID);
            return PartialView(Mapper.Map<List<PortPoolViewModel>>(portPools));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByDeviceID(int id)
        {
            var device = await DeviceService.GetByIDAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            var ports = await PortService.GetAllByDeviceIDAsync(id);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<List<PortViewModel>>(ports));
        }

        protected async Task<IActionResult> BaseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await PortService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var device = await DeviceService.GetByIDAsync(item.DeviceID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(item));
        }

        protected async Task<IActionResult> BaseCreate(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await DeviceService.GetByIDAsync(id.Value);
            await PopulatePortBandwidthsDropDownList();
            await PopulatePortConnectorsDropDownList();
            await PopulatePortStatusesDropDownList();
            await PopulatePortSfpsDropDownList();
            await PopulatePortRolesDropDownList(device.DeviceRoleID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View();
        }

        protected async Task<IActionResult> BaseCreate(PortViewModel portModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var port = Mapper.Map<Port>(portModel);
                    await PortValidator.ValidateNewAsync(port);
                    if (PortValidator.ValidationDictionary.IsValid)
                    {
                        await PortService.AddAsync(port);
                        return RedirectToAction("GetAllByDeviceID", new { id = port.DeviceID });
                    }
                }
            }
            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var device = await DeviceService.GetByIDAsync(portModel.DeviceID);
            await PopulatePortBandwidthsDropDownList();
            await PopulatePortConnectorsDropDownList();
            await PopulatePortStatusesDropDownList();
            await PopulatePortSfpsDropDownList();
            await PopulatePortRolesDropDownList(device.DeviceRoleID);
            await PopulatePortPoolsDropDownList(portModel.PortRoleID, portModel.PortPoolID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(portModel);
        }

        protected async Task<ActionResult> BaseEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await PortService.GetByIDAsync(id.Value);
            if (port == null)
            {
                return NotFound();
            }

            await PopulatePortBandwidthsDropDownList(port.PortBandwidthID);
            await PopulatePortConnectorsDropDownList(port.PortConnectorID);
            await PopulatePortStatusesDropDownList(port.PortStatusID);
            await PopulatePortSfpsDropDownList(port.PortSfpID);
            await PopulatePortRolesDropDownList(port.Device.DeviceRoleID, port.PortPool.PortRoleID);
            await PopulatePortPoolsDropDownList(port.PortPool.PortRoleID, port.PortPoolID);

            var device = await DeviceService.GetByIDAsync(port.DeviceID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(port));
        }

        protected async Task<ActionResult> BaseEdit(int id, PortViewModel portModel)
        {
            if (id != portModel.ID)
            {
                return NotFound();
            }

            var currentPort = await PortService.GetByIDAsync(id);
            if (currentPort == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Port was deleted by another user.");
            }

            var port = Mapper.Map<Port>(portModel);
            try
            {
                await PortValidator.ValidateChangesAsync(port);
                if (PortValidator.ValidationDictionary.IsValid)
                {
                    await PortService.UpdateAsync(port);
                    return RedirectToAction("GetAllByDeviceID", new { id = port.DeviceID });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (port.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {port.Name}");
                }

                var proposedType = (string)exceptionEntry.Property("Type").CurrentValue;
                if (port.Type != proposedType)
                {
                    ModelState.AddModelError("Type", $"Current value: {port.Type}");
                }

                var proposedPortBandwidthID = (int)exceptionEntry.Property("PortBandwidthID").CurrentValue;
                if (port.PortBandwidthID != proposedPortBandwidthID)
                {
                    ModelState.AddModelError("PortBandwidthID", $"Current value: {port.PortBandwidth.BandwidthGbps}");
                }

                var proposedPortConnectorID = (int)exceptionEntry.Property("PortConnectorID").CurrentValue;
                if (port.PortConnectorID != proposedPortConnectorID)
                {
                    ModelState.AddModelError("PortConnectorID", $"Current value: {port.PortConnector.Name}");
                }

                var proposedPortSfpID = (int)exceptionEntry.Property("PortSfpID").CurrentValue;
                if (port.PortSfpID != proposedPortSfpID)
                {
                    ModelState.AddModelError("PortSfpID", $"Current value: {port.PortSfp.Name}");
                }

                var proposedPortStatusID = (int)exceptionEntry.Property("PortStatusID").CurrentValue;
                if (port.PortStatusID != proposedPortStatusID)
                {
                    ModelState.AddModelError("PortStatusID", $"Current value: {port.PortStatus.Name}");
                }

                var proposedPortPoolID = (int)exceptionEntry.Property("PortPoolID").CurrentValue;
                if (port.PortPoolID != proposedPortPoolID)
                {
                    ModelState.AddModelError("PortPoolID", $"Current value: {port.PortPool.Name}");
                }

                var proposedTenantID = (int)exceptionEntry.Property("TenantID").CurrentValue;
                if (port.TenantID != proposedTenantID)
                {
                    ModelState.AddModelError("TenantID", $"Current value: {port.Tenant.Name}");
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

            await PopulatePortBandwidthsDropDownList(port.PortBandwidthID);
            await PopulatePortConnectorsDropDownList(port.PortConnectorID);
            await PopulatePortStatusesDropDownList(port.PortStatusID);
            await PopulatePortSfpsDropDownList(port.PortSfpID);
            var portPool = await PortPoolService.GetByIDAsync(port.PortPoolID);
            await PopulatePortRolesDropDownList(currentPort.Device.DeviceRoleID, portPool.PortRoleID);
            await PopulatePortPoolsDropDownList(currentPort.PortPool.PortRoleID, port.PortPoolID);
            var device = await DeviceService.GetByIDAsync(port.DeviceID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(port));
        }

        protected async Task<IActionResult> BaseDelete(int? id, int? deviceID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await PortService.GetByIDAsync(id.Value);
            if (port == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByDeviceID", new { id = deviceID });
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
                    + "click the Back to Device hyperlink.";
            }

            var device = await DeviceService.GetByIDAsync(port.DeviceID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(port));
        }

        protected async Task<IActionResult> BaseDelete(PortViewModel portModel)
        {
            try
            {
                var port = await PortService.GetByIDAsync(portModel.ID);
                if (port == null)
                {
                    return RedirectToAction("GetAllByDeviceID", new
                    {
                        id = portModel.DeviceID
                    });
                }

                this.Validator.ValidationDictionary.Clear();
                await PortValidator.ValidateDeleteAsync(port);
                if (!PortValidator.ValidationDictionary.IsValid)
                {
                    ViewData["ErrorMessage"] = "Failed to delete the Port.";
                    return View(Mapper.Map<PortViewModel>(port));
                }

                await PortService.DeleteAsync(Mapper.Map<Port>(portModel));
                return RedirectToAction("GetAllByDeviceID", new { id = port.DeviceID });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = portModel.ID,
                    deviceID = portModel.DeviceID
                });
            }
        }

        private async Task PopulatePortBandwidthsDropDownList(object selectedPortBandwidth = null)
        {
            var portBandwidths = await PortBandwidthService.GetAllAsync();
            ViewBag.PortBandwidthID = new SelectList(Mapper.Map<List<PortBandwidthViewModel>>(portBandwidths.OrderBy(x => x.BandwidthGbps)), 
                "PortBandwidthID", "BandwidthGbps", selectedPortBandwidth);
        }

        private async Task PopulatePortConnectorsDropDownList(object selectedPortConnector = null)
        {
            var portConnectors = await PortConnectorService.GetAllAsync();
            ViewBag.PortConnectorID = new SelectList(Mapper.Map<List<PortConnectorViewModel>>(portConnectors),
                "PortConnectorID", "Name", selectedPortConnector);
        }

        private async Task PopulatePortSfpsDropDownList(object selectedPortSfp = null)
        {
            var portSfps = await PortSfpService.GetAllAsync();
            ViewBag.PortSfpID = new SelectList(Mapper.Map<List<PortSfpViewModel>>(portSfps),
                "PortSfpID", "Name", selectedPortSfp);
        }

        private async Task PopulatePortStatusesDropDownList(object selectedPortStatus = null)
        {
            var portStatuses = await PortStatusService.GetAllAsync();
            ViewBag.PortStatusID = new SelectList(Mapper.Map<List<PortStatusViewModel>>(portStatuses),
                "PortStatusID", "Name", selectedPortStatus);
        }

        private async Task PopulatePortRolesDropDownList(int deviceRoleID, object selectedPortRole = null)
        {
            var portRoles = await PortRoleService.GetAllByDeviceRoleIDAsync(deviceRoleID);
            ViewBag.PortRoleID = new SelectList(Mapper.Map<List<PortRoleViewModel>>(portRoles),
                "PortRoleID", "Name", selectedPortRole);
        }

        private async Task PopulatePortPoolsDropDownList(int portRoleID, object selectedPortPool = null)
        {
            var portPools = await PortPoolService.GetAllByPortRoleIDAsync(portRoleID);
            ViewBag.PortPoolID = new SelectList(Mapper.Map<List<PortPoolViewModel>>(portPools),
                "PortPoolID", "Name", selectedPortPool);
        }
    }
}
