namespace Contador_para_Wizard.Models {
    public class Jugador {
        public string nombre {  get; set; }
        public int puntos { get; set; }
        public int apuesta { get; set; }
        public Jugador(string nombre) { 
            this.nombre = nombre;
            this.puntos = 0;
            this.apuesta = 0;
            }
        }
    }
