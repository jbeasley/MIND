using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Api.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Mind.WebUI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelStateExtensions
    {
        public static void PopulateModelState(this ModelStateDictionary modelState, object model)
        {
            // Add the values of the properties of the current model to the modelstate dictionary
            foreach (var property in model.GetType().GetProperties())
            {
                modelState.AddModelError(property.Name, property.GetValue(model).ToString());
            }
        }

        public static void AddUpdatePreconditionFailedMessage(this ModelStateDictionary modelState)
        {
            modelState.AddModelError(string.Empty, "The record you attempted to edit " +
                "was modified by another user after you got the original values. The " +
                "edit operation was cancelled and the current values in the database " +
                "have been displayed. If you still want to edit this record, click " +
                "the Save button again.");
        }

        public static void AddDatabaseUpdateExceptionMessage(this ModelStateDictionary modelState)
        {
            modelState.AddModelError(string.Empty, "An error occurred while updating the database. " +
                 "Try again, and if the problem persists report it to your system administrator.");
        }
    }
}
