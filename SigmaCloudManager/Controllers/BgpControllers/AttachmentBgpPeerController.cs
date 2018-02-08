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
    public class AttachmentBgpPeerController : BaseBgpPeerController
    {
        public AttachmentBgpPeerController(IBgpPeerService bgpPeerService,
            IAttachmentService attachmentService,
            IRoutingInstanceService vrfService,
            IBgpPeerValidator bgpPeerValidator,
            IMapper mapper) : base(bgpPeerService, vrfService, bgpPeerValidator, mapper)
        {
            AttachmentService = attachmentService;
        }

        private IAttachmentService AttachmentService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByRoutingInstanceID(BgpPeerNavigationViewModel nav)
        {
            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            if (nav.ShowWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = "The Attachment now requires sync with the network. "
                            + $"Return to the <a href = '/TenantAttachment/GetAllByTenantID?tenantID={attachment.TenantID}'>Attachments List</a> and sync the Attachment.";
            }

            return await base.BaseGetAllByRoutingInstanceID(nav);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? bgpPeerID, int? attachmentID)
        {
            if (attachmentID == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return await base.BaseDetails(bgpPeerID);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? routingInstanceID, int? attachmentID)
        {
            if (attachmentID == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return base.BaseCreate(routingInstanceID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoutingInstanceID,IpAddress,AutonomousSystem,Md5Password,MaximumRoutes,IsBfdEnabled,IsMultiHop")]
            BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            try
            {
                return await base.BaseCreate(bgpPeerModel, nav);
            }

            finally
            {
                var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
                ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? bgpPeerID, int? attachmentID)
        {
            if (attachmentID == null)
            {
                return NotFound();
            }

            var attachment = await AttachmentService.GetByIDAsync(attachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return await base.BaseEdit(bgpPeerID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int bgpPeerID, [Bind("BgpPeerID,RoutingInstanceID,IpAddress,AutonomousSystem,Md5Password" +
            "MaximumRoutes,IsBfdEnabled,IsMultiHop,RowVersion")] BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            try {

                return await base.BaseEdit(bgpPeerID, bgpPeerModel, nav);
            }

            finally
            {
                var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
                ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? bgpPeerID, BgpPeerNavigationViewModel nav, bool? concurrencyError = false)
        {
            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return await base.BaseDelete(bgpPeerID: bgpPeerID, concurrencyError: concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            var attachment = await AttachmentService.GetByIDAsync(nav.AttachmentID.Value);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return await base.BaseDelete(bgpPeerModel, nav);
        }
    }
}
