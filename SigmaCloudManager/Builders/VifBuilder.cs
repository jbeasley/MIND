using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;
using Mind.Models;

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
        private readonly IDataApi _novaApiClient;


        public VifBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType,
                          IRoutingInstanceDirector> routingInstanceDirectorFactory,
                          IDataApi novaApiClient) : base(unitOfWork)
        {
            _vif = new Vif
            {
                Created = true,
                ShowCreatedAlert = true,
                NetworkStatus = Models.NetworkStatusEnum.NotStaged,
                Vlans = new List<Vlan>()
            };

            _routingInstanceDirectorFactory = routingInstanceDirectorFactory;
            _novaApiClient = novaApiClient;
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

        public virtual IVifBuilder CleanUpRoutingInstance()
        {
            _args.Add(nameof(CleanUpRoutingInstance), null);
            return this;
        }

        public virtual IVifBuilder CleanUpContractBandwidthPool()
        {
            _args.Add(nameof(CleanUpContractBandwidthPool), null);
            return this;
        }

        public virtual IVifBuilder SyncToNetworkPut(bool? syncToNetworkPut)
        {
            if (syncToNetworkPut.HasValue) _args.Add(nameof(SyncToNetworkPut), syncToNetworkPut);
            return this;
        }

        public virtual IVifBuilder CleanUpNetwork(bool? cleanUpNetwork)
        {
            if (cleanUpNetwork.HasValue) _args.Add(nameof(CleanUpNetwork), cleanUpNetwork);
            return this;
        }

        public async virtual Task<Vif> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForVif)))
            {
                // Update an existing vif
                await SetVifAsync();

                if (_args.ContainsKey(nameof(WithContractBandwidth)))
                {
                    await UpdateContractBandwidthPoolAsync();
                }

                if (_args.ContainsKey(nameof(WithIpv4)))
                {
                    SetIpv4();
                }

                // Cache the reference to the current routing instance for possible deletion later
                var routingInstance = _vif.RoutingInstance;

                if (_args.ContainsKey(nameof(UseExistingRoutingInstance)))
                {
                    // Associate a pre-existing routing instance with the vif 
                    //(this may even be the same routing instance as currently associated)
                    RemoveVifFromRoutingInstance();
                    await AssociateExistingRoutingInstanceAsync();

                    if (_args.ContainsKey(nameof(WithRoutingInstance)))
                    {
                        // Perform any updates on the routing instance, e.g. add/modify/delete BGP peers
                        await UpdateRoutingInstanceAsync();
                    }
                }

                else if (_args.ContainsKey(nameof(WithNewRoutingInstance)))
                {
                    RemoveVifFromRoutingInstance();
                    await CreateRoutingInstanceAsync();
                }

                else if (_args.ContainsKey(nameof(WithRoutingInstance)))
                {
                    // Update the existing routing instance, e.g.add/modify/delete BGP peers
                    await UpdateRoutingInstanceAsync();
                }

                // Check to cleanup the cached routing instance if it is no longer being used
                if (_args.ContainsKey(nameof(CleanUpRoutingInstance)))
                {
                    DeleteRoutingInstance(routingInstance);
                }
            }
            else
            {
                // Create a new vif
                if (_args.ContainsKey(nameof(ForAttachment)))
                {
                    await SetAttachmentAsync();
                }

                if (_args.ContainsKey(nameof(ForTenant)))
                {
                    await SetTenantAsync();
                }

                if (_args.ContainsKey(nameof(WithVifRole)))
                {
                    await SetVifRoleAsync();
                }

                CreateVlans();

                if (_args.ContainsKey(nameof(WithIpv4)))
                {
                    SetIpv4();
                }

                if (_args.ContainsKey(nameof(WithRequestedVlanTag)))
                {
                    SetRequestedVlanTag();
                }

                else
                {
                    await AutoAllocateVlanTagAsync();
                }

                if (_args.ContainsKey(nameof(WithContractBandwidth)))
                {
                    await CreateContractBandwidthPoolAsync();
                }

                if (_vif.VifRole.RoutingInstanceType.IsDefault)
                {
                    await AssociateDefaultRoutingInstanceAsync();

                    if (_args.ContainsKey(nameof(WithRoutingInstance)))
                    {
                        // Perform any updates on the default routing instance, e.g. add/modify/delete BGP peers
                        await UpdateRoutingInstanceAsync();
                    }
                }

                else if (_vif.VifRole.RoutingInstanceType.IsVrf)
                {
                    if (_args.ContainsKey(nameof(UseExistingRoutingInstance)))
                    {
                        await AssociateExistingRoutingInstanceAsync();

                        if (_args.ContainsKey(nameof(WithRoutingInstance)))
                        {
                            // Perform any updates on the existing routing instance, e.g. add/modify/delete BGP peers
                            await UpdateRoutingInstanceAsync();
                        }
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

            if (_args.ContainsKey(nameof(WithTrustReceivedCosAndDscp)))
            {
                SetTrustReceivedCosAndDscp();
            }

            await SetMtuAsync();

            // Has the vif been created correctly?
            _vif.Validate();

            // Check to sync the vif to the network with a put operation
            if (_args.ContainsKey(nameof(SyncToNetworkPut)))
            {
                var syncToNetworkPut = (bool)_args[nameof(SyncToNetworkPut)];
                if (syncToNetworkPut)
                {
                    await SyncVifToNetworkPutAsync();
                }
            }

            return _vif;
        }

        /// <summary>
        /// Destroy the vif
        /// </summary>
        /// <returns>A task</returns>
        public async Task DestroyAsync()
        {
            if (_args.ContainsKey(nameof(ForVif)))
            {
                await SetVifToDeleteAsync();

                RemoveVifFromRoutingInstance();

                if (_args.ContainsKey(nameof(CleanUpRoutingInstance))) DeleteRoutingInstance(_vif.RoutingInstance);
                if (_args.ContainsKey(nameof(CleanUpContractBandwidthPool))) DeleteContractBandwidthPool();

                // Check to delete the vif from the network
                if (_args.ContainsKey(nameof(CleanUpNetwork)))
                {
                    var cleanUpNetwork = (bool)_args[nameof(CleanUpNetwork)];
                    if (cleanUpNetwork)
                    {
                        await DeleteVifFromNetworkAsync();
                        await CheckDeleteRoutingInstanceFromNetworkAsync();
                        await CheckDeleteContractBandwidthPoolFromNetworkAsync();
                    }
                }

                _unitOfWork.VifRepository.Delete(_vif);
            }
        }

        /// <summary>
        /// Sync the vif to the network using a put operation
        /// </summary>
        /// <returns>The vif</returns>
        public async Task<Vif> SyncToNetworkPutAsync()
        {
            if (_args.ContainsKey(nameof(ForVif)))
            {
                await SetVifAsync();
                await SyncVifToNetworkPutAsync();
            }

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
                              query: q => q.IncludeValidationProperties(),
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
                                     AsTrackable: true)
                                     select contractBandwidths)
                                    .SingleOrDefault();

            if (contractBandwidth == null) throw new BuilderBadArgumentsException($"The requested contract bandwidth of '{contractBandwidthMbps}' " +
               $"Mbps is not valid.");

            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,
                ContractBandwidth = contractBandwidth,

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
                                                        vif =>
                                                        vif.ContractBandwidthPool)
                                                       .SingleOrDefault(
                                                        pool =>
                                                        pool.Name == contractBandwidthPoolName);

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
                _vif.ContractBandwidthPool.TrustReceivedCosAndDscp = trustReceivedCosAndDscp;
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

            if (routingInstanceType.IsVrf)
            {
                var routingInstanceRequest = _args.ContainsKey(nameof(WithRoutingInstance)) ? (RoutingInstanceRequest)_args[nameof(WithRoutingInstance)] : null;
                var routingInstanceDirector = _routingInstanceDirectorFactory(routingInstanceType) as IVrfRoutingInstanceDirector;
                var routingInstance = await routingInstanceDirector.BuildAsync(vif: _vif,
                                                                               request: routingInstanceRequest);

                _vif.RoutingInstanceID = null;
                _vif.RoutingInstance = routingInstance;
                _vif.RoutingInstance.Vifs.Add(_vif);
            }
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
                                           query: q => q.Include(x => x.Vifs),
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            if (existingRoutingInstance == null)
            {
                throw new BuilderBadArgumentsException($"The routing instance with name " +
                $"'{routingInstanceName}' was not found.");
            }

            // Add the vif to the vifs collection of the existing routing instance. This is needed for validation checks
            // such as validation of the reachability of BGP peeers 
            existingRoutingInstance.Vifs.Add(_vif);

            _vif.RoutingInstance = existingRoutingInstance;
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

        protected internal virtual async Task UpdateRoutingInstanceAsync()
        {
            if (_vif.VifRole.RoutingInstanceType != null)
            {
                var routingInstanceRequest = (RoutingInstanceRequest)_args[nameof(WithRoutingInstance)];
                var routingInstanceDirector = _routingInstanceDirectorFactory(_vif.VifRole.RoutingInstanceType);

                await routingInstanceDirector.UpdateAsync(routingInstanceId: _vif.RoutingInstance.RoutingInstanceID, request: routingInstanceRequest);
            }
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
            var useJumboMtu = _args.ContainsKey(nameof(WithJumboMtu)) && (bool)_args[nameof(WithJumboMtu)];

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
                                     AsTrackable: true)
                                     select contractBandwidths)
                                     .SingleOrDefault();

            if (contractBandwidth == null) throw new BuilderBadArgumentsException($"The requested contract bandwidth of {_args[(nameof(WithContractBandwidth))]} " +
               $"Mbps is not valid.");

            _vif.ContractBandwidthPool.ContractBandwidthID = contractBandwidth.ContractBandwidthID;
            _vif.ContractBandwidthPool.ContractBandwidth = contractBandwidth;
        }

        protected internal virtual async Task SetVifToDeleteAsync()
        {
            var vifId = (int)_args[nameof(ForVif)];
            var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                    q =>
                       q.VifID == vifId,
                       query: q => q.IncludeDeleteValidationProperties(),
                       AsTrackable: true)
                       select result)
                       .SingleOrDefault();

            _vif = vif ?? throw new BuilderBadArgumentsException($"Could not find vif with ID '{vifId}' to delete");
        }

        protected internal virtual void RemoveVifFromRoutingInstance()
        {
            var vifs = _vif.RoutingInstance.Vifs.Where(vif => vif.VifID == _vif.VifID);
            _vif.RoutingInstance.Vifs = _vif.RoutingInstance.Vifs.Except(vifs).ToList();
        }

        /// <summary>
        /// Check to delete the routing instance from the database and delete.
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected internal virtual void DeleteRoutingInstance(RoutingInstance routingInstance)
        {
            try
            {
                routingInstance.ValidateDelete();
                _unitOfWork.RoutingInstanceRepository.Delete(routingInstance);
            }

            catch (IllegalDeleteAttemptException)
            {
                // Just catch the exception but nothing to do...
            }

        }        

        /// <summary>
        /// Check to delete the contract bandwidth pool for the vif from the database and delete.
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected internal virtual void DeleteContractBandwidthPool() 
        {
            if (_vif.ContractBandwidthPool != null)
            {
                _vif.ContractBandwidthPool.ValidateDelete();
                _unitOfWork.ContractBandwidthPoolRepository.Delete(_vif.ContractBandwidthPool);
            }
        }

        /// <summary>
        /// Sync the vif to the network using a put operation
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected internal virtual async Task SyncVifToNetworkPutAsync()
        {
            var vifDto = _vif.ToNovaVifDto();
            var routingInstanceDto = _vif.RoutingInstance.ToNovaRoutingInstanceDto();
            var contractBandwidthPoolDto = _vif.ContractBandwidthPool.ToNovaContractBandwidthPoolDto();

            try
            {
                // Ordering is important here because the YANG model requires that the contract bandwidth pool and 
                // routing instance exist prior to the provisioning of the vif

                // Add the contract bandwidth pool
                await _novaApiClient.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNamePutAsync
                    (_vif.Attachment.Device.Name, _vif.Attachment.PortType, _vif.Attachment.PortName, _vif.ContractBandwidthPool.Name, contractBandwidthPoolDto, true);

                // Add the routing instance
                await _novaApiClient.DataAttachmentAttachmentPePePeNameVrfVrfVrfNamePutAsync(_vif.Attachment.Device.Name, _vif.RoutingInstance.Name, routingInstanceDto, true);

                // Add the vif
                await _novaApiClient.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdPutAsync
                    (_vif.Attachment.Device.Name, _vif.Attachment.PortType, _vif.Attachment.PortName, _vif.VlanTag, vifDto, true);

                _vif.NetworkStatus = Models.NetworkStatusEnum.Active;
            }

            catch (ApiException)
            {
                // Set network status on the vif and then rethrow the exception to be caught
                // further up the stack
                _vif.NetworkStatus = Models.NetworkStatusEnum.ActivationFailure;
                throw;
            }
        }

        /// <summary>
        /// Delete the vif from the network.
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected internal virtual async Task DeleteVifFromNetworkAsync()
        {
            try
            {
                await _novaApiClient.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdVifVifVlanIdDeleteAsync(
                    _vif.Attachment.Device.Name, _vif.Attachment.PortType, _vif.Attachment.PortName, _vif.VlanTag, true).ConfigureAwait(false);
            }

            catch (ApiException)
            {
                // Add logging here 
            }
        }

        /// <summary>
        /// Check to delete the routing instance for the vif from the network. If the routing instance
        /// is not used by any other attachment or vif than the current vif then the routing instance
        /// can be deleted.
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected internal virtual async Task CheckDeleteRoutingInstanceFromNetworkAsync()
        {
            if (_vif.RoutingInstance != null && _vif.RoutingInstance.RoutingInstanceType.Type == RoutingInstanceTypeEnum.TenantFacingVrf)
            {
                // Check if the current vif is the only vif using the routing instance and no 
                // attachments are using the routing instance. If so delete the routing instance from the network
                if (!_vif.RoutingInstance.Vifs.Any(x => x.VifID != _vif.VifID) &&
                    !_vif.RoutingInstance.Attachments.Any())
                {
                    try
                    {
                        await _novaApiClient.DataAttachmentAttachmentPePePeNameVrfVrfVrfNameDeleteAsync(
                            _vif.Attachment.Device.Name, _vif.RoutingInstance.Name, true).ConfigureAwait(false);
                    }

                    catch (ApiException)
                    {
                        // Add logging here
                    }
                }
            }
        }

        /// <summary>
        /// Check to delete the contract bandwidth pool for the vif from the network. If the contract bandwidth pool
        /// is not used by any other attachment or vif than the current vif then the contract bandwidth pool
        /// can be deleted.
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected async Task CheckDeleteContractBandwidthPoolFromNetworkAsync()
        {
            if (_vif.ContractBandwidthPool != null)
            {
                // Check if the current vif is the only vif using the contract bandwidth pool and no 
                // attachments are using the contract bandwidth pool. If so delete the contract bandwidth pool.
                if (!_vif.ContractBandwidthPool.Vifs.Any(x => x.VifID != _vif.VifID) &&
                    !_vif.ContractBandwidthPool.Attachments.Any())
                {
                    try
                    {
                        await _novaApiClient.DataAttachmentAttachmentPePePeNameTaggedAttachmentInterfaceTaggedAttachmentInterfaceInterfaceTypeTaggedAttachmentInterfaceInterfaceIdContractBandwidthPoolContractBandwidthPoolNameDeleteAsync(
                            _vif.Attachment.Device.Name, _vif.Attachment.PortType, _vif.Attachment.PortName, _vif.ContractBandwidthPool.Name, true).ConfigureAwait(false);
                    }

                    catch (ApiException)
                    {
                        // Add logging here
                    }
                }
            }
        }

        /// <summary>
        /// Set IPv4 properties for the vif
        /// </summary>
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
