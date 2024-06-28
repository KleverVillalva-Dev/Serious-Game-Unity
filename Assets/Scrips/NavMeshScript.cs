using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshScript : MonoBehaviour
{
    [SerializeField] Transform jugador;
    NavMeshAgent agent;
    string tagJugador = "Player";
    void Start()
    {
        //jugador = GameObject.FindWithTag(tagJugador).transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(jugador.transform.position);
    }

}
