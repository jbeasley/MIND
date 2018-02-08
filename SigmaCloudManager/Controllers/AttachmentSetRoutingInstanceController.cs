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
using SCM.Models.RequestModels;
using SCM.Validators;

namespace SCM.Controllers
{
    public class AttachmentSetRoutingInstanceController : BaseViewController
    {
        public AttachmentSetRoutingInstanceController(IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IAttachmentSetService attachmentSetService,
            IVpnService vpnService, 
            IAttachmentSetRoutingInstanceValidator attachmentSetRoutingInstanceValidator,
            IMapper mapper)
        {
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            AttachmentSetService = attachmentSetService;
            VpnService = vpnService;
            Mapper = mapper;

            AttachmentSetRoutingInstanceValidator = attachmentSetRoutingInstanceValidator;
            this.Validator = attachmentSetRoutingInstanceValidator;
        }

        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IVpnService VpnService { get; set; }
        private IAttachmentSetRoutingInstanceValidator AttachmentSetRoutingInstanceValidator { get; set; }
        private IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id, bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);
            ViewBag.AttachmentSet = attachmentSet;

            await AttachmentSetRoutingInstanceValidator.ValidateRoutingInstancesConfiguredCorrectlyAsync(attachmentSet);
            if (AttachmentSetRoutingInstanceValidator.ValidationDictionary.IsValid)
            {
                ViewData["SuccessMessage"] = "The VRFs for this Attachment Set are configured correctly!";
            }

            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                  + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var attachmentSetRoutingInstances = await AttachmentSetRoutingInstanceService.GetAllByAttachmentSetIDAsync(id.Value);
            return View(Mapper.Map<List<AttachmentSetRoutingInstanceViewModel>>(attachmentSetRoutingInstances));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await AttachmentSetRoutingInstanceService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(item.AttachmentSetID);

