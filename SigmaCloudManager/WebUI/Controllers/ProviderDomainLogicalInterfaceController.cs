using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mind.Models.RequestModels;
using Mind.Services;
using SCM.Controllers;
using SCM.Data;
using SCM.Models;
using Mind.WebUI.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mind.WebUI.Models;
using Mind.Builders;
using Mind.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainLogicalInterfaceController : BaseViewController
    {
        private readonly IProviderDomainLogicalInterfaceService _logicalInterfaceService;
        public ProviderDomainLogicalInterfaceController(IProviderDomainLogicalInterfaceService logicalInterfaceService, IUnitOfWork unitOfWork, IMapper mapper) :
                base(unitOfWork, mapper)
        {
            _logicalInterfaceService = logicalInterfaceService;
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> GetAllByRoutingInstanceID(int? routingInstanceId)
        {
            var routingInstance = await _unitOfWork.RoutingInstanceRepository.GetByIDAsync(routingInstanceId.Value);
            ViewBag.RoutingInstance = Mapper.Map<ProviderDomainRoutingInstanceViewModel>(routingInstance);

            var logicalInterfaces = await _unitOfWork.LogicalInterfaceRepository.GetAsync(
                            q =>
                            q.RoutingInstanceID == routingInstanceId,
                            query: q => q.IncludeDeepProperties(),
                            AsTrackable: false);

            return View(Mapper.Map<List<LogicalInterfaceViewModel>>(logicalInterfaces));
        }

        [HttpGet]
        [ValidateProviderDomainLogicalInterfaceExists]
        public async Task<IActionResult> Details(int? logicalInterfaceId)
        {
            var logicalInterface = await _unitOfWork.LogicalInterfaceRepository.GetAsync(
                q =>
                q.LogicalInterfaceID == logicalInterfaceId,
                query: q => q.IncludeDeepProperties(),
                AsTrackable: false);

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateLogicalInterfaceTypesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateProviderDomainRoutingInstanceExists]
        public async Task<IActionResult> Create(int? routingInstanceId, ProviderDomainLogicalInterfaceRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = Mapper.Map<ProviderDomainLogicalInterfaceRequest>(model);
                    await _logicalInterfaceService.AddAsync(routingInstanceId.Value, request);
                    return RedirectToAction(nameof(GetAllByRoutingInstanceID), new { routingInstanceId });
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

            var routingInstance = await _unitOfWork.RoutingInstanceRepository.GetByIDAsync(routingInstanceId.Value);
            ViewBag.RoutingInstance = Mapper.Map<ProviderDomainRoutingInstanceViewModel>(routingInstance);

            return View(model);
        }

        [HttpGet]
        [ValidateProviderDomainLogicalInterfaceExists]
        public async Task<ActionResult> Edit(int? logicalInterfaceId)
        {
            var logicalInterface = await _logicalInterfaceService.GetByIDAsync(logicalInterfaceId.Value);
            var routingInstance = await _unitOfWork.RoutingInstanceRepository.GetByIDAsync(logicalInterface.RoutingInstanceID);
            ViewBag.RoutingInstance = Mapper.Map<ProviderDomainRoutingInstanceViewModel>(routingInstance);

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateProviderDomainLogicalInterfaceExists]
        public async Task<ActionResult> Edit(int? logicalInterfaceId, LogicalInterfaceUpdateViewModel updateModel)
        {
            var logicalInterface = await _logicalInterfaceService.GetByIDAsync(logicalInterfaceId.Value);
            if (logicalInterface.HasPreconditionFailed(Request, updateModel.RowVersion.ToString()))
            {
                return View(Mapper.Map<LogicalInterfaceUpdateViewModel>(logicalInterface));
            }

            var update = Mapper.Map<LogicalInterfaceUpdate>(updateModel);

            try
            {
                await _logicalInterfaceService.UpdateAsync(logicalInterfaceId.Value, update);
                return RedirectToAction(nameof(GetAllByRoutingInstanceID), new { routingInstanceId = logicalInterface.RoutingInstanceID });
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

            var routingInstance = await _unitOfWork.RoutingInstanceRepository.GetByIDAsync(logicalInterface.RoutingInstanceID);
            ViewBag.RoutingInstance = Mapper.Map<ProviderDomainRoutingInstanceViewModel>(routingInstance);

            return View(Mapper.Map<LogicalInterfaceUpdateViewModel>(logicalInterface));

        }

        [HttpGet]
        [ValidateProviderDomainRoutingInstanceExists]
        public async Task<IActionResult> Delete(int? routingInstanceId, int? logicalInterfaceId, bool? concurrencyError = false)
        {
            var item = await _logicalInterfaceService.GetByIDAsync(logicalInterfaceId.Value);
            if (item == null)
            {
                return RedirectToAction(nameof(GetAllByRoutingInstanceID), new { routingInstanceId });
            }

            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

            return View(Mapper.Map<ProviderDomainAttachmentViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LogicalInterfaceViewModel model)
        {
            var logicalInterface = await _logicalInterfaceService.GetByIDAsync(model.LogicalInterfaceId.Value);
            if (logicalInterface == null) return RedirectToAction(nameof(GetAllByRoutingInstanceID), new { routingInstanceId = model.RoutingInstanceId });

            if (logicalInterface.HasPreconditionFailed(Request, model.RowVersion.ToString()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    routingInstanceId = logicalInterface.RoutingInstanceID,
                    logicalInterfaceId = logicalInterface.LogicalInterfaceID,
                    concurrencyError = true
                });
            }

            try
            {
                await _logicalInterfaceService.DeleteAsync(logicalInterface.LogicalInterfaceID);
                return RedirectToAction(nameof(GetAllByRoutingInstanceID), new { routingInstanceId = logicalInterface.RoutingInstanceID });
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        /// <summary>
        /// Helper to populate a drop-down list of logical interface type options
        /// </summary>
        /// <param name="selectedLogicalInterfaceType"></param>
        private void PopulateLogicalInterfaceTypesDropDownList(object selectedLogicalInterfaceType = null)
        {
            var logicalInterfaceTypesList = new List<SelectListItem>();
            foreach (var logicalInterfaceType in Enum.GetValues(typeof(LogicalInterfaceTypeEnum)))
            {
                logicalInterfaceTypesList.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(LogicalInterfaceTypeEnum), logicalInterfaceType),
                    Value = logicalInterfaceType.ToString()
                });
            }

            ViewBag.LogicalInterfaceType = logicalInterfaceTypesList;
        }
    }
}
