using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonDispararFillAmout : MonoBehaviour
{
    public Image imagen;
    public float cd;
    private float cdTotal;

    Jugador_Comportamiento jugadorScript;

    private void Start()
    {
        jugadorScript = FindObjectOfType<Jugador_Comportamiento>();
        cdTotal = jugadorScript.cd;
    }

    private void Update()
    {
        cd = jugadorScript.cDLanzamiento;

        imagen.fillAmount = cd / cdTotal;
    }
}
