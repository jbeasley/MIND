using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public class ModelStateWrapper : IValidationDictionary
    {

        private ModelStateDictionary _modelState;

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        public void Clear() 
        {
            _modelState.Clear();
        }

        public bool IsValid
        {
            get { return _modelState.IsValid; }
        }
    }
}
