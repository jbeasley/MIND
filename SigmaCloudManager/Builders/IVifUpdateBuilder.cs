﻿using Mind.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IVifUpdateBuilder
    {
        IVifUpdateBuilder ForVif(int vifId);
        IVifUpdateBuilder WithContractBandwidth(int? contractBandwidthMbps);
        IVifUpdateBuilder WithExistingRoutingInstance(string existingRoutingInstanceName);
        IVifUpdateBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp);
        IVifUpdateBuilder WithIpv4(List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4AddressesAndMask);
        IVifUpdateBuilder WithJumboMtu(bool? useJumboMtu);
        Task<SCM.Models.Vif> UpdateAsync();
    }
}
