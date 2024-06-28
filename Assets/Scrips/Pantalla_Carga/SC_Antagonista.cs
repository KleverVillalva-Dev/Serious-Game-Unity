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

    [SerializeField] float speed = 3;

    private void Start()
    {
        antagonista = GameObject.FindWithTag("Enemigo");
        animatorAntagonista = GameObject.FindWithTag("Enemigo").GetComponent<Animator>();
        animatorAntagonista.SetTrigger("Hablando");

        textoAMostrar = GameManager.instance.textosAntagonista[GameManager.instance.indexTextoAntagonista];
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


        animatorAntagonista.SetTrigger("Correr");
        // Actualizamos la rotación del Cientifico (izquierda) a 0, -270, 0

        antagonista.transform.rotation = Quaternion.Euler(0, -270, 0);

        // Movimiento hacia adelante
        float moveDuration = 3f;
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            antagonista.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3); // Dejar tiempo para anim de correr
   
        //Setear nueva escena.
        if (GameManager.instance.indexTextoAntagonista == 1)
        { 
            //Index es 1 porque lo aumente en este nivel.
             Carga_Nivel.Nivel_A_Cargar("SelectorDeNivel");
        }
        else
        {
            Carga_Nivel.Nivel_A_Cargar("SelectorDeNivel");
        }
    }
}
