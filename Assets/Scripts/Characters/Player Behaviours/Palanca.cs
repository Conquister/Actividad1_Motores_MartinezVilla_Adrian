using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    [SerializeField] private GameManagerSO GM;
    [SerializeField] private int idPalanca;
    private bool isActive = false;
    [SerializeField] AudioClip leverSound;
    private AudioSource audioSource;
    
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interactuar()
    {
        if(!isActive)
        {
            isActive = true;
            PlaySound();
            transform.localRotation = Quaternion.Euler(0f,270f,0f);
            StartCoroutine(DelayBeforeTriggeredEvent());
        }
    }

    private IEnumerator DelayBeforeTriggeredEvent()
    {
        yield return new WaitForSeconds(leverSound.length);
        GM.PulsadorPulsado(idPalanca);
    }

    private void PlaySound()
    {
        if (audioSource != null && leverSound != null)
        {
            audioSource.PlayOneShot(leverSound);
        }
    }
}
