using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mind.Builders;
using Mind.Models;
using Mind.Models.RequestModels;
using Mind.Services;
using Mind.WebUI.Attributes;
using Mind.WebUI.Models;
using SCM.Controllers;
using SCM.Data;
using SCM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.Controllers
{
    public class InfrastructureRoutingInstanceController : BaseViewController
    {
        private readonly IInfrastructureRoutingInstanceService _routingInstanceService;

        public InfrastructureRoutingInstanceController(IUnitOfWork unitOfWork, IMapper mapper, 
        IInfrastructureRoutingInstanceService routingInstanceService) : base(unitOfWork, mapper)
        {
            _routingInstanceService = routingInstanceService;
        }

        [HttpGet]
        public IActionResult GetBgpPeersComponent(int? routingInstanceId)
        {
            return ViewComponent("BgpPeers", new { routingInstanceId });
        }

        [HttpPost]
        public IActionResult GetBgpPeerGridData([FromBody]List<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            return ViewComponent("BgpPeersGridData", new { bgpPeerRequests });
        }

        [HttpGet]
        [ValidateInfrastructureDeviceExists]
        public async Task<IActionResult> GetAllByDeviceID(int? deviceId)
        {
            var routingInstances = await _routingInstanceService.GetAllByDeviceIDAsync(deviceId.Value,
            deep: true, asTrackable: false);
                   
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(deviceId);
            ViewBag.Device = _mapper.Map<InfrastructureDeviceViewModel>(device);

            return View(_mapper.Map<List<InfrastructureRoutingInstanceViewModel>>(routingInstances));
        }

        [HttpGet]
        [ValidateInfrastructureRoutingInstanceExists]
        public async Task<IActionResult> Details(int? routingInstanceId)
        {
            var routingInstance = await _routingInstanceService.GetByIDAsync(routingInstanceId.Value, 
            deep: true, asTrackable: false);                                

            return View(Mapper.Map<InfrastructureRoutingInstanceViewModel>(routingInstance));
        }

        [HttpGet]
        [ValidateInfrastructureRoutingInstanceExists]
        public async Task<ActionResult> Edit(int? routingInstanceId)
        {
            var routingInstance = await _routingInstanceService.GetByIDAsync(routingInstanceId.Value, deep: true, asTrackable: false);
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(routingInstance.DeviceID);
            ViewBag.Device = _mapper.Map<InfrastructureDeviceViewModel>(device);

            return View(_mapper.Map<RoutingInstanceRequestViewModel>(routingInstance));
        }

        [HttpPost]
        [ValidateInfrastructureRoutingInstanceExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? routingInstanceId, RoutingInstanceRequestViewModel update)
        {
            var routingInstance = await _routingInstanceService.GetByIDAsync(routingInstanceId.Value, 
            deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (routingInstance.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(routingInstance.GetConcurrencyToken());
                }
                else
                {
                    var routingInstanceUpdate = _mapper.Map<RoutingInstanceRequest>(update);

                    try
                    {
                        await _routingInstanceService.UpdateAsync(routingInstanceId.Value, routingInstanceUpdate);
                        return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = routingInstance.DeviceID });
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

                    catch (ServiceBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (DbUpdateException)
                    {
                        ModelState.AddDatabaseUpdateExceptionMessage();
                    }
                }
            }

            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(routingInstance.DeviceID);
            ViewBag.Device = _mapper.Map<InfrastructureDeviceViewModel>(device);

            return View(update);
        }

        [HttpGet]
        [ValidateInfrastructureRoutingInstanceExists]
        public async Task<ActionResult> EditBgpPeers(int? routingInstanceId)
        {
            var routingInstance = await _routingInstanceService.GetByIDAsync(routingInstanceId.Value, deep: true, asTrackable: false);
            ViewBag.RoutingInstance = _mapper.Map<InfrastructureRoutingInstanceViewModel>(routingInstance);

            return View(_mapper.Map<RoutingInstanceBgpPeersRequestViewModel>(routingInstance));
        }

        [HttpPost]
        [ValidateInfrastructureRoutingInstanceExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBgpPeers(int? routingInstanceId, RoutingInstanceBgpPeersRequestViewModel update)
        {
            var routingInstance = await _routingInstanceService.GetByIDAsync(routingInstanceId.Value,
            deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (routingInstance.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(routingInstance.GetConcurrencyToken());
                }
                else
                {
                    var routingInstanceUpdate = _mapper.Map<RoutingInstanceRequest>(update);

                    try
                    {
                        await _routingInstanceService.UpdateAsync(routingInstanceId.Value, routingInstanceUpdate);
                        return RedirectToAction(nameof(EditBgpPeers), new { routingInstanceId });
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

                    catch (ServiceBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (DbUpdateException)
                    {
                        ModelState.AddDatabaseUpdateExceptionMessage();
                    }
                }
            }

            ViewBag.RoutingInstance = _mapper.Map<InfrastructureRoutingInstanceViewModel>(routingInstance);

            return View(update);
        }
    }
}
