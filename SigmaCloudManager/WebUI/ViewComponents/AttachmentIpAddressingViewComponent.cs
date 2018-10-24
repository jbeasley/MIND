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
    public class AttachmentIpAddressingViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IList<Ipv4AddressAndMaskViewModel> _model;

        public AttachmentIpAddressingViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _model = new List<Ipv4AddressAndMaskViewModel>();
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentId, string portPoolName, string attachmentRoleName,
            int? attachmentBandwidthGbps, bool? isMultiport, IList<Ipv4AddressAndMaskViewModel> currentModel)
        {
            if (currentModel != null) return View(currentModel);

            if (attachmentId.HasValue)
            {
                var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                              q =>
                                  q.AttachmentID == attachmentId,
                                  query: q => q.Include(x => x.Interfaces)
                                               .Include(x => x.AttachmentRole),
                                  AsTrackable: false)
                                  select result)
                                 .SingleOrDefault();

                if (attachment == null) return Content(string.Empty);
                if (!attachment.AttachmentRole.IsLayer3Role) return Content(string.Empty);

                _model = attachment.Interfaces.Select(
                                                    x =>
                                                    new Ipv4AddressAndMaskViewModel
                                                    {
                                                        IpAddress = x.IpAddress,
                                                        SubnetMask = x.SubnetMask
                                                    })
                                                 .ToList();
                return View(_model);
            }

            var attachmentRole = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                                  q =>
                                  q.PortPool.Name == portPoolName &&
                                  q.Name == attachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null) return Content(string.Empty);
            if (!attachmentRole.IsLayer3Role) return Content(string.Empty);

            var attachmentBandwidth = (from result in await _unitOfWork.AttachmentBandwidthRepository.GetAsync(
                                       q =>
                                       q.BandwidthGbps == attachmentBandwidthGbps,
                                       AsTrackable: false)
                                       select result)
                                      .SingleOrDefault();

            if (attachmentBandwidth == null) return Content(string.Empty);

            var numIpAddressesRequired = 1;
            if (isMultiport.GetValueOrDefault())
            {
                if (attachmentBandwidth.SupportedByMultiPort)
                {
                    numIpAddressesRequired = attachmentBandwidth.BandwidthGbps / attachmentBandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
                }
            }

            for (var i = 0; i < numIpAddressesRequired; i++)
            {
                _model.Add(new Ipv4AddressAndMaskViewModel());
            }

            return View(_model);
        }
    }
}
