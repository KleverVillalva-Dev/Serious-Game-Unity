using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoNivel2 : MonoBehaviour
{
    // MODIF
    public string tipo;
    public string pregunta;
    public string detalles;
    public string[] opciones = new string[4];

    public int opcionCorrecta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // MODIF
            Nivel_Ejercicios_Manager.Instance.preguntaUi.SetActive(true);
            Nivel_Ejercicios_Manager.Instance.tmpPregunta.text = pregunta;
            Nivel_Ejercicios_Manager.Instance.tmpDetalles.text = detalles;

            //Setear botones
            for (int i = 0; i < opciones.Length; i++)
            {
                Nivel_Ejercicios_Manager.Instance.textoRespuestas_opciones[i].text = opciones[i];
            }
        }
    }
}
