using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Ports
    /// </summary>
    public class PortValidator : BaseValidator, IPortValidator
    {
        public PortValidator(IPortService portService, IPortStatusService portStatusService) {

            PortService = portService;
            PortStatusService = portStatusService;
        }
        
        private IPortService PortService { get; }
        private IPortStatusService PortStatusService { get; }

        /// <summary>
        /// Validate a new Port.
        /// </summary>
        /// <param name="port"></param>
        public async Task ValidateNewAsync(Port port)
        {
            var portStatus = await PortStatusService.GetByIDAsync(port.PortStatusID);
            if (portStatus.PortStatusType == PortStatusType.Assigned)
            {
                ValidationDictionary.AddError(string.Empty, $"The port status cannot be manually set to 'Assigned'. The port status is "
                         + "automatically changed to 'Assigned' when the port is allocated for either an Infrastructure Attachment or a Tenant Attachment. "
                         + "Choose another Port Status option. If you want the port to be eligible for allocation to an Attachment then set the Port Status "
                         + "to 'Free'.");
            }
        }

        /// <summary>
        /// Validate a Port can be deleted.
        /// </summary>
        /// <param name="port"></param>
        public async Task ValidateDeleteAsync(Port port)
        {
            var portStatus = await PortStatusService.GetByIDAsync(port.PortStatusID);
            if (portStatus.PortStatusType != PortStatusType.Free)
            {
                ValidationDictionary.AddError(string.Empty, "The port cannot be deleted because the port status is not 'Free'.");
            }
        }

        /// <summary>
        /// Validate changes to a Port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(Port port)
        {
            var currentPort = await PortService.GetByIDAsync(port.ID);
            var currentPortStatus = await PortStatusService.GetByIDAsync(currentPort.PortStatusID);
            if (currentPortStatus.PortStatusType == PortStatusType.Assigned)
            {
                if (port.Name != currentPort.Name)
                {
                    ValidationDictionary.AddError(string.Empty, "The name cannot be changed because the port is assigned.");
                }
                if (port.Type != currentPort.Type)
                {
                    ValidationDictionary.AddError(string.Empty, "The type cannot be changed because the port is assigned.");
                }
                if (port.PortBandwidthID != currentPort.PortBandwidthID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Port Bandwidth cannot be changed because the port is assigned.");
                }
                if (currentPort.TenantID != port.TenantID)
                {
                    ValidationDictionary.AddError(string.Empty, "The assigned Tenant of the port cannot be changed because the port is assigned.");
                }
                if (currentPort.PortPoolID != port.PortPoolID)
                {
                    ValidationDictionary.AddError(string.Empty, "The port pool cannot be changed because the port is assigned.");
                }
            }

            var portStatus = await PortStatusService.GetByIDAsync(port.PortStatusID);
            if (currentPort.PortStatus.PortStatusType == PortStatusType.Assigned)
            {
                if (portStatus.PortStatusType != PortStatusType.Assigned)
                {
                    ValidationDictionary.AddError(string.Empty, $"The port status cannot be changed to '{portStatus.Name}' because the port is assigned.");
                }
            }
            else 
            {
                if (portStatus.PortStatusType == PortStatusType.Assigned)
                {
                    ValidationDictionary.AddError(string.Empty, $"The port status cannot be manually changed to 'Assigned'. The port status is "
                        + "automatically changed to 'Assigned' when the port is allocated for either an Infrastructure Attachment or a Tenant Attachment.");
                }
            }
        }
    }
}
