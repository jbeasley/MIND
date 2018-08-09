using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using Mind.Services;

namespace SCM.Controllers
{
    public class ExtranetVpnMemberController : BaseViewController
    {
        public ExtranetVpnMemberController(IExtranetVpnMemberService extranetVpnMemberService,
            ITenantService tenantService,
            IVpnService vpnService,
            IMapper mapper,
            IExtranetVpnMemberValidator extranetVpnMemberValidator)
        {
            ExtranetVpnMemberService = extranetVpnMemberService;
            TenantService = tenantService;
            VpnService = vpnService;
            Mapper = mapper;
            ExtranetVpnMemberValidator = extranetVpnMemberValidator;
            this.Validator = extranetVpnMemberValidator;
        }

        private IExtranetVpnMemberService ExtranetVpnMemberService { get; }
        private IVpnService VpnService { get; }
        private ITenantService TenantService { get; }
        private IMapper Mapper { get; }
        private IExtranetVpnMemberValidator ExtranetVpnMemberValidator { get; }

        [HttpGet]
        public async Task<PartialViewResult> MemberVpns(int tenantID)
        {
            var vpns = await VpnService.GetAllByTenantIDAsync(tenantID, isExtranet: false, includeProperties: false);
            return PartialView(Mapper.Map<List<VpnViewModel>>(vpns));
        }

        [HttpGet]
        public async Task<PartialViewResult> ExtranetVpns(int tenantID)
        {
            var vpns = await VpnService.GetAllByTenantIDAsync(tenantID, isExtranet: true, includeProperties: false);
            return PartialView(Mapper.Map<List<VpnViewModel>>(vpns));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByExtranetVpnID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpn = await VpnService.GetByIDAsync(id.Value);
            if (extranetVpn == null)
            {
                return NotFound();
            }

            ViewBag.ExtranetVpn = Mapper.Map<VpnViewModel>(extranetVpn);
            var extranetVpnMembers = await ExtranetVpnMemberService.GetAllByExtranetVpnIDAsync(id.Value);

            return View(Mapper.Map<List<ExtranetVpnMemberViewModel>>(extranetVpnMembers));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByMemberVpnID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberVpn = await VpnService.GetByIDAsync(id.Value);
            if (memberVpn == null)
            {
                return NotFound();
            }

            ViewBag.MemberVpn = Mapper.Map<VpnViewModel>(memberVpn);
            var extranetVpnMembers = await ExtranetVpnMemberService.GetAllByMemberVpnIDAsync(id.Value);

            return View(Mapper.Map<List<ExtranetVpnMemberViewModel>>(extranetVpnMembers));
        }

