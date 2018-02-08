using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public class RegionViewModel
    {
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "A Region must be selected.")]
        public int RegionID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Locale Community AS Number")]
        public int AutonomousSystemNumber { get; set; }
        [Display(Name = "Locale Community Number")]
        public int Number { get; set; }
       [Display(Name = "Locale Community")]
        public string LocaleCommunityName { get; set; }
        public byte[] RowVersion { get; set; }
    }
}