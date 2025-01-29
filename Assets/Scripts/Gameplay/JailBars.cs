using System.Collections;
using UnityEngine;

public class JailBars : MonoBehaviour
{
    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    [SerializeField] private Camera jailDoorsCamera;
    [SerializeField] private float cameraDuration = 4f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip openingDoorSound;
    private float speed = 1f;
    [SerializeField] private GameObject childToOpen;
    [SerializeField] private float delayBeforeOpeningBars = 5f;

    // Start is called before the first frame update
    void Start()
    {
        jailDoorsCamera.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        sphereCollectorManagerSO.OnAllSpheresPlaced += DelayedOpenBars;
    }

        private void OnDisable()
    {
        sphereCollectorManagerSO.OnAllSpheresPlaced -= DelayedOpenBars;
    }

    private void DelayedOpenBars()
    {
        StartCoroutine (OpenBarsAfterDelay());
    }

    private IEnumerator OpenBarsAfterDelay()
    {
        yield return new WaitForSeconds (delayBeforeOpeningBars);
        OpenBars();
    }

    private void OpenBars()
    {
        ActivateCamera();

        if(audioSource != null && openingDoorSound != null)
        {
            audioSource.PlayOneShot(openingDoorSound);
        }
        
        StartCoroutine(MoveBars());       
    }

    private IEnumerator MoveBars()
    {
        while (childToOpen.transform.position.y < 6f)
        {
            Vector3 newPosition = childToOpen.transform.position + Vector3.up * speed * Time.deltaTime;
            
            if (newPosition.y >= 6f)
            {
                newPosition.y = 6f;
            }

            childToOpen.transform.position = newPosition;
            yield return null;
        }
    }

    private void ActivateCamera ()
    {
        jailDoorsCamera.enabled = true;
        Invoke(nameof(DeactivateCamera), cameraDuration);
    }

    private void DeactivateCamera()
    {
        jailDoorsCamera.enabled = false;
    }

}
