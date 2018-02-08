using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IValidationDictionary
    {
        bool IsValid { get; }
        void Clear();
        void AddError(string key, string errorMessage);
    }
}
