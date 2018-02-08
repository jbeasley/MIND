using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Hubs;
using SCM.Validators;

namespace SCM.Controllers
{
    public class InfrastructureDeviceController : BaseDeviceController
    {
        public InfrastructureDeviceController(IDeviceService deviceService,
            IPlaneService planeService,
            IDeviceRoleService deviceRoleService,
            IDeviceModelService deviceModelService,
            IDeviceStatusService deviceStatusService,
            ILocationService locationService,
            IMapper mapper,
            IDeviceValidator deviceValidator) : base(deviceService, 
                deviceRoleService, 
                deviceModelService, 
                deviceStatusService, 
                mapper, 
                deviceValidator)
        {
            PlaneService = planeService;
            LocationService = locationService;
        }

        private IPlaneService PlaneService { get; }
        private ILocationService LocationService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll(DeviceNavigationViewModel nav)
        {
            var devices = await DeviceService.GetAllAsync(isProviderDomainRole: true,  searchString: nav.SearchString);

            ViewData["SuccessMessage"] = FormatAsHtmlList(devices
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            ViewData["NetworkWarningMessage"] = FormatAsHtmlList(devices
                .Where(x => x.RequiresSync && x.ShowRequiresSyncAlert)
                .Select(x => $"{x.Name} requires sync with the network.").ToList());

            return View(Mapper.Map<List<DeviceViewModel>>(devices));
        }

        [HttpPost]
        public async Task<IActionResult> ClearCreatedAlerts(int id)
        {
            var devices = await DeviceService.GetAllAsync(isProviderDomainRole: true, created: true, showCreatedAlert: true);
            foreach (var device in devices)
            {
                device.ShowCreatedAlert = false;
            }

            try
            {
                await DeviceService.UpdateAsync(devices);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAll", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> ClearRequiresSyncAlerts(int id)
        {
            var devices = await DeviceService.GetAllAsync(isProviderDomainRole: true, requiresSync: true, showRequiresSyncAlert: true);
            foreach (var device in devices)
            {
                device.ShowRequiresSyncAlert = false;
            }

            try
            {
                await DeviceService.UpdateAsync(devices);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAll", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }
     
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulatePlanesDropDownList();
            await PopulateLocationsDropDownList();
            await PopulateDeviceRolesDropDownList(isProviderDomainRole: true);

            return await base.BaseCreate();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,PlaneID,DeviceRoleID,DeviceModelID,DeviceStatusID," +
            "UseLayer2InterfaceMtu,LocationID")] InfrastructureDeviceRequestViewModel device)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await DeviceService.AddAsync(Mapper.Map<Device>(device));
                    return RedirectToAction("GetAll");
                }
                catch (DbUpdateException /** ex **/ )
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }

            await PopulateLocationsDropDownList(device.LocationID);
            await PopulatePlanesDropDownList(device.PlaneID);
            await base.PopulateDeviceModelsDropDownList(device.DeviceModelID);
            await base.PopulateDeviceStatusesDropDownList(device.DeviceStatusID);
            await base.PopulateDeviceRolesDropDownList(isProviderDomainRole: true, selectedDeviceRole:device.DeviceRoleID);

            return View(device);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await DeviceService.GetByIDAsync(id.Value);
            if (device == null)
            {
                return NotFound();
            }

            await PopulateDeviceRolesDropDownList(isProviderDomainRole: true);
            return await base.BaseEdit(device);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("DeviceID,Name,Description,LocationID,PlaneID,DeviceRoleID,DeviceModelID,DeviceStatusID," +
            "UseLayer2InterfaceMtu,RowVersion")] DeviceUpdateViewModel deviceUpdateModel, DeviceNavigationViewModel nav)
        {
            if (id != deviceUpdateModel.DeviceID)
            {
                return NotFound();
            }

            var device = await DeviceService.GetByIDAsync(id);
            if (device == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Device was deleted by another user.");
            }

            try
            {
                nav.RedirectAction = "GetAll";
                return await base.BaseEdit(deviceUpdateModel, device, nav);
            }

            finally
            {
                await PopulateDeviceRolesDropDownList(isProviderDomainRole: true, selectedDeviceRole: device.DeviceRoleID);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, DeviceNavigationViewModel nav, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await base.BaseDelete(id.Value, nav, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeviceViewModel deviceModel)
        {
            var device = await DeviceService.GetByIDAsync(deviceModel.DeviceID);
            if (device == null)
            {
                return RedirectToAction("GetAll");
            }

            try
            {
                DeviceValidator.ValidationDictionary.Clear();
                DeviceValidator.ValidateDelete(device);
                if (DeviceValidator.ValidationDictionary.IsValid)
                {
                    if (!device.Created)
                    {
                        // Device has been sync'd with the network (indicated by the 'Created = false' check.
                        // Therefore, delete data on the Device from the network first.

                        await DeviceService.DeleteFromNetworkAsync(device);
                    }

                    await DeviceService.DeleteAsync(Mapper.Map<Device>(deviceModel));
                    return RedirectToAction("GetAll");
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { concurrencyError = true, id = deviceModel.DeviceID });
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            return View(Mapper.Map<DeviceViewModel>(device));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromNetwork(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await DeviceService.GetByIDAsync(id.Value);
            if (device == null)
            {
                ViewData["DeviceDeletedMessage"] = "The Device has been deleted by another user. Return to the list.";
                return View("DeviceDeleted");
            }

            try
            {
                await DeviceService.DeleteFromNetworkAsync(device);
                ViewData["SuccessMessage"] = "The Device has been deleted from the network.";
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            device.RequiresSync = true;
            return View("Delete", Mapper.Map<DeviceViewModel>(device));
        }

        private async Task PopulatePlanesDropDownList(object selectedPlane = null)
        {
            var planes = await PlaneService.GetAllAsync();
            ViewBag.PlaneID = new SelectList(Mapper.Map<List<PlaneViewModel>>(planes), "PlaneID", "Name", selectedPlane);
        }

        private async Task PopulateLocationsDropDownList(object selectedLocation = null)
        {
            var locations = await LocationService.GetAllAsync();
            ViewBag.LocationID = new SelectList(Mapper.Map<List<LocationViewModel>>(locations), "LocationID", "SiteName", selectedLocation);
        }
    }
}
