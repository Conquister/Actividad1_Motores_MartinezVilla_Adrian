using UnityEngine;
using TMPro;

public class FuturasActualizaciones : MonoBehaviour
{
    public GameObject messageText;  //Referencia al gameObject Text - TextMeshPro "FuturasActualizacionesTEXT" del Canvas
    public float displayTime = 3f;  //Duración del mensaje en pantalla
    private float timer;            //Temporizador
    
    
    private void OnTriggerEnter (Collider other){
        if(other.GetComponent<Player>() != null){
            messageText.SetActive(true);            //Activa el mensaje
            timer = displayTime;                    //Resetea el temporizador
        }

         Debug.Log("Trigger activado por: " + other.name);
    if (other.GetComponent<Player>() != null)
    {
        Debug.Log("Es el jugador");
        messageText.SetActive(true);
        timer = displayTime;
    }
    }

    void Update()
    {
        if (messageText.activeSelf){
            timer -= Time.deltaTime;                //Reduce el tiempo restante
            if (timer <= 0){
                messageText.SetActive(false);       //Desactiva el mensaje cuando se termine el tiempo (3f)
            }
        }        
    }
}
