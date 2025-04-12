using Contador_para_Wizard.Interfaces;
using Contador_para_Wizard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contador_para_Wizard.Controllers {
    [Route("partida")]
    public class PartidaController : Controller {

        private readonly IPartidaManager PartidaManager;

        public PartidaController(IPartidaManager manager) {
            PartidaManager = manager;
            }

        [HttpPost("crearpartida")]
        public IActionResult NuevaPartida(string j1,string j2,string j3,string j4,string j5,string j6) {
            string[] arrArgsparaNewGame = { j1,j2,j3,j4,j5,j6 };
            PartidaManager.NewGame(arrArgsparaNewGame);
            return Redirect("/partida");
            }


        [HttpPost("principiodejuego")]
        public IActionResult PrincipiodeJuego(int a1, int a2, int a3, int a4, int a5, int a6) {
            int[] apuestasArr = {a1,a2,a3,a4,a5,a6};
            List<int> apuestasList = apuestasArr.ToList();

            try {
                PartidaManager.CrearApuestas(apuestasList);
                }
            catch (Exception ex) {
                ViewData["error"] = ex.Message;
                return View("Views/PartidaInicio.cshtml",PartidaManager.GetJugadores());
                }

            ViewData["Repartidor"] = PartidaManager.GetJugadores().ElementAt(PartidaManager.GetRepartidor()).nombre;

            ViewData["numJuego"] = PartidaManager.GetNumJuego();

            if (PartidaManager.IsWinner()) {
                return View("/Views/Winner.cshtml",PartidaManager.GoToWinner());
                }

            return View("Views/PartidaFin.cshtml",PartidaManager.GetJugadores());
            }

        [HttpPost("findejuego")]
        public IActionResult FindeJuego(int p1,int p2,int p3,int p4,int p5,int p6) {
            int[] puntosArr = { p1,p2,p3,p4,p5,p6 };
            List<int> puntosList = puntosArr.ToList();

            try {
                PartidaManager.CrearPuntos(puntosList);
                }
            catch (Exception ex) {
                ViewData["error"] = ex.Message;
                return View("Views/PartidaFin.cshtml",PartidaManager.GetJugadores());
                }


            return View("Views/PartidaInicio.cshtml",PartidaManager.GetJugadores());
            }
        }
    }
