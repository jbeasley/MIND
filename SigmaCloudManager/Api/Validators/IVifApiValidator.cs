using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Api.Models;
using SCM.Models;

namespace SCM.Api.Validators
{
    public interface IVifApiValidator : IApiValidator
    {
        Task ValidateNewAsync(VifRequestApiModel request);
        void ValidateDelete(Vif vif);
    }
}
