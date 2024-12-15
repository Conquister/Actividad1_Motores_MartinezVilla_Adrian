using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneButtonsManager : MonoBehaviour
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
        ResetGameState();
    }
    
    public void ExitGame ()
    {
        Debug.Log ("Cerrando el juego...");
        Application.Quit ();
    }

    private void ResetGameState()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Estado del juego reiniciando...");
    }
}
