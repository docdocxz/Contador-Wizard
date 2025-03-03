using Contador_para_Wizard.Models;

namespace Contador_para_Wizard.Interfaces {
    public interface IPartidaManager {
        PartidaData Datos { get; set; }
        void NewGame(string[] players);
        void CrearApuestas(List<int> apuestas);
        void CrearPuntos(List<int> puntos);
        List<Jugador> GoToWinner();
        }
    }
