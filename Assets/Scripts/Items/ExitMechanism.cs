using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMechanism : MonoBehaviour
{
    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    
    // DOORS
    public float leftDoorAngle;
    public float rightDoorAngle;
    public float doorRotationSpeed = 2f;
    private Quaternion leftDoorInitialRotation;
    private Quaternion leftDoorTargetRotation;
    private Quaternion rightDoorInitialRotation;
    private Quaternion rightDoorTargetRotation;
    private bool isOpening = false;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    // PLATFORM + CANON
    public float targetY = 2f;
    public float riseSpeed = 1f;
    private Vector3 platofrmInitialPosition;
    private Vector3 platformTargetPosition;
    private bool isRising = false;
    [SerializeField] GameObject ascendingPlatform;

    // CAMERA
    [SerializeField] private Camera exitMechanismCamera;
    [SerializeField] private float cameraDuration = 5f;

    // SOUND
    private AudioSource audioSource;
    [SerializeField] private AudioClip elevatorDoorOpen;


    // Start is called before the first frame update
    void Start()
    {
        // DOORS
        leftDoorInitialRotation = leftDoor.transform.rotation;
        leftDoorTargetRotation = Quaternion.Euler(leftDoor.transform.rotation.eulerAngles.x, leftDoor.transform.rotation.eulerAngles.y, leftDoorAngle);
        rightDoorInitialRotation = rightDoor.transform.rotation;
        rightDoorTargetRotation = Quaternion.Euler(rightDoor.transform.rotation.eulerAngles.x, rightDoor.transform.rotation.eulerAngles.y, rightDoorAngle);
    
        // PLATFORM + CANON
        platofrmInitialPosition = ascendingPlatform.transform.localPosition;
        platformTargetPosition = new Vector3 (platofrmInitialPosition.x, targetY, platofrmInitialPosition.z);

        // CAMERA
        exitMechanismCamera.enabled = false;

        // SOUND
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        sphereCollectorManagerSO.OnAllSpheresPlaced += ActivateExitMechanism;
    }

    private void OnDisable()
    {
        sphereCollectorManagerSO.OnAllSpheresPlaced -= ActivateExitMechanism;
    }

    // Update is called once per frame
    void Update()
    {
        // DOORS
        if (isOpening)
        {
            Debug.Log ("Opening doors");
            if (Quaternion.Angle(leftDoor.transform.rotation, leftDoorTargetRotation) > 1f)
            {
                leftDoor.transform.rotation = Quaternion.Lerp(leftDoor.transform.rotation, leftDoorTargetRotation, Time.deltaTime * doorRotationSpeed);
            }

            if (Quaternion.Angle(rightDoor.transform.rotation, rightDoorTargetRotation) > 1f)
            {
                rightDoor.transform.rotation = Quaternion.Lerp(rightDoor.transform.rotation, rightDoorTargetRotation, Time.deltaTime * doorRotationSpeed);
            }
            
        }

        // PLATFORM + CANON
        if (isRising)
        {
            ascendingPlatform.transform.localPosition = Vector3.Lerp (ascendingPlatform.transform.localPosition, platformTargetPosition, Time.deltaTime * riseSpeed);
        }
    }

    public void ActivateExitMechanism()
    {
        ActivateCamera();
        PlaySound();
        OpenDoor();
        StartRising();
    }

    public void OpenDoor()
    {
        isOpening = true;
    }

    public void StartRising()
    {
        isRising = true;
    }

    private void ActivateCamera ()
    {
        exitMechanismCamera.enabled = true;
        Invoke(nameof(DeactivateCamera), cameraDuration);
    }

    private void DeactivateCamera()
    {
        exitMechanismCamera.enabled = false;
    }

    private void PlaySound()
    {
        if (audioSource != null && elevatorDoorOpen != null)
        {
            audioSource.PlayOneShot(elevatorDoorOpen);
        }
    }
}
