using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contador_para_Wizard.Models {
    public partial class PartidaManager {
        public class PartidaData {
            public string SessionID { get; set; }
            public Jugador[]? Jugadores { get; set; }
            public int cantidadJugadores { get; set; }
            public int numJuego { get; set; }
            public int repartidor { get; set; }
            public int CantidaddeJuegos { get; set; }
            public bool ToWinner { get; set; }
            public bool IsPrincipiodeJuego { get; set; }

            public void UpdateSession() {
                string datosactuales = JsonSerializer.Serialize(this);
                File.WriteAllText($@"{Environment.CurrentDirectory}\Partidas\{this.SessionID}.json",datosactuales);
                }
            }
        }
    }
