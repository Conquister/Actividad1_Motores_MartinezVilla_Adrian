using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPopup;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button startButton;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        else
        {
            Debug.LogError ("AudioSource o VolumeSlider no asignados.");
        }


        if (howToPlayPopup != null)
        {
            howToPlayPopup.SetActive(false);
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public void ToggleHowToPlayPopup ()
    {
        if (howToPlayPopup != null)
        {
            howToPlayPopup.SetActive(!howToPlayPopup.activeSelf);
        }
    }
    

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame ()
    {
        Debug.Log ("Cerrando el juego...");
    }
}
