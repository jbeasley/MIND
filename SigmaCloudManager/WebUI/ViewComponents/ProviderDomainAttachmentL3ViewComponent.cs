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
    public class ProviderDomainAttachmentL3ViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ProviderDomainAttachmentL3RequestViewModel _model;

        public ProviderDomainAttachmentL3ViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _model = new ProviderDomainAttachmentL3RequestViewModel();
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentId, string portPoolName, string attachmentRoleName, 
            int? attachmentBandwidthGbps, bool? isMultiport, ProviderDomainAttachmentL3RequestViewModel currentL3Model)
        {
            if (attachmentId.HasValue)
            {
                var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                              q =>
                                  q.AttachmentID == attachmentId,
                                  query: q => q.Include(x => x.ContractBandwidthPool.ContractBandwidth)
                                  .Include(x => x.Interfaces),
                                  AsTrackable: false)
                                  select result)
                                 .SingleOrDefault();

                if (attachment == null) return Content(string.Empty);

                _model.ContractBandwidthMbps = attachment.ContractBandwidthPool.ContractBandwidth.BandwidthMbps;
                _model.Ipv4Addresses = attachment.Interfaces
                                                 .Select(x => new Ipv4AddressAndMaskViewModel
                                                 {
                                                     IpAddress = x.IpAddress,
                                                     SubnetMask = x.SubnetMask
                                                 })
                                                 .ToList();
                _model.NumIpAddressesRequired = _model.Ipv4Addresses.Count;
                _model.TrustReceivedCosAndDscp = attachment.ContractBandwidthPool.TrustReceivedCosDscp;

                await PopulateContractBandwidthsDropDownList();

                return View(_model);
            }
            else
            {
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

                var model = new ProviderDomainAttachmentL3RequestViewModel()
                {
                    NumIpAddressesRequired = numIpAddressesRequired
                };

                if (currentL3Model != null)
                {
                    model.ContractBandwidthMbps = currentL3Model.ContractBandwidthMbps;
                    model.Ipv4Addresses = currentL3Model.Ipv4Addresses;
                    model.TrustReceivedCosAndDscp = currentL3Model.TrustReceivedCosAndDscp;
                }

                await PopulateContractBandwidthsDropDownList();
                return View(model);
            }
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
