using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contador_para_Wizard.Models {
    public class PartidaData {
        public string SessionID { get; set; }
        public Jugador[]? Jugadores { get; set; }
        public int cantidadJugadores { get; set; }
        public int numJuego { get; set; }
        public int repartidor { get; set; }
        public int CantidaddeJuegos { get; set; }
        public bool ToWinner { get; set; }
        public bool IsPrincipiodeJuego { get; set; }

        public PartidaData(string sesion) {
            if (!File.Exists($@"{Environment.CurrentDirectory}\Partidas\{sesion}.json")) {
                CrearSesion();
                } else {
                JsonSerializer.Deserialize<PartidaData>(File.ReadAllText($@"{Environment.CurrentDirectory}\Partidas\{sesion}.json"));
                }
            }

        public void CrearSesion() {
            DateTime now = DateTime.Now;
            string PartidaName = now.Hour.ToString() + now.Minute.ToString() + now.Day.ToString() + now.Month.ToString() + now.Year.ToString();

            SessionID = PartidaName;
            Jugadores = new Jugador[0];
            cantidadJugadores = 0;
            numJuego = 1;
            repartidor = 0;
            CantidaddeJuegos = 0;
            ToWinner = false;

            string JSONdefaultData = JsonSerializer.Serialize(this);

            File.WriteAllText($@"{Environment.CurrentDirectory}\Partidas\{PartidaName}.json",JSONdefaultData);
            }


        public void UpdateSession() {
            string datosactuales = JsonSerializer.Serialize(this);
            File.WriteAllText($@"{Environment.CurrentDirectory}\Partidas\{this.SessionID}.json",datosactuales);
            }
        }
    }
