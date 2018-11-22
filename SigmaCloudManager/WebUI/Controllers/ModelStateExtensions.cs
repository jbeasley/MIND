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
        public static void RemoveConcurrencyTokenItem(this ModelStateDictionary modelState)
        {
            modelState.Remove("RowVersion");
        }

        public static void AddUpdatePreconditionFailedMessage(this ModelStateDictionary modelState)
        {
            modelState.AddModelError(string.Empty, "The record you attempted to edit " +
                "was modified by another user after you got the original values. The " +
                "edit operation was cancelled. If you still want to edit this record, click " +
                "the Save button again. Otherwise refresh the page to see the current values. " +
                "Any unsaved changes will be lost.");
        }

        public static void AddDatabaseUpdateExceptionMessage(this ModelStateDictionary modelState)
        {
            modelState.AddModelError(string.Empty, "An error occurred while updating the database. " +
                 "Try again, and if the problem persists report it to your system administrator.");
        }

        public static void AddNovaClientApiExceptionMessage(this ModelStateDictionary modelState)
        {
            modelState.AddModelError(string.Empty, "An error occurred while updating the network. " +
                 "No changes have been applied. Try again, and if the problem persists report it to your system administrator.");
        }
    }
}
