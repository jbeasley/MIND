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
    public static class ViewDataExtensions
    {
        public static void AddDeletePreconditionFailedMessage(this ViewDataDictionary viewData)
        {
            viewData["ErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. The "
                    + "delete operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to delete this record, click "
                    + "the Delete button again.";
        }

        public static void AddDatabaseUpdateExceptionMessage(this ViewDataDictionary viewData)
        {
            viewData["ErrorMessage"] = "An error occurred while updating the database. " +
                 "Try again, and if the problem persists report it to your system administrator.";
        }
    }
}
