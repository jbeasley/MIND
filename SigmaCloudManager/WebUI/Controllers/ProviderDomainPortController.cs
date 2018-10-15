using Microsoft.AspNetCore.Mvc;
using SCM.Controllers;
using SCM.Models;
using SCM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainPortController : BaseViewController
    {

        [HttpGet]
        public async Task<PartialViewResult> Devices(int? locationId, int? planeId)
        {
            var query = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                        q.LocationID == locationId,
                        AsTrackable: false)
                         select result);

            if (planeId.HasValue) query = query.Where(x => x.PlaneID == planeId);
            var devices = query.ToList();

            return PartialView(Mapper.Map<List<DeviceViewModel>>(devices));
        }

        [HttpGet]
        public async Task<PartialViewResult> Ports(int? deviceId, int? portId)
        {
            var port = await _unitOfWork.PortRepository.GetByIDAsync(portId);
            var result = await _unitOfWork.PortRepository.GetAsync(
                q =>
                q.DeviceID == deviceId &&
                q.PortStatus.PortStatusType == PortStatusTypeEnum.Free &&
                q.ID != port.ID,
                AsTrackable: false);

            return PartialView(Mapper.Map<List<PortViewModel>>(result));
        }

        [HttpGet]
        [ValidateProviderDomainInterfaceExists]
        protected async Task<IActionResult> GetAllPortsByInterfaceId(int? interfaceId)
        {
            var iface = (from result in await _unitOfWork.InterfaceRepository.GetAsync(
                      q =>
                         q.InterfaceID == interfaceId,
                         query: q => q.IncludeDeepProperties(),
                         AsTrackable: false)
                         select result)
                         .SingleOrDefault();

            var attachment = await _attachmentService.GetByIDAsync(iface.AttachmentID, deep: true);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Interface = Mapper.Map<InterfaceViewModel>(iface);

            return View(Mapper.Map<List<PortViewModel>>(iface.Ports));
        }

        protected async Task<ActionResult> Edit(int? portId, AttachmentPortUpdateViewModel updateModel)
        {
            var currentInterface = await InterfaceService.GetByIDAsync(updateModel.InterfaceID);
            if (currentInterface == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var portUpdate = Mapper.Map<AttachmentPortUpdate>(updateModel);
                    await AttachmentValidator.ValidateChangesAsync(portUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await AttachmentService.UpdateAttachmentPortAsync(portUpdate);
                        return RedirectToAction("GetAllPortsByInterfaceID", new
                        {
                            id = currentInterface.InterfaceID,
                            showWarningMessage = true
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedDeviceID = (int)exceptionEntry.Property("DeviceID").CurrentValue;
                if (currentPort.Device.DeviceID != proposedDeviceID)
                {
                    ModelState.AddModelError("DeviceID", $"Current value: {currentPort.Device.Name}");
                }

                var proposedPortID = (int)exceptionEntry.Property("PortID").CurrentValue;
                if (currentPort.ID != proposedPortID)
                {
                    ModelState.AddModelError("PortID", $"Current value: {currentPort.FullName}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            var attachment = await AttachmentService.GetByIDAsync(currentInterface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Interface = Mapper.Map<InterfaceViewModel>(currentInterface);
            var device = currentInterface.Device;
            await PopulatePortsDropDownList(updateModel.DeviceID, currentPort.PortPoolID, updateModel.PortID);

            return View(Mapper.Map<AttachmentPortUpdateViewModel>(currentPort));
        }

        [HttpGet]
        [ValidateProviderDomainPortExists]
        public async Task<ActionResult> Edit (int? portId)
        {
            var attachmentInterface = await InterfaceService.GetByIDAsync(currentPort.InterfaceID.Value);
            var attachment = await AttachmentService.GetByIDAsync(attachmentInterface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            ViewBag.Interface = Mapper.Map<InterfaceViewModel>(attachmentInterface);
            var device = attachmentInterface.Device;
            await PopulatePortsDropDownList(device.DeviceID, currentPort.PortPoolID, currentPort.ID);

            return View(Mapper.Map<AttachmentPortUpdateViewModel>(currentPort));
        }

        protected async Task PopulateDevicesByLocationDropDownList(int locationID, int? planeID = null, object selectedDevice = null)
        {
            var devices = await DeviceService.GetAllByLocationIDAsync(locationID, planeID);
            ViewBag.DeviceID = new SelectList(Mapper.Map<List<DeviceViewModel>>(devices), "DeviceID", "Name", selectedDevice);
        }

        protected async Task PopulatePortsDropDownList(int deviceID, int? portPoolID, int currentPortID)
        {
            var ports = await PortService.GetAllByDeviceIDAsync(deviceID, portPoolID);
            var result = ports.Where(x => x.PortStatus.PortStatusType == Models.PortStatusTypeEnum.Free || x.ID == currentPortID);
            ViewBag.PortID = new SelectList(Mapper.Map<List<PortViewModel>>(result), "ID", "FullName", currentPortID);
        }

    }
}
