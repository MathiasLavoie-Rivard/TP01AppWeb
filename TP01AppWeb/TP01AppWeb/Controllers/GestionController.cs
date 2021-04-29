using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Controllers
{
    public class GestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
