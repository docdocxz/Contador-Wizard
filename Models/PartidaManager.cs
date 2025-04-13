using Contador_para_Wizard.Interfaces;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Contador_para_Wizard.Models {
    public partial class PartidaManager : IPartidaManager {
        public PartidaManager(IHttpContextAccessor contexto) {
            HttpContext ctx = contexto.HttpContext;

            if (!File.Exists($@"{Environment.CurrentDirectory}\Partidas\{ctx.Session.GetString("SessionID")}.json")) {
                CrearNuevaSesion();
                ctx.Session.SetString("SessionID",Datos.SessionID);
                } else {
                Datos = JsonSerializer.Deserialize<PartidaManager.PartidaData>(File.ReadAllText($@"{Environment.CurrentDirectory}\Partidas\{ctx.Session.GetString("SessionID")}.json"));
                }
            }

        private void CrearNuevaSesion() {
            DateTime now = DateTime.Now;
            string PartidaName = now.Hour.ToString() + now.Minute.ToString() + now.Day.ToString() + now.Month.ToString() + now.Year.ToString();

            Datos = new PartidaData() {
                SessionID = PartidaName,
                Jugadores = new Jugador[0],
                cantidadJugadores = 0,
                numJuego = 1,
                repartidor = 0,
                CantidaddeJuegos = 0,
                ToWinner = false,
                IsPrincipiodeJuego = true
                };

            string JSONdefaultData = JsonSerializer.Serialize(Datos);

            File.WriteAllText($@"{Environment.CurrentDirectory}\Partidas\{PartidaName}.json",JSONdefaultData);
            }

        private PartidaData Datos { get; set; }
        public Jugador[] GetJugadores() {
            return Datos.Jugadores;
            }
        public Jugador GetJugadores(int index) {
            return Datos.Jugadores.ElementAt(index);
            }
        public Jugador GetJugadores(string name) {
            return Datos.Jugadores.Where(p => p.nombre == name).Single();
            }

        public int GetRepartidor() {
            return Datos.repartidor;
            }
        public int GetNumJuego() {
            return Datos.numJuego;
            }

        public bool IsWinner() {
            return Datos.ToWinner;
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
            apuestas.RemoveRange(Datos.cantidadJugadores,apuestas.Count - Datos.cantidadJugadores);

            int player = 0;
            foreach (var p in apuestas) {
                Datos.Jugadores.ElementAt(player).apuesta = p;
                player++;
                }

            if (apuestas.Sum() == Datos.numJuego) {
                int apuestaRepartidor = Datos.Jugadores.ElementAt(Datos.repartidor).apuesta;
                Datos.Jugadores.ElementAt(Datos.repartidor).apuesta = 0;
                throw new Exception($"La cantidad de apuestas no puede ser igual al número de ronda. {Datos.repartidor} no puede apostar {apuestaRepartidor}");
                }
            Datos.UpdateSession();
            }

        public void CrearPuntos(List<int> puntos) {
            puntos.RemoveRange(Datos.cantidadJugadores,puntos.Count - Datos.cantidadJugadores);

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

            foreach (var i in Datos.Jugadores) {
                i.apuesta = 0;
                }

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
