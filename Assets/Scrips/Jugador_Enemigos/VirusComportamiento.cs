using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusComportamiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private string tagJugador = "Player";
    private Transform jugador;
    [SerializeField] private float distanciaJugador; //Quitar serializado
    [SerializeField] private float distanciaMinimaDanio;
    [SerializeField] private float distanciaMinimaSeguimiento;

    private bool seguirJugador = false;

    Jugador_Comportamiento jugadorScript;

    private void Start()
    {
        jugador = GameObject.FindWithTag(tagJugador).transform;
        jugadorScript = FindObjectOfType<Jugador_Comportamiento>();
    }

    private void Update()
    {
        //Sigue al jugador mientras el juego no este en pausa. Por ahora se pausa unicamente  al abrir pergaminos.
        if (jugador != null && seguirJugador && !GameManager.instance.JuegoEnPausa)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x, jugador.position.y + 4, jugador.position.z), velocidad * Time.deltaTime);
        }
        distanciaJugador = Vector3.Distance(transform.position, jugador.position);
        DaniarSiLoToca(); //Cambiar nombre
        SeguirEnDistanciaMinima();
    }

    private void SeguirEnDistanciaMinima()
    {
        if (distanciaJugador <= distanciaMinimaSeguimiento)
        {
            seguirJugador = true;
        }
    }
    private void DaniarSiLoToca()
    {
        //Medir distancia con el jugador en todo momento.   
        if (distanciaJugador <= distanciaMinimaDanio)
        {
            jugadorScript.vidaActual--;
            Destroy(gameObject);
        }
    }

}
