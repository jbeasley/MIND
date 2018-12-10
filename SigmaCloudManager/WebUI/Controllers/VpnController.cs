using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Controllers;
using Mind.Services;
using SCM.Data;
using Mind.Models;
using Mind.Builders;
using Mind.WebUI.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mind.WebUI.Models;
using Mind.Models.RequestModels;
using Mind.WebUI.ViewComponents;
using SCM.Models;
using IO.NovaVpnSwagger.Client;

namespace Mind.WebUI.Controllers
{
    public class VpnController : BaseViewController
    {
        private readonly IVpnService _vpnService;
        public VpnController(IVpnService vpnService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _vpnService = vpnService;
        }

        [HttpGet]
        [ValidateVpnExists]
        public async Task<IActionResult> Details(int? vpnId)
        {
            var item = await _vpnService.GetByIDAsync(vpnId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<VpnViewModel>(item));
        }

        [HttpPost]
        public IActionResult GetVpnAttachmentSetsGridData([FromBody]List<VpnAttachmentSetRequestViewModel> vpnAttachmentSetRequests)
        {
            return ViewComponent("VpnAttachmentSetsGridData", new { vpnAttachmentSetRequests });
        }

        [HttpGet]
        public async Task<PartialViewResult> AddressFamilies (string protocolType)
        {
            var addressFamilies = await _unitOfWork.AddressFamilyRepository.GetAsync(
                q =>
                q.VpnProtocolType.Name == protocolType,
                AsTrackable: false);

            return PartialView(_mapper.Map<List<AddressFamilyViewModel>>(addressFamilies));
        }

        [HttpGet]
        public async Task<PartialViewResult> TopologyTypes(string protocolType)
        {
            var topologyTypes = await _unitOfWork.VpnTopologyTypeRepository.GetAsync(
                q =>
                q.VpnProtocolType.Name == protocolType,
                AsTrackable: false);

            return PartialView(_mapper.Map<List<VpnTopologyTypeViewModel>>(topologyTypes));
        }

        [HttpGet]
        public async Task<PartialViewResult> ParticipantTenants(int ownerTenantId, string tenancyType)
        {
            var query = (from result in await _unitOfWork.TenantRepository.GetAsync()
                         select result);

            if (tenancyType == TenancyTypeEnum.Single.ToString()) query = query.Where(x => x.TenantID == ownerTenantId);

            return PartialView(_mapper.Map<List<TenantViewModel>>(query.ToList()));
        }

        [HttpGet]
        public async Task<PartialViewResult> AttachmentSets(string tenantName)
        {
            var attachmentSets = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                                  q =>
                                  q.Tenant.Name == tenantName,
                                  query: q => q.IncludeDeepProperties())
                                  select result)
                                  .ToList();

