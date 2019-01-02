using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mind.WebUI.Controllers
{
    public class InfrastructureServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
