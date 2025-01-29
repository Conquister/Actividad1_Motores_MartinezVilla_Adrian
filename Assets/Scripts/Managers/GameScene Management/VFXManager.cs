using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class VFXManager : MonoBehaviour
{   
    [System.Serializable]
    public class VFX
    {
        public string name;
        public GameObject VFXPrefab;
        public float duration = 2f;
    }

    [SerializeField] private VFX[] VFXArray;

    [SerializeField] private GameManagerSO gameManagerSO;

    private void OnEnable()
    {
        gameManagerSO.OnPlayerCured += PlayerCured;
    }

    private void OnDisable()
    {
        gameManagerSO.OnPlayerCured -= PlayerCured;
    }

    private void PlayerCured()
    {
        GameObject healVFX = GetVFXByName ("Heal");
        if (healVFX != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                Vector3 VFXPosition = player.transform.position;
                VFXPosition.y = 0;
                GameObject VFXInstance = Instantiate(healVFX, VFXPosition, Quaternion.identity, player.transform);
                Destroy (VFXInstance, GetVFXDurationByName("Heal"));
            }
        }
    }
    
    private GameObject GetVFXByName (string name)
    {
        foreach (VFX vfx in VFXArray)
        {
            if (vfx.name == name)
            {
                return vfx.VFXPrefab;
            }
        }
        return null;
    }

    private float GetVFXDurationByName(string name)
    {
        foreach (VFX vfx in VFXArray)
        {
            if (vfx.name == name)
            {
                return vfx.duration;
            }
        }
        return 2f;
    }
}
