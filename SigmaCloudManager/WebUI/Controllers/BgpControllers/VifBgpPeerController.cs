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
using Microsoft.EntityFrameworkCore;
using SCM.Validators;
using System.Dynamic;

namespace SCM.Controllers
{
    public class VifBgpPeerController : BaseBgpPeerController
    {
        public VifBgpPeerController(IBgpPeerService bgpPeerService,
            IVifService vifService,
            IAttachmentService attachmentService,
            IRoutingInstanceService vrfService,
            IBgpPeerValidator bgpPeerValidator,
            IMapper mapper) : base(bgpPeerService, vrfService, bgpPeerValidator, mapper)
        {
            AttachmentService = attachmentService;
            VifService = vifService;
        }

        private IAttachmentService AttachmentService { get; }
        private IVifService VifService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByRoutingInstanceID(BgpPeerNavigationViewModel nav)
        {
            var vif = await VifService.GetByIDAsync(nav.VifID.Value);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            if (nav.ShowWarningMessage)
            {
                ViewData["WarningMessage"] = "The Vif now requires sync with the network. "
                            + $"Return to the <a href = '/TenantVif/GetAllByattachmentID/{vif.AttachmentID}'>Vifs List</a> and sync the Vif.";
            }

            return await base.BaseGetAllByRoutingInstanceID(nav);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? bgpPeerID, int vifID)
        {
            await PopulateViewBag(vifID);
            return await base.BaseDetails(bgpPeerID);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int vrfID, int vifID)
        {
            await PopulateViewBag(vifID);
            return base.BaseCreate(vrfID);
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
                await PopulateViewBag(nav.VifID.Value);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? bgpPeerID, int vifID)
        {
            await PopulateViewBag(vifID);
            return await base.BaseEdit(bgpPeerID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int bgpPeerID, [Bind("BgpPeerID,RoutingInstanceID,IpAddress,AutonomousSystem,Md5Password" +
            "MaximumRoutes,IsBfdEnabled,IsMultiHop,RowVersion")] BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
            try
            {
                return await base.BaseEdit(bgpPeerID, bgpPeerModel, nav);
            }

            finally
            {
                await PopulateViewBag(nav.VifID.Value);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? bgpPeerID, BgpPeerNavigationViewModel nav, bool? concurrencyError = false)
        {

            await PopulateViewBag(nav.VifID.Value);
            return await base.BaseDelete(bgpPeerID, nav, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BgpPeerViewModel bgpPeerModel, BgpPeerNavigationViewModel nav)
        {
          
            await PopulateViewBag(nav.VifID.Value);
            return await base.BaseDelete(bgpPeerModel, nav);
        }

        private async Task PopulateViewBag(int vifID)
        {
            var vif = await VifService.GetByIDAsync(vifID);
            ViewBag.Vif = Mapper.Map<VifViewModel>(vif);
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
        }
    }
}
