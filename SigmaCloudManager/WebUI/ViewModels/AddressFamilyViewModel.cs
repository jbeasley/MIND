namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of am address family
    /// </summary>
    public class AddressFamilyViewModel
    {
        /// <summary>
        /// The ID of the address family
        /// </summary>
        /// <value>Integer value denoting the ID of the address family</value>
        public int AddressFamilyID { get; private set; }

        /// <summary>
        /// The name of the address family
        /// </summary>
        /// <value>String value denoting the name of the address family</value>
        public string Name { get; private set; }
    }
}
