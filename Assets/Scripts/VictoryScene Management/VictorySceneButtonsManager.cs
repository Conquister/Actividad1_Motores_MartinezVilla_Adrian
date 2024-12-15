using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneButtonsManager : MonoBehaviour
{
    [SerializeField] public enum SceneNames                             // Creamos la estructura enum para las diferentes escenas del juego: MainMenu, Game y Victory
    {
        MainMenu,
        Game,
        Victory
    }

    public void GoToMainMenu ()                                         // Este método nos llevará a la escena MainMenu. Se asignará al método OnClick del gameObject Button 'MainMenuButton' de la UI
    {
        SceneManager.LoadScene (SceneNames.MainMenu.ToString());      
        ResetGameState();                                               // Llámamos al método ResetGameState()
    }
    
    public void ExitGame ()                                             // Este método cierra la aplicación, pero solo en la Build. De momento no está en uso.
    {
        Debug.Log ("Cerrando el juego...");
        Application.Quit ();
    }

    private void ResetGameState()                                       // Este método hace que reseteen los estados del juego. Es peligroso, pero ahora funciona para, si es el caso, al acceder de nuevo
    {                                                                   // al MainMenu, volver a empezar el laberinto
        PlayerPrefs.DeleteAll();
        Debug.Log("Estado del juego reiniciando...");
    }
}
