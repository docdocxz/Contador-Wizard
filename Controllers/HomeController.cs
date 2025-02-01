using Microsoft.AspNetCore.Mvc;

namespace Contador_para_Wizard_.Controllers {
    public class HomeController : Controller {
        [Route("/")]
        [Route("/home")]
        public IActionResult Home() {
            return View();
            }
        }
    }
