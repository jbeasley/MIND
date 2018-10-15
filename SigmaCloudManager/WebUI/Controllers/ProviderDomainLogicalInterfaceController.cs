using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mind.Models.RequestModels;
using Mind.Services;
using SCM.Controllers;
using SCM.Data;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [ValidateModelState]
        public async Task<IActionResult> Create(int? routingInstanceId, LogicalInterfaceViewModel model)
        {

            var request = Mapper.Map<ProviderDomainLogicalInterfaceRequest>(model);
            await _logicalInterfaceService.AddAsync(routingInstanceId.Value, request);
            return RedirectToAction(nameof(GetAllByRoutingInstanceId), new { routingInstanceId });

            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(model);
        }

        [HttpGet]
        [ValidateProviderDomainLogicalInterfaceExists]
        public async Task<ActionResult> Edit(int? logicalInterfaceId)
        {
            var logicalInterface = await LogicalInterfaceService.GetByIDAsync(logicalInterfaceID);
            var routingInstance = await _attachmentService.GetByIDAsync(attachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<LogicalInterfaceViewModel>(logicalInterface));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateProviderDomainLogicalInterfaceExists]
        public async Task<ActionResult> Edit(int? logicalInterfaceId, LogicalInterfaceUpdateViewModel updateModel)
        {
            var logicalInterfaceUpdate = Mapper.Map<LogicalInterface>(updateModel);
            await _logicalInterfaceService.UpdateAsync(logicalInterfaceId.Value, logicalInterfaceUpdate);
            return RedirectToAction(nameof(GetAllByRoutingInstanceID), new { routingInstanceId });

            var attachment = await AttachmentService.GetByIDAsync(attachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<LogicalInterfaceViewModel>(currentLogicalInterface));
        }

        [HttpGet]
        [ValidateProviderDomainLogicalInterfaceExists]
        public async Task<IActionResult> Delete(int? logicalInterfaceId, bool? concurrencyError = false)
        {
            var item = await _logicalInterfaceService.GetByIDAsync(logicalInterfaceId.Value);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LogicalInterfaceViewModel logicalInterfaceModel)
        {
            try
            {
                await _logicalInterfaceService.DeleteAsync(Mapper.Map<LogicalInterface>(logicalInterfaceModel));
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

        /// <summary>
        /// Helper to populate the Logical Interface Tyes drop-down list
        /// </summary>
        /// <param name="selectedLogicalInterfaceType"></param>
        private void PopulateLogicalInterfaceTypesDropDownList(object selectedLogicalInterfaceType = null)
        {
            var logicalInterfaceTypesList = new List<SelectListItem>();
            foreach (var logicalInterfaceType in Enum.GetValues(typeof(Models.ViewModels.LogicalInterfaceType)))
            {
                logicalInterfaceTypesList.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(Models.ViewModels.LogicalInterfaceType),
                    logicalInterfaceType),
                    Value = logicalInterfaceType.ToString()
                });
            }

            ViewBag.LogicalInterfaceType = logicalInterfaceTypesList;
        }
    }
}
