using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCamerasController : MonoBehaviour
{
    [SerializeField] private GameManagerSO GM;
    [SerializeField] private int idCamera;
    [SerializeField] private float cameraDuration = 5.5f;

    private Camera doorCamera;

    // Start is called before the first frame update
    void Start()
    {
        doorCamera = GetComponent<Camera>();
        GM.OnCameraEvent += ActivateCamera;

        if (doorCamera != null)
        {
            doorCamera.enabled = false;
        }
    }

    private void ActivateCamera (int idActivador)
    {
        if (idActivador == idCamera && doorCamera != null)
        {
            doorCamera.enabled = true;
            Invoke(nameof(DeactivateCamera), cameraDuration);
        }
    }

    private void DeactivateCamera()
    {
        if (doorCamera != null)
        {
            doorCamera.enabled = false;
        }
    }

    private void Destroy ()
    {
        if (GM != null)
        {
            GM.OnCameraEvent -= ActivateCamera;
        }
    }
}
