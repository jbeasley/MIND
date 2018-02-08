using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Api.Models;
using SCM.Services;
using SCM.Validators;

namespace SCM.Api.Validators
{
    public class DeviceApiValidator : BaseApiValidator, IDeviceApiValidator
    {
        public DeviceApiValidator(
            IPlaneService planeService,
            ILocationService locationService,
            IPortBandwidthService portBandwidthService,
            IDeviceValidator deviceValidator)
        {
            PlaneService = planeService;
            LocationService = locationService;
            PortBandwidthService = portBandwidthService;
            DeviceValidator = deviceValidator;
        }

        private IPlaneService PlaneService { get; set; }
        private ILocationService LocationService { get; set; }
        private IPortBandwidthService PortBandwidthService { get; set; }
        private IDeviceValidator DeviceValidator { get; set; }


        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                DeviceValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new Device request
        /// </summary>
        /// <param name="deviceApiModel"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(DeviceRequestApiModel apiRequest)
        {
            var location = await LocationService.GetByIDAsync(apiRequest.LocationID.Value);
            if (location == null)
            {
                ValidationDictionary.AddError("LocationID", "The Location was not found.");
            }

            var plane = await PlaneService.GetByIDAsync(apiRequest.PlaneID.Value);
            if (plane == null)
            {
                ValidationDictionary.AddError("PlaneID", "The Plane was not found.");
            }

            // Check if any ports are specified and validate the port bandwidth of each port found

            if (apiRequest.Ports != null)
            {
                foreach (var port in apiRequest.Ports)
                {
                    var portBandwidth = await PortBandwidthService.GetByIDAsync(port.PortBandwidthID.Value);
                    if (portBandwidth == null)
                    {
                        ValidationDictionary.AddError("PortBandwidthID", "The requested Port Bandwidth " 
                            + $"for Port '{port.Name}' was not found.");
                    }
                }
            }
        }

        /// <summary>
        /// Validate a Device can be deleted.
        /// </summary>
        /// <param name="device"></param>
        public void ValidateDelete(Device device)
        {
            // Hand-off to Device Validator

            DeviceValidator.ValidateDelete(device);
        }
    }
}
