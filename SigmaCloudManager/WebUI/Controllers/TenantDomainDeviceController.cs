using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Models;
using SCM.Controllers;
using Mind.Services;
using SCM.Data;
using Mind.Models;
using Mind.Builders;
using Mind.WebUI.Attributes;
using Mind.WebUI.Models;
using Mind.Models.RequestModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mind.WebUI.Controllers
{
    public class TenantDomainDeviceController : BaseViewController
    {
        private readonly ITenantDomainDeviceService _tenantDomainDeviceService;
        public TenantDomainDeviceController(ITenantDomainDeviceService tenantDomainDeviceService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _tenantDomainDeviceService = tenantDomainDeviceService;
        }

        [HttpGet]
        public IActionResult GetLocationSelectorComponent(LocationSelectorViewModel locationSelectorModel)
        {
            return ViewComponent("LocationSelector", locationSelectorModel);
        }

        [HttpGet]
        public IActionResult GetPortsComponent(List<PortRequestOrUpdateViewModel> portRequests)
        {
            return ViewComponent("TenantDomainDevicePorts", new
            {
                portRequests
            });
        }

        [HttpPost]
        public IActionResult GetPortsGridData([FromBody]List<PortRequestOrUpdateViewModel> ports)
        {
            return ViewComponent("PortsGridData", new { ports });
        }

        [HttpGet]
        [ValidateTenantDomainDeviceExists]
        public async Task<IActionResult> Details(int? tenantDomainDeviceId)
        {
            var item = await _tenantDomainDeviceService.GetByIDAsync(tenantDomainDeviceId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<TenantDomainDeviceViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> GetAllByTenantID(int? tenantId)
        {
            var tenantDomainDevices = await _unitOfWork.DeviceRepository.GetAsync(
                    q =>
                    q.TenantID == tenantId.Value,
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<List<TenantDomainDeviceViewModel>>(tenantDomainDevices));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, TenantDomainDeviceRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<TenantDomainDeviceRequest>(requestModel);

                    var attachment = await _tenantDomainDeviceService.AddAsync(tenantId.Value, request);
                    return RedirectToAction(nameof(GetAllByTenantID), new { tenantId });
                }

                catch (BuilderBadArgumentsException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (BuilderUnableToCompleteException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (IllegalStateException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (DbUpdateException)
                {
                    ModelState.AddDatabaseUpdateExceptionMessage();
                }
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(requestModel);
        }

        [HttpGet]
        [ValidateTenantDomainDeviceExists]
        public async Task<ActionResult> Edit(int? deviceId)
        {
            var tenantDomainDevice = await _tenantDomainDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantDomainDevice.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantDomainDeviceUpdateViewModel>(tenantDomainDevice));
        }

        [HttpPost]
        [ValidateTenantDomainDeviceExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? deviceId, TenantDomainDeviceUpdateViewModel update)
        {
            var tenantDomainDevice = await _tenantDomainDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (tenantDomainDevice.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(tenantDomainDevice.GetConcurrencyToken());
                }
                else
                {

                    var tenantDomainDeviceUpdate = _mapper.Map<TenantDomainDeviceUpdate>(update);

                    try
                    {
                        await _tenantDomainDeviceService.UpdateAsync(deviceId.Value, tenantDomainDeviceUpdate);
                        return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = tenantDomainDevice.TenantID });
                    }

                    catch (BuilderBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (BuilderUnableToCompleteException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (IllegalStateException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (IllegalDeleteAttemptException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (DbUpdateException)
                    {
                        ModelState.AddDatabaseUpdateExceptionMessage();
                    }
                }
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantDomainDevice.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantDomainDeviceUpdateViewModel>(tenantDomainDevice));
        }

        [HttpGet]
        [ValidateTenantDomainDeviceExists]
        public async Task<IActionResult> Delete(int? deviceId, bool? concurrencyError = false)
        {
            var item = await _tenantDomainDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantDomainDeviceDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantDomainDeviceDeleteViewModel model)
        {
            var tenantDomainDevice = await _tenantDomainDeviceService.GetByIDAsync(model.DeviceId.Value, deep: true, asTrackable: false);
            if (tenantDomainDevice == null) return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = model.TenantId });

            if (tenantDomainDevice.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    deviceId = tenantDomainDevice.DeviceID,
                    concurrencyError = true
                });
            }

            try
            {
                await _tenantDomainDeviceService.DeleteAsync(tenantDomainDevice.DeviceID);
                return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = tenantDomainDevice.TenantID });
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantDomainDevice.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantDomainDeviceViewModel>(tenantDomainDevice));
        }
    }
}