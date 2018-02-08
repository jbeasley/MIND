using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.ViewModels
{
    public class AttachmentPortUpdateViewModel
    {
        public int AttachmentID { get; set; }
        public int InterfaceID { get; set; }
        [Required(ErrorMessage = "A port must be selected")]
        public int PortID { get; set; }
        public int CurrentPortID { get; set; }
        public int LocationID { get; set; }
        public int? PlaneID { get; set; }
        [Display(Name = "Plane")]
        public PlaneViewModel Plane { get; set; }
        [Required(ErrorMessage = "A device must be selected")]
        public int DeviceID { get; set; }
        [Display(Name = "Device")]
        public DeviceViewModel Device { get; set; }
        [Display(Name = "Port")]
        public PortViewModel Port { get; set; }
        public string Name { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
