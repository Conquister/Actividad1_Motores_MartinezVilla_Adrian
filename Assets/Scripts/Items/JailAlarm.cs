using System.Collections;
using UnityEngine;

public class JailAlarm : MonoBehaviour
{

    [SerializeField] private Light alarmLight;
    [SerializeField] private float blinkInterval = 0.5f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alarmSound;

    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    [SerializeField] private float delayBeforeActivatingAlarm = 5f; 

    private bool isAlarmActive = false;
    // Start is called before the first frame update
    void Start()
    {
        if (sphereCollectorManagerSO != null)
        {
            sphereCollectorManagerSO.OnAllSpheresPlaced += DelayBeforeActivating;
        }
        if (alarmLight != null)
        {
            alarmLight.enabled = false;
        }    

        if(audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy ()
    {
        if (sphereCollectorManagerSO != null)
        {
            sphereCollectorManagerSO.OnAllSpheresPlaced -= DelayBeforeActivating;
        }
    }

    private void DelayBeforeActivating()
    {
        StartCoroutine(ActivateAlarm());
    }
    private IEnumerator ActivateAlarm()
    {
        yield return new WaitForSeconds(delayBeforeActivatingAlarm);
        
        if(!isAlarmActive)
        {
            isAlarmActive = true;
            
            if (alarmLight != null)
            {
                alarmLight.enabled = true;
            }
            
            StartCoroutine(BlinkLight());

            if (audioSource != null && alarmSound != null)
            {
                audioSource.PlayOneShot(alarmSound);
            }
        }
    }

    public void DeactivateAlarm ()
    {
        isAlarmActive = false;

        if(alarmLight != null)
        {
            alarmLight.enabled = false;
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator BlinkLight()
    {
        while (isAlarmActive)
        {
            if(alarmLight != null)
            {
                alarmLight.enabled = !alarmLight.enabled;
            }
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
