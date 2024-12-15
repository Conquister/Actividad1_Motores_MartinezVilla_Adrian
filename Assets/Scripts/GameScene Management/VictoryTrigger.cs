using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{

    [SerializeField] private string victorySceneName = "Victory";
    
    private void OnTriggerEnter (Collider other)
    {
        // Verificamos si el objeto que entra en el Trigger es el gameObject con el Tag Player
        if (other.CompareTag("Player"))
        {
            Debug.Log ("El Player ha entrado en el área de victoria.");
            LoadVictoryScene();
        }
    }

    private void LoadVictoryScene()
    {
        // Cargamos la escena de victoria
        SceneManager.LoadScene(victorySceneName);
    }
}
