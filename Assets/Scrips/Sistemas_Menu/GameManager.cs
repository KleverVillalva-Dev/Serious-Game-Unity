using UnityEngine;
using UnityEngine.SceneManagement;
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
        "\"¡Oh, has demostrado que puedes recitar las teorías! Pero, " +
        "¿puedes sobrevivir a la vorágine de la aplicación práctica? " +
        "Prepárate para naufragar en un mar de complejidades genéticas," +
        " donde cada error será como un golpe de marea en tu camino hacia la victoria.\" ", 
        //Segunda frase
        "\"¡Has logrado superar los desafíos hasta ahora,\r\npero subestimas la complejidad que aguarda en la siguiente etapa! En la evaluación final," +
        "\r\ntus habilidades serán puestas a prueba contra la verdadera esencia de la genética. Prepárate \r\npara enfrentarte a desafíos que desafiarán " +
        "incluso tus conocimientos más profundos.\r\n¿Crees que eres lo suficientemente astuto como para prevalecer contra los secretos más oscuros\r\nde" +
        " la herencia?\""
    };
    public int indexTextoAntagonista = 0; //Cuando termine el juego recordar reiniciar index.

    //Datos para mostrar en Resultados

    //Nivel conceptos
    public int conceptos_Intentos; //Veces que se reinicia el nivel
    public int concepos_VirusEliminados;
    public float conceptos_tiempo; //Pensarlo

    //Nivel Ejercicios
    public int ejercicios_Intentos; //Veces que se reinicia el nivel
    public int ejercicios_RespuestasIncorrectas;
    public int ejercicios_VirusEliminados;
    public float ejercicios_tiempo;

    //Nivel Evaluacion
    public int evaluacion_Intentos;
    public int evaluacion_RespuestasIncorrectas;
    public float evaluacion_Tiempo;

    ///MUSICA
    ///
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        SetMusicaFondo(SceneManager.GetActiveScene().name);
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        SetMusicaFondo(newScene.name);
    }

    private void SetMusicaFondo(string sceneName)
    {
        if (AudioManager.instance == null) return;

        switch (sceneName)
        {
            case "MenúPrincipal":
                AudioManager.instance.MusicaFondo(AudioManager.instance.entorno_MenuPrincipal);
                break;
            case "Nivel_1_Conceptos":
                AudioManager.instance.MusicaFondo(AudioManager.instance.entorno_Conceptos);
                break;
            case "Nivel_2_Ejercicios":
                AudioManager.instance.MusicaFondo(AudioManager.instance.entorno_Ejercicios);
                break;
            case "Nivel_3_Evaluacion":
                AudioManager.instance.MusicaFondo(AudioManager.instance.entorno_Evaluacion);
                break;
            case "SC_Antagonista":
                AudioManager.instance.MusicaFondo(AudioManager.instance.entorno_MenuPrincipal);
                break;
            case "Resultados":
                AudioManager.instance.MusicaFondo(AudioManager.instance.entorno_MenuPrincipal);
                break;
        }
    }


    //Selector de niveles
    // Crear un boleano para cada nivel superado.
    public bool nivel_Conceptos_Superado = false;
    public bool nivel_Ejercicios_Superado = false;
    // Al tener boleano de nivel 1 y 2 true, se desbloquea el nivel 3
    // Cada vez que pasamos de nivel darle a true en el boleano. Ir a selector de niveles cada vez que pasemos un nivel.
}
