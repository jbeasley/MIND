using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class ContractBandwidthPoolViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContractBandwidthPoolViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(ContractBandwidthComponentViewModel model)
        {
            // If contract bandwidth pool is supplied then return it for rendering the view
            if (model.ContractBandwidthPool != null)
            {
                await PopulateContractBandwidthsDropDownList(model.ContractBandwidthPool.ContractBandwidthMbps);
                return View(model.ContractBandwidthPool);
            }

            if (!string.IsNullOrEmpty(model.PortPoolName) && !string.IsNullOrEmpty(model.AttachmentRoleName))
            {
                return await GetViewByAttachmentRoleAsync(model.PortPoolName, model.AttachmentRoleName);
            }

            if (!string.IsNullOrEmpty(model.AttachmentRoleName) && !string.IsNullOrEmpty(model.VifRoleName))
            {
                return await GetViewByVifRoleAsync(model.AttachmentRoleName, model.VifRoleName);
            }

            if (model.AttachmentId.HasValue)
            {
                return await GetViewByAttachmentIdAsync(model.AttachmentId.Value);
            }

            if (model.VifId.HasValue)
            {
                return await GetViewByVifIdAsync(model.VifId.Value);
            }

            // Default - return null to indicate no contract bandwidth is required
            return View(model: null as ContractBandwidthPoolViewModel);
        }

        /// <summary>
        /// </summary>
        /// <returns>The view by attachment identifier</returns>
        /// <param name="attachmentId">Attachment identifier.</param>
        private async Task<IViewComponentResult> GetViewByAttachmentIdAsync(int attachmentId)
        {
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                          q =>
                              q.AttachmentID == attachmentId,
                              query: q => q.Include(x => x.ContractBandwidthPool.ContractBandwidth)
                                           .Include(x => x.AttachmentRole),
                              AsTrackable: false)
                              select result)
                             .SingleOrDefault();

            if (attachment == null || !attachment.AttachmentRole.RequireContractBandwidth)
            {
                return View(model: null as ContractBandwidthPoolViewModel);
            }

            await PopulateContractBandwidthsDropDownList(attachment.ContractBandwidthPool.ContractBandwidth.BandwidthMbps);
            return View(model: _mapper.Map<ContractBandwidthPoolViewModel>(attachment.ContractBandwidthPool));
        }

        /// <summary>
        /// </summary>
        /// <returns>The view by vif identifier async.</returns>
        /// <param name="vifId">Vif identifier.</param>
        private async Task<IViewComponentResult> GetViewByVifIdAsync(int vifId)
        {
            var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                  q => q.VifID == vifId,
                       query: q => q.Include(x => x.VifRole)
                       .Include(x => x.ContractBandwidthPool.ContractBandwidth),
                       AsTrackable: false)
                       select result)
                       .SingleOrDefault();

            if (vif == null || !vif.VifRole.RequireContractBandwidth)
            {
                return View(model: null as ContractBandwidthViewModel);
            }

            await PopulateContractBandwidthsDropDownList(vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps);
            return View(model: _mapper.Map<ContractBandwidthPoolViewModel>(vif.ContractBandwidthPool));
        }

        /// <summary>
        /// </summary>
        /// <returns>The view by attachment role async.</returns>
        /// <param name="portPoolName">Port pool name.</param>
        /// <param name="attachmentRoleName">Attachment role name.</param>
        private async Task<IViewComponentResult> GetViewByAttachmentRoleAsync(string portPoolName, string attachmentRoleName)
        {
            var attachmentRole = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                                q =>
                                  q.Name == attachmentRoleName &&
                                  q.PortPool.Name == portPoolName)
                                  select result)
                                 .SingleOrDefault();

            if (attachmentRole == null) return View(model: null as ContractBandwidthViewModel);
            if (attachmentRole.RequireContractBandwidth)
            {
                await PopulateContractBandwidthsDropDownList();
                return View(model: new ContractBandwidthPoolViewModel());
            }

            return View(model: null as ContractBandwidthPoolViewModel);
        }

        /// <summary>
        /// </summary>
        /// <returns>The view by vif role async.</returns>
        /// <param name="attachmentRoleName">Attachment role name.</param>
        /// <param name="vifRoleName">Vif role name.</param>
        private async Task<IViewComponentResult> GetViewByVifRoleAsync(string attachmentRoleName, string vifRoleName)
        {
            var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                         q =>
                           q.Name == vifRoleName &&
                           q.AttachmentRole.Name == attachmentRoleName)
                           select result)
                           .SingleOrDefault();

            if (vifRole == null) return View(model: null as ContractBandwidthPoolViewModel);
            if (vifRole.RequireContractBandwidth)
            {
                await PopulateContractBandwidthsDropDownList();
                return View(model: new ContractBandwidthPoolViewModel());
            }

            return View(model: null as ContractBandwidthPoolViewModel);
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
