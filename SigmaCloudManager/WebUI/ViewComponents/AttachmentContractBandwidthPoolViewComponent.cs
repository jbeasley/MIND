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
    public class AttachmentContractBandwidthPoolViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ContractBandwidthPoolViewModel _model;

        public AttachmentContractBandwidthPoolViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _model = new ContractBandwidthPoolViewModel();
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentId, string portPoolName, string attachmentRoleName,
            int? attachmentBandwidthGbps, ContractBandwidthPoolViewModel currentModel)
        {
            if (attachmentId.HasValue)
            {
                var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                              q =>
                                  q.AttachmentID == attachmentId,
                                  query: q => q.Include(x => x.ContractBandwidthPool.ContractBandwidth)
                                               .Include(x => x.Interfaces)
                                               .Include(x => x.AttachmentRole),
                                  AsTrackable: false)
                                  select result)
                                 .SingleOrDefault();

                // MUST pass null as the model to the view - see https://github.com/aspnet/Announcements/issues/221
                if (attachment == null) return View(model: null as ContractBandwidthViewModel);
                if (attachment.AttachmentRole.RequireContractBandwidth)
                {
                    await PopulateContractBandwidthsDropDownList(attachment.ContractBandwidthPool.ContractBandwidth.BandwidthMbps);
                    _model.TrustReceivedCosAndDscp = attachment.ContractBandwidthPool.TrustReceivedCosAndDscp;

                    return View(_model);
                }
                else
                {
                    return View(model: null as ContractBandwidthViewModel);
                }
            }

            var attachmentRole = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                                  q =>
                                  q.PortPool.Name == portPoolName &&
                                  q.Name == attachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null) return View(model: null as ContractBandwidthViewModel);
            if (!attachmentRole.IsLayer3Role) return View(model: null as ContractBandwidthViewModel);

            if (attachmentRole.RequireContractBandwidth) await PopulateContractBandwidthsDropDownList();
            if (currentModel != null) return View(currentModel);

            return View(_model);
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
