using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneButtons : MonoBehaviour
{
    [SerializeField] public enum SceneNames
    {
        MainMenu,
        Game,
        Victory
    }

    public void GoToMainMenu ()
    {
        Debug.Log ("Regresando al Main Menu...");
        SceneManager.LoadScene (SceneNames.MainMenu.ToString());

        // Para reiniciar el estado del MainMenuManager
        MainMenuSceneButtonsManager mainMenuManager = FindFirstObjectByType<MainMenuSceneButtonsManager>();
        if (mainMenuManager != null)
        {
            mainMenuManager.enabled = true;     // Asegurarse de que el script esté activo
        }
    }
    
    public void ExitGame ()
    {
        Debug.Log ("Cerrando el juego...");
        Application.Quit ();
    }
}
