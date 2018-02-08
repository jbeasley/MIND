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
    /// Validator for Devices
    /// </summary>
    public class DeviceValidator : BaseValidator, IDeviceValidator
    {
        public DeviceValidator(IDeviceService deviceService, IDeviceStatusService deviceStatusService)
        {
            DeviceService = deviceService;
            DeviceStatusService = deviceStatusService;
        }

        private IDeviceService DeviceService { get; }
        private IDeviceStatusService DeviceStatusService { get; }
      
        /// <summary>
        /// Validate changes to a Device
        /// </summary>
        /// <param name="device"></param>
        public async Task ValidateChangesAsync(Device device)
        {
            var currentDevice = await DeviceService.GetByIDAsync(device.DeviceID);

            if (currentDevice.Ports.Any())
            {
                if (device.DeviceRoleID != currentDevice.DeviceRoleID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Device Role cannot be changed because ports for the device are defined.");
                }
            }

            if (currentDevice.Ports.Where(x => x.PortStatus.PortStatusType == PortStatusType.Assigned).Any())
            {

                // The name property must not be changed because this is used as a key
                // in the network service model.

                // LocationID and PlaneID properties must not be changed because these properties affect the 
                // association of VRFs with VPNs using Attachment Sets.

                if (device.Name != currentDevice.Name)
                {
                    ValidationDictionary.AddError(string.Empty, "The Name cannot be changed because ports of the device are in the 'Assigned' state.");
                }

                if (device.PlaneID != currentDevice.PlaneID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Plane cannot be changed because ports of the device are in the 'Assigned' state.");
                }

                if (device.LocationID != currentDevice.LocationID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Location cannot be changed because ports of the device are in the 'Assigned' state.");
                }

                var deviceStatus = await DeviceStatusService.GetByIDAsync(device.DeviceStatusID);
                if (deviceStatus.DeviceStatusType != DeviceStatusType.Production)
                {
                    ValidationDictionary.AddError(string.Empty, "The Device Status must be 'Production' because ports of the device are in the 'Assigned' state.");
                }
            }
        }

        /// <summary>
        /// Validate deletion of a Device. A Device can be deleted only if all ports have 
        /// a port status of 'Free'.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public void ValidateDelete(Device device)
        {
            var allocatedPorts = device.Ports.Where(q => q.PortStatus.PortStatusType != PortStatusType.Free);
            if (allocatedPorts.Count() > 0)
            {
                ValidationDictionary.AddError(string.Empty, "The Device cannot be deleted because Ports are allocated.");
            }
        }
    }
}
