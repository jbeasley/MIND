using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class LocationController : BaseViewController
    {
        public LocationController(ILocationService locationService,
            ISubRegionService subRegionService,
            ILocationValidator locationValidator,
            IMapper mapper)
        {
            LocationService = locationService;
            SubRegionService = subRegionService;
            LocationValidator = locationValidator;
            this.Validator = locationValidator;
            Mapper = mapper;
        }

        private ILocationService LocationService { get; }
        private ISubRegionService SubRegionService { get; }
        private ILocationValidator LocationValidator { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await LocationService.GetAllAsync();
            return View(Mapper.Map<List<LocationViewModel>>(locations));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBySubRegionID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRegion = await SubRegionService.GetByIDAsync(id.Value);
            if (subRegion == null)
            {
                return NotFound();
            }

            ViewBag.SubRegion = subRegion;
            var locations = await LocationService.GetAllBySubRegionIDAsync(id.Value);
            return View(Mapper.Map<List<LocationViewModel>>(locations));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await LocationService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var subRegion = await SubRegionService.GetByIDAsync(item.SubRegionID);
            ViewBag.SubRegion = subRegion;

            return View(Mapper.Map<LocationViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            var subRegion = await SubRegionService.GetByIDAsync(id.Value);
            if (subRegion == null)
            {
                return NotFound();
            }

            ViewBag.SubRegion = subRegion;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteName,AutonomousSystemNumber,Number,SubRegionID")] LocationViewModel location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await LocationService.AddAsync(Mapper.Map<Location>(location));
                    return RedirectToAction("GetAllBySubRegionID", new { id = location.SubRegionId });
                }

                catch (DbUpdateException /** ex **/ )
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }

            var subRegion = await SubRegionService.GetByIDAsync(location.SubRegionId);
            ViewBag.SubRegion = subRegion;

            return View(Mapper.Map<LocationViewModel>(location));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await LocationService.GetByIDAsync(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            var subRegion = await SubRegionService.GetByIDAsync(location.SubRegionID);
            ViewBag.SubRegion = subRegion;

            return View(Mapper.Map<LocationViewModel>(location));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("LocationID,SiteName,AutonomousSystemNumber,Number,SubRegionID,RowVersion")]
            LocationViewModel locationModel)
        {
            if (id != locationModel.LocationId)
            {
                return NotFound();
            }

            var location = await LocationService.GetByIDAsync(locationModel.LocationId);
            if (location == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Location was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var updateLocation = Mapper.Map<Location>(locationModel);
                    await LocationService.UpdateAsync(updateLocation);

                    return RedirectToAction("GetAllBySubRegionID", new { id = updateLocation.SubRegionID });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedSiteName = (string)exceptionEntry.Property("SiteName").CurrentValue;
                if (location.SiteName != proposedSiteName)
                {
                    ModelState.AddModelError("SiteName", $"Current value: {location.SiteName}");
                }

                var proposedAutonomousSystemNumber = (int)exceptionEntry.Property("AutonomousSystemNumber").CurrentValue;
                if (location.AutonomousSystemNumber != proposedAutonomousSystemNumber)
                {
                    ModelState.AddModelError("AutonomousSystemNumber", $"Current value: {location.AutonomousSystemNumber}");
                }

                var proposedNumber = (int)exceptionEntry.Property("Number").CurrentValue;
                if (location.Number != proposedNumber)
                {
                    ModelState.AddModelError("Number", $"Current value: {location.Number}");
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

            var subRegion = await SubRegionService.GetByIDAsync(location.SubRegionID);
            ViewBag.SubRegion = subRegion;

            return View(Mapper.Map<LocationViewModel>(location));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? subRegionID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await LocationService.GetByIDAsync(id.Value);
            if (location == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllBySubRegionID", new { id = subRegionID });
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

            var subRegion = await SubRegionService.GetByIDAsync(location.SubRegionID);
            ViewBag.SubRegion = subRegion;

            return View(Mapper.Map<LocationViewModel>(location));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LocationViewModel locationModel)
        {
            var location = await LocationService.GetByIDAsync(locationModel.LocationId);
            if (location == null)
            {
                return RedirectToAction("GetAllBySubRegionID", new
                {
                    id = locationModel.SubRegionId
                });
            }

            try
            {
                this.Validator.ValidationDictionary.Clear();
                await LocationValidator.ValidateDeleteAsync(location);
                if (LocationValidator.ValidationDictionary.IsValid)
                {
                    await LocationService.DeleteAsync(Mapper.Map<Location>(locationModel));
                    return RedirectToAction("GetAllBySubRegionID", new { id = location.SubRegionID });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = location.LocationID,
                    subRegionID = location.SubRegionID
                });
            }

            var subRegion = await SubRegionService.GetByIDAsync(location.SubRegionID);
            ViewBag.SubRegion = subRegion;

            return View(Mapper.Map<LocationViewModel>(location));
        }
    }
}
