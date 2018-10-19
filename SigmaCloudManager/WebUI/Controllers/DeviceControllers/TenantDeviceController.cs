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
using Mind.Services;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class TenantDeviceController : BaseDeviceController
    {
        public TenantDeviceController(IDeviceService deviceService,
            IPlaneService planeService,
            IDeviceRoleService deviceRoleService,
            IDeviceModelService deviceModelService,
            IDeviceStatusService deviceStatusService,
            ILocationService locationService,
            ITenantService tenantService,
            IMapper mapper,
            IDeviceValidator deviceValidator) : base(deviceService, 
                deviceRoleService, 
                deviceModelService, 
                deviceStatusService, 
                mapper, 
                deviceValidator)
        {
            TenantService = tenantService;
        }

        private ITenantService TenantService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(DeviceNavigationViewModel nav)
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

            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            var devices = await DeviceService.GetAllByTenantIDAsync(nav.TenantID.Value, nav.SearchString);

            return View(Mapper.Map<List<DeviceViewModel>>(devices));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }
     
        [HttpGet]
        public async Task<IActionResult> Create(int? tenantID)
        {
            if (tenantID == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(tenantID.Value);
            if (tenant == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            await PopulateDeviceRolesDropDownList(isTenantDomainRole: true);
            
            return await base.BaseCreate();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,TenantID,DeviceRoleID,DeviceModelID,DeviceStatusID," +
            "UseLayer2InterfaceMtu")] TenantDeviceRequestViewModel device)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await DeviceService.AddAsync(Mapper.Map<Device>(device));
                    return RedirectToAction("GetAllByTenantID", new { tenantID = device.TenantID });
                }
                catch (DbUpdateException /** ex **/ )
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }

            await base.PopulateDeviceModelsDropDownList(device.DeviceModelID);
            await base.PopulateDeviceStatusesDropDownList(device.DeviceStatusID);
            await base.PopulateDeviceRolesDropDownList(isTenantDomainRole: true, selectedDeviceRole:device.DeviceRoleID);
            var tenant = await TenantService.GetByIDAsync(device.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

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

            var tenant = await TenantService.GetByIDAsync(device.TenantID.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            await PopulateDeviceRolesDropDownList(isTenantDomainRole: true);

            return await base.BaseEdit(device);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("DeviceID,Name,Description,TenantID,DeviceRoleID,DeviceModelID,DeviceStatusID," +
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
                nav.RedirectAction = "GetAllByTenantID";
                nav.TenantID = device.TenantID;
                return await base.BaseEdit(deviceUpdateModel, device, nav);
            }

            finally
            {
                var tenant = await TenantService.GetByIDAsync(device.TenantID.Value);
                ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
                await PopulateDeviceRolesDropDownList(isTenantDomainRole: true, selectedDeviceRole: device.DeviceRoleID);
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
                return RedirectToAction("GetAllByTenantID", new { tennatID = deviceModel.TenantID });
            }

            try
            {
                DeviceValidator.ValidationDictionary.Clear();
                DeviceValidator.ValidateDelete(device);
                if (DeviceValidator.ValidationDictionary.IsValid)
                {
                    await DeviceService.DeleteAsync(Mapper.Map<Device>(deviceModel));
                    return RedirectToAction("GetAllByTenantID", new { tenantID = device.TenantID });
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
    }
}