            return View(Mapper.Map<AttachmentSetRoutingInstanceViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> CreateStep1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            await PopulateLocationsDropDownList(attachmentSet);
            await PopulatePlanesDropDownList();
            ViewBag.AttachmentSet = attachmentSet;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStep2([Bind("AttachmentSetID,LocationID,PlaneID,TenantID")] AttachmentSetRoutingInstanceRequestViewModel request)
        {
            await PopulateRoutingInstancesDropDownList(Mapper.Map<AttachmentSetRoutingInstanceRequest>(request));
            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(request.AttachmentSetID);
            ViewBag.AttachmentSetRoutingInstanceRequest = request;

            return View(new AttachmentSetRoutingInstanceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentSetID,RoutingInstanceID,AdvertisedIpRoutingPreference,LocalIpRoutingPreference,"
            + "MulticastDesignatedRouterPreference")] AttachmentSetRoutingInstanceViewModel attachmentSetRoutingInstanceModel,
            [Bind("AttachmentSetID,TenantID,LocationID,PlaneID")] AttachmentSetRoutingInstanceRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                var attachmentSetRoutingInstance = Mapper.Map<AttachmentSetRoutingInstance>(attachmentSetRoutingInstanceModel);
                await AttachmentSetRoutingInstanceValidator.ValidateNewAsync(attachmentSetRoutingInstance);

                if (AttachmentSetRoutingInstanceValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await AttachmentSetRoutingInstanceService.AddAsync(attachmentSetRoutingInstance);
                        var vpns = await VpnService.GetAllByAttachmentSetIDAsync(attachmentSetRoutingInstance.AttachmentSetID);

                        return RedirectToAction("GetAllByAttachmentSetID", new
                        {
                            id = attachmentSetRoutingInstance.AttachmentSetID,
                            showWarningMessage = vpns.Any()
                        });
                    }

                    catch (DbUpdateException /** ex **/ )
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }
                }
            } 

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetRoutingInstanceModel.AttachmentSetID);
            await PopulateRoutingInstancesDropDownList(Mapper.Map<AttachmentSetRoutingInstanceRequest>(request));
            ViewBag.AttachmentSetRoutingInstanceRequest = request;

            return View("CreateStep2", attachmentSetRoutingInstanceModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByIDAsync(id.Value);
            if (attachmentSetRoutingInstance == null)
            {
                return NotFound();
            }

            await PopulateRoutingInstancesDropDownList(new AttachmentSetRoutingInstanceRequest
            {
                AttachmentSetID = attachmentSetRoutingInstance.AttachmentSetID,
                LocationID = attachmentSetRoutingInstance.RoutingInstance.Device.LocationID,
                TenantID = attachmentSetRoutingInstance.AttachmentSet.TenantID,
                PlaneID = attachmentSetRoutingInstance.RoutingInstance.Device.PlaneID
            });

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetID);
            return View(Mapper.Map<AttachmentSetRoutingInstanceViewModel>(attachmentSetRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, 
            [Bind("AttachmentSetRoutingInstanceID,AttachmentSetID,RoutingInstanceID,AdvertisedIpRoutingPreference,LocalIpRoutingPreference,"
            + "MulticastDesignatedRouterPreference,RowVersion")] AttachmentSetRoutingInstanceViewModel attachmentSetRoutingInstanceModel)
        {
            if (id != attachmentSetRoutingInstanceModel.AttachmentSetRoutingInstanceID)
            {
                return NotFound();
            }

            var currentAttachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByIDAsync(id);
            if (currentAttachmentSetRoutingInstance == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await AttachmentSetRoutingInstanceService.UpdateAsync(Mapper.Map<AttachmentSetRoutingInstance>(attachmentSetRoutingInstanceModel));
                    var vpns = await VpnService.GetAllByAttachmentSetIDAsync(attachmentSetRoutingInstanceModel.AttachmentSetID);
                    
                    return RedirectToAction("GetAllByAttachmentSetID", 
                        new
                        {
                            id = currentAttachmentSetRoutingInstance.AttachmentSetID,
                            showWarningMessage = vpns.Any(),
                            tenantID = currentAttachmentSetRoutingInstance.RoutingInstance.TenantID
                        });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedAdvertisedIpRoutingPreference = (int?)exceptionEntry.Property("AdvertisedIpRoutingPreference").CurrentValue;
                if (currentAttachmentSetRoutingInstance.AdvertisedIpRoutingPreference != proposedAdvertisedIpRoutingPreference)
                {
                    ModelState.AddModelError("AdvertisedIpRoutingPreference", $"Current value: {currentAttachmentSetRoutingInstance.AdvertisedIpRoutingPreference}");
                }

                var proposedLocalIpRoutingPreference = (int?)exceptionEntry.Property("LocalIpRoutingPreference").CurrentValue;
                if (currentAttachmentSetRoutingInstance.LocalIpRoutingPreference != proposedLocalIpRoutingPreference)
                {
                    ModelState.AddModelError("LocalIpRoutingPreference", $"Current value: {currentAttachmentSetRoutingInstance.LocalIpRoutingPreference}");
                }

                var proposedMulticastDesignatedRouterPreference = (int?)exceptionEntry.Property("MulticastDesignatedRouterPreference").CurrentValue;
                if (currentAttachmentSetRoutingInstance.MulticastDesignatedRouterPreference != proposedMulticastDesignatedRouterPreference)
                {
                    ModelState.AddModelError("MulticastDesignatedRouterPreference", $"Current value: {currentAttachmentSetRoutingInstance.MulticastDesignatedRouterPreference}");
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

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetRoutingInstanceModel.AttachmentSetID);
            return View(Mapper.Map<AttachmentSetRoutingInstanceViewModel>(currentAttachmentSetRoutingInstance));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByIDAsync(id.Value);
            if (attachmentSetRoutingInstance == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByAttachmentSetID", new { id = attachmentSetID });
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

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetID);
            return View(Mapper.Map<AttachmentSetRoutingInstanceViewModel>(attachmentSetRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AttachmentSetRoutingInstanceViewModel attachmentSetRoutingInstanceModel)
        {
            var attachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByIDAsync(attachmentSetRoutingInstanceModel.AttachmentSetRoutingInstanceID);
            if (attachmentSetRoutingInstance == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = attachmentSetRoutingInstanceModel.AttachmentSetID
                });
            }

            try
            {
                await AttachmentSetRoutingInstanceValidator.ValidateDeleteAsync(attachmentSetRoutingInstance);
                if (AttachmentSetRoutingInstanceValidator.ValidationDictionary.IsValid)
                {
                    await AttachmentSetRoutingInstanceService.DeleteAsync(Mapper.Map<AttachmentSetRoutingInstance>(attachmentSetRoutingInstanceModel));
                    var vpns = await VpnService.GetAllByAttachmentSetIDAsync(attachmentSetRoutingInstance.AttachmentSetID);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = attachmentSetRoutingInstance.AttachmentSetID,
                        showWarningMessage = vpns.Any()
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID,
                    attachmentSetID = attachmentSetRoutingInstance.AttachmentSetID
                });
            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetRoutingInstanceModel.AttachmentSetID);

            return View(Mapper.Map<AttachmentSetRoutingInstanceViewModel>(attachmentSetRoutingInstance));
        }

        private async Task PopulatePlanesDropDownList(object selectedPlane = null)
        {
            var planes = await AttachmentSetRoutingInstanceService.UnitOfWork.PlaneRepository.GetAsync();
            ViewBag.PlaneID = new SelectList(planes, "PlaneID", "Name", selectedPlane);
        }

        private async Task PopulateLocationsDropDownList(AttachmentSet attachmentSet)
        {
            IEnumerable<Location> locations = await AttachmentSetRoutingInstanceService.UnitOfWork.LocationRepository.GetAsync(q => q.SubRegion.RegionID == attachmentSet.RegionID);
            if (attachmentSet.SubRegionID != null)
            {
                locations = locations.Where(q => q.SubRegionID == attachmentSet.SubRegionID);
            }
 
            ViewBag.LocationID = new SelectList(locations, "LocationID", "SiteName");
        }

        private async Task PopulateRoutingInstancesDropDownList(AttachmentSetRoutingInstanceRequest request, object selectedRoutingInstance = null)
        { 
            var vrfs = await AttachmentSetRoutingInstanceService.GetCandidateRoutingInstances(request);
            ViewBag.RoutingInstanceID = new SelectList(vrfs, "RoutingInstanceID", "Name", selectedRoutingInstance);
        }
    }
}
