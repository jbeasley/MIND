using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class AttachmentPortPoolAndRoleViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentPortPoolAndRoleViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task <IViewComponentResult> InvokeAsync(AttachmentPortPoolAndRoleComponentViewModel model)
        {
            var tasks = new List<Task>
            {
                PopulatePortPoolsDropDownList(model?.PortRoleTypeEnumName, model?.PortPoolName)
            };

            if (model?.PortPoolName != null)
            {
                tasks.Add(PopulateAttachmentRolesDropDownList(model?.PortPoolName, model?.DeviceRoleId, model?.AttachmentRoleName));
            }

            await Task.WhenAll(tasks);

            return View(model);
        }

        private async Task PopulatePortPoolsDropDownList(string portRoleTypeEnumName, object selectedPortPool = null)
        {
            var portPools = await _unitOfWork.PortPoolRepository.GetAsync(
                        q =>
                        q.PortRole.PortRoleType == Enum.Parse<SCM.Models.PortRoleTypeEnum>(portRoleTypeEnumName),
                        AsTrackable: false);

            ViewBag.PortPool = new SelectList(_mapper.Map<List<PortPoolViewModel>>(portPools), "Name", "Name", selectedPortPool);
        }

        private async Task PopulateAttachmentRolesDropDownList(string portPoolName, int? deviceRoleId = null, object selectedAttachmentRole = null)
        {
            var query = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                            q =>
                            q.PortPool.Name == portPoolName,
                            query: q => q.Include(x => x.DeviceRoleAttachmentRoles))
                         select result);

            if (deviceRoleId.HasValue) query = query.Where(
                                                        x =>
                                                        x.DeviceRoleAttachmentRoles
                                                    .Any(q =>
                                                         q.DeviceRoleID == deviceRoleId));

            var attachmentRoles = query.ToList();
            ViewBag.AttachmentRole = new SelectList(_mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles),
                "Name", "Name", selectedAttachmentRole);
        }
    }
}
