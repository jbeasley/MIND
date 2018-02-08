using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IValidator
    {
        IValidationDictionary ValidationDictionary { get; set; }
    }
}
