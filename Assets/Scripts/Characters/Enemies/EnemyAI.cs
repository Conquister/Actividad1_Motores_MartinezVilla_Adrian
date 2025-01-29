using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private SphereCollectorManager sphereColletorManagerGM;
    private GameObject player;
    private NavMeshAgent agent;
    private bool isChasing = false;
    [SerializeField] private float delayBeforeChasingPlayer = 5f; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerDynamic>().gameObject;
    }

    private void OnEnable()
    {
        sphereColletorManagerGM.OnAllSpheresPlaced += DelayBeforeChasing;
    }

    private void OnDisable()
    {
        sphereColletorManagerGM.OnAllSpheresPlaced -= DelayBeforeChasing;
    }

    private void DelayBeforeChasing()
    {
        StartCoroutine(StartChasingPlayer());
    }

    private IEnumerator StartChasingPlayer()
    {
        yield return new WaitForSeconds (delayBeforeChasingPlayer);
        isChasing = true;
    }

    void Update()
    {
       if (isChasing)
       {
        ChasePlayer();
       }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }    
}
