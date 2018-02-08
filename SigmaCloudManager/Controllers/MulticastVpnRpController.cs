using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;

namespace SCM.Controllers
{
    public class MulticastVpnRpController : BaseViewController
    {
        public MulticastVpnRpController(IMulticastVpnRpService multicastVpnRpService,
            IAttachmentSetService attachmentSetService,
            IMapper mapper, 
            IMulticastVpnRpValidator multicastVpnRpValidator)
        {
            MulticastVpnRpService = multicastVpnRpService;
            AttachmentSetService = attachmentSetService;
            Mapper = mapper;

            MulticastVpnRpValidator = multicastVpnRpValidator;
            this.Validator = multicastVpnRpValidator;
        }

        private IMulticastVpnRpService MulticastVpnRpService { get; }
        private IMulticastVpnRpValidator MulticastVpnRpValidator { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id, bool showWarning = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (showWarning)
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var multicastVpnRps = await MulticastVpnRpService.GetAllByAttachmentSetIDAsync(id.Value);
            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);

            return View(Mapper.Map<List<MulticastVpnRpViewModel>>(multicastVpnRps));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await MulticastVpnRpService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(item.AttachmentSetID);
            return View(Mapper.Map<MulticastVpnRpViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentSetID,IpAddress")] MulticastVpnRpViewModel multicastVpnRpModel)
        {
            if (ModelState.IsValid)
            {
                var multicastVpnRp = Mapper.Map<MulticastVpnRp>(multicastVpnRpModel);
                await MulticastVpnRpValidator.ValidateNewAsync(multicastVpnRp);
                if (MulticastVpnRpValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await MulticastVpnRpService.AddAsync(multicastVpnRp);
                        return RedirectToAction("GetAllByAttachmentSetID", new
                        {
                            id = multicastVpnRp.AttachmentSetID,
                            showWarning = true
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

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(multicastVpnRpModel.AttachmentSetID);
            return View("Create", multicastVpnRpModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multicastVpnRp = await MulticastVpnRpService.GetByIDAsync(id.Value);
            if (multicastVpnRp == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(multicastVpnRp.AttachmentSetID);
            return View(Mapper.Map<MulticastVpnRpViewModel>(multicastVpnRp));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("MulticastVpnRpID,AttachmentSetID,IpAddress,RowVersion")] MulticastVpnRpViewModel multicastVpnRpModel)
        {
            if (id != multicastVpnRpModel.MulticastVpnRpID)
            {
                return NotFound();
            }

            var currentMulticastVpnRp = await MulticastVpnRpService.GetByIDAsync(id);
            if (currentMulticastVpnRp == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            var multicastVpnRp = Mapper.Map<MulticastVpnRp>(multicastVpnRpModel);

            try
            {
                await MulticastVpnRpService.UpdateAsync(multicastVpnRp);
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = multicastVpnRp.AttachmentSetID,
                    showWarning = true
                });
            }
            
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedIpAddress = (string)exceptionEntry.Property("IpAddress").CurrentValue;
                if (currentMulticastVpnRp.IpAddress != proposedIpAddress)
                {
                    ModelState.AddModelError("IpAddress", $"Current value: {currentMulticastVpnRp.IpAddress}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(multicastVpnRp.AttachmentSetID);
            return View(Mapper.Map<MulticastVpnRpViewModel>(currentMulticastVpnRp));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multicastVpnRp = await MulticastVpnRpService.GetByIDAsync(id.Value);
            if (multicastVpnRp == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByVpnAttachmenSetID", new
                    {
                        id = multicastVpnRp.AttachmentSetID,
                        showWarning = true
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

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(multicastVpnRp.AttachmentSetID);
            return View(Mapper.Map<MulticastVpnRpViewModel>(multicastVpnRp));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MulticastVpnRpViewModel multicastVpnRpModel)
        {
            var multicastVpnRp = await MulticastVpnRpService.GetByIDAsync(multicastVpnRpModel.MulticastVpnRpID);
            if (multicastVpnRp == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = multicastVpnRpModel.AttachmentSetID
                });
            }

            try
            {

                MulticastVpnRpValidator.ValidationDictionary.Clear();
                await MulticastVpnRpValidator.ValidateDeleteAsync(multicastVpnRp);

                if (MulticastVpnRpValidator.ValidationDictionary.IsValid)
                {
                    await MulticastVpnRpService.DeleteAsync(multicastVpnRp);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = multicastVpnRp.AttachmentSetID,
                        showWarning = true
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = multicastVpnRpModel.MulticastVpnRpID
                });
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";

            }

            ViewBag.AttachmentSet = await AttachmentSetService.GetByIDAsync(multicastVpnRp.AttachmentSetID);
            return View(Mapper.Map<MulticastVpnRpViewModel>(multicastVpnRp));
        }
    }
}
