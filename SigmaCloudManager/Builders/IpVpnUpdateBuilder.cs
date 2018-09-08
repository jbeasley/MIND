using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class IpVpnUpdateBuilder : IpVpnBuilder, IIpVpnUpdateBuilder
    {
        public IpVpnUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IIpVpnUpdateBuilder ForVpn(int vpnId)
        {
            _args.Add(nameof(ForVpn), vpnId);
            return this;
        }

        IIpVpnUpdateBuilder IIpVpnUpdateBuilder.WithExtranet(bool? isExtranet)
        {
            base.WithExtranet(isExtranet);
            return this;
        }

        IIpVpnUpdateBuilder IIpVpnUpdateBuilder.WithMulticastVpnDirectionType(string multicastVpnDirectionType)
        {
            base.WithMulticastVpnDirectionType(multicastVpnDirectionType);
            return this;
        }

        IIpVpnUpdateBuilder IIpVpnUpdateBuilder.WithDescription(string description)
        {
            base.WithDescription(description);
            return this;
        }

        IIpVpnUpdateBuilder IIpVpnUpdateBuilder.WithName(string name)
        {
            base.WithName(name);
            return this;
        }

        IIpVpnUpdateBuilder IIpVpnUpdateBuilder.WithRegion(string regionName)
        {
            base.WithRegion(regionName);
            return this;
        }

        IIpVpnUpdateBuilder IIpVpnUpdateBuilder.WithTenancyType(string tenancyName)
        {
            base.WithTenancyType(tenancyName);
            return this;
        }

        public async Task<Vpn> UpdateAsync()
        {
            await SetVpnAsync();
            if (_args.ContainsKey(nameof(WithName))) base.SetName();
            if (_args.ContainsKey(nameof(WithDescription))) base.SetDescription();
            if (_args.ContainsKey(nameof(WithTenancyType))) await base.SetTenancyTypeAsync();
            if (_args.ContainsKey(nameof(WithRegion))) await SetRegionAsync();
            if (_args.ContainsKey(nameof(WithExtranet))) base.SetExtranet();
            if (_args.ContainsKey(nameof(WithMulticastVpnDirectionType))) await base.SetMulticastVpnDirectionTypeAsync();

            return base._vpn;
        }

        private async Task SetVpnAsync()
        {
            var vpnId = (int)_args[nameof(ForVpn)];
            var vpn = (from result in await _unitOfWork.VpnRepository.GetAsync(
                    q =>
                       q.VpnID == vpnId,
                       AsTrackable: true)
                       select result)
                       .Single();

            base._vpn = vpn;
        }
    }
}
