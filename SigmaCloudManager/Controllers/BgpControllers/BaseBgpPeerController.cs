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
using Mind.Services;

namespace SCM.Controllers
{
    public class BaseBgpPeerController : BaseViewController
    {
        protected internal readonly IBgpPeerService _bgpPeerService;
        protected internal readonly IRoutingInstanceService _routingInstanceService;

        public BaseBgpPeerController(IBgpPeerService bgpPeerService, 
            IRoutingInstanceService vrfService, 
            IBgpPeerValidator bgpPeerValidator, 
            IMapper mapper) : base(bgpPeerService, mapper)
        {
           _bgpPeerService = bgpPeerService;
           _routingInstanceService = vrfService;
        }

        protected async Task<IActionResult> BaseGetAllByRoutingInstanceID(BgpPeerNavigationViewModel nav)
        {
            var bgpPeers = await _bgpPeerService.GetAllByRoutingInstanceIDAsync(nav.RoutingInstanceID.Value);
            return View(Mapper.Map<List<BgpPeerViewModel>>(bgpPeers));
        }

        protected async Task<IActionResult> BaseDetails(int? bgpPeerID)
        {
            if (bgpPeerID == null)
            {
                return NotFound();
            }

            var item = await _bgpPeerService.GetByIDAsync(bgpPeerID.Value);
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
                    await _bgpPeerService.AddAsync(bgpPeer);
                    return RedirectToAction("GetAllByRoutingInstanceID", nav);
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

            var bgpPeer = await _bgpPeerService.GetByIDAsync(bgpPeerID.Value);

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

            var currentBgpPeer = await _bgpPeerService.GetByIDAsync(bgpPeerID);
            if (currentBgpPeer == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var bgpPeer = Mapper.Map<BgpPeer>(bgpPeerModel);
                    await _bgpPeerService.UpdateAsync(bgpPeer);
                    nav.ShowWarningMessage = true;
                    nav.RoutingInstanceID = bgpPeer.RoutingInstanceID;

                    return RedirectToAction("GetAllByRoutingInstanceID", nav);
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpv4PeerAddress = (string)exceptionEntry.Property("Ipv4PeerAddress").CurrentValue;
                if (currentBgpPeer.Ipv4PeerAddress != proposedIpv4PeerAddress)
                {
                    ModelState.AddModelError("Ipv4PeerAddress", $"Current value: {currentBgpPeer.Ipv4PeerAddress}");
                }

                var proposedAutonomousSystem = (int)exceptionEntry.Property("Peer2ByteAutonomousSystem").CurrentValue;
                if (currentBgpPeer.Peer2ByteAutonomousSystem != proposedAutonomousSystem)
                {
                    ModelState.AddModelError("Peer2ByteAutonomousSystem", $"Current value: {currentBgpPeer.Peer2ByteAutonomousSystem}");
                }

                var proposedPeerPassword = (string)exceptionEntry.Property("Md5Password").CurrentValue;
                if (currentBgpPeer.PeerPassword != proposedPeerPassword)
                {
                    ModelState.AddModelError("PeerPassword", $"Current value: {currentBgpPeer.PeerPassword}");
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

            var bgpPeer = await _bgpPeerService.GetByIDAsync(bgpPeerID.Value);
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
            var currentBgpPeer = await _bgpPeerService.GetByIDAsync(bgpPeerModel.BgpPeerID);
            if (currentBgpPeer == null)
            {
                return RedirectToAction("GetAllByRoutingInstanceID", nav);
            }

            try
            {
                await _bgpPeerService.DeleteAsync(bgpPeerModel.BgpPeerID);
                return RedirectToAction("GetAllByRoutingInstanceID", nav);
            }

            catch (ServiceValidationException /* ex */)
            {
                nav.BgpPeerID = bgpPeerModel.BgpPeerID;

                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", nav);
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                nav.ConcurrencyError = true;
                nav.BgpPeerID = bgpPeerModel.BgpPeerID;

                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", nav);
            }
        }
    }
}
