using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    public class AttachmentBandwidthViewModel
    {
        /// <summary>
        /// The ID of the attachment bandwidth
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment bandwidth</value>
        public int AttachmentBandwidthID { get; set; }

        /// <summary>
        /// The bandwidth value in Gbps
        /// </summary>
        /// <value>Integer denoting the bandwidth value</value>
        public int BandwidthGbps { get; set; }
    }
}