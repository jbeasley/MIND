using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the status component of a port
    /// </summary>
    public class PortStatusComponentViewModel
    {
        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>Member of the PortStatusTypeEnum enunmeration</value>
        [Required]
        [Display(Name = "Port Status")]
        public PortStatusTypeEnum? PortStatus { get; set; } = PortStatusTypeEnum.Free;

    }
}