            return PartialView(_mapper.Map<List<AttachmentSetViewModel>>(attachmentSets));
        }

        [HttpGet]
        [ValidateTenantExists]
        [SetTenantCookieState]
        public async Task<IActionResult> GetAllByTenantID(int? tenantId)
        {
            var vpns = await _unitOfWork.VpnRepository.GetAsync(
                    q =>
                    q.TenantID == tenantId.Value,
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);               
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<List<VpnViewModel>>(vpns));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateProtocolsDropDownList();
            await PopulateRegionsDropDownList();
            PopulatePlanesDropDownList();
            PopulateTenancyTypesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, VpnRequestViewModel requestModel, bool? syncToNetwork)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<VpnRequest>(requestModel);

                    var attachment = await _vpnService.AddAsync(tenantId.Value, request, syncToNetwork.GetValueOrDefault());
                    return RedirectToAction(nameof(GetAllByTenantID), new { tenantId });
                }

                catch (BuilderBadArgumentsException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (BuilderUnableToCompleteException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (IllegalStateException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (ServiceBadArgumentsException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (DbUpdateException)
                {
                    ModelState.AddDatabaseUpdateExceptionMessage();
                }

                catch (ApiException)
                {
                    ModelState.AddNovaClientApiExceptionMessage();
                }
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            await PopulateRegionsDropDownList(requestModel.Region);
            PopulateProtocolsDropDownList(requestModel.ProtocolType);
            await PopulateAddressFamiliesDropDownList(requestModel.ProtocolType.ToString(), requestModel.AddressFamily);
            await PopulateTopologyTypesDropDownList(requestModel.ProtocolType.ToString(), requestModel.TopologyType);
            PopulatePlanesDropDownList(requestModel.Plane);
            PopulateTenancyTypesDropDownList(requestModel.TenancyType);
            await PopulateTenantsDropDownList(tenantId.Value, requestModel.TenancyType.ToString());

            return View(requestModel);
        }

        [HttpGet]
        [ValidateVpnExists]
        public async Task<ActionResult> Edit(int? vpnId)
        {
            var vpn = await _vpnService.GetByIDAsync(vpnId.Value, deep: true, asTrackable: false);
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(vpn.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateTenancyTypesDropDownList(vpn.VpnTenancyType.Name);
            await PopulateRegionsDropDownList(vpn.Region);
            await PopulateTenantsDropDownList(vpn.TenantID, vpn.VpnTenancyType.Name);

            return View(_mapper.Map<VpnUpdateViewModel>(vpn));
        }

        [HttpPost]
        [ValidateVpnExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? vpnId, VpnUpdateViewModel update, bool? syncToNetwork)
        {
            var vpn = await _vpnService.GetByIDAsync(vpnId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (vpn.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(vpn.GetConcurrencyToken());
                }
                else
                {
                    var attachmentUpdate = _mapper.Map<VpnUpdate>(update);

                    try
                    {
                        await _vpnService.UpdateAsync(vpnId.Value, attachmentUpdate, syncToNetwork.GetValueOrDefault());
                        return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = vpn.TenantID });
                    }

                    catch (BuilderBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (BuilderUnableToCompleteException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (IllegalStateException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (IllegalDeleteAttemptException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (ServiceBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (DbUpdateException)
                    {
                        ModelState.AddDatabaseUpdateExceptionMessage();
                    }

                    catch (ApiException)
                    {
                        ModelState.AddNovaClientApiExceptionMessage();
                    }
                }
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(vpn.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateTenancyTypesDropDownList(update.TenancyType);
            await PopulateRegionsDropDownList(update.Region);
            await PopulateTenantsDropDownList(vpn.TenantID, update.TenancyType.ToString());

            return View(update);
        }

        [HttpGet]
        [ValidateVpnExists]
        public async Task<IActionResult> Delete(int? vpnId, bool? concurrencyError = false)
        {
            var item = await _vpnService.GetByIDAsync(vpnId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<VpnDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnDeleteViewModel model)
        {
            var vpn = await _vpnService.GetByIDAsync(model.VpnId.Value, deep: true, asTrackable: false);
            if (vpn == null) return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = model.TenantId });

            if (vpn.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    vpnId = vpn.VpnID,
                    concurrencyError = true
                });
            }

            try
            {
                await _vpnService.DeleteAsync(vpn.VpnID);
                return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = vpn.TenantID });
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            catch (ApiException)
            {
                ModelState.AddNovaClientApiExceptionMessage();
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(vpn.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<VpnDeleteViewModel>(vpn));
        }

        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await _unitOfWork.RegionRepository.GetAsync();
            ViewBag.Region = new SelectList(_mapper.Map<List<RegionViewModel>>(regions),
                "Name", "Name", selectedRegion);
        }

        private async Task PopulateAddressFamiliesDropDownList(string protocolType, object selectedAddressFamily = null)
        {
            var addressFamilies = (from result in await _unitOfWork.AddressFamilyRepository.GetAsync(
                    q =>
                    q.VpnProtocolType.Name == protocolType)
                                 select result)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Name })
                    .ToList();

            ViewBag.AddressFamily = new SelectList(addressFamilies, "Value", "Text", selectedAddressFamily);
        }

        private void PopulateTenancyTypesDropDownList(object selectedTenancyType = null)
        {
            IList<SelectListItem> list = Enum.GetValues(typeof(TenancyTypeEnum))
                .Cast<TenancyTypeEnum>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();

            ViewBag.TenancyType = new SelectList(list, "Value", "Text", selectedTenancyType);
        }

        private async Task PopulateTopologyTypesDropDownList(string protocolType, object selectedTopologyType = null)
        {
            var topologyTypes = (from result in await _unitOfWork.VpnTopologyTypeRepository.GetAsync(
                    q =>
                    q.VpnProtocolType.Name == protocolType)
                    select result)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Name})
                    .ToList();

            ViewBag.TopologyType = new SelectList(topologyTypes, "Value", "Text", selectedTopologyType);
        }

        private void PopulatePlanesDropDownList(object selectedPlane = null)
        {
            IList<SelectListItem> list = Enum.GetValues(typeof(PlaneEnum))
                .Cast<PlaneEnum>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }).ToList();

            ViewBag.Plane = new SelectList(list, "Value", "Text", selectedPlane);
        }

        private void PopulateProtocolsDropDownList(object selectedProtocol = null)
        {
            IList<SelectListItem> list = Enum.GetValues(typeof(ProtocolTypeEnum))
                .Cast<ProtocolTypeEnum>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();

            ViewBag.ProtocolType = new SelectList(list, "Value", "Text", selectedProtocol);
        }

        private async Task PopulateTenantsDropDownList(int ownerTenantId, string tenancyType)
        {
            var query = (from result in await _unitOfWork.TenantRepository.GetAsync()
                         select result);

            if (tenancyType == TenancyTypeEnum.Single.ToString()) query = query.Where(x => x.TenantID == ownerTenantId);

            ViewBag.ParticipantTenant = new SelectList(query.ToList(), "Name", "Name");
        }
    }
}