using Contador_para_Wizard.Interfaces;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Contador_para_Wizard.Models {
    public class PartidaManager : IPartidaManager {

        public PartidaData Datos { get; set; }

        public PartidaManager(IHttpContextAccessor contexto) {
            HttpContext ctx = contexto.HttpContext;
            if (!File.Exists($@"{Environment.CurrentDirectory}\Partidas\{ctx.Session.GetString("SessionID")}.json")) {
                string SessionFile = CrearSesion();
                ctx.Session.SetString("SessionID",SessionFile);
                }
            this.Datos = JsonSerializer.Deserialize<PartidaData>(File.ReadAllText($@"{Environment.CurrentDirectory}\Partidas\{ctx.Session.GetString("SessionID")}.json"));
            }

        private string CrearSesion() {
            DateTime now = DateTime.Now;
            string PartidaName = now.Hour.ToString() + now.Minute.ToString() + now.Day.ToString() + now.Month.ToString() + now.Year.ToString();

            PartidaData NewSession = new PartidaData { SessionID = PartidaName,Jugadores = new Jugador[0],cantidadJugadores = 0,numJuego = 1,repartidor = 0,CantidaddeJuegos = 0,ToWinner = false };

            string JSONdefaultData = JsonSerializer.Serialize(NewSession);

            File.WriteAllText($@"{Environment.CurrentDirectory}\Partidas\{PartidaName}.json",JSONdefaultData);

            return PartidaName;
            }

        private void ApuestasDump() {
            foreach (var i in Datos.Jugadores) {
                i.apuesta = 0;
                }
            }

        public void NewGame(string[] players) {
            List<Jugador> jugadoresbuff = new List<Jugador>();
            foreach (var p in players) {
                if (p != "default") {
                    jugadoresbuff.Add(new Jugador(p));
                    }
                }
            Datos.Jugadores = jugadoresbuff.ToArray();
            Datos.cantidadJugadores = jugadoresbuff.ToArray().Count();
            Datos.CantidaddeJuegos = 60 / Datos.cantidadJugadores;
            Datos.UpdateSession();
            }

        public void CrearApuestas(List<int> apuestas) {
            int player = 0;
            foreach (var p in apuestas) {
                Datos.Jugadores.ElementAt(player).apuesta = p;
                player++;
                }

            if (apuestas.Sum() == Datos.numJuego) {
                Datos.Jugadores.ElementAt(Datos.repartidor).apuesta = 0;
                throw new Exception($"La cantidad de apuestas no puede ser igual al número de ronda." /*{Datos.repartidor} no puede apostar {}.*/);
                }
            Datos.UpdateSession();
            }

        public void CrearPuntos(List<int> puntos) {
            if (puntos.Sum() != Datos.numJuego) {
                throw new Exception("Algo salió mal. La cantidad de puntos repartidos no coincide con la cantidad de manos jugadas.");
                }

            int player = 0;
            foreach (var p in puntos) {
                if (Datos.Jugadores.ElementAt(player).apuesta == p) {
                    Datos.Jugadores.ElementAt(player).puntos = Datos.Jugadores.ElementAt(player).puntos + 20 + p * 10;
                    }
                if (Datos.Jugadores.ElementAt(player).apuesta != p) {
                    int delta = Math.Abs(p - Datos.Jugadores.ElementAt(player).apuesta);
                    Datos.Jugadores.ElementAt(player).puntos = Datos.Jugadores.ElementAt(player).puntos - delta * 10;
                    }
                player++;
                }
            ApuestasDump();
            Datos.numJuego++;
            Datos.repartidor++;

            if (Datos.repartidor == Datos.cantidadJugadores) {
                Datos.repartidor = 0;
                }

            if (Datos.numJuego > Datos.CantidaddeJuegos) {
                Datos.ToWinner = true;
                }
            Datos.UpdateSession();
            }

        public List<Jugador> GoToWinner() {
            return Datos.Jugadores.OrderByDescending(x => x.puntos).ToList();
            }
        }
    }
