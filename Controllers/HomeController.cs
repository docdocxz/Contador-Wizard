using Contador_para_Wizard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contador_para_Wizard.Controllers {
    public class HomeController : Controller {
        [Route("/")]
        [Route("/home")]
        public IActionResult Home() {
            return View("/Views/Home.cshtml");
            }

        [HttpPost("/home/crearpartida")]
        public IActionResult NuevaPartida(string j1, string j2, string j3,string j4, string j5, string j6) {
            string[] arrArgsparaNewGame = { j1, j2, j3, j4, j5, j6};
            PartidaManager.NewGame(arrArgsparaNewGame);
            ViewData["numJuego"] = PartidaManager.numJuego;
            ViewData["Repartidor"] = PartidaManager.Jugadores.ElementAt(PartidaManager.repartidor).nombre;
            return Redirect("/Partida");
            }
        }
    }
