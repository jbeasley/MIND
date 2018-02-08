using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;

namespace SCM.Controllers
{
    public class AttachmentSetVpnController : BaseVpnController
    {
        public AttachmentSetVpnController(IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            IMapper mapper) : 
            base (vpnService, attachmentSetService, mapper)
        {
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpns = await VpnService.GetAllByAttachmentSetIDAsync(id.Value);
            ViewData["SuccessMessage"] = FormatAsHtmlList(vpns
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            ViewData["NetworkWarningMessage"] = FormatAsHtmlList(vpns
                .Where(x => x.RequiresSync && x.ShowRequiresSyncAlert)
                .Select(x => $"{x.Name} requires sync with the network.").ToList());

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);

            return View(Mapper.Map<List<VpnViewModel>>(vpns));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCreatedAlerts(int id)
        {
            var vpns = await VpnService.GetAllByAttachmentSetIDAsync(id, created: true, showCreatedAlert: true);
            foreach (var vpn in vpns)
            {
                vpn.ShowCreatedAlert = false;
            }

            try
            {
                await VpnService.UpdateAsync(vpns);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAllByAttachmentSetID", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearRequiresSyncAlerts(int id)
        {
            var vpns = await VpnService.GetAllByAttachmentSetIDAsync(id, requiresSync: true, showRequiresSyncAlert: true);
            foreach (var vpn in vpns)
            {
                vpn.ShowRequiresSyncAlert = false;
            }

            try
            {
                await VpnService.UpdateAsync(vpns);
            }

            catch (DbUpdateConcurrencyException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("GetAllByAttachmentSetID", new { id });
        }
    }
}
