using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;
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
        private readonly Func<RoutingInstanceType, IVrfRoutingInstanceDirector> _routingInstanceDirectorFactory;

        public VifBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IVrfRoutingInstanceDirector> routingInstanceDirectorFactory) : base(unitOfWork)
        {
            _vif = new Vif
            {
                Created = true,
                ShowCreatedAlert = true,
                Vlans = new List<Vlan>()
            };

            _routingInstanceDirectorFactory = routingInstanceDirectorFactory;
        }

        public virtual IVifBuilder ForAttachment(int? attachmentId)
        {
            if (attachmentId.HasValue) _args.Add(nameof(ForAttachment), attachmentId);
            return this;
        }

        public virtual IVifBuilder ForVif(int? vifId)
        {
            if (vifId.HasValue) _args.Add(nameof(ForVif), vifId);
            return this;
        }

        public virtual IVifBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual IVifBuilder WithVifRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName)) _args.Add(nameof(WithVifRole), roleName);
            return this;
        }

        public virtual IVifBuilder AutoAllocateVlanTag(bool? autoAllocateVlanTag, string vlanTagRangeName = _defaultVlanTagRange)
        {
            if (autoAllocateVlanTag.HasValue && autoAllocateVlanTag.Value) _args.Add(nameof(AutoAllocateVlanTag), vlanTagRangeName);
            return this;
        }

        public virtual IVifBuilder WithRequestedVlanTag(int? vlanTag)
        {
            if (vlanTag.HasValue) _args.Add(nameof(WithRequestedVlanTag), vlanTag);
            return this;
        }

        public virtual IVifBuilder WithContractBandwidth(int? contractBandwidthMbps)
        {
            if (contractBandwidthMbps.HasValue) _args.Add(nameof(WithContractBandwidth), contractBandwidthMbps);
            return this;
        }

        public virtual IVifBuilder WithExistingContractBandwidthPool(string existingContractBandwidthPoolName)
        {
            if (!string.IsNullOrEmpty(existingContractBandwidthPoolName)) _args.Add(nameof(WithExistingContractBandwidthPool), existingContractBandwidthPoolName);
            return this;
        }

        public virtual IVifBuilder UseExistingRoutingInstance(string existingRoutingInstanceName)
        {
            if (!string.IsNullOrEmpty(existingRoutingInstanceName)) _args.Add(nameof(UseExistingRoutingInstance), existingRoutingInstanceName);
            return this;
        }

        public virtual IVifBuilder WithNewRoutingInstance(bool? createNewRoutingInstance)
        {
            if (createNewRoutingInstance.HasValue) _args.Add(nameof(WithNewRoutingInstance), createNewRoutingInstance);
            return this;
        }

        public virtual IVifBuilder WithRoutingInstance(RoutingInstanceRequest routingInstanceRequest)
        {
            if (routingInstanceRequest != null) _args.Add(nameof(WithRoutingInstance), routingInstanceRequest);
            return this;
        }

        public virtual IVifBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            if (trustReceivedCosAndDscp.HasValue) _args.Add(nameof(WithTrustReceivedCosAndDscp), trustReceivedCosAndDscp);
            return this;
        }

        public virtual IVifBuilder WithIpv4(List<Ipv4AddressAndMask> ipv4AddressesAndMasks)
        {
            if (ipv4AddressesAndMasks != null && ipv4AddressesAndMasks.Any()) _args.Add(nameof(WithIpv4), ipv4AddressesAndMasks);
            return this;
        }

        public virtual IVifBuilder WithJumboMtu(bool? useJumboMtu)
        {
            if (useJumboMtu.HasValue) _args.Add(nameof(WithJumboMtu), useJumboMtu);
            return this;
        }

        public async virtual Task<Vif> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForVif)))
            {
                await SetVifAsync();
                if (_args.ContainsKey(nameof(WithContractBandwidth))) await UpdateContractBandwidthPoolAsync();
                if (_vif.VifRole.RoutingInstanceType.IsVrf)
                {
                    if (_args.ContainsKey(nameof(UseExistingRoutingInstance)))
                    {
                        await AssociateExistingRoutingInstanceAsync();
                    }
                    else if (_args.ContainsKey(nameof(WithNewRoutingInstance)))
                    {
                        await CreateRoutingInstanceAsync();
                    }
                }
            }
            else
            {
                if (_args.ContainsKey(nameof(ForAttachment))) await SetAttachmentAsync();
                if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
                if (_args.ContainsKey(nameof(WithVifRole))) await SetVifRoleAsync();
                CreateVlans();
                if (_args.ContainsKey(nameof(WithRequestedVlanTag)))
                {
                    SetRequestedVlanTag();
                }
                else
                {
                    await AutoAllocateVlanTagAsync();
                }
                if (_args.ContainsKey(nameof(WithContractBandwidth))) await CreateContractBandwidthPoolAsync();
                if (_vif.VifRole.RoutingInstanceType.IsDefault)
                {
                    await AssociateDefaultRoutingInstanceAsync();
                }
                else if (_vif.VifRole.RoutingInstanceType.IsVrf)
                {
                    if (_args.ContainsKey(nameof(UseExistingRoutingInstance)))
                    {
                        await AssociateExistingRoutingInstanceAsync();
                    }
                    else
                    {
                        await CreateRoutingInstanceAsync();
                    }
                }
            }

            if (_args.ContainsKey(nameof(WithExistingContractBandwidthPool)))
            {
                AssociateExistingContractBandwidthPool();
            }
            if (_args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();
            await SetMtuAsync();
            if (_args.ContainsKey(nameof(WithIpv4))) SetIpv4();

            _vif.Validate();

            return _vif;
        }

        protected internal virtual async Task SetVifAsync()
        {
            var vifId = (int)_args[nameof(ForVif)];
            var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                q =>
                    q.VifID == vifId,
                    query: x => x.IncludeValidationProperties(),
                    AsTrackable: true)
                       select result)
                    .SingleOrDefault();

            _vif = vif ?? throw new BuilderBadArgumentsException($"Could not find the vif with ID '{vifId}'.");
        }

        protected internal virtual async Task SetAttachmentAsync()
        {
            var attachmentId = (int)_args[nameof(ForAttachment)];
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                              q.AttachmentID == attachmentId,
                              query: q => 
                                     q.Include(x => x.Device)
                                      .Include(x => x.AttachmentBandwidth)
                                      .Include(x => x.Vifs)
                                      .ThenInclude(x => x.ContractBandwidthPool.ContractBandwidth)
                                      .Include(x => x.AttachmentRole)
                                      .Include(x => x.Interfaces)
                                      .ThenInclude(x => x.Vlans),
                              AsTrackable: true)
                              select result)
                              .SingleOrDefault();

            _vif.Attachment = attachment ?? throw new BuilderBadArgumentsException($"Could not find the attachment with ID '{attachmentId}'.");
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _vif.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' was not found.");
        }

        protected internal virtual async Task SetVifRoleAsync()
        {
            var vifRoleName = _args[nameof(WithVifRole)].ToString();
            var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                    q =>
                           q.AttachmentRoleID == _vif.Attachment.AttachmentRoleID
                           && q.Name == vifRoleName,
                           query: q => 
                                  q.Include(x => x.AttachmentRole.PortPool.PortRole)
                                   .Include(x => x.RoutingInstanceType),
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

            _vif.VlanTagRange = vlanTagRange ?? throw new BuilderBadArgumentsException($"Unable to create a vif. The vlan tag range '{vlanTagRangeName}' was not found.");

            int? vlanTag = Enumerable.Range(vlanTagRange.VlanTagRangeStart, vlanTagRange.VlanTagRangeCount)
                                     .Except(_vif.Attachment.Vifs.Select(vif => vif.VlanTag))
                                     .FirstOrDefault();

            _vif.VlanTag = vlanTag ?? throw new BuilderUnableToCompleteException("Failed to allocate a free vlan tag. "
                    + "Please contact your administrator to report this issue, or try another range.");
        }

        protected internal virtual void SetRequestedVlanTag()
        {
            var requestedVlanTag = (int)_args[nameof(WithRequestedVlanTag)];
            if (_vif.Attachment.Vifs.Where(q => q.VlanTag == requestedVlanTag).Any()) throw new BuilderUnableToCompleteException("The requested vlan tag " +
                "is already assigned. Try again with a different vlan tag.");

            _vif.VlanTag = requestedVlanTag;
        }

        protected internal virtual async Task CreateContractBandwidthPoolAsync()
        {
            var contractBandwidthMbps = (int)_args[nameof(WithContractBandwidth)];
            var contractBandwidth = (from contractBandwidths in await _unitOfWork.ContractBandwidthRepository.GetAsync(
                                  q =>
                                     q.BandwidthMbps == contractBandwidthMbps, 
                                     AsTrackable: false)
                                     select contractBandwidths)
                                    .SingleOrDefault();

            if (contractBandwidth == null) throw new BuilderBadArgumentsException($"The requested contract bandwidth of '{contractBandwidthMbps}' " +
               $"Mbps is not valid.");

            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,

                // Tenant may not be present if, for example, the vif is for a tenant domain attachment
                TenantID = _vif.Tenant?.TenantID,
                Name = Guid.NewGuid().ToString("N")         
            };

            _vif.ContractBandwidthPool = contractBandwidthPool;
        }

        protected internal virtual void AssociateExistingContractBandwidthPool()
        {
            var contractBandwidthPoolName = _args[nameof(WithExistingContractBandwidthPool)].ToString();
            var contractBandwidthPool = _vif.Attachment.Vifs
                                                       .Select(
                                                        x => 
                                                        x.ContractBandwidthPool)
                                                       .Where(
                                                        x => 
                                                        x.Name == contractBandwidthPoolName)
                                                       .SingleOrDefault();

            _vif.ContractBandwidthPool = contractBandwidthPool ?? throw new BuilderBadArgumentsException($"The requested association to contract bandwidth pool '{contractBandwidthPoolName}' is not valid. " +
                    $"The contract bandwidth pool was not found. Check that the specified contract bandwidth pool name is correct and that it belongs to " +
                    $"another vif of same attachment as the vif to be updated.");
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

            var routingInstanceType = (from routingInstanceTypes in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                     q =>
                                       q.RoutingInstanceTypeID == _vif.VifRole.RoutingInstanceTypeID.Value)
                                       select routingInstanceTypes)
                                       .Single();

            var routingInstanceRequest = _args.ContainsKey(nameof(WithRoutingInstance)) ? (RoutingInstanceRequest)_args[nameof(WithRoutingInstance)] : null;
            var routingInstanceDirector = _routingInstanceDirectorFactory(routingInstanceType);
            var routingInstance = await routingInstanceDirector.BuildAsync(deviceId: _vif.Attachment.DeviceID,
                                                                           tenantId: _vif.Tenant?.TenantID,
                                                                           request: routingInstanceRequest);

            _vif.RoutingInstance = routingInstance;
        }

        protected internal virtual async Task AssociateExistingRoutingInstanceAsync()
        {
            var routingInstanceName = _args[nameof(UseExistingRoutingInstance)].ToString();
            var tenantId = _vif.Tenant?.TenantID;

            var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                        x => 
                                           x.Name == routingInstanceName &&
                                           x.TenantID == tenantId &&
                                           x.DeviceID == _vif.Attachment.DeviceID,
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            _vif.RoutingInstance = existingRoutingInstance ?? throw new BuilderBadArgumentsException($"The routing instance with name " +
                $"'{routingInstanceName}' was not found.");
            _vif.RoutingInstanceID = existingRoutingInstance.RoutingInstanceID;
        }

        protected internal virtual async Task AssociateDefaultRoutingInstanceAsync()
        {
            var defaultRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                    x =>
                                           x.DeviceID == _vif.Attachment.Device.DeviceID &&
                                           x.RoutingInstanceType.IsDefault,
                                           AsTrackable: true)
                                          select routingInstances)
                                           .SingleOrDefault();

            _vif.RoutingInstance = defaultRoutingInstance ?? throw new BuilderUnableToCompleteException("Could not find the default routing " +
                $"instance for device '{_vif.Attachment.Device.Name}'. Please report this issue to your system administrator.");
        }

        protected internal virtual void CreateVlans()
        {
            (from iface in _vif.Attachment.Interfaces
            select iface)
            .ToList()
            .ForEach(
               iface =>
                {
                    _vif.Vlans.Add(new Vlan { Interface = iface });
                });
        }

        protected internal virtual async Task SetMtuAsync()
        {
            var useLayer2InterfaceMtu = _vif.Attachment.Device.UseLayer2InterfaceMtu;
            var useJumboMtu = _args.ContainsKey(nameof(WithJumboMtu)) ? (bool)_args[nameof(WithJumboMtu)] : false;

            var mtu = (from mtus in await _unitOfWork.MtuRepository.GetAsync(
                    x => 
                       x.ValueIncludesLayer2Overhead == useLayer2InterfaceMtu && x.IsJumbo == useJumboMtu,
                       AsTrackable: true)
                       select mtus)
                       .SingleOrDefault();

            _vif.Mtu = mtu;
        }

        protected internal virtual async Task UpdateContractBandwidthPoolAsync()
        {
            // If no contract bandwidth pool exists already then we need to create one
            if (_vif.ContractBandwidthPool == null)
            {
                await CreateContractBandwidthPoolAsync();
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

        private void SetIpv4()
        {
            var ipv4Addresses = (List<SCM.Models.RequestModels.Ipv4AddressAndMask>)_args[nameof(WithIpv4)];
            if (_vif.VifRole.IsLayer3Role)
            {
                _vif.Vlans
                    .ToList()
                    .ForEach(
                        x =>
                        {
                            var ipv4AddressAndMask = ipv4Addresses?.FirstOrDefault();
                            x.IpAddress = ipv4AddressAndMask?.IpAddress;
                            x.SubnetMask = ipv4AddressAndMask?.SubnetMask;
                            if (ipv4AddressAndMask != null) ipv4Addresses.Remove(ipv4AddressAndMask);
                        });

            }
        }
    }
}
