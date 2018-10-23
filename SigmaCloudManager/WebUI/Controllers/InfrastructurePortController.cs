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
using Mind.Services;
using Mind.WebUI.Models;
using SCM.Controllers;
using SCM.Data;
using Mind.WebUI.Attributes;
using Mind.Models.RequestModels;
using Mind.Builders;
using Mind.Models;

namespace Mind.WebUI.Controllers
{
    public class InfrastructurePortController : BaseViewController
    {
        private readonly IInfrastructurePortService _portService;
        public InfrastructurePortController(IInfrastructurePortService portService, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _portService = portService;
        }

        [HttpGet]
        public async Task<PartialViewResult> PortPools(int? portRoleId)
        {
            var portPools = await _unitOfWork.PortPoolRepository.GetAsync(
                                    q =>
                                    q.PortRoleID == portRoleId,
                                    AsTrackable: false);

            return PartialView(Mapper.Map<List<PortPoolViewModel>>(portPools));
        }

        [HttpGet]
        [ValidateInfrastructureDeviceExists]
        public async Task<IActionResult> GetAllByDeviceID(int? deviceId)
        {
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(deviceId);
            var ports = await _portService.GetAllByDeviceIDAsync(deviceId.Value);
            ViewBag.Device = Mapper.Map<InfrastructureDeviceViewModel>(device);

            return View(Mapper.Map<List<PortViewModel>>(ports));
        }

        [HttpGet]
        [ValidateInfrastructurePortExists]
        public async Task<IActionResult> Details(int? portId)
        {
            var port = await _portService.GetByIDAsync(portId.Value);
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(port.DeviceID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(port));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? deviceId)
        {
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(deviceId);
            await PopulatePortBandwidthsDropDownList();
            await PopulatePortConnectorsDropDownList();
            await PopulatePortStatusesDropDownList();
            await PopulatePortSfpsDropDownList();
            await PopulatePortRolesDropDownList(device.DeviceRoleID);
            ViewBag.Device = Mapper.Map<InfrastructureDeviceViewModel>(device);

            return View();
        }

        [HttpPost]
        [ValidateInfrastructureDeviceExists]
        public async Task<IActionResult> Create(int deviceId, PortRequestViewModel portModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = Mapper.Map<PortRequest>(portModel);
                    var port = await _portService.AddAsync(deviceId, request);
                    return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = port.DeviceID });
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

            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(deviceId);
            ViewBag.Device = Mapper.Map<InfrastructureDeviceViewModel>(device);
            await PopulatePortBandwidthsDropDownList(portModel.PortBandwidthGbps);
            await PopulatePortConnectorsDropDownList(portModel.PortConnector);
            await PopulatePortStatusesDropDownList(portModel.PortStatus);
            await PopulatePortSfpsDropDownList(portModel.PortSfp);
            await PopulatePortRolesDropDownList(device.DeviceRoleID);
            await PopulatePortPoolsDropDownList(portModel.PortRole, portModel.PortPool);

