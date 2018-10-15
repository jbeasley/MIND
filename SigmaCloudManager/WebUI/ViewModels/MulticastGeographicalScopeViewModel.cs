using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.ViewModels
{
    public class MulticastGeographicalScopeViewModel
    {
        [Display(AutoGenerateField = false)]
        public int MulticastGeographicalScopeID { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "The name must contain letters and numbers only and no whitespace.")]
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
