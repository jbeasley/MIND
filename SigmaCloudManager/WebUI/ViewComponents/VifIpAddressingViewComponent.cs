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
    public class VifIpAddressingViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IList<Ipv4AddressAndMaskViewModel> _model;

        public VifIpAddressingViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _model = new List<Ipv4AddressAndMaskViewModel>();
        }

        public async Task<IViewComponentResult> InvokeAsync(int? vifId, int? attachmentId, 
            string vifRoleName, IList<Ipv4AddressAndMaskViewModel> currentModel)
        {
            if (currentModel != null) return View(currentModel);

            if (vifId.HasValue)
            {
                var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                              q =>
                                  q.VifID == vifId,
                                  query: q => q.Include(x => x.Vlans)
                                               .Include(x => x.VifRole),
                                  AsTrackable: false)
                                  select result)
                                 .SingleOrDefault();

                if (vif == null) return Content(string.Empty);
                if (!vif.VifRole.IsLayer3Role) return Content(string.Empty);

                _model = vif.Vlans.Select(
                                   x =>
                                   new Ipv4AddressAndMaskViewModel
                                   {
                                       IpAddress = x.IpAddress,
                                       SubnetMask = x.SubnetMask
                                   })
                                   .ToList();
                return View(_model);
            }

            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                              q =>
                              q.AttachmentID == attachmentId,
                              query: q => q.Include(x => x.AttachmentRole)
                                           .Include(x => x.Interfaces),
                              AsTrackable: false)
                              select result)
                              .SingleOrDefault();

            if (attachment == null) return Content(string.Empty);

            var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                         q =>
                         q.AttachmentRoleID == attachment.AttachmentRoleID &&
                         q.Name == vifRoleName)
                         select result)
                         .SingleOrDefault();

            if (vifRole == null) return Content(string.Empty);
            if (!vifRole.IsLayer3Role) return Content(string.Empty);

            for (var i = 0; i < attachment.Interfaces.Count; i++)
            {
                _model.Add(new Ipv4AddressAndMaskViewModel());
            }

            return View(_model);
        }
    }
}
