using UnityEngine;

public class ObjetoNivel2 : MonoBehaviour
{
    // MODIF
    public string tipo;
    public string pregunta;
    public string detalles;
    public string[] opciones = new string[4];
    public int opcionCorrecta;

    //Para Punnet


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // MODIF
            if (tipo == "Selección Múltiple")
            {
                Nivel_Ejercicios_Manager.Instance.preguntaUi_SeleccionMultiple.SetActive(true); // Si es de tipo Seleccion Multiple activar esta
                                                                                                //Sino, activar una interfase.
                Nivel_Ejercicios_Manager.Instance.tmpPregunta_SeleccionMultiple.text = pregunta;
                Nivel_Ejercicios_Manager.Instance.tmpDetalles_SeleccionMultiple.text = detalles;

                //Setear botones
                for (int i = 0; i < opciones.Length; i++)
                {
                    Nivel_Ejercicios_Manager.Instance.textoRespuestas_opciones[i].text = opciones[i];
                }
            }else if(tipo == "Punnett")
            {
                Nivel_Ejercicios_Manager.Instance.preguntaUi_Punnett.SetActive(true);

                Nivel_Ejercicios_Manager.Instance.tmpPregunta_Punnett.text = pregunta;
                Nivel_Ejercicios_Manager.Instance.tmpDetalles_Punnettt.text = detalles;

                //Setear botones
                for (int i = 0; i < opciones.Length; i++)
                {
                    Nivel_Ejercicios_Manager.Instance.textoRespuestas_Punnett[i].text = opciones[i];
                }
            }

            Debug.Log("la opcion correcta es "+ opcionCorrecta +" "+ opciones[opcionCorrecta]);
            
        }
    }
}
