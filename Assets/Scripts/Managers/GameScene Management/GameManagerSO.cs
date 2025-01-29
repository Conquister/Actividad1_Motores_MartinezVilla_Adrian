using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePlayManager", menuName = "Managers/GamePlayManager")]
public class GameManagerSO : ScriptableObject
{
    public event Action<int> OnActivadorActivado;
    public event Action<int> OnCameraEvent;
    public event Action OnPlayerHit;
    public event Action OnPlayerCured;
    public event Action OnMissionAccomplished;
    
    
    public void PulsadorPulsado(int idActivador)
    {
        OnActivadorActivado?.Invoke(idActivador);
        OnCameraEvent?.Invoke(idActivador);
    }

    public void PlayerHit()
    {
        OnPlayerHit?.Invoke();
    }

    public void PlayerCured()
    {
        OnPlayerCured?.Invoke();
    }

    public void MissionAccomplished()
    {
        Debug.Log (" El cañón me ha avisado");
        OnMissionAccomplished?.Invoke();
    }
}
