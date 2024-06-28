using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusComportamiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private string tagJugador = "Player";
    private Transform jugador;
    [SerializeField] private float distanciaMinimaDanio;
    [SerializeField] private float distanciaMinimaSeguimiento;
    [SerializeField] GameObject mmp_Virus;
    private Rigidbody rb;

    private bool seguirJugador = false;

    Jugador_Comportamiento jugadorScript;

    NavMeshAgent agent;

    private void Start()
    {
        jugador = GameObject.FindWithTag(tagJugador).transform;
        jugadorScript = FindObjectOfType<Jugador_Comportamiento>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidad;
    }

    private void Update()
    {
        velocidad = PanelOpciones.instance.velocidadVirus;
        agent.speed = velocidad;

        // Sigue al jugador mientras el juego no esté en pausa. Por ahora se pausa únicamente al abrir pergaminos.
        if (jugador != null && seguirJugador && !GameManager.instance.JuegoEnPausa)
        {
            agent.destination = jugador.transform.position;
            mmp_Virus.SetActive(true);
        }
        else
        {
            agent.ResetPath(); //Detenerse
        }

        float distanciaJugador = Vector3.Distance(transform.position, jugador.position);
        DanarSiLoToca(distanciaJugador); // Cambiar nombre
        SeguirEnDistanciaMinima(distanciaJugador);
    }

    private void SeguirEnDistanciaMinima(float distanciaJugador)
    {
        if (distanciaJugador <= distanciaMinimaSeguimiento)
        {
            seguirJugador = true;
        }
    }

    private void DanarSiLoToca(float distanciaJugador)
    {
        // Medir distancia con el jugador en todo momento.
        if (distanciaJugador <= distanciaMinimaDanio)
        {
            jugadorScript.vidaActual--;
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaMinimaSeguimiento);
    }
}
