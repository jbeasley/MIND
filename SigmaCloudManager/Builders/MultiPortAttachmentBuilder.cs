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
        public MultiPortAttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public override async Task<Attachment> BuildAsync()
        {
            await Task.WhenAll(new List<Task> {
                base.BuildAsync(),
                CreateMultiPortId()
            });

            _attachment.IsMultiPort = true;

            return _attachment;
        }

        protected internal override async Task CreateAttachmentRoleAsync()
        {
            await base.CreateAttachmentRoleAsync();
            if (!_attachment.AttachmentRole.SupportedByMultiPort) throw new BuilderBadArgumentsException($"The requested attachment role " +
                $"'{_attachment.AttachmentRole.Name}' is not supported with a multiport attachment.");
        }

        protected internal override async Task CreateAttachmentBandwidthAsync()
        {
            await base.CreateAttachmentBandwidthAsync();
            if (!_attachment.AttachmentBandwidth.SupportedByMultiPort) throw new BuilderBadArgumentsException($"The requested attachment " +
                $"bandwidth '{_attachment.AttachmentBandwidth.BandwidthGbps} Gbps' is not supported with a multiport attachment.");
 
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
            var isLayer3Role = _attachment.AttachmentRole.IsLayer3Role;
            if (isLayer3Role)
            {
                ipv4Addresses = (List<SCM.Models.RequestModels.Ipv4AddressAndMask>)_args["ipv4Addresses"];
                if (!ipv4Addresses.Any()) throw new BuilderBadArgumentsException("An IPv4 address and subnet mask is required in order to create a layer 3 enabled " +
                        "interface for the attachment.");

                if (ipv4Addresses.Count < _numPortsRequired) throw new BuilderBadArgumentsException("An insufficient number of IPv4 addressses has been supplied. " +
                    $"{_numPortsRequired} IPv4 addresses are needed to create the multiport attachment.");
            }

            var ports = _ports.ToList();
            _attachment.Interfaces = new List<Interface>();
            
            for (var i = 0; i < _numPortsRequired; i++)
            {
                var iface = new Interface
                {
                    DeviceID = _attachment.Device.DeviceID,
                    Ports = new List<Port> { ports[i] },
                    IpAddress = isLayer3Role ? ipv4Addresses[i].IpAddress : null,
                    SubnetMask = isLayer3Role ? ipv4Addresses[i].SubnetMask : null
                };

                _attachment.Interfaces.Add(iface);
            }
        }

        private async Task CreateMultiPortId()
        {
            var usedMultiPortIds = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(q => q.DeviceID == _attachment.DeviceID && q.IsMultiPort)
                                    select attachments)
                        .Select(q => q.ID).Where(q => q != null)
                        .ToList();

            int? id = Enumerable.Range(1, 65535).Except(usedMultiPortIds.Select(q => q.Value)).FirstOrDefault();

            _attachment.ID = id ?? throw new BuilderUnableToCompleteException("Unable to assign an ID value to the multiport attachment. "
                    + $"It seems that all IDs in the range 1 - 65535 for device '{_attachment.Device.Name}' have been used. " +
                    $"Contact your system administrator to report this issue.");
        }
    }
}
