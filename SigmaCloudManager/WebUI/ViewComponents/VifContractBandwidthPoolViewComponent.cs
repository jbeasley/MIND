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
    public class VifContractBandwidthPoolViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ContractBandwidthPoolViewModel _model;

        public VifContractBandwidthPoolViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _model = new ContractBandwidthPoolViewModel();
        }

        public async Task<IViewComponentResult> InvokeAsync(int? vifId, int? attachmentId, string vifRoleName, ContractBandwidthPoolViewModel currentModel)
        {
            if (vifId.HasValue)
            {
                var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                              q =>
                                  q.VifID == vifId,
                                  query: q => q.Include(x => x.ContractBandwidthPool.ContractBandwidth)
                                               .Include(x => x.Vlans)
                                               .Include(x => x.VifRole),
                                  AsTrackable: false)
                                  select result)
                                 .SingleOrDefault();

                if (vif == null) return Content(string.Empty);
                if (vif.VifRole.RequireContractBandwidth)
                {
                    await PopulateContractBandwidthsDropDownList(vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps);
                    _model.TrustReceivedCosAndDscp = vif.ContractBandwidthPool.TrustReceivedCosAndDscp;

                    return View(_model);
                }
                else
                {
                    return Content(string.Empty);
                }
            }

            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                  q =>
                  q.AttachmentID == attachmentId,
                  query: q => q.Include(x => x.AttachmentRole),
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

            if (vifRole.RequireContractBandwidth) await PopulateContractBandwidthsDropDownList();
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
