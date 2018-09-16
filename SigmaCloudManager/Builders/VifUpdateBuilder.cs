using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating an existing vif. The builder exposes a fluent API.
    /// </summary>
    public class VifUpdateBuilder : VifBuilder, IVifUpdateBuilder
    {
        public VifUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IVifUpdateBuilder ForVif(int vifId)
        {
            _args.Add(nameof(ForVif), vifId);
            return this;
        }

        public IVifUpdateBuilder WithNewRoutingInstance(bool? newRoutingInstance)
        {
            if (newRoutingInstance.HasValue && newRoutingInstance.Value) _args.Add(nameof(WithNewRoutingInstance), newRoutingInstance.Value);
            return this;
        }

        IVifUpdateBuilder IVifUpdateBuilder.WithIpv4(List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4AddressesAndMask)
        {
            base.WithIpv4(ipv4AddressesAndMask);
            return this;
        }

        IVifUpdateBuilder IVifUpdateBuilder.WithJumboMtu(bool? useJumboMtu)
        {
            base.WithJumboMtu(useJumboMtu);
            return this;
        }

        IVifUpdateBuilder IVifUpdateBuilder.WithContractBandwidth(int? contractBandwidthMbps)
        {
            base.WithContractBandwidth(contractBandwidthMbps);
            return this;
        }

        IVifUpdateBuilder IVifUpdateBuilder.WithExistingContractBandwidthPool(string existingContractBandwidthPoolName)
        {
            base.WithExistingContractBandwidthPool(existingContractBandwidthPoolName);
            return this;
        }

        IVifUpdateBuilder IVifUpdateBuilder.WithExistingRoutingInstance(string existingRoutingInstanceName)
        {
            base.WithExistingRoutingInstance(existingRoutingInstanceName);
            return this;
        }

        IVifUpdateBuilder IVifUpdateBuilder.WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            base.WithTrustReceivedCosAndDscp(trustReceivedCosAndDscp);
            return this;
        }

        public async Task<SCM.Models.Vif> UpdateAsync()
        {
            await SetVifAsync();
            if (_args.ContainsKey(nameof(WithContractBandwidth)))
            {
                await UpdateContractBandwidthPoolAsync();
            }
            else if (_args.ContainsKey(nameof(WithExistingContractBandwidthPool)))
            {
                base.AssociateExistingContractBandwidthPool();
            }
            if (_args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) base.SetTrustReceivedCosAndDscp();
            if (_args.ContainsKey(nameof(WithNewRoutingInstance)))
            {
                _vif.RoutingInstanceID = null;
                await base.CreateRoutingInstanceAsync();
            }
            else if (_args.ContainsKey(nameof(WithExistingRoutingInstance)))
            {
                await base.AssociateExistingRoutingInstanceAsync();
            }
            if (_args.ContainsKey(nameof(WithJumboMtu))) await base.SetMtuAsync();
            if (_args.ContainsKey(nameof(WithIpv4))) SetIpv4();

            _vif.Validate();

            return base._vif;

        }

        private async Task UpdateContractBandwidthPoolAsync()
        {
            // If no contract bandwidth pool exists already then we need to create one
            if (_vif.ContractBandwidthPool == null)
            {
                await base.CreateContractBandwidthPoolAsync();
                return;
            }

            // We simply need to repoint the contract bandwidth pool to the new contract bandwidth
            var contractBandwidthMbps = (int)_args[nameof(WithContractBandwidth)];
            var contractBandwidth = (from contractBandwidths in await _unitOfWork.ContractBandwidthRepository.GetAsync(
                                  q =>
                                     q.BandwidthMbps == contractBandwidthMbps,
                                     AsTrackable: false)
                                     select contractBandwidths)
                                     .SingleOrDefault();

            if (contractBandwidth == null) throw new BuilderBadArgumentsException($"The requested contract bandwidth of {_args[(nameof(WithContractBandwidth))]} " +
               $"Mbps is not valid.");

            _vif.ContractBandwidthPool.ContractBandwidthID = contractBandwidth.ContractBandwidthID;
        }

        private async Task SetVifAsync()
        {
            var vifId = (int)_args[nameof(ForVif)];
            var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                q =>
                    q.VifID == vifId,
                    query: x => x.IncludeValidationProperties(),
                    AsTrackable: true)
                    select result)
                    .SingleOrDefault();

            base._vif = vif ?? throw new BuilderBadArgumentsException($"Could not find the vif with ID '{vifId}'.");
        }

        private void SetIpv4()
        {
            var ipv4AddressesAndMasks = (List<Ipv4AddressAndMask>)_args[nameof(WithIpv4)];
            if (base._vif.VifRole.IsLayer3Role)
            { 
                base._vif.Vlans
                    .ToList()
                    .ForEach(
                        x =>
                            {
                                var ipv4AddressAndMask = ipv4AddressesAndMasks.FirstOrDefault();
                                x.IpAddress = ipv4AddressAndMask?.IpAddress;
                                x.SubnetMask = ipv4AddressAndMask?.SubnetMask;
                                if (ipv4AddressAndMask != null) ipv4AddressesAndMasks.Remove(ipv4AddressAndMask);
                            });

            }
        }
    }
}
