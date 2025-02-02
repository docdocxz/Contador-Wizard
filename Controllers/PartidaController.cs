using Contador_para_Wizard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contador_para_Wizard.Controllers {
    public class PartidaController : Controller {
        private static bool IsPrincipiodeJuego = true;
        private static string errormsg = String.Empty;

        [Route("/partida")]
        public IActionResult Partida() {
            try {
                ViewData["Repartidor"] = PartidaManager.Jugadores.ElementAt(PartidaManager.repartidor).nombre;
                }
            catch (ArgumentOutOfRangeException) {
                PartidaManager.repartidor = 0;
                ViewData["Repartidor"] = PartidaManager.Jugadores.ElementAt(PartidaManager.repartidor).nombre;
                }
            ViewData["numJuego"] = PartidaManager.numJuego;
            ViewData["error"] = errormsg;


            if (IsPrincipiodeJuego) {
                return View("Views/PartidaInicio.cshtml",PartidaManager.Jugadores);
                } else {
                if (PartidaManager.ToWinner == true) {
                    return View("/Views/Winner.cshtml",PartidaManager.GoToWinner());
                    }
                return View("Views/PartidaFin.cshtml",PartidaManager.Jugadores);
                }
            }

        [HttpPost("/partida/principiodejuego")]
        public IActionResult PrincipiodeJuego(int a1, int a2, int a3, int a4, int a5, int a6) {
            int[] apuestasArr = {a1,a2,a3,a4,a5,a6};
            List<int> apuestasList = apuestasArr.ToList();
            apuestasList.RemoveRange(PartidaManager.cantidadJugadores,apuestasArr.Length - PartidaManager.cantidadJugadores);

            try {
                PartidaManager.CrearApuestas(apuestasList);
                }
            catch (Exception ex) {
                errormsg = ex.Message;
                return Redirect("/partida");
                }

            IsPrincipiodeJuego = false;
            errormsg = String.Empty;
            return Redirect("/partida");
            }

        [HttpPost("/partida/findejuego")]
        public IActionResult FindeJuego(int p1,int p2,int p3,int p4,int p5,int p6) {
            int[] puntosArr = { p1,p2,p3,p4,p5,p6 };
            List<int> puntosList = puntosArr.ToList();
            puntosList.RemoveRange(PartidaManager.cantidadJugadores,puntosArr.Length - PartidaManager.cantidadJugadores);

            try {
                PartidaManager.CrearPuntos(puntosList);
                }
            catch (Exception ex) {
                errormsg = ex.Message;
                return Redirect("/partida");
                }

            IsPrincipiodeJuego = true;
            errormsg = String.Empty;
            return Redirect("/partida");
            }
        }
    }
