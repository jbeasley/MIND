using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.NetModels.VpnNetModels
{
    public class RouteTargetNetModel
    {
        public string AdministratorSubField { get; set; }
        public string AssignedNumberSubField { get; set; }
    }

    public abstract class PeBaseNetModel
    {
        public string PEName { get; set; }
    }

    public abstract class RoutingInstanceBaseNetModel
    {
        public int RoutingInstanceID { get; set; }
        public string RoutingInstanceName { get; set; }
    }

    public abstract class VpnAttachmentSetBaseNetModel
    {
        public string Name { get; set; }
    }

    public abstract class VpnServiceBaseNetModel
    {
        public string Name { get; set; }
        public string ProtocolType { get; set; }
    }
}
