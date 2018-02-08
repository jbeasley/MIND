using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating Contract Bandwidth Pools
    /// </summary>
    public class ContractBandwidthPoolFactory : BaseFactory, IContractBandwidthPoolFactory
    {
        public ContractBandwidthPoolFactory(IMapper mapper) : base(mapper)
        {
        }

        /// <summary>
        /// Create a new Contract Bandwidth Pool
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FactoryResult New(ContractBandwidthPool contractBandwidthPool)
        {
            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = contractBandwidthPool
            };

            contractBandwidthPool.Name = Guid.NewGuid().ToString("N");

            return result;
        }
    }
}
