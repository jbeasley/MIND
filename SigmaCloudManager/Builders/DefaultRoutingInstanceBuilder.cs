using SCM.Data;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for default routing instances. The builder exposes a fluent API.
    /// </summary>
    public class DefaultRoutingInstanceBuilder : BaseBuilder, IDefaultRoutingInstanceBuilder
    {
        private readonly RoutingInstance _routingInstance;

        public DefaultRoutingInstanceBuilder(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _routingInstance = new RoutingInstance
            {
                Name = Guid.NewGuid().ToString("N")                 
            };
        }

        public virtual IDefaultRoutingInstanceBuilder ForDevice(Device device)
        {
            if (device != null) _args.Add(nameof(ForDevice), device);
            return this;
        }

        public virtual async Task<RoutingInstance> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForDevice))) SetDevice();
            await SetRoutingInstanceTypeAsync();
            _routingInstance.Validate();
            return _routingInstance;
        }

        protected internal virtual void SetDevice()
        {
            var device = (Device)_args[nameof(ForDevice)];
            _routingInstance.Device = device;

        }
        
        protected internal virtual async Task SetRoutingInstanceTypeAsync()
        {
            var routingInstanceType = (from result in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                    q =>
                                       q.IsDefault == true,
                                       AsTrackable: true)
                                       select result)
                                       .SingleOrDefault();

            _routingInstance.RoutingInstanceType = routingInstanceType;
        }
    }
}
