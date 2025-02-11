using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoNivel2 : MonoBehaviour
{
    // MODIF
    public int tipo;
    public string pregunta;
    public string detalles;
    public string informacion;
    public string[] opciones = new string[4];
    public int opcionCorrecta;
    Color colorOriginalBoton;
    //Para Punnet

    // cambiamos string "tipo" por entero id_tipo, 1 para multiple 2 para punnet.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.JuegoEnPausa = true;
            AudioManager.instance.MusicaEspecial(AudioManager.instance.sfx_ejercicio);
            // MODIF
            if (tipo == 1)// 1 "Selecci�n M�ltiple"
            {
                Nivel_Ejercicio_Manager.Instance.preguntaUi_SeleccionMultiple.SetActive(true); // Si es de tipo Seleccion Multiple activar esta
                Nivel_Ejercicio_Manager.Instance.ReactivarBotonesDesactivados();                                 //Sino, activar una interfase.
                Nivel_Ejercicio_Manager.Instance.ReactivarBotonesDesactivados_Punnet();                                 //Sino, activar una interfase.
                Nivel_Ejercicio_Manager.Instance.tmpPregunta_SeleccionMultiple.text = pregunta;
                Nivel_Ejercicio_Manager.Instance.tmpDetalles_SeleccionMultiple.text = detalles;
                Nivel_Ejercicio_Manager.Instance.tmpInformacion.text = informacion;
                //XXXXXXXXX Agregar aqui la info que recibiremos de la appi con la informacion. Pasarla al ejerciciomanager.

                //Setear botones
                for (int i = 0; i < opciones.Length; i++)
                {
                    Nivel_Ejercicio_Manager.Instance.textoRespuestas_opciones[i].text = opciones[i];
                }
            }else if(tipo == 2) // "Punnett"
            {
                Nivel_Ejercicio_Manager.Instance.preguntaUi_Punnett.SetActive(true);

                Nivel_Ejercicio_Manager.Instance.tmpPregunta_Punnett.text = pregunta;
                Nivel_Ejercicio_Manager.Instance.tmpDetalles_Punnettt.text = detalles;
                Nivel_Ejercicio_Manager.Instance.tmpInformacion.text = informacion;

                //Setear botones
                for (int i = 0; i < opciones.Length; i++)
                {
                    Nivel_Ejercicio_Manager.Instance.textoRespuestas_Punnett[i].text = opciones[i];
                }
            }

            Debug.Log("la opcion correcta es "+ opcionCorrecta +" "+ opciones[opcionCorrecta]);
            Nivel_Ejercicio_Manager.Instance.opcionCorrectaEnBoton = opciones[opcionCorrecta].ToString();

            // Falta revisar si ya se recolectaron los 10 para pasar al siguiente nivel
            // Esto lo puedo hacer en el boton de cerrar

            ConfigurarBotones();
            Nivel_Ejercicio_Manager.Instance.objetoRecolectableAgarrado++;
            Destroy(this.gameObject);
        }
    }

    //Aqui configuramos la funcion correcta para cada uno de los botones
    #region TesteandoNuevaConfig
    //public void ConfigurarBotones()
    //{
    //    if (tipo == 1) //Multiples
    //    {
    //        for (int i = 0; i < Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesMultiples.Length; i++)
    //        {
    //            Button boton = Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesMultiples[i].GetComponent<Button>();
    //            boton.onClick.RemoveAllListeners();

    //            if (i == opcionCorrecta)
    //            {
    //                boton.onClick.AddListener(Nivel_Ejercicio_Manager.Instance.RespuestaCorrecta);
    //            }
    //            else
    //            {
    //                boton.onClick.AddListener(Nivel_Ejercicio_Manager.Instance.RespuestaIncorrecta);
    //            }
    //        }
    //    }
    //    if (tipo == 2) //Punet
    //    {
    //        for (int i = 0; i < Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesPunnet.Length; i++)
    //        {
    //            Button boton = Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesPunnet[i].GetComponent<Button>();
    //            boton.onClick.RemoveAllListeners();

    //            if (i == opcionCorrecta)
    //            {
    //                boton.onClick.AddListener(Nivel_Ejercicio_Manager.Instance.RespuestaCorrecta);
    //            }
    //            else
    //            {
    //                boton.onClick.AddListener(Nivel_Ejercicio_Manager.Instance.RespuestaIncorrecta);
    //            }
    //        }
    //    }
    //} 
    #endregion
    public void ConfigurarBotones()
    {
        if (tipo == 1) // Multiples
        {
            for (int i = 0; i < Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesMultiples.Length; i++)
            {
                Button boton = Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesMultiples[i].GetComponent<Button>();
                boton.onClick.RemoveAllListeners();

                // Reset button color

                //1D467E
                string hexColor = "#1D467E";
                Color color;
                if (ColorUtility.TryParseHtmlString(hexColor, out color))
                {
                    boton.GetComponent<Image>().color = color;
                }
                else
                {
                    Debug.LogError("Error al convertir el color hexadecimal.");
                }

                if (i == opcionCorrecta)
                {
                    boton.onClick.AddListener(() => Nivel_Ejercicio_Manager.Instance.RespuestaCorrecta(boton));
                }
                else
                {
                    boton.onClick.AddListener(() => Nivel_Ejercicio_Manager.Instance.RespuestaIncorrecta(boton));
                }
            }
        }
        else if (tipo == 2) // Punnet
        {
            for (int i = 0; i < Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesPunnet.Length; i++)
            {
                Button boton = Nivel_Ejercicio_Manager.Instance.botonesRespuestas_OpcionesPunnet[i].GetComponent<Button>();
                boton.onClick.RemoveAllListeners();

                // Reset button color

                string hexColor = "#1D467E";
                Color color;
                if (ColorUtility.TryParseHtmlString(hexColor, out color))
                {
                    boton.GetComponent<Image>().color = color;
                }
                else
                {
                    Debug.LogError("Error al convertir el color hexadecimal.");
                }

                if (i == opcionCorrecta)
                {
                    boton.onClick.AddListener(() => Nivel_Ejercicio_Manager.Instance.RespuestaCorrecta(boton));
                }
                else
                {
                    boton.onClick.AddListener(() => Nivel_Ejercicio_Manager.Instance.RespuestaIncorrecta(boton));
                }
            }
        }
    }

}
