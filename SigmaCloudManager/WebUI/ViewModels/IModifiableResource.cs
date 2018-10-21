using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    public interface IModifiableResource
    {
        byte[] RowVersion { get; set; }
    }
}
