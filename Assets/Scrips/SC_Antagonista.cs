using System.Collections;
using TMPro;
using UnityEngine;

public class SC_Antagonista : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpDialogo;
    [SerializeField] string textoAMostrar;
    [SerializeField] float espera_Antes_Iniciar;
    [SerializeField] float velocidadTipeo;
    [SerializeField] GameObject antagonista;
    [SerializeField] float tiempoParaLeer;
    Animator animatorAntagonista; //Setar anim de correr luego de leer el texto.

    private void Start()
    {
        textoAMostrar = GameManager.instance.textosAntagonista[GameManager.instance.indexTextoAntagonista];
        GameManager.instance.indexTextoAntagonista++;
        StartCoroutine(TiperCaracteres(tmpDialogo, textoAMostrar, 0, velocidadTipeo));
    }
    public IEnumerator TiperCaracteres(TextMeshProUGUI textoARemplazar, string textoAMostrar, float delayPausas, float delayEntreTipeo)
    {
        yield return new WaitForSeconds(espera_Antes_Iniciar);
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

        yield return new WaitForSeconds(tiempoParaLeer);
        //Setar anim de correr
        yield return new WaitForSeconds(0); // Dejar tiempo para anim de correr
   
        //Setear nueva escena.
        if (GameManager.instance.indexTextoAntagonista == 1)
        { 
            //Index es 1 porque lo aumente en este nivel.
             Carga_Nivel.Nivel_A_Cargar("Nivel_2_Ejercicios");
        }
        else
        {
            Carga_Nivel.Nivel_A_Cargar("NIVEL NUEVO 3 aun inexistente");
        }
    }
}
