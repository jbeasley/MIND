using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for vifs. The builder exposes a fluent UI.
    /// </summary>
    public class VifBuilder : BaseBuilder, IVifBuilder
    {
        protected internal Vif _vif;
        private const string _defaultVlanTagRange = "Default";
        private readonly Func<RoutingInstanceType, IRoutingInstanceDirector> _routingInstanceDirectorFactory;

        public VifBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : base(unitOfWork)
        {
            _vif = new Vif
            {
                Created = true,
                ShowCreatedAlert = true,
                Vlans = new List<Vlan>()
            };

            _routingInstanceDirectorFactory = routingInstanceDirectorFactory;
        }

        public virtual IVifBuilder ForAttachment(int attachmentId)
        {
            _args.Add(nameof(ForAttachment), attachmentId);
            return this;
        }

        public virtual IVifBuilder ForTenant(int tenantId)
        {
            _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual IVifBuilder WithVifRole(string roleName)
        {
            _args.Add(nameof(WithVifRole), roleName);
            return this;
        }

        public virtual IVifBuilder AutoAllocateVlanTag(bool? autoAllocateVlanTag, string vlanTagRangeName = _defaultVlanTagRange)
        {
            if (autoAllocateVlanTag.HasValue && autoAllocateVlanTag.Value) _args.Add(nameof(AutoAllocateVlanTag), vlanTagRangeName);
            return this;
        }

        public virtual IVifBuilder WithRequestedVlanTag(int? vlanTag)
        {
            if (vlanTag != null) _args.Add(nameof(WithRequestedVlanTag), vlanTag);
            return this;
        }

        public virtual IVifBuilder WithContractBandwidth(int? contractBandwidthMbps)
        {
            if (contractBandwidthMbps != null) _args.Add(nameof(WithContractBandwidth), contractBandwidthMbps);
            return this;
        }

        public virtual IVifBuilder WithExistingContractBandwidthPool(string existingContractBandwidthPoolName)
        {
            if (!string.IsNullOrEmpty(existingContractBandwidthPoolName)) _args.Add(nameof(WithExistingContractBandwidthPool), existingContractBandwidthPoolName);
            return this;
        }

        public virtual IVifBuilder WithExistingRoutingInstance(string existingRoutingInstanceName)
        {
            if (!string.IsNullOrEmpty(existingRoutingInstanceName)) _args.Add(nameof(WithExistingRoutingInstance), existingRoutingInstanceName);
            return this;
        }

        public virtual IVifBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            if (trustReceivedCosAndDscp != null) _args.Add(nameof(WithTrustReceivedCosAndDscp), trustReceivedCosAndDscp);
            return this;
        }

        public virtual IVifBuilder WithIpv4(List<Ipv4AddressAndMask> ipv4AddressesAndMasks)
        {
            if (ipv4AddressesAndMasks != null && ipv4AddressesAndMasks.Any()) _args.Add(nameof(WithIpv4), ipv4AddressesAndMasks);
            return this;
        }

        public virtual IVifBuilder WithJumboMtu(bool? useJumboMtu)
        {
            if (useJumboMtu != null) _args.Add(nameof(WithJumboMtu), useJumboMtu);
            return this;
        }

        public async virtual Task<Vif> BuildAsync()
        {
            await SetAttachmentAsync();
            await SetTenantAsync();
            await SetVifRoleAsync();
            if (_args.ContainsKey(nameof(WithContractBandwidth)))
            {
                await CreateContractBandwidthPoolAsync();
            }
            else if (_args.ContainsKey(nameof(WithExistingContractBandwidthPool)))
            {
                AssociateExistingContractBandwidthPool();
            }

            if (_args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();
            CreateVlans();
            await SetMtuAsync();
            if (_args.ContainsKey(nameof(WithRequestedVlanTag)))
            {
                SetRequestedVlanTag();
            }
            else
            {
                await AutoAllocateVlanTagAsync();
            }

            if (_args.ContainsKey(nameof(WithExistingRoutingInstance))) 
            {
                await AssociateExistingRoutingInstanceAsync();
            }
            else
            {
                await CreateRoutingInstanceAsync();
            }

            return _vif;
        }

        protected internal virtual async Task SetAttachmentAsync()
        {
            var attachmentId = (int)_args[nameof(ForAttachment)];
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                              q.AttachmentID == attachmentId,
                              includeProperties: "Device," +
                              "AttachmentBandwidth," +
                              "Vifs.ContractBandwidthPool.ContractBandwidth," +
                              "AttachmentRole," +
                              "Interfaces.Vlans",
                              AsTrackable: true)
                              select result)
                              .Single();

            if (!attachment.IsTagged) throw new BuilderBadArgumentsException("A vif cannot be created for the given attachment because the attachment is not " +
                "enabled for tagging.");
            _vif.Attachment = attachment;
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .Single();

            _vif.Tenant = tenant;
        }

        protected internal virtual async Task SetVifRoleAsync()
        {
            var vifRoleName = _args[nameof(WithVifRole)].ToString();
            var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                    q =>
                           q.AttachmentRoleID == _vif.Attachment.AttachmentRoleID
                           && q.Name == vifRoleName,
                           AsTrackable: true)
                           select result)
                           .SingleOrDefault();

            _vif.VifRole = vifRole ?? throw new BuilderBadArgumentsException("Unable to create a vif with the supplied arguments. The name of the vif role " +
                $"'{vifRoleName}' does not exist in the context of the current attachment. The vifRole argument must belong to the parent attachment role " +
                $"which for the current attachment is '{_vif.Attachment.AttachmentRole.Name}'");

            _vif.IsLayer3 = vifRole.IsLayer3Role;
            _vif.RequiresSync = vifRole.RequireSyncToNetwork;
            _vif.ShowRequiresSyncAlert = vifRole.RequireSyncToNetwork;
        }

        protected internal virtual async Task AutoAllocateVlanTagAsync()
        {
            var vlanTagRangeName = _args.ContainsKey(nameof(AutoAllocateVlanTag)) ? _args[nameof(AutoAllocateVlanTag)].ToString() : _defaultVlanTagRange;
            var vlanTagRange = (from result in await _unitOfWork.VlanTagRangeRepository.GetAsync(
                q =>
                    q.Name == vlanTagRangeName)
                                select result)
                               .SingleOrDefault();

            _vif.VlanTagRange = vlanTagRange ?? throw new BuilderUnableToCompleteException($"Unable to create a vif. The vlan tag range '{vlanTagRangeName}' was not found.");

            int? vlanTag = Enumerable.Range(vlanTagRange.VlanTagRangeStart, vlanTagRange.VlanTagRangeCount)
                                     .Except(_vif.Attachment.Vifs.Select(vif => vif.VlanTag))
                                     .FirstOrDefault();

            _vif.VlanTag = vlanTag ?? throw new BuilderUnableToCompleteException("Failed to allocate a free vlan tag. "
                    + "Please contact your administrator to report this issue, or try another range.");
        }

        protected internal virtual void SetRequestedVlanTag()
        {
            var requestedVlanTag = (int)_args[nameof(WithRequestedVlanTag)];
            if (_vif.Attachment.Vifs.Where(q => q.VlanTag == requestedVlanTag).Any()) throw new BuilderBadArgumentsException("The requested vlan tag " +
                "is already assigned. Try again with a different vlan tag.");

            _vif.VlanTag = requestedVlanTag;
        }

        protected internal virtual async Task CreateContractBandwidthPoolAsync()
        {
            var contractBandwidthMbps = (int)_args[nameof(WithContractBandwidth)];
            var aggContractBandwidthMbps = _vif.Attachment.Vifs
                .Where(vif => vif.VifID != _vif.VifID)
                .Select(
                vif => vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps)
                .Aggregate(0, (x, y) => x + y);

            var attachmentBandwidthMbps = _vif.Attachment.AttachmentBandwidth.BandwidthGbps * 1000;

            if (attachmentBandwidthMbps < (aggContractBandwidthMbps + contractBandwidthMbps))
            {
                throw new BuilderBadArgumentsException($"The requested contract bandwidth of {contractBandwidthMbps} Mbps is greater " +
                    $"than the remaining available bandwidth of the attachment ({attachmentBandwidthMbps - aggContractBandwidthMbps} Mbps).");
            }

            var contractBandwidth = (from contractBandwidths in await _unitOfWork.ContractBandwidthRepository.GetAsync(
                                  q =>
                                     q.BandwidthMbps == contractBandwidthMbps, 
                                     AsTrackable: false)
                                     select contractBandwidths)
                                    .SingleOrDefault();

            if (contractBandwidth == null) throw new BuilderBadArgumentsException($"The requested contract bandwidth of {_args[(nameof(WithContractBandwidth))]} " +
               $"Mbps is not valid.");

            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,
                TenantID = _vif.Tenant.TenantID,
                Name = Guid.NewGuid().ToString("N")         
            };

            _vif.ContractBandwidthPool = contractBandwidthPool;
        }

        protected internal virtual void AssociateExistingContractBandwidthPool()
        {
            var contractBandwidthPoolName = _args[nameof(WithExistingContractBandwidthPool)].ToString();
            var contractBandwidthPool = _vif.Attachment.Vifs
                                                       .Select(x => x.ContractBandwidthPool)
                                                       .Where(x => x.Name == contractBandwidthPoolName)
                                                       .SingleOrDefault();

            if (contractBandwidthPool == null)
            {
                throw new BuilderBadArgumentsException($"The requested association to contract bandwidth pool '{contractBandwidthPoolName}' is not valid. " +
                    $"The contract bandwidth pool was not found. Check that the specified contract bandwidth pool name is correct and that it belongs to " +
                    $"another vif of same attachment as the vif to be updated.");
            }

            _vif.ContractBandwidthPoolID = contractBandwidthPool.ContractBandwidthPoolID;
        }

        protected internal virtual void SetTrustReceivedCosAndDscp()
        {
            if (_vif.ContractBandwidthPool != null)
            {
                var trustReceivedCosAndDscp = (bool)_args[nameof(WithTrustReceivedCosAndDscp)];
                _vif.ContractBandwidthPool.TrustReceivedCosDscp = trustReceivedCosAndDscp;
            }
        }

        protected internal virtual async Task CreateRoutingInstanceAsync()
        {
            if (_vif.VifRole.RoutingInstanceTypeID == null) throw new BuilderUnableToCompleteException("A routing instance type is required " +
                "for the vif role but was not found.");

            var routingInstanceType = (from routingInstanceTypes in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(q =>
            q.RoutingInstanceTypeID == _vif.VifRole.RoutingInstanceTypeID.Value)
                                       select routingInstanceTypes)
                                       .Single();

            var routingInstanceDirector = _routingInstanceDirectorFactory(routingInstanceType);
            var routingInstance = await routingInstanceDirector.BuildAsync(deviceId: _vif.Attachment.DeviceID,
                                                                           tenantId: _vif.Tenant.TenantID);

            _vif.RoutingInstance = routingInstance;
        }

        protected internal virtual async Task AssociateExistingRoutingInstanceAsync()
        {
            var routingInstanceName = _args[nameof(WithExistingRoutingInstance)].ToString();

            var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                    x => 
                                           x.Name == routingInstanceName
                                           && x.TenantID == _vif.Tenant.TenantID
                                           && x.DeviceID == _vif.Attachment.DeviceID,
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            _vif.RoutingInstance = existingRoutingInstance ?? throw new BuilderBadArgumentsException("Could not find existing routing " +
                $"instance '{routingInstanceName}' belonging to tenant '{_vif.Tenant.Name}'. Check that the routing instance exists and that " +
                $"it belongs to the same provider domain device as the vif to be updated.");

            _vif.RoutingInstanceID = existingRoutingInstance.RoutingInstanceID;
        }

        protected internal virtual void CreateVlans()
        {
            List<Ipv4AddressAndMask> ipv4AddressesAndMasks = null;

            var isLayer3Role = _vif.VifRole.IsLayer3Role;
            if (isLayer3Role)
            {
                if (_args.ContainsKey(nameof(WithIpv4))) ipv4AddressesAndMasks = (List<Ipv4AddressAndMask>)_args[nameof(WithIpv4)];
                if (ipv4AddressesAndMasks == null || ipv4AddressesAndMasks.ToList().Count < _vif.Attachment.Interfaces.Count)
                {
                    throw new BuilderBadArgumentsException("IPv4 addresses and subnet masks are required in order to create a layer 3 " +
                                            $"enabled vif. {_vif.Attachment.Interfaces.Count} vlans are required to create the vif so " +
                                            $"the same number of IPv4 addresses and subnet masks are required.");
                }
            }

           (from iface in _vif.Attachment.Interfaces
            select iface)
            .ToList()
            .ForEach(
               x =>
                {
                    var ipv4AddressAndMask = ipv4AddressesAndMasks.First();
                    _vif.Vlans.Add(new Vlan
                    {
                        Interface = x,
                        IpAddress = isLayer3Role ? ipv4AddressAndMask.IpAddress : null,
                        SubnetMask = isLayer3Role ? ipv4AddressAndMask.SubnetMask : null                     
                    });
                    ipv4AddressesAndMasks.Remove(ipv4AddressAndMask);
                });
        }

        protected internal virtual async Task SetMtuAsync()
        {
            var useLayer2InterfaceMtu = _vif.Attachment.Device.UseLayer2InterfaceMtu;
            var useJumboMtu = _args.ContainsKey(nameof(WithJumboMtu)) ? (bool)_args[nameof(WithJumboMtu)] : false;

            var mtu = (from mtus in await _unitOfWork.MtuRepository.GetAsync(
                x => 
                    x.ValueIncludesLayer2Overhead == useLayer2InterfaceMtu && x.IsJumbo == useJumboMtu)
                       select mtus)
                       .Single();

            _vif.MtuID = mtu.MtuID;
        }
    }
}
