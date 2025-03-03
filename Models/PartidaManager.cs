using Contador_para_Wizard.Interfaces;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Contador_para_Wizard.Models {
    public class PartidaManager : IPartidaManager {

        public PartidaData Datos { get; set; }

        public PartidaManager() {
            Datos = new PartidaData();
            }

        private void ApuestasDump() {
            foreach (var i in Datos.Jugadores) {
                i.apuesta = 0;
                }
            }

        public void NewGame(string[] players) {
            foreach (var p in players) {
                if (p != "default") {
                    Datos.Jugadores.Add(new Jugador(p));
                    Datos.cantidadJugadores++;
                    }
                }
            Datos.CantidaddeJuegos = 60 / Datos.cantidadJugadores;
            }

        public void CrearApuestas(List<int> apuestas) {
            int player = 0;
            foreach (var p in apuestas) {
                Datos.Jugadores.ElementAt(player).apuesta = p;
                player++;
                }

            if (apuestas.Sum() == Datos.numJuego) {
                Datos.Jugadores.ElementAt(Datos.repartidor).apuesta = 0;
                throw new Exception($"La cantidad de apuestas no puede ser igual al número de ronda. {Datos.repartidor} no puede apostar {Datos.numJuego}.");
                }
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

            if (Datos.numJuego > Datos.CantidaddeJuegos) {
                Datos.ToWinner = true;
                }
            }

        public List<Jugador> GoToWinner() {
            return Datos.Jugadores.OrderByDescending(x => x.puntos).ToList();
            }
        }
    }
