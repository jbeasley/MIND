using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Data;

namespace SCM.Validators
{
    public abstract class BaseValidator
    {
        protected internal readonly IUnitOfWork _unitOfWork;

        public BaseValidator()
        {
        }

        public BaseValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IValidationDictionary ValidationDictionary { get; set; }
        public bool IsValid { get
            {
                return ValidationDictionary.IsValid;
            }
        }
    }
}
