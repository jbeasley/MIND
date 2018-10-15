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
    public class BaseDeviceController : BaseViewController
    {
        public BaseDeviceController(IDeviceService deviceService,
            IDeviceRoleService deviceRoleService,
            IDeviceModelService deviceModelService,
            IDeviceStatusService deviceStatusService,
            IMapper mapper,
            IDeviceValidator deviceValidator)
        {
            DeviceService = deviceService;
            DeviceRoleService = deviceRoleService;
            DeviceModelService = deviceModelService;
            DeviceStatusService = deviceStatusService;

            DeviceValidator = deviceValidator;
            this.Validator = deviceValidator;

            Mapper = mapper;
        }

        protected IDeviceService DeviceService { get; }
        protected IDeviceRoleService DeviceRoleService { get; }
        protected IDeviceModelService DeviceModelService { get; }
        protected IDeviceStatusService DeviceStatusService { get; }
        protected IMapper Mapper { get; }
        protected IDeviceValidator DeviceValidator { get; }

        protected async Task<IActionResult> BaseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await DeviceService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<DeviceViewModel>(item));
        }
     
        protected async Task<IActionResult> BaseCreate()
        {
            await PopulateDeviceModelsDropDownList();
            await PopulateDeviceStatusesDropDownList();

            return View();
        }

        protected async Task<ActionResult> BaseEdit(Device device)
        {
            await PopulateDeviceModelsDropDownList(device.DeviceModelID);
            await PopulateDeviceStatusesDropDownList(device.DeviceStatusID);

            return View(Mapper.Map<DeviceUpdateViewModel>(device));
        }

        protected async Task<ActionResult> BaseEdit(DeviceUpdateViewModel deviceUpdateModel, Device currentDevice, DeviceNavigationViewModel nav)
        {
            try
            {
                // Map changes into the current device

                Mapper.Map<DeviceUpdateViewModel, Device>(deviceUpdateModel, currentDevice);

                await DeviceValidator.ValidateChangesAsync(currentDevice);
                if (DeviceValidator.ValidationDictionary.IsValid)
                {
                    await DeviceService.UpdateAsync(currentDevice);
                    return RedirectToAction(nav.RedirectAction, nav);
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (currentDevice.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {currentDevice.Name}");
                }

                var proposedDescription = (string)exceptionEntry.Property("Description").CurrentValue;
                if (currentDevice.Description != proposedDescription)
                {
                    ModelState.AddModelError("Description", $"Current value: {currentDevice.Description}");
                }

                var proposedDeviceRoleID = (int)exceptionEntry.Property("DeviceRoleID").CurrentValue;
                if (currentDevice.DeviceRoleID != proposedDeviceRoleID)
                {
                    ModelState.AddModelError("DeviceRoleID", $"Current value: {currentDevice.DeviceRole.Name}");
                }

                var proposedDeviceModelID = (int)exceptionEntry.Property("DeviceModelID").CurrentValue;
                if (currentDevice.DeviceModelID != proposedDeviceModelID)
                {
                    ModelState.AddModelError("DeviceModelID", $"Current value: {currentDevice.DeviceModel.Name}");
                }

                var proposedDeviceStatusID = (int)exceptionEntry.Property("DeviceStatusID").CurrentValue;
                if (currentDevice.DeviceStatusID != proposedDeviceStatusID)
                {
                    ModelState.AddModelError("DeviceStatusID", $"Current value: {currentDevice.DeviceStatus.Name}");
                }

                var proposedUseLayer2InterfaceMtu = (bool)exceptionEntry.Property("UseLayer2InterfaceMtu").CurrentValue;
                if (currentDevice.UseLayer2InterfaceMtu != proposedUseLayer2InterfaceMtu)
                {
                    ModelState.AddModelError("UseLayer2InterfaceMtu", $"Current value: {currentDevice.UseLayer2InterfaceMtu}");
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
                await PopulateDeviceModelsDropDownList(currentDevice.DeviceModelID);
                await PopulateDeviceStatusesDropDownList(currentDevice.DeviceStatusID);
            }

            return View(Mapper.Map<DeviceUpdateViewModel>(currentDevice));
        }

        protected async Task<IActionResult> BaseDelete(int id, DeviceNavigationViewModel nav, bool? concurrencyError = false)
        { 

            var device = await DeviceService.GetByIDAsync(id);
            if (device == null)
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

            return View(Mapper.Map<DeviceViewModel>(device));
        }

        protected async Task<IActionResult> BaseDelete(DeviceViewModel deviceModel, DeviceNavigationViewModel nav)
        {
            var device = await DeviceService.GetByIDAsync(deviceModel.DeviceID);
            if (device == null)
            {
                return RedirectToAction(nav.RedirectAction, nav);
            }

            try
            {
                DeviceValidator.ValidationDictionary.Clear();
                DeviceValidator.ValidateDelete(device);
                if (DeviceValidator.ValidationDictionary.IsValid)
                {
                    await DeviceService.DeleteAsync(device);
                    return RedirectToAction(nav.RedirectAction, nav);
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)

                nav.ConcurrencyError = true;
                nav.DeviceID = deviceModel.DeviceID;

                return RedirectToAction("Delete", nav);
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            return View(Mapper.Map<DeviceViewModel>(device));
        }

        protected async Task PopulateDeviceRolesDropDownList(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null, 
            object selectedDeviceRole = null)
        {
            var deviceRoles = await DeviceRoleService.GetAllAsync(isProviderDomainRole: isProviderDomainRole, isTenantDomainRole: isTenantDomainRole);
            ViewBag.DeviceRoleID = new SelectList(Mapper.Map<List<DeviceRoleViewModel>>(deviceRoles), "DeviceRoleID", "Name", selectedDeviceRole);
        }

        protected async Task PopulateDeviceModelsDropDownList(object selectedDeviceModel = null)
        {
            var deviceModels = await DeviceModelService.GetAllAsync();
            ViewBag.DeviceModelID = new SelectList(Mapper.Map<List<DeviceModelViewModel>>(deviceModels), "DeviceModelID", "Name", selectedDeviceModel);
        }

        protected async Task PopulateDeviceStatusesDropDownList(object selectedDeviceStatus = null)
        {
            var deviceStatuses = await DeviceStatusService.GetAllAsync();
            ViewBag.DeviceStatusID = new SelectList(Mapper.Map<List<DeviceStatusViewModel>>(deviceStatuses), "DeviceStatusID", "Name", selectedDeviceStatus);
        }
    }
}
