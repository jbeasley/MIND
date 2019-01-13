using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class VifRoleViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VifRoleViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(VifRoleComponentViewModel model)
        {
            if (model != null)
            {
                if (model.AttachmentRoleId.HasValue)
                {
                    await PopulateVifRolesDropDownList(model.AttachmentRoleId.Value, model?.VifRoleName);
                }
            }

            return View(model);
        }

        private async Task PopulateVifRolesDropDownList(int attachmentRoleId, object selectedVifRole = null)
        {
            var vifRoles = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                         q =>
                            q.AttachmentRoleID == attachmentRoleId)
                            select result)
                            .ToList();

            ViewBag.VifRole = new SelectList(_mapper.Map<List<VifRoleViewModel>>(vifRoles),
                "Name", "Name", selectedVifRole);
        }
    }
}
