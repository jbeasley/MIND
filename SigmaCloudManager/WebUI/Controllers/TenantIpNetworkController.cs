using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using Microsoft.EntityFrameworkCore;
using SCM.Validators;
using Mind.Services;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class TenantIpNetworkController : BaseViewController
    {
        private readonly ITenantIpNetworkService _tenantIpNetworkService;
        private readonly IVpnService _vpnService;
        private readonly ITenantService _tenantService;

        public TenantIpNetworkController(ITenantIpNetworkService tenantIpNetworkService,
            IVpnService vpnService,
            ITenantService tenantService,
            IMapper mapper,
            ITenantIpNetworkValidator tenantIpNetworkValidator) : base(tenantIpNetworkService, mapper)
        {
            _tenantIpNetworkService = tenantIpNetworkService;
            _vpnService = vpnService;
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(int? id, string searchString = "", bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantIpNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(id.Value, searchString);
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var tenant = await _tenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantIpNetworks));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _tenantIpNetworkService.GetByIDAsync(id.Value, deep: true);
            if (item == null)
            {
                return NotFound();
            }

            var tenant = await _tenantService.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantIpNetworkViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _tenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,Ipv4Prefix,Ipv4Length,Ipv4LessThanOrEqualToLength,AllowExtranet")] TenantIpNetworkViewModel tenantIpNetworkModel)
        {
            if (ModelState.IsValid)
            {
                var tenantIpNetwork = Mapper.Map<TenantIpNetwork>(tenantIpNetworkModel);

                try
                {
                    await _tenantIpNetworkService.AddAsync(Mapper.Map<TenantIpNetwork>(tenantIpNetworkModel));
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantIpNetworkModel.TenantID
                    });
                }

                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }

            var tenant = await _tenantService.GetByIDAsync(tenantIpNetworkModel.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantIpNetworkViewModel>(tenantIpNetworkModel));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(id.Value, deep: true);
            if (tenantIpNetwork == null)
            {
                return NotFound();
            }

            var tenant = await _tenantService.GetByIDAsync(tenantIpNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantIpNetworkViewModel>(tenantIpNetwork));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("TenantIpNetworkID,TenantID,Ipv4Prefix," +
            "Ipv4Length,Ipv4LessThanOrEqualToLength,AllowExtranet,RowVersion")] TenantIpNetworkViewModel tenantIpNetworkModel)
        {
            if (id != tenantIpNetworkModel.TenantIpNetworkID)
            {
                return NotFound();
            }

            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(id, deep: true);
            if (tenantIpNetwork == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Tenant Network was deleted by another user.");
            }

            try
            {
                var item = Mapper.Map<TenantIpNetwork>(tenantIpNetworkModel);
                await _tenantIpNetworkService.UpdateAsync(item);

                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantIpNetwork.TenantID
                });
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpv4Prefix = (string)exceptionEntry.Property("Ipv4Prefix").CurrentValue;
                if (tenantIpNetwork.Ipv4Prefix != proposedIpv4Prefix)
                {
                    ModelState.AddModelError("Ipv4Prefix", $"Current value: {tenantIpNetwork.Ipv4Prefix}");
                }

                var proposedIpv4Length = (int)exceptionEntry.Property("Ipv4Length").CurrentValue;
                if (tenantIpNetwork.Ipv4Length != proposedIpv4Length)
                {
                    ModelState.AddModelError("Ipv4Length", $"Current value: {tenantIpNetwork.Ipv4Length}");
                }

                var proposedIpv4LessThanOrEqualToLength = (int?)exceptionEntry.Property("Ipv4LessThanOrEqualToLength").CurrentValue;
                if (tenantIpNetwork.Ipv4LessThanOrEqualToLength != proposedIpv4LessThanOrEqualToLength)
                {
                    ModelState.AddModelError("Ipv4LessThanOrEqualToLength", $"Current value: {tenantIpNetwork.Ipv4LessThanOrEqualToLength}");
                }

                var proposedAllowExtranet = (bool)exceptionEntry.Property("AllowExtranet").CurrentValue;
                if (tenantIpNetwork.AllowExtranet != proposedAllowExtranet)
                {
                    ModelState.AddModelError("AllowExtranet", $"Current value: {tenantIpNetwork.AllowExtranet}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /* ex */ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            var tenant = await _tenantService.GetByIDAsync(tenantIpNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantIpNetworkViewModel>(tenantIpNetwork));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? tenantID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(id.Value, deep: true);
            if (tenantIpNetwork == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByTenantID", new { id = tenantID });
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

            var tenant = await _tenantService.GetByIDAsync(tenantIpNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantIpNetworkViewModel>(tenantIpNetwork));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantIpNetworkViewModel tenantIpNetworkModel)
        {
            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(tenantIpNetworkModel.TenantIpNetworkID, deep: true);
            if (tenantIpNetwork == null)
            {
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantIpNetworkModel.TenantID
                });
            }

            try
            {
                await _tenantIpNetworkService.DeleteAsync(tenantIpNetworkModel.TenantIpNetworkID);
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantIpNetworkModel.TenantID
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = tenantIpNetworkModel.TenantIpNetworkID,
                    tenantID = tenantIpNetworkModel.TenantID
                });
            }

            catch (ServiceValidationException)
            {
                var tenant = await _tenantService.GetByIDAsync(tenantIpNetwork.TenantID);
                ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

                return View(Mapper.Map<TenantIpNetworkViewModel>(tenantIpNetwork));
            }
        }
    }
}
