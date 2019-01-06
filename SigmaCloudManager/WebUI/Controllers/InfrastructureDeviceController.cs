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
using System.Linq;

namespace Mind.WebUI.Controllers
{
    public class InfrastructureDeviceController : BaseViewController
    {
        private readonly IInfrastructureDeviceService _infrastructureDeviceService;
        public InfrastructureDeviceController(IInfrastructureDeviceService infrastructureDeviceService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _infrastructureDeviceService = infrastructureDeviceService;
        }

        [HttpGet]
        public IActionResult GetLocationSelectorComponent(LocationSelectorViewModel locationSelectorModel)
        {
            return ViewComponent("LocationSelector", locationSelectorModel);
        }

        [HttpPost]
        public IActionResult GetPortForm([FromBody]PortRequestOrUpdateViewModel portModel)
        {
            if (portModel == null)
            {
                portModel = new PortRequestOrUpdateViewModel();
            }

            portModel.IsTenantDomainRole = false;
            portModel.IsProviderDomainRole = true;

            return ViewComponent("PortForm", new { portModel });
        }

        [HttpPost]
        public IActionResult GetPortsGridData([FromBody]PortsGridDataViewModel portsGridData)
        {
            return ViewComponent("PortsGridData", new { portsGridData });
        }

        [HttpGet]
        public IActionResult GetPortProfileComponent(string portRole)
        {
            return ViewComponent("PortProfile", new PortProfileComponentViewModel() { PortRole = portRole, IsProviderDomainRole = true });
        }

        [HttpGet]
        [ValidateInfrastructureDeviceExists]
        public async Task<IActionResult> Details(int? deviceId)
        {
            var item = await _infrastructureDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);
            item.Ports = item.Ports
                .OrderBy(
                    q =>
                    q.Type)
                .ThenBy(
                    q =>
                    q.Name)
                .ToList();

            return View(_mapper.Map<InfrastructureDeviceViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var infrastructureDevices = await _unitOfWork.DeviceRepository.GetAsync(
                    q =>
                    q.DeviceRole.IsProviderDomainRole, 
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            return View(_mapper.Map<List<InfrastructureDeviceViewModel>>(infrastructureDevices));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InfrastructureDeviceRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<InfrastructureDeviceRequest>(requestModel);
                    await _infrastructureDeviceService.AddAsync(request);

                    return RedirectToAction(nameof(GetAll));
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

            return View(requestModel);
        }

        [HttpGet]
        [ValidateInfrastructureDeviceExists]
        public async Task<ActionResult> Edit(int? deviceId)
        {
            var infrastructureDevice = await _infrastructureDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<InfrastructureDeviceUpdateViewModel>(infrastructureDevice));
        }

        [HttpPost]
        [ValidateInfrastructureDeviceExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? deviceId, InfrastructureDeviceUpdateViewModel update)
        {
            var infrastructureDevice = await _infrastructureDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (infrastructureDevice.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(infrastructureDevice.GetConcurrencyToken());
                }
                else
                {

                    var infrastructureDeviceUpdate = _mapper.Map<InfrastructureDeviceUpdate>(update);

                    try
                    {
                        await _infrastructureDeviceService.UpdateAsync(deviceId.Value, infrastructureDeviceUpdate);
                        return RedirectToAction(nameof(GetAll));
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

            return View(update);
        }

        [HttpGet]
        [ValidateInfrastructureDeviceExists]
        public async Task<IActionResult> Delete(int? deviceId, bool? concurrencyError = false)
        {
            var item = await _infrastructureDeviceService.GetByIDAsync(deviceId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

            return View(_mapper.Map<InfrastructureDeviceDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(InfrastructureDeviceDeleteViewModel model)
        {
            var infrastructureDevice = await _infrastructureDeviceService.GetByIDAsync(model.DeviceId.Value, deep: true, asTrackable: false);
            if (infrastructureDevice == null) return RedirectToAction(nameof(GetAll));

            if (infrastructureDevice.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    deviceId = infrastructureDevice.DeviceID,
                    concurrencyError = true
                });
            }

            try
            {
                await _infrastructureDeviceService.DeleteAsync(infrastructureDevice.DeviceID);
                return RedirectToAction(nameof(GetAll));
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            return View(model);
        }
    }
}