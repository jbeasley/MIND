using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SCM.Models.ViewModels;
using SCM.Services;

namespace SCM.Controllers
{
    public class InfrastructureController : BaseViewController
    {
        public InfrastructureController(IDeviceRoleService deviceRoleService, 
            IDeviceRolePortRoleService deviceRolePortRoleService,
            IPortRoleService portRoleService,
            IPortPoolService portPoolService,
            IAttachmentRoleService attachmentRoleService,
            IVifRoleService vifRoleService,
            IMapper mapper)
        {
            DeviceRoleService = deviceRoleService;
            DeviceRolePortRoleService = deviceRolePortRoleService;
            PortRoleService = portRoleService;
            PortPoolService = portPoolService;
            AttachmentRoleService = attachmentRoleService;
            VifRoleService = vifRoleService;
            Mapper = mapper;
        }

        private IDeviceRoleService DeviceRoleService { get; }
        private IDeviceRolePortRoleService DeviceRolePortRoleService { get; }
        private IPortRoleService PortRoleService { get; }
        private IPortPoolService PortPoolService { get; }
        private IAttachmentRoleService AttachmentRoleService { get; }
        private IVifRoleService VifRoleService { get; set; }
        private IMapper Mapper { get; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeviceRoles()
        {
            var deviceRoles = await DeviceRoleService.GetAllAsync();
            return View(Mapper.Map<List<DeviceRoleViewModel>>(deviceRoles));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortRolesByDeviceRoleID(int? deviceRoleID, string searchString)
        {
            if (deviceRoleID == null)
            {
                return NotFound();
            }

            var deviceRole = await DeviceRoleService.GetByIDAsync(deviceRoleID.Value);
            if (deviceRole == null)
            {
                return NotFound();
            }

            ViewBag.DeviceRole = Mapper.Map<DeviceRoleViewModel>(deviceRole);
            var deviceRolePortRoles = await DeviceRolePortRoleService.GetAllByDeviceRoleIDAsync(deviceRoleID.Value, searchString);

            return View(Mapper.Map<List<DeviceRolePortRoleViewModel>>(deviceRolePortRoles));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortPoolsByPortRoleID(int? portRoleID, int? deviceRoleID)
        {
            if (portRoleID == null || deviceRoleID == null)
            {
                return NotFound();
            }

            var portRole = await PortRoleService.GetByIDAsync(portRoleID.Value);
            if (portRole == null)
            {
                return NotFound();
            }

            var deviceRole = await DeviceRoleService.GetByIDAsync(deviceRoleID.Value);
            if (deviceRole == null)
            {
                return NotFound();
            }

            ViewBag.PortRole = Mapper.Map<PortRoleViewModel>(portRole);
            ViewBag.DeviceRole = Mapper.Map<DeviceRoleViewModel>(deviceRole);
            var portPools = await PortPoolService.GetAllByPortRoleIDAsync(portRoleID.Value);

            return View(Mapper.Map<List<PortPoolViewModel>>(portPools));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttachmentRolesByPortPoolID(int? portPoolID, int? deviceRoleID)
        {
            if (portPoolID == null || deviceRoleID == null)
            {
                return NotFound();
            }

            var portPool = await PortPoolService.GetByIDAsync(portPoolID.Value);
            if (portPool == null)
            {
                return NotFound();
            }

            var deviceRole = await DeviceRoleService.GetByIDAsync(deviceRoleID.Value);
            if (deviceRole == null)
            {
                return NotFound();
            }

            ViewBag.PortPool = Mapper.Map<PortPoolViewModel>(portPool);
            ViewBag.DeviceRole = Mapper.Map<DeviceRoleViewModel>(deviceRole);
            var attachmentRoles = await AttachmentRoleService.GetAllByPortPoolIDAsync(portPoolID.Value,deviceRoleID.Value);

            return View(Mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVifRolesByAttachmentRoleID(int? attachmentRoleID, int? deviceRoleID)
        {
            if (attachmentRoleID == null)
            {
                return NotFound();
            }

            var attachmentRole = await AttachmentRoleService.GetByIDAsync(attachmentRoleID.Value);
            if (attachmentRole == null)
            {
                return NotFound();
            }

            var deviceRole = await DeviceRoleService.GetByIDAsync(deviceRoleID.Value);
            if (deviceRole == null)
            {
                return NotFound();
            }

            ViewBag.attachmentRole = Mapper.Map<AttachmentRoleViewModel>(attachmentRole);
            ViewBag.DeviceRole = Mapper.Map<DeviceRoleViewModel>(deviceRole);
            var vifRoles = await VifRoleService.GetAllByAttachmentRoleIDAsync(attachmentRoleID.Value);

            return View(Mapper.Map<List<VifRoleViewModel>>(vifRoles));
        }
    }
}
