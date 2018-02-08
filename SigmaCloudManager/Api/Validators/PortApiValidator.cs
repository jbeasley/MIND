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
    public class PortApiValidator : BaseApiValidator, IPortApiValidator
    {
        public PortApiValidator(IDeviceService deviceService, 
            IPortBandwidthService portBandwidthService,
            IPortValidator portValidator)
        {
            DeviceService = deviceService;
            PortBandwidthService = portBandwidthService;
            PortValidator = portValidator;

        }

        public IDeviceService DeviceService { get; set; }
        public IPortBandwidthService PortBandwidthService { get; set; }
        public IPortValidator PortValidator { get; set; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                PortValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new Port request
        /// </summary>
        /// <param name="portApiModel"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(PortRequestApiModel apiRequest)
        {
            var device = await DeviceService.GetByIDAsync(apiRequest.DeviceID.Value);
            if (device == null)
            {
                ValidationDictionary.AddError("DeviceID", "The Device was not found.");
            }

            // Validate the port bandwidth 

            var portBandwidth = await PortBandwidthService.GetByIDAsync(apiRequest.PortBandwidthID.Value);
            if (portBandwidth == null)
            {
                ValidationDictionary.AddError("PortBandwidthID", "The requested Port Bandwidth "
                    + $"for Port '{apiRequest.Name}' was not found.");
            }
        }

        /// <summary>
        /// Validate a Port can be deleted.
        /// </summary>
        /// <param name="port"></param>
        public async Task ValidateDeleteAsync(Port port)
        {
            // Hand-off to Port Validator

            await PortValidator.ValidateDeleteAsync(port);
        }
    }
}
