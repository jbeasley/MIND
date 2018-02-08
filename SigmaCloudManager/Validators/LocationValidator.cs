using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Locations
    /// </summary>
    public class LocationValidator : BaseValidator, ILocationValidator
    {
        public LocationValidator(ILocationService locationService, IDeviceService deviceService)
        {
            LocationService = locationService;
            DeviceService = deviceService;
        }

        private ILocationService LocationService { get; }
        private IDeviceService DeviceService { get; }

        /// <summary>
        /// Validate deletion of a Location. The Location cannot be deleted if any Devices
        /// belong to the Location.
        /// </summary>
        /// <param name="location"></param>
        public async Task ValidateDeleteAsync(Location location)
        {
            var devices = await DeviceService.GetAllByLocationIDAsync(location.LocationID);
            if (devices.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Location '{location.SiteName}' cannot be deleted because Devices are allocated.");
            }
        }
    }
}
