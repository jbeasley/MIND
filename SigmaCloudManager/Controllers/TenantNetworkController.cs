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

namespace SCM.Controllers
{
    public class TenantNetworkController : BaseViewController
    {
        public TenantNetworkController(ITenantNetworkService tenantNetworkService,
            IVpnService vpnService,
            ITenantService tenantService,
            IMapper mapper,
            ITenantNetworkValidator tenantNetworkValidator)
        {
            TenantNetworkService = tenantNetworkService;
            VpnService = vpnService;
            TenantService = tenantService;
            Mapper = mapper;
            TenantNetworkValidator = tenantNetworkValidator;
            this.Validator = tenantNetworkValidator;
        }

        private ITenantNetworkService TenantNetworkService { get; set; }
        private IVpnService VpnService { get; set; }
        private ITenantService TenantService { get; set; }
        private IMapper Mapper { get; set; }
        private ITenantNetworkValidator TenantNetworkValidator { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(int? id, string searchString = "", bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantNetworks = await TenantNetworkService.GetAllByTenantIDAsync(id.Value, searchString);
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<List<TenantNetworkViewModel>>(tenantNetworks));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await TenantNetworkService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantNetworkViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,IpPrefix,Length,LessThanOrEqualToLength,AllowExtranet")] TenantNetworkViewModel tenantNetworkModel)
        {
            if (ModelState.IsValid)
            {
                // Normalise the IP Prefix according to the Cidr length.
                // e.g. - 10.1.1.0/16 becomes 10.1.0.0/16

                var network = IPNetwork.Parse($"{tenantNetworkModel.IpPrefix}/{tenantNetworkModel.Length}");
                tenantNetworkModel.IpPrefix = network.Network.ToString();

                var tenantNetwork = Mapper.Map<TenantNetwork>(tenantNetworkModel);
                await TenantNetworkValidator.ValidateNewAsync(tenantNetwork);

                if (TenantNetworkValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await TenantNetworkService.AddAsync(Mapper.Map<TenantNetwork>(tenantNetworkModel));
                        return RedirectToAction("GetAllByTenantID", new
                        {
                            id = tenantNetworkModel.TenantID
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
            }

            var tenant = await TenantService.GetByIDAsync(tenantNetworkModel.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantNetworkViewModel>(tenantNetworkModel));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantNetwork = await TenantNetworkService.GetByIDAsync(id.Value);
            if (tenantNetwork == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(tenantNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantNetworkViewModel>(tenantNetwork));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("TenantNetworkID,TenantID,IpPrefix," +
            "Length,LessThanOrEqualToLength,AllowExtranet,RowVersion")] TenantNetworkViewModel tenantNetworkModel)
        {
            if (id != tenantNetworkModel.TenantNetworkID)
            {
                return NotFound();
            }

            var tenantNetwork = await TenantNetworkService.GetByIDAsync(id);
            if (tenantNetwork == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Tenant Network was deleted by another user.");
            }

            try
            {
                // Normalise the IP Prefix according to the Cidr length.
                // e.g. - 10.1.1.0/16 becomes 10.1.0.0/16

                var network = IPNetwork.Parse($"{tenantNetworkModel.IpPrefix}/{tenantNetworkModel.Length}");
                tenantNetworkModel.IpPrefix = network.Network.ToString();

                var item = Mapper.Map<TenantNetwork>(tenantNetworkModel);
                await TenantNetworkValidator.ValidateChangesAsync(item);

                if (TenantNetworkValidator.ValidationDictionary.IsValid)
                {
                    await TenantNetworkService.UpdateAsync(item);

                    // Check if VPNs need re-sync to network as a result of the change to the Tenant Network
                    // and generate a message for the view if so

                    var vpns = await VpnService.GetAllByTenantNetworkIDAsync(tenantNetwork.TenantNetworkID);

                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantNetwork.TenantID,
                        showWarningMessage = vpns.Any()
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpPrefix = (string)exceptionEntry.Property("IpPrefix").CurrentValue;
                if (tenantNetwork.IpPrefix != proposedIpPrefix)
                {
                    ModelState.AddModelError("IpPrefix", $"Current value: {tenantNetwork.IpPrefix}");
                }

                var proposedLength = (int)exceptionEntry.Property("Length").CurrentValue;
                if (tenantNetwork.Length != proposedLength)
                {
                    ModelState.AddModelError("Length", $"Current value: {tenantNetwork.Length}");
                }

                var proposedLessThanOrEqualToLength = (int?)exceptionEntry.Property("LessThanOrEqualToLength").CurrentValue;
                if (tenantNetwork.LessThanOrEqualToLength != proposedLessThanOrEqualToLength)
                {
                    ModelState.AddModelError("LessThanOrEqualToLength", $"Current value: {tenantNetwork.LessThanOrEqualToLength}");
                }

                var proposedAllowExtranet = (bool)exceptionEntry.Property("AllowExtranet").CurrentValue;
                if (tenantNetwork.AllowExtranet != proposedAllowExtranet)
                {
                    ModelState.AddModelError("AllowExtranet", $"Current value: {tenantNetwork.AllowExtranet}");
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

            var tenant = await TenantService.GetByIDAsync(tenantNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantNetworkViewModel>(tenantNetwork));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? tenantID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantNetwork = await TenantNetworkService.GetByIDAsync(id.Value);
            if (tenantNetwork == null)
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

            var tenant = await TenantService.GetByIDAsync(tenantNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantNetworkViewModel>(tenantNetwork));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantNetworkViewModel tenantNetworkModel)
        {
            var tenantNetwork = await TenantNetworkService.GetByIDAsync(tenantNetworkModel.TenantNetworkID);
            if (tenantNetwork == null)
            {
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantNetworkModel.TenantID
                });
            }

            try
            {
                this.Validator.ValidationDictionary.Clear();
                await TenantNetworkValidator.ValidateDeleteAsync(tenantNetwork);
                if (TenantNetworkValidator.ValidationDictionary.IsValid)
                {
                    await TenantNetworkService.DeleteAsync(Mapper.Map<TenantNetwork>(tenantNetworkModel));
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantNetworkModel.TenantID
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = tenantNetworkModel.TenantNetworkID,
                    tenantID = tenantNetworkModel.TenantID
                });
            }

            var tenant = await TenantService.GetByIDAsync(tenantNetwork.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantNetworkViewModel>(tenantNetwork));

        }
    }
}