        [HttpGet]
        public async Task<IActionResult> DetailsByExtranetVpn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await ExtranetVpnMemberService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<ExtranetVpnMemberViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> DetailsByMemberVpn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await ExtranetVpnMemberService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<ExtranetVpnMemberViewModel>(item));
        }
     
        [HttpGet]
        public async Task<IActionResult> CreateByExtranetVpn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpn = await VpnService.GetByIDAsync(id.Value);
            if (extranetVpn == null)
            {
                return NotFound();
            }

            ViewBag.ExtranetVpn = Mapper.Map<VpnViewModel>(extranetVpn);
            await PopulateTenantsDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByExtranetVpn([Bind("TenantID,ExtranetVpnID,MemberVpnID")] ExtranetVpnMemberViewModel extranetVpnMemberModel)
        {
            if (ModelState.IsValid)
            {
                var extranetVpnMember = Mapper.Map<ExtranetVpnMember>(extranetVpnMemberModel);
                await ExtranetVpnMemberValidator.ValidateNewAsync(extranetVpnMember);
                if (ExtranetVpnMemberValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await ExtranetVpnMemberService.AddAsync(Mapper.Map<ExtranetVpnMember>(extranetVpnMember));
                        return RedirectToAction("GetAllByExtranetVpnID", new
                        {
                            id = extranetVpnMember.ExtranetVpnID
                        });
                    }
                    catch (DbUpdateException /** ex **/ )
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }
                }
            }

            var extranetVpn = await VpnService.GetByIDAsync(extranetVpnMemberModel.ExtranetVpnID);
            ViewBag.ExtranetVpn = Mapper.Map<VpnViewModel>(extranetVpn);
            await PopulateTenantsDropDownList(extranetVpnMemberModel.TenantID);
            await PopulateMemberVpnsDropDownList(extranetVpnMemberModel.TenantID, extranetVpnMemberModel.ExtranetVpnID);

            return View(extranetVpnMemberModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateByMemberVpn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberVpn = await VpnService.GetByIDAsync(id.Value);
            if (memberVpn == null)
            {
                return NotFound();
            }

            ViewBag.MemberVpn = Mapper.Map<VpnViewModel>(memberVpn);
            await PopulateTenantsDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByMemberVpn([Bind("TenantID,ExtranetVpnID,MemberVpnID")] ExtranetVpnMemberViewModel extranetVpnMemberModel)
        {
            if (ModelState.IsValid)
            {
                var extranetVpnMember = Mapper.Map<ExtranetVpnMember>(extranetVpnMemberModel);
                await ExtranetVpnMemberValidator.ValidateNewAsync(extranetVpnMember);
                if (ExtranetVpnMemberValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await ExtranetVpnMemberService.AddAsync(Mapper.Map<ExtranetVpnMember>(extranetVpnMember));
                        return RedirectToAction("GetAllByMemberVpnID", new
                        {
                            id = extranetVpnMember.MemberVpnID
                        });
                    }
                    catch (DbUpdateException /** ex **/ )
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }
                }
            }

            var memberVpn = await VpnService.GetByIDAsync(extranetVpnMemberModel.MemberVpnID);
            ViewBag.MemberVpn = Mapper.Map<VpnViewModel>(memberVpn);
            await PopulateTenantsDropDownList(extranetVpnMemberModel.TenantID);
            await PopulateExtranetVpnsDropDownList(extranetVpnMemberModel.TenantID, extranetVpnMemberModel.MemberVpnID);

            return View(extranetVpnMemberModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteByExtranetVpn(int? id, int? extranetVpnID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (extranetVpnID == null)
            {
                return NotFound();
            }


            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(id.Value);
            if (extranetVpnMember == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByExtranetVpnID", new
                    {
                        id = extranetVpnID
                    });
                }

                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was cancelled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteByExtranetVpn(ExtranetVpnMemberViewModel extranetVpnMemberModel)
        {
            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnMemberModel.ExtranetVpnMemberID);
            if (extranetVpnMember == null)
            {
                return RedirectToAction("GetAllByExtranetVpnID", new
                {
                    id = extranetVpnMemberModel.ExtranetVpnID
                });
            }

            try
            {
                await ExtranetVpnMemberService.DeleteAsync(extranetVpnMember);
                return RedirectToAction("GetAllByExtranetVpnID", new
                {
                    id = extranetVpnMemberModel.ExtranetVpnID
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("DeleteByExtranetVpn", new
                {
                    concurrencyError = true,
                    id = extranetVpnMemberModel.ExtranetVpnMemberID,
                    extranetVpnID = extranetVpnMemberModel.ExtranetVpnID
                });
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            return View(Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteByMemberVpn(int? id, int? memberVpnID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (memberVpnID == null)
            {
                return NotFound();
            }


            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(id.Value);
            if (extranetVpnMember == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByMemberVpnID", new
                    {
                        id = memberVpnID
                    });
                }

                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was cancelled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteByMemberVpn(ExtranetVpnMemberViewModel extranetVpnMemberModel)
        {
            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnMemberModel.ExtranetVpnMemberID);
            if (extranetVpnMember == null)
            {
                return RedirectToAction("GetAllByMemberVpnID", new
                {
                    id = extranetVpnMemberModel.MemberVpnID
                });
            }

            try
            {
                await ExtranetVpnMemberService.DeleteAsync(extranetVpnMember);
                return RedirectToAction("GetAllByMemberVpnID", new
                {
                    id = extranetVpnMemberModel.MemberVpnID
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("DeleteByMemberVpn", new
                {
                    concurrencyError = true,
                    id = extranetVpnMemberModel.ExtranetVpnMemberID,
                    memberVpnID = extranetVpnMemberModel.MemberVpnID
                });
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";
            }

            return View(Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember));
        }

        private async Task PopulateTenantsDropDownList(object selectedTenant = null)
        {
            var tenants = await TenantService.GetAllAsync();
            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants), "TenantID", "Name", selectedTenant);
        }

        private async Task PopulateMemberVpnsDropDownList(int tenantID, object selectedVpn = null)
        {
            var vpns = await VpnService.GetAllByTenantIDAsync(tenantID, isExtranet: false);
            ViewBag.MemberVpnID = new SelectList(Mapper.Map<List<VpnViewModel>>(vpns), "VpnID", "Name", selectedVpn);
        }

        private async Task PopulateExtranetVpnsDropDownList(int tenantID, object selectedVpn = null)
        {
            var vpns = await VpnService.GetAllByTenantIDAsync(tenantID, isExtranet: true);
            ViewBag.ExtranetVpnID = new SelectList(Mapper.Map<List<VpnViewModel>>(vpns), "VpnID", "Name", selectedVpn);
        }
    }
}