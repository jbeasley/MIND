using SCM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.RequestModels
{
    public class AttachmentPortUpdate
    {
        public int AttachmentID { get; set; }
        public int PortID { get; set; }
        public int CurrentPortID { get; set; }
    }
}
