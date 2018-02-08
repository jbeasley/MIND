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
using SCM.Validators;

namespace SCM.Controllers
{
    public class InfrastructurePortController : BasePortController
    {
        public InfrastructurePortController(IPortService portService,
            IPortConnectorService portConnectorService,
            IPortRoleService portRoleService,
            IPortPoolService portPoolService,
            IPortStatusService portStatusService,
            IPortSfpService portSfpService,
            IPortBandwidthService portBandwidthService,
            ITenantService tenantService,
            IDeviceService deviceService,
            IPortValidator portValidator, IMapper mapper) : base(portService,
                portConnectorService,
                portRoleService,
                portPoolService,
                portStatusService,
                portSfpService,
                portBandwidthService,
                tenantService,
                deviceService,
                portValidator,
                mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return await base.BaseDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            return await base.BaseCreate(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceID,Type,Name,PortConnectorID,PortSfpID,PortStatusID,PortRoleID," +
            "PortPoolID,TenantID,PortBandwidthID")] PortViewModel portModel)
        {
            return await base.BaseCreate(portModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            return await base.BaseEdit(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("ID,Type,Name,DeviceID,TenantID,InterfaceID,PortBandwidthID,PortConnectorID," +
            "PortSfpID,PortStatusID,PortRoleID,PortPoolID,RowVersion")] PortViewModel portModel)
        {
            return await base.BaseEdit(id, portModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? deviceID, bool? concurrencyError = false)
        {
            return await base.BaseDelete(id, deviceID, concurrencyError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PortViewModel portModel)
        {
            return await base.BaseDelete(portModel);
        }
    }
}
