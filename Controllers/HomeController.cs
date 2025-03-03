using Contador_para_Wizard.Interfaces;
using Contador_para_Wizard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contador_para_Wizard.Controllers {
    public class HomeController : Controller {

        [Route("/")]
        [Route("/home")]
        public IActionResult Home() {
            return View("/Views/Home.cshtml");
            }
        }
    }
