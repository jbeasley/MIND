using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using SCM.Services;
using System.Collections.Generic;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating Route Targets
    /// </summary>
    public class RouteTargetFactory : BaseFactory, IRouteTargetFactory
    {
        public RouteTargetFactory(IMapper mapper,
            IUnitOfWork unitOfWork) : base(mapper)
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Create a new Route Target
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewAsync(RouteTargetRequest request)
        {
            var routeTarget = Mapper.Map<RouteTarget>(request);
            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = routeTarget
            };

            var dbResult = await UnitOfWork.RouteTargetRangeRepository.GetAsync(q => q.RouteTargetRangeID == request.RouteTargetRangeID);
            var rtRange = dbResult.SingleOrDefault();

            if (rtRange == null)
            {
                throw new FactoryFailureException($"The Route Target range '{rtRange.Name}' was not found.");
            }

            if (request.AutoAllocateAssignedNumberSubField)
            {
                var currentRts = await UnitOfWork.RouteTargetRepository.GetAsync(q => q.RouteTargetRangeID == request.RouteTargetRangeID);

                // Allocate a new unused rt from the rt range, excluding currently allocated RTs

                int? newAssignedNumber = Enumerable.Range(rtRange.AssignedNumberSubFieldStart, rtRange.AssignedNumberSubFieldCount)
                    .Except(currentRts.Select(rt => rt.AssignedNumberSubField)).FirstOrDefault();

                if (newAssignedNumber == null)
                {
                    throw new FactoryFailureException("Failed to allocate a free Route Target. "
                        + "Please contact your administrator, or try another range.");
                }

                routeTarget.RouteTargetRangeID = rtRange.RouteTargetRangeID;
                routeTarget.AssignedNumberSubField = newAssignedNumber.Value;
            }
            else
            {
                var rtQuery = await UnitOfWork.RouteTargetRepository.GetAsync(q => q.RouteTargetRangeID == rtRange.RouteTargetRangeID
                    && q.AssignedNumberSubField == request.RequestedAssignedNumberSubField, includeProperties: "Vpn");
                var rt = rtQuery.SingleOrDefault();

                if (rt == null)
                {
                    routeTarget.AssignedNumberSubField = request.RequestedAssignedNumberSubField.Value;
                }
                else
                {
                    throw new FactoryFailureException($"The requested Route Target is already in use for VPN '{rt.Vpn.Name}'. "
                                                      + "Try again with a different Route Target.");
                }
            }

            return result;
        }

        /// <summary>
        /// Create a number of Route Targets as needed for a VPN. The number of Route Targets
        /// created depends on the topology of the VPN.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewForVpnAsync(RouteTargetRequest request)
        {
            var result = new FactoryResult
            {
                IsSuccess = true
            };

            var rtRangeResult = await UnitOfWork.RouteTargetRangeRepository.GetAsync(q => q.RouteTargetRangeID == request.RouteTargetRangeID);
            var rtRange = rtRangeResult.SingleOrDefault();

            if (rtRange == null)
            {
                throw new FactoryFailureException($"The requested Route Target Range '{rtRange.Name}' was not found.");
            }

            var vpnTopologyType = await UnitOfWork.VpnTopologyTypeRepository.GetByIDAsync(request.VpnTopologyTypeID);
            if (vpnTopologyType == null)
            {
                throw new FactoryFailureException("The VPN Topology Type is not valid.");
            }

            // Get some Route Targets

            var GetRouteTargetsResult = await GetRouteTargetAssignedNumbersAsync(vpnTopologyType, rtRange);
            if (!GetRouteTargetsResult.IsSuccess)
            {
                return GetRouteTargetsResult;
            }

            var rtAssignedNumbers = (List<int>)GetRouteTargetsResult.Item;
            var rts = new List<RouteTarget>();

            for (int i = 0; i <= rtAssignedNumbers.Count() - 1; i++)
            {
                rts.Add(new RouteTarget
                {
                    AssignedNumberSubField = rtAssignedNumbers.ToList()[i],
                    RouteTargetRangeID = rtRange.RouteTargetRangeID,
                    IsHubExport = vpnTopologyType.TopologyType == TopologyType.HubandSpoke && i == 1
                });
            }

            result.Item = rts;
            return result;
        }

        /// <summary>
        /// Get some free assigned numbers for Route Targets
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task<FactoryResult> GetRouteTargetAssignedNumbersAsync(VpnTopologyType vpnTopologyType, 
                                                                              RouteTargetRange rtRange)
        {
            var result = new FactoryResult
            {
                IsSuccess = true
            };

            // Get used RTs from repository - these will be eliminated when calculated free route targets

            var usedRTs = await UnitOfWork.RouteTargetRepository.GetAsync(q => q.RouteTargetRangeID == rtRange.RouteTargetRangeID);

            // Allocate new unused RTs from the RT range

            IEnumerable<int> rtAssignedNumbers = null;

            if (vpnTopologyType.TopologyType == TopologyType.AnytoAny)
            {
                // One RT required for Any-to-Any VPN

                rtAssignedNumbers = Enumerable.Range(rtRange.AssignedNumberSubFieldStart, rtRange.AssignedNumberSubFieldCount)
                    .Except(usedRTs.Select(q => q.AssignedNumberSubField)).Take(1);

                if (rtAssignedNumbers.Count() != 1)
                {
                    throw new FactoryFailureException("Failed to allocate a free Route Target. " 
                        + "Please contact your administrator, or try another range.");
                }
            }
            else if (vpnTopologyType.TopologyType == TopologyType.HubandSpoke)
            {
                // Two RTs required for Hub-and-Spoke VPN

                rtAssignedNumbers = Enumerable.Range(rtRange.AssignedNumberSubFieldStart, rtRange.AssignedNumberSubFieldCount)
                    .Except(usedRTs.Select(q => q.AssignedNumberSubField)).Take(2);

                if (rtAssignedNumbers.Count() != 2)
                {
                    throw new FactoryFailureException("Failed to allocate two free Route Targets. " 
                        + "Please contact your administrator, or try another range.");
                }
            }

            result.Item = rtAssignedNumbers.ToList();
            return result;
        }
    }
}