            return View(portModel);
        }

        [HttpGet]
        [ValidateInfrastructurePortExists]
        public async Task<ActionResult> Edit(int? portId)
        {
            var port = (from result in await _unitOfWork.PortRepository.GetAsync(
                    q =>
                        q.ID == portId,
                        query: q => q.Include(x => x.PortPool.PortRole)
                                 .Include(x => x.PortBandwidth)
                                 .Include(x => x.PortConnector)
                                 .Include(x => x.PortStatus)
                                 .Include(x => x.PortSfp)
                                 .Include(x => x.Device),
                        AsTrackable: false)
                        select result)
                        .Single();

                await PopulatePortBandwidthsDropDownList(port.PortBandwidth.BandwidthGbps);
            await PopulatePortConnectorsDropDownList(port.PortConnector.Name);
            await PopulatePortStatusesDropDownList(port.PortStatus.Name);
            await PopulatePortSfpsDropDownList(port.PortSfp.Name);
            await PopulatePortRolesDropDownList(port.Device.DeviceRoleID, port.PortPool.PortRole.Name);
            await PopulatePortPoolsDropDownList(port.PortPool.PortRole.Name, port.PortPool.Name);
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(port.DeviceID);
            ViewBag.Device = Mapper.Map<InfrastructureDeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(port));
        }

        [HttpPost]
        [ValidateInfrastructurePortExists]
        public async Task<ActionResult> Edit(int? portId, PortUpdateViewModel updateModel)
        {
            var port = (from result in await _unitOfWork.PortRepository.GetAsync(
                    q =>
                        q.ID == portId,
                        query: q => 
                                q.Include(x => x.PortPool.PortRole)
                                 .Include(x => x.PortBandwidth)
                                 .Include(x => x.PortConnector)
                                 .Include(x => x.PortStatus)
                                 .Include(x => x.PortSfp)
                                 .Include(x => x.Device),
                        AsTrackable: false)
                        select result)
                        .Single();

            if (ModelState.IsValid)
            {

                if (port.HasPreconditionFailed(Request, updateModel.RowVersion.ToString()))
                {
                    ModelState.PopulateFromModel(port);
                    return View(Mapper.Map<PortUpdateViewModel>(port));
                }

                var update = Mapper.Map<PortUpdate>(updateModel);

                try
                {
                    await _portService.UpdateAsync(portId.Value, update);
                    return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = port.DeviceID });
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

            await PopulatePortBandwidthsDropDownList(port.PortBandwidth.BandwidthGbps);
            await PopulatePortConnectorsDropDownList(port.PortConnector.Name);
            await PopulatePortStatusesDropDownList(port.PortStatus.Name);
            await PopulatePortSfpsDropDownList(port.PortSfp.Name);
            await PopulatePortRolesDropDownList(port.Device.DeviceRoleID, port.PortPool.PortRole.Name);
            await PopulatePortPoolsDropDownList(port.PortPool.PortRole.Name, port.PortPoolID);
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(port.DeviceID);
            ViewBag.Device = Mapper.Map<DeviceViewModel>(device);

            return View(Mapper.Map<PortViewModel>(port));
        }

        [HttpGet]
        [ValidateInfrastructurePortExists]
        public async Task<IActionResult> Delete(int? portId, bool? concurrencyError = false)
        {
            var item = await _portService.GetByIDAsync(portId.Value);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

            return View(Mapper.Map<ProviderDomainAttachmentViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PortViewModel model)
        {
            var port = await _portService.GetByIDAsync(model.PortId.Value);
            if (port == null) return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = model.DeviceId });

            if (port.HasPreconditionFailed(Request, model.RowVersion.ToString()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    portId = port.ID,
                    concurrencyError = true
                });
            }

            try
            {
                await _portService.DeleteAsync(port.ID);
                return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = port.DeviceID });
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            return View(Mapper.Map<PortViewModel>(port));
        }

        private async Task PopulatePortBandwidthsDropDownList(object selectedPortBandwidth = null)
        {
            var portBandwidths = await _unitOfWork.PortBandwidthRepository.GetAsync();
            ViewBag.PortBandwidth = new SelectList(Mapper.Map<List<PortBandwidthViewModel>>(portBandwidths.OrderBy(x => x.BandwidthGbps)),
                "BandwidthGbps", "BandwidthGbps", selectedPortBandwidth);
        }

        private async Task PopulatePortConnectorsDropDownList(object selectedPortConnector = null)
        {
            var portConnectors = await _unitOfWork.PortConnectorRepository.GetAsync();
            ViewBag.PortConnector = new SelectList(Mapper.Map<List<PortConnectorViewModel>>(portConnectors),
                "Name", "Name", selectedPortConnector);
        }

        private async Task PopulatePortSfpsDropDownList(object selectedPortSfp = null)
        {
            var portSfps = await _unitOfWork.PortSfpRepository.GetAsync();
            ViewBag.PortSfp = new SelectList(Mapper.Map<List<PortSfpViewModel>>(portSfps),
                "Name", "Name", selectedPortSfp);
        }

        private async Task PopulatePortStatusesDropDownList(object selectedPortStatus = null)
        {
            var portStatuses = await _unitOfWork.PortStatusRepository.GetAsync();
            ViewBag.PortStatus = new SelectList(Mapper.Map<List<PortStatusViewModel>>(portStatuses),
                "Name", "Name", selectedPortStatus);
        }

        private async Task PopulatePortRolesDropDownList(int deviceRoleId, object selectedPortRole = null)
        {
            var portRoles = await _unitOfWork.PortRoleRepository.GetAsync(
                q =>
                    q.DeviceRolePortRoles.Where(x => x.DeviceRoleID == deviceRoleId).Any());

            ViewBag.PortRole = new SelectList(Mapper.Map<List<PortRoleViewModel>>(portRoles),
                "Name", "Name", selectedPortRole);
        }

        private async Task PopulatePortPoolsDropDownList(string portRoleName, object selectedPortPool = null)
        {
            var portPools = await _unitOfWork.PortPoolRepository.GetAsync(
                    q => 
                        q.PortRole.Name == portRoleName);

            ViewBag.PortPool = new SelectList(Mapper.Map<List<PortPoolViewModel>>(portPools),
                "Name", "Name", selectedPortPool);
        }
    }
}
