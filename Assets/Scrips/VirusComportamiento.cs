using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusComportamiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private string tagJugador = "Player";
    private Transform jugador;
    [SerializeField] private float distanciaJugador;
    [SerializeField] private float distanciaMinima;

    private bool trigerActivado = false;

    Jugador_Comportamiento jugadorScript;

    private void Start()
    {
        jugador = GameObject.FindWithTag(tagJugador).transform;
        jugadorScript = FindObjectOfType<Jugador_Comportamiento>();
    }

    private void Update()
    {
        if (jugador != null && trigerActivado)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x, jugador.position.y + 4, jugador.position.z), velocidad * Time.deltaTime);
        }
        distanciaJugador = Vector3.Distance(transform.position, jugador.position);
        DistanciaConJugador(); //Cambiar nombre
    }

    private void DistanciaConJugador()
    {
        //Medir distancia con el jugador en todo momento.   
        if (distanciaJugador <= distanciaMinima)
        {
            jugadorScript.vidaActual--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            trigerActivado = true;
        }
    }
}
