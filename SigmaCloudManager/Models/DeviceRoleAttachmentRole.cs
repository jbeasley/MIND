using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models
{

    public class DeviceRoleAttachmentRole
    {
        public int DeviceRoleAttachmentRoleID { get; private set; }
        public int DeviceRoleID { get; set; }
        public int AttachmentRoleID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual DeviceRole DeviceRole { get; set; }
        public virtual AttachmentRole AttachmentRole { get; set; }
    }
}