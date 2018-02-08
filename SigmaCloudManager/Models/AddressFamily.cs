using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models
{
    public class AddressFamily
    {
        public int AddressFamilyID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } 
    }
}
