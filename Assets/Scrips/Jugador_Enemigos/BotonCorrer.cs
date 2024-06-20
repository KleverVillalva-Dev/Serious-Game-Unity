using UnityEngine;
using UnityEngine.EventSystems;

public class BotonCorrer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Jugador_Comportamiento jugador;
    private float velocidadOriginal;
    private bool botonAPretado = false;
    private bool teclaCorrerPresionada = false;

    private void Start()
    {
        jugador = GameObject.FindWithTag("Player").GetComponent<Jugador_Comportamiento>();
        velocidadOriginal = jugador.velocidad;
    }

    void Update()
    {
        // Detectar si la tecla 'C' está siendo presionada
        if (Input.GetKeyDown(KeyCode.C))
        {
            teclaCorrerPresionada = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            teclaCorrerPresionada = false;
        }

        // Combinar el estado del botón y la tecla
        if (botonAPretado || teclaCorrerPresionada)
        {
            jugador.velocidad = velocidadOriginal * 1.45f;
            jugador.animator.SetBool("Correr", true);
        }
        else
        {
            jugador.velocidad = velocidadOriginal;
            jugador.animator.SetBool("Correr", false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        botonAPretado = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        botonAPretado = false;
    }
}

