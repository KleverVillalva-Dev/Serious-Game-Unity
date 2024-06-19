using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resultados : MonoBehaviour
{
    public string textoAMostrar;
    [SerializeField] TextMeshProUGUI tmp_TextoMostrar;
    [SerializeField] GameObject boton_ReiniciarJuego;

    private void Start()
    {
        textoAMostrar = "NIVEL CONCEPTOS\r\n\r\nNumero de intentos: " + GameManager.instance.conceptos_Intentos +
            "\r\nConceptos encontrados: 10" +
            "\r\nVirus eliminados: " + GameManager.instance.concepos_VirusEliminados +
            "\r" +
            "\n\r\nNIVEL EJERCICIOS\r" +
            "\n" +
            "\r\nNúmero de intentos: " + GameManager.instance.ejercicios_Intentos +
            "\r\nRespuestas incorrectas: " + GameManager.instance.ejercicios_RespuestasIncorrectas +
            "\r\nVirus eliminados: " + GameManager.instance.ejercicios_VirusEliminados +

            "\r\n\r\nNIVEL EVALUACION" +
            "\r" +
            "\n\r\nNúmero de intentos " + GameManager.instance.evaluacion_Intentos +
            "\r\nRespuestas incorrectas: " + GameManager.instance.evaluacion_RespuestasIncorrectas;

          StartCoroutine(TiperCaracteres(tmp_TextoMostrar, textoAMostrar, 0, 0.07f));
    }

    public IEnumerator TiperCaracteres(TextMeshProUGUI textoARemplazar, string textoAMostrar, float delayPausas, float delayEntreTipeo)
    {
        yield return new WaitForSeconds(1);
        textoARemplazar.text = "";
        char delay = ';';

        foreach (char caracter in textoAMostrar.ToCharArray())
        {

            if (caracter == delay)
            {
                yield return new WaitForSeconds(delayPausas); // int delayPausas
            }

            textoARemplazar.text = textoARemplazar.text.Replace(";", "");

            yield return new WaitForSeconds(delayEntreTipeo); //delayEntreTipeo
            textoARemplazar.text += caracter;
        }
        boton_ReiniciarJuego.SetActive(true);
    }

    public void BotonReiniciarJuego()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);

        //Reiniciar varialbes
        GameManager.instance.concepos_VirusEliminados = 0;
        GameManager.instance.conceptos_Intentos = 0;
        GameManager.instance.conceptos_tiempo = 0;

        GameManager.instance.ejercicios_Intentos = 0;
        GameManager.instance.ejercicios_RespuestasIncorrectas = 0;
        GameManager.instance.ejercicios_VirusEliminados = 0;
        GameManager.instance.ejercicios_tiempo = 0;

        GameManager.instance.evaluacion_Intentos = 0;
        GameManager.instance.evaluacion_RespuestasIncorrectas = 0;
        GameManager.instance.evaluacion_Tiempo = 0;

        GameManager.instance.indexTextoAntagonista = 0;

        //Volver al menú

        SceneManager.LoadScene("MenúPrincipal");
    }
}
