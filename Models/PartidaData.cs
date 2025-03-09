using System.Text.Json;

namespace Contador_para_Wizard.Models {
    public class PartidaData {
        public Jugador[]? Jugadores { get; set; }
        public int cantidadJugadores { get; set; }
        public int numJuego { get; set; }
        public int repartidor { get; set; }
        public int CantidaddeJuegos { get; set; }
        public bool ToWinner { get; set; }
        }
    }
