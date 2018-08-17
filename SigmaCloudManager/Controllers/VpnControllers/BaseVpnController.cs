using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Hubs;
using Mind.Services;

namespace SCM.Controllers
{
    public abstract class BaseVpnController : BaseViewController
    {
        public BaseVpnController(IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            IMapper mapper)
        {
            VpnService = vpnService;
            AttachmentSetService = attachmentSetService;
            Mapper = mapper;
        }

        internal IVpnService VpnService { get; set; }
        internal IAttachmentSetService AttachmentSetService { get; set; }
        internal IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, int? attachmentSetID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var vpn = Mapper.Map<VpnViewModel>(item);
            if (attachmentSetID != null)
            {
                var attachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetID.Value);
                vpn.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            }

            return View(vpn);
        }
    }
}