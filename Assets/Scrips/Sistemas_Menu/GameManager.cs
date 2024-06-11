using UnityEngine;
using static GetDatos;

public class GameManager : MonoBehaviour
{
    #region Singletone
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    public bool JuegoEnPausa = false;

    // 0 para femenino, 1 para masculino
    public int personajeSeleccionado;

    public Conceptos[] diezConceptosNivel1                     = new Conceptos[10]; //Estos se deben mantener para la evaluacion.
    public Ejercicio[] diezEjerciciosNivel2                    = new Ejercicio[10];
    public PreguntasEvaluacion[] diezPreguntasEvaluacionNivel3 = new PreguntasEvaluacion[10];

    //Texto para escena transicion
    public string[] textosAntagonista = new string[2]
    {
        //Primera frase
        "\"�Oh, has demostrado que puedes recitar las teor�as! Pero, " +
        "�puedes sobrevivir a la vor�gine de la aplicaci�n pr�ctica? " +
        "Prep�rate para naufragar en un mar de complejidades gen�ticas," +
        " donde cada error ser� como un golpe de marea en tu camino hacia la victoria.\" ", 
        //Segunda frase
        "\"�Has logrado superar los desaf�os hasta ahora,\r\npero subestimas la complejidad que aguarda en la siguiente etapa! En la evaluaci�n final," +
        "\r\ntus habilidades ser�n puestas a prueba contra la verdadera esencia de la gen�tica. Prep�rate \r\npara enfrentarte a desaf�os que desafiar�n " +
        "incluso tus conocimientos m�s profundos.\r\n�Crees que eres lo suficientemente astuto como para prevalecer contra los secretos m�s oscuros\r\nde" +
        " la herencia?\""
    };
    public int indexTextoAntagonista = 0; //Cuando termine el juego recordar reiniciar index.
}
