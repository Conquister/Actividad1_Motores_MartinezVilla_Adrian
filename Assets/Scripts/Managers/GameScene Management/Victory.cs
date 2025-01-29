using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    private float durationBeforeLoading = 5f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (gameManagerSO!= null)
        {
            gameManagerSO.OnMissionAccomplished += Greetings;
        }
    }

    private void OnDisable()
    {
        if (gameManagerSO!= null)
        {
            gameManagerSO.OnMissionAccomplished -= Greetings;
        }
    }

    private void Greetings()
    {
        Debug.Log ("El manager me ha notificado de que el player ha sido lanzado por el cañón");
        StartCoroutine(LoadVictorySceneAfterDelay());
    }

    private IEnumerator LoadVictorySceneAfterDelay()
    {
        yield return new WaitForSeconds(durationBeforeLoading);
        SceneManager.LoadScene(0);
    }
}
