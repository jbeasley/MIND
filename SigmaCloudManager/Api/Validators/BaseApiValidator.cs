using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public abstract class BaseApiValidator
    {
        public BaseApiValidator()
        {
        }
        public IValidationDictionary ValidationDictionary { get; set; }
    }
}
