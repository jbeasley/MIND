using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using Microsoft.EntityFrameworkCore;

namespace SCM.Controllers
{
    public class BaseBgpPeerController : BaseViewController
    {
        public BaseBgpPeerController(IBgpPeerService bgpPeerService, 
            IRoutingInstanceService vrfService, 
            IBgpPeerValidator bgpPeerValidator, 
            IMapper mapper)
        {
           BgpPeerService = bgpPeerService;
           RoutingInstanceService = vrfService;
           Mapper = mapper;

           BgpPeerValidator = bgpPeerValidator;
           this.Validator = bgpPeerValidator;
        }

        public IBgpPeerService BgpPeerService { get; }
        public IRoutingInstanceService RoutingInstanceService { get; }
        public IMapper Mapper { get; }
        private IBgpPeerValidator BgpPeerValidator { get; }

        protected async Task<IActionResult> BaseGetAllByRoutingInstanceID(BgpPeerNavigationViewModel nav)
        {
            var bgpPeers = await BgpPeerService.GetAllByRoutingInstanceIDAsync(nav.RoutingInstanceID.Value);
            return View(Mapper.Map<List<BgpPeerViewModel>>(bgpPeers));
        }

        protected async Task<IActionResult> BaseDetails(int? bgpPeerID)
        {
            if (bgpPeerID == null)
            {
                return NotFound();
            }

            var item = await BgpPeerService.GetByIDAsync(bgpPeerID.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<BgpPeerViewModel>(item));
        }

        protected IActionResult BaseCreate(int? bgpPeerID)
        {
            if (bgpPeerID == null)
            {
                return NotFound();
            }

            return View();
        }

        protected async Task<IActionResult> BaseCreate(BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bgpPeer = Mapper.Map<BgpPeer>(bgpPeerModel);
                    await BgpPeerValidator.ValidateNewAsync(bgpPeer);

                    if (BgpPeerValidator.ValidationDictionary.IsValid)
                    {
                        await BgpPeerService.AddAsync(bgpPeer);
                        return RedirectToAction("GetAllByRoutingInstanceID", nav);
                    }
                }
            }
            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(Mapper.Map<BgpPeerViewModel>(bgpPeerModel));
        }

        protected async Task<ActionResult> BaseEdit(int? bgpPeerID)
        {
            if (bgpPeerID == null)
            {
                return NotFound();
            }

            var bgpPeer = await BgpPeerService.GetByIDAsync(bgpPeerID.Value);

            if (bgpPeer == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<BgpPeerViewModel>(bgpPeer));
        }

        protected async Task<ActionResult> BaseEdit(int bgpPeerID, BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            if (bgpPeerID != bgpPeerModel.BgpPeerID)
            {
                return NotFound();
            }

            var currentBgpPeer = await BgpPeerService.GetByIDAsync(bgpPeerID);
            if (currentBgpPeer == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var bgpPeer = Mapper.Map<BgpPeer>(bgpPeerModel);
                    await BgpPeerValidator.ValidateChangesAsync(bgpPeer);

                    if (BgpPeerValidator.ValidationDictionary.IsValid)
                    {
                        await BgpPeerService.UpdateAsync(bgpPeer);
                        nav.ShowWarningMessage = true;
                        nav.RoutingInstanceID = bgpPeer.RoutingInstanceID;

                        return RedirectToAction("GetAllByRoutingInstanceID", nav);
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpAddress = (string)exceptionEntry.Property("IpAddress").CurrentValue;
                if (currentBgpPeer.IpAddress != proposedIpAddress)
                {
                    ModelState.AddModelError("IpAddress", $"Current value: {currentBgpPeer.IpAddress}");
                }

                var proposedAutonomousSystem = (int)exceptionEntry.Property("AutonomousSystem").CurrentValue;
                if (currentBgpPeer.AutonomousSystem != proposedAutonomousSystem)
                {
                    ModelState.AddModelError("AutonomousSystem", $"Current value: {currentBgpPeer.AutonomousSystem}");
                }

                var proposedMd5Password = (string)exceptionEntry.Property("Md5Password").CurrentValue;
                if (currentBgpPeer.Md5Password != proposedMd5Password)
                {
                    ModelState.AddModelError("Md5Password", $"Current value: {currentBgpPeer.Md5Password}");
                }

                if (currentBgpPeer.MaximumRoutes != null)
                {
                    var proposedMaximumRoutes = (int)exceptionEntry.Property("MaximumRoutes").CurrentValue;
                    if (currentBgpPeer.MaximumRoutes != proposedMaximumRoutes)
                    {
                        ModelState.AddModelError("MaximumRoutes", $"Current value: {currentBgpPeer.MaximumRoutes}");
                    }
                }

                var proposedIsMultiHop = (bool)exceptionEntry.Property("IsMultiHop").CurrentValue;
                if (currentBgpPeer.IsMultiHop != proposedIsMultiHop)
                {
                    ModelState.AddModelError("IsMultiHop", $"Current value: {currentBgpPeer.IsMultiHop}");
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

            return View(Mapper.Map<BgpPeerViewModel>(currentBgpPeer));
        }

        protected async Task<IActionResult> BaseDelete(int? bgpPeerID, BgpPeerNavigationViewModel nav = null, bool? concurrencyError = false)
        {
            if (bgpPeerID == null)
            {
                return NotFound();
            }

            var bgpPeer = await BgpPeerService.GetByIDAsync(bgpPeerID.Value);
            if (bgpPeer == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByRoutingInstanceID", nav);
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

            return View(Mapper.Map<BgpPeerViewModel>(bgpPeer));
        }

        protected async Task<IActionResult> BaseDelete(BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            var currentBgpPeer = await BgpPeerService.GetByIDAsync(bgpPeerModel.BgpPeerID);
            if (currentBgpPeer == null)
            {
                return RedirectToAction("GetAllByRoutingInstanceID", nav);
            }

            try
            {
                BgpPeerValidator.ValidationDictionary.Clear();
                BgpPeerValidator.ValidateDelete(currentBgpPeer);

                if (BgpPeerValidator.ValidationDictionary.IsValid)
                {
                    await BgpPeerService.DeleteAsync(Mapper.Map<BgpPeer>(bgpPeerModel));
                    nav.ShowWarningMessage = true;

                    return RedirectToAction("GetAllByRoutingInstanceID", nav);
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                nav.ConcurrencyError = true;
                nav.BgpPeerID = bgpPeerModel.BgpPeerID;

                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", nav);
            }

            return View(Mapper.Map<BgpPeerViewModel>(currentBgpPeer));
        }
    }
}
