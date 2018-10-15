using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.ViewModels
{
    public class MtuViewModel
    {
        [Display(AutoGenerateField = false)]
        public int MtuID { get; set; }
        public int MtuValue { get; set; }
        public bool ValueIncludesLayer2Overhead { get; set; }
    }
}
