using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{

    [SerializeField] private string victorySceneName = "Victory";           // Declaramos la variable 'victorySceneName' de tipo String y la inicializamos con "Victory"
    
    private void OnTriggerEnter (Collider other)                            // Este método se ejecuta cuando otro GameObject de la escena entra en contacto con el Compontente 'Collider' de este GameObject
    {
        if (other.CompareTag("Player"))                                     // Verificamos si el GameObject que ha entrado en contacto tiene una etiqueta "Player". Es decir, si es nuestro Player 
        {
            LoadVictoryScene();                                             // En caso de serlo, llamamos al método LoadVictoryScene()
        }
    }

    private void LoadVictoryScene()                                         // Este método carga la escena Victory
    {
        SceneManager.LoadScene(victorySceneName);
    }
}
