using UnityEngine;
using UnityEngine.UI;

public class BarraDevida : MonoBehaviour
{
    public Image barraDeVida;
    private float vidaMaxima;

    Jugador_Comportamiento jugadorScript;
    private void Start()
    {
        jugadorScript = FindObjectOfType<Jugador_Comportamiento>();
        vidaMaxima = jugadorScript.vidaMaxima;
    }
    private void Update()
    {
        barraDeVida.fillAmount = jugadorScript.vidaActual / vidaMaxima;
    }
}
