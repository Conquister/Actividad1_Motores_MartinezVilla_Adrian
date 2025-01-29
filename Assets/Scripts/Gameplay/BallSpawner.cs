using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 impulseDirection = new Vector3(0, 10, 2);
    
    void Start ()
    {
        StartCoroutine(Spawnear());
    }

    private IEnumerator Spawnear()
    {
        if (ballPrefab == null)
        {
            Debug.LogError ($"El SpawnPoint {gameObject.name} tiene un prefab no asignado");
            yield break;
        }

        Debug.Log ($"El SpawnPoint {gameObject.name} está instanciando {ballPrefab.name}");
        while (true)
        {
            GameObject ballInstance = Instantiate (ballPrefab, transform.position, Quaternion.identity);
            BallBehaviour ballBehaviour = ballInstance.GetComponent<BallBehaviour>();

            ballBehaviour.SetImpulseDirection(impulseDirection);
            yield return new WaitForSeconds(2f);
        }
    }
}
