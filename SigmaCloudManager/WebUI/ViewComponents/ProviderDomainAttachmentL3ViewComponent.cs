using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{ 
    public class ProviderDomainAttachmentL3ViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProviderDomainAttachmentL3ViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string portPoolName, string attachmentRoleName, 
            int? attachmentBandwidthGbps, bool? bundleRequired, bool? multiportRequired)
        {
            var attachmentRole = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                                  q =>
                                  q.PortPool.Name == portPoolName && 
                                  q.Name == attachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole != null)
            {
                if (attachmentRole.IsLayer3Role)
                {
                    if (attachmentBandwidthGbps.HasValue)
                    {
                        var attachmentBandwidth = (from result in await _unitOfWork.AttachmentBandwidthRepository.GetAsync(
                                                q =>
                                                   q.BandwidthGbps == attachmentBandwidthGbps.Value,
                                                   AsTrackable: false)
                                                   select result)
                                                   .SingleOrDefault();

                        if (attachmentBandwidth != null)
                        {
                            var numIpAddressesRequired = 1;
                            if (multiportRequired.GetValueOrDefault()) {
                                {
                                    if (attachmentBandwidth.SupportedByMultiPort)
                                    {
                                        numIpAddressesRequired = attachmentBandwidth.BandwidthGbps / attachmentBandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
                                    }
                                }
                            }

                            var model = new ProviderDomainAttachmentL3RequestViewModel()
                            {
                                NumIpAddressesRequired = numIpAddressesRequired
                            };

                            await PopulateContractBandwidthsDropDownList();
                            return View(model);
                        }
                    }
                }
            }

            return Content(string.Empty);
        }

        private async Task PopulateContractBandwidthsDropDownList(object selectedContractBandwidth = null)
        {
            var contractBandwidths = await _unitOfWork.ContractBandwidthRepository.GetAsync();
            ViewBag.ContractBandwidth = new SelectList(_mapper.Map<List<ContractBandwidthViewModel>>(contractBandwidths)
                .OrderBy(b => b.BandwidthMbps),
                "BandwidthMbps", "BandwidthMbps", selectedContractBandwidth);
        }
    }
}
