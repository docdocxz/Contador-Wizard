using Contador_para_Wizard.Models;

namespace Contador_para_Wizard.Interfaces {
    public interface IPartidaManager {
        Jugador[] GetJugadores();
        int GetRepartidor();
        int GetNumJuego();
        bool IsWinner();
        void NewGame(string[] players);
        void CrearApuestas(List<int> apuestas);
        void CrearPuntos(List<int> puntos);
        List<Jugador> GoToWinner();
        }
    }
