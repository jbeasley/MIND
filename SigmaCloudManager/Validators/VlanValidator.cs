using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Vlans
    /// </summary>
    public class VlanValidator : BaseValidator, IVlanValidator
    {
        public VlanValidator(IVlanService vlanService, IVifService vifService)
        {
            VlanService = vlanService;
            VifService = vifService;
        }

        private IVlanService VlanService { get; }
        private IVifService VifService { get; }

        /// <summary>
        /// Validate changes to a vlan.
        /// </summary>
        /// <param name="vlan"></param>
        public async Task ValidateChangesAsync(Vlan vlan)
        {
            var vif = await VifService.GetByIDAsync(vlan.VifID);
            if (!vif.IsLayer3)
            {
                if (!string.IsNullOrEmpty(vlan.IpAddress))
                {
                    {
                        ValidationDictionary.AddError(string.Empty, $"An IP address cannot be assigned to the vlan because " +
                            $"the VIF '{vif.Name}' is not enabled for IPv4.");
                    }
                }

                if (!string.IsNullOrEmpty(vlan.SubnetMask))
                {
                    {
                        ValidationDictionary.AddError(string.Empty, $"An IP address cannot be assigned to the vlan because " +
                            $"the VIF '{vif.Name}' is not enabled for IPv4.");
                    }
                }
            }
        }
    }
}
