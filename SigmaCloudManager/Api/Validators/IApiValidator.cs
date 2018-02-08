using SCM.Api.Models;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface IApiValidator
    {
        IValidationDictionary ValidationDictionary { get; set; }
    }
}
