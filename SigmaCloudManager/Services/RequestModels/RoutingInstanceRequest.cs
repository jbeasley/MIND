
namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model of a routing instance request
    /// </summary>
    public class RoutingInstanceRequest
    {
        /// <summary>
        /// The requested name of the routing instance
        /// </summary>
        /// <value>A string value denoting the requested name of the routing instance</value>
        public string Name { get; set; }

        /// <summary>
        /// The requested route distinguisher range from which a new route distinguisher should be assigned
        /// </summary>
        /// <value>A member of the RouteDistinguisherRangeTypeEnum enumeration</value>
        public RouteDistinguisherRangeTypeEnum? RangeType { get; set; }

        /// <summary>
        /// The requested administrator sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the requested assigned-number sub-field of the routing instance</value>
        public int? AdministratorSubField { get; set; }

        /// <summary>
        /// The requested assigned-number sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the requested assigned-number sub-field of the routing instance</value>
        public int? AssignedNumberSubField { get; set; }
    }
}
