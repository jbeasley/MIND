using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Data;
using SCM.Services;
using SCM.Models;


namespace Mind.Builders
{
    /// <summary>
    /// Builder for multi-port attachments. The builder exposes a fluent API.
    /// </summary>
    public class MultiPortAttachmentBuilder : AttachmentBuilder<MultiPortAttachmentBuilder>, IAttachmentBuilder<MultiPortAttachmentBuilder>
    {
        public MultiPortAttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IVrfRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public override async Task<Attachment> BuildAsync()
        {
            await base.BuildAsync();
            await CreateMultiPortIdAsync();
            _attachment.IsMultiPort = true;
            _attachment.Validate();

            return _attachment;
        }

        protected internal override void SetNumberOfPortsRequired()
        {
            var attachmentBandwidth = _attachment.AttachmentBandwidth;
            _numPortsRequired = attachmentBandwidth.BandwidthGbps / attachmentBandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
        }

        protected internal override void SetPortBandwidthRequired()
        {
            _portBandwidthRequired = _attachment.AttachmentBandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
        }

        protected internal override void CreateInterfaces()
        {
            List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4Addresses = null;
            if (_args.ContainsKey(nameof(WithIpv4))) ipv4Addresses = (List<SCM.Models.RequestModels.Ipv4AddressAndMask>)_args[nameof(WithIpv4)];
            var isLayer3Role = _attachment.AttachmentRole.IsLayer3Role;
            var ports = _ports.ToList();
            _attachment.Interfaces = new List<Interface>();
            
            for (var i = 0; i < _numPortsRequired; i++)
            {
                var ipv4AddressAndMask = ipv4Addresses?.FirstOrDefault();
                var iface = new Interface
                {
                    DeviceID = _attachment.Device.DeviceID,
                    Ports = new List<Port> { ports[i] },
                    IpAddress = ipv4AddressAndMask?.IpAddress,
                    SubnetMask = ipv4AddressAndMask?.SubnetMask
                };

                if (ipv4AddressAndMask != null) ipv4Addresses.Remove(ipv4AddressAndMask);
                _attachment.Interfaces.Add(iface);
            }
        }

        private async Task CreateMultiPortIdAsync()
        {
            var usedMultiPortIds = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(
                                q => 
                                    q.DeviceID == _attachment.DeviceID && q.IsMultiPort)
                                    select attachments)
                                    .Select(q => q.ID)
                                    .Where(q => q != null)
                                    .ToList();

            int? id = Enumerable.Range(1, 65535).Except(usedMultiPortIds.Select(q => q.Value)).FirstOrDefault();

            _attachment.ID = id ?? throw new BuilderUnableToCompleteException("Unable to assign an ID value to the multiport attachment. "
                    + $"It seems that all IDs in the range 1 - 65535 for device '{_attachment.Device.Name}' have been used. " +
                    $"Contact your system administrator to report this issue.");
        }
    }
}
