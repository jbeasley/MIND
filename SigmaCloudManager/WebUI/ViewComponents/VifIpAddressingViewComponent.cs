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

                // MUST pass null as the model to the view - see https://github.com/aspnet/Announcements/issues/221
                if (vif == null) return View(model: null as List<Ipv4AddressAndMaskViewModel>);
                if (!vif.VifRole.IsLayer3Role) return View(model: null as List<Ipv4AddressAndMaskViewModel>);

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

            if (attachment == null) return View(model: null as List<Ipv4AddressAndMaskViewModel>);

            var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                         q =>
                         q.AttachmentRoleID == attachment.AttachmentRoleID &&
                         q.Name == vifRoleName)
                         select result)
                         .SingleOrDefault();

            if (vifRole == null) return View(model: null as List<Ipv4AddressAndMaskViewModel>);
            if (!vifRole.IsLayer3Role) return View(model: null as List<Ipv4AddressAndMaskViewModel>);

            for (var i = 0; i < attachment.Interfaces.Count; i++)
            {
                _model.Add(new Ipv4AddressAndMaskViewModel());
            }

            return View(_model);
        }
    }
}
