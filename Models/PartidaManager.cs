using System.Numerics;
using System.Runtime.CompilerServices;

namespace Contador_para_Wizard.Models {
    public static class PartidaManager {
        public static List<Jugador> Jugadores = new List<Jugador>();
        public static int cantidadJugadores = 0;
        public static int numJuego = 1;
        public static int repartidor = 0;

        private static void ApuestasDump() {
            foreach (var i in Jugadores) {
                i.apuesta = 0;
                }
            }

        public static void NewGame(string[] players) {
            foreach (var p in players) {
                if (p != "default") {
                    Jugadores.Add(new Jugador(p));
                    cantidadJugadores++;
                    }
                }
            }

        public static void CrearApuestas(List<int> apuestas) {
            int player = 0;
            foreach (var p in apuestas) {
                Jugadores.ElementAt(player).apuesta = p;
                player++;
                }

            if (apuestas.Sum() == numJuego) {
                Jugadores.ElementAt(repartidor).apuesta = 0;
                throw new Exception($"La cantidad de apuestas no puede ser igual al número de ronda. {repartidor} no puede apostar {numJuego}.");
                }
            }

        public static void CrearPuntos(List<int> puntos) {
            if (puntos.Sum() != numJuego) {
                throw new Exception("Algo salió mal. La cantidad de puntos repartidos no coincide con la cantidad de manos jugadas.");
                }

            int player = 0;
            foreach (var p in puntos) {
                if (Jugadores.ElementAt(player).apuesta == p) {
                    Jugadores.ElementAt(player).puntos = Jugadores.ElementAt(player).puntos + 20 + p * 10;
                    }
                if (Jugadores.ElementAt(player).apuesta != p) {
                    int delta = Math.Abs(p - Jugadores.ElementAt(player).apuesta);
                    Jugadores.ElementAt(player).puntos = Jugadores.ElementAt(player).puntos - delta * 10;
                    }
                player++;
                }

            ApuestasDump();
            }
        }
    }
