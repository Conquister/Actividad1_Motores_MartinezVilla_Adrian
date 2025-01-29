using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpheresCounter : MonoBehaviour
{
    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private Image spheresCounterImage;
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject finalMessage;
    [SerializeField] private GameObject warningMessage;
    [SerializeField] private GameObject notificationsBox;
    [SerializeField] private GameObject notificationsBackgroundImage;
    [SerializeField] private GameObject pickUpMessage;
    [SerializeField] private GameObject placeSphereMessage;  
    [SerializeField] private GameObject letsGetOutMessage;
    [SerializeField] private GameObject OnMissionAccomplishedScreen;
    [SerializeField] private GameObject HUD;                                

    private void Start()
    {
        sphereCollectorManagerSO.ResetManager();
        Debug.Log("ResetManager llamado. collectedSpheres = " + sphereCollectorManagerSO.GetCollectedSpheres());
        UpdateSpheresCounterUI(sphereCollectorManagerSO.GetCollectedSpheres());
    }

    private void OnEnable()
    {
        // Nos suscribimos a los eventos del SO
        sphereCollectorManagerSO.OnPickUpSphereBoxesAndMessagesShow += ShowPickUpBoxesAndMessages;
        sphereCollectorManagerSO.OnSphereCollected += UpdateSpheresCounterUI;
        sphereCollectorManagerSO.OnAllSpheresCollected += ShowFinalMessage;
        sphereCollectorManagerSO.OnAllSpheresPlaced += ShowWarningMessage;
        sphereCollectorManagerSO.OnPlacedSphereBoxesAndMessagesShow += ShowPlaceBoxesAndMessages;
        sphereCollectorManagerSO.OnReadyToGetOutBoxesAndMessagesShow += ShowLetsGetOutBoxesAndMessages;
        gameManagerSO.OnMissionAccomplished += LoadGreetingsScreen;
        
    }

    private void OnDisable()
    {
        // Nos desuscribimos a los eventos del SO
        sphereCollectorManagerSO.OnPickUpSphereBoxesAndMessagesShow -= ShowPickUpBoxesAndMessages;
        sphereCollectorManagerSO.OnSphereCollected -= UpdateSpheresCounterUI;
        sphereCollectorManagerSO.OnAllSpheresCollected -= ShowFinalMessage;
        sphereCollectorManagerSO.OnAllSpheresPlaced -= ShowWarningMessage;
        sphereCollectorManagerSO.OnPlacedSphereBoxesAndMessagesShow -= ShowPlaceBoxesAndMessages;
        sphereCollectorManagerSO.OnReadyToGetOutBoxesAndMessagesShow -= ShowLetsGetOutBoxesAndMessages;
        gameManagerSO.OnMissionAccomplished -= LoadGreetingsScreen;
    }

    public void ShowPickUpBoxesAndMessages()
    {
        StartCoroutine (ShowPickUpBoxesAndMessagesForDuration(3f));
    }
    private IEnumerator ShowPickUpBoxesAndMessagesForDuration (float duration)
    {
        notificationsBox.SetActive(true);
        notificationsBackgroundImage.SetActive(true);
        pickUpMessage.SetActive(true);

        yield return new WaitForSeconds (duration);

        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);
        pickUpMessage.SetActive(false);
    }


    public void ShowPlaceBoxesAndMessages()
    {
        StartCoroutine (ShowPlaceBoxesAndMessagesForDuration (6f));
    }
    private IEnumerator ShowPlaceBoxesAndMessagesForDuration (float duration)
    {
        notificationsBox.SetActive(true);
        notificationsBackgroundImage.SetActive(true);
        placeSphereMessage.SetActive(true);

        yield return new WaitForSeconds (duration);
        
        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);
        placeSphereMessage.SetActive(false);
    }


    private void UpdateSpheresCounterUI (int spheresCollectedCounter)
    {
        // Actualiza el sprite del contador según las esferas que tengamos recogidas
        if (spheresCollectedCounter >= 0 && spheresCollectedCounter < numberSprites.Length)
        {
            spheresCounterImage.sprite = numberSprites[spheresCollectedCounter];
        }
    }

    private void ShowFinalMessage()
    {
        StartCoroutine (ShowFinalMessageForDuration(5f));
    }

    private IEnumerator ShowFinalMessageForDuration (float duration)
    {
        yield return new WaitForSeconds (duration);
        notificationsBox.SetActive(true);
        notificationsBackgroundImage.SetActive(true);
        finalMessage.SetActive(true);

        yield return new WaitForSeconds (duration);

        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);
        finalMessage.SetActive(false);
    }

    private void ShowWarningMessage()
    {
        StartCoroutine (ShowWarningMessageForDuration (10f));
    }

    private IEnumerator ShowWarningMessageForDuration(float duration)
    {
        yield return new WaitForSeconds (duration);
        notificationsBox.SetActive(true);
        notificationsBackgroundImage.SetActive(true);
        warningMessage.SetActive(true);

        yield return new WaitForSeconds (duration);

        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);
        warningMessage.SetActive(false);
    }

    private void ShowLetsGetOutBoxesAndMessages()
    {
        StartCoroutine(ShowLetsGetOutBoxesAndMessagesForDuration(5f));
    }

    private IEnumerator ShowLetsGetOutBoxesAndMessagesForDuration(float duration)
    {
        notificationsBox.SetActive(true);
        notificationsBackgroundImage.SetActive(true);
        letsGetOutMessage.SetActive(true);

         yield return new WaitForSeconds (duration);

        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);
        warningMessage.SetActive(false);
    }   

    private void LoadGreetingsScreen()
    {
        OnMissionAccomplishedScreen.SetActive(true);
        HUD.SetActive (false);
    }
}
