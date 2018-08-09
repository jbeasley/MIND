using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public abstract class BaseValidator
    {
        public BaseValidator()
        {
        }

        public IValidationDictionary ValidationDictionary { get; set; }
        public bool IsValid { get
            {
                return ValidationDictionary.IsValid;
            }
        }
    }
}
