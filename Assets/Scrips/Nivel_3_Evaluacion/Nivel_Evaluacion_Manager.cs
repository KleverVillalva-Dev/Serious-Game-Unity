using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Nivel_Evaluacion_Manager : MonoBehaviour
{
    #region Singletone
    public static Nivel_Evaluacion_Manager Instance;
    private void Awake()
    {
        //Instanciar jugador dependiendo de seleccion
        personajePrefab[GameManager.instance.personajeSeleccionado].SetActive(true);

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    [SerializeField] GameObject[] personajePrefab;
    [SerializeField] Transform posInstanciarJugador;

    //Panel de preguntas y respuestas
    [SerializeField] TextMeshProUGUI textoPregunta;
    [SerializeField] TextMeshProUGUI textoDetalles;
    [SerializeField] GameObject[] botonesRespuestas;

    public PreguntasEvaluacion[] diezPreguntasEvaluacion = new PreguntasEvaluacion[10];

    private string[] preguntaTxt = new string[10];
    private string[] detallesTxt = new string[10];
    private string[] explicacionTxt = new string[10];

    // Almacena las opciones de cada pregunta
    private string[,] todasOpciones = new string[10, 4];
    private bool[,] esOpcionCorrecta = new bool[10, 4];

    private int indexPregunta;

    //Para barras de vida
    private Animator animJugador;

    public Image barraDeVidaJugador;
    private float vidaMaximaJugador = 3;
    private float viadaActualJugador;

    //Enemigo
    private Animator animEnemigo;

    public Image barraDeVidaEnemigo;
    private float vidaMaximaEnemigo = 7;
    private float vidaActualEnemigo;

    private void Start()
    {
        viadaActualJugador = vidaMaximaJugador;
        vidaActualEnemigo = vidaMaximaEnemigo;
        animJugador = GameObject.FindWithTag("Player").GetComponent<Animator>();
        animEnemigo = GameObject.FindWithTag("Enemigo").GetComponent<Animator>();
        StartCoroutine(ArrayEvaluacionesDatos());
    }

    private void Update()
    {
        barraDeVidaJugador.fillAmount = viadaActualJugador / vidaMaximaJugador;
        barraDeVidaEnemigo.fillAmount = vidaActualEnemigo / vidaMaximaEnemigo;
    }
    IEnumerator ArrayEvaluacionesDatos()
    {
        while (!GetDatos.instance.evaluacionCargado)
        {
            yield return null;
        }

        for (int i = 0; i < diezPreguntasEvaluacion.Length; i++)
        {
            diezPreguntasEvaluacion[i] = GameManager.instance.diezPreguntasEvaluacionNivel3[i];

            preguntaTxt[i] = diezPreguntasEvaluacion[i].texto_pregunta;
            detallesTxt[i] = diezPreguntasEvaluacion[i].detalles;
            explicacionTxt[i] = diezPreguntasEvaluacion[i].explicacion_solucion;

            for (int j = 0; j < 4; j++)
            {
                todasOpciones[i, j] = diezPreguntasEvaluacion[i].opcionesEvaluacion[j].texto_opcion;
                esOpcionCorrecta[i, j] = diezPreguntasEvaluacion[i].opcionesEvaluacion[j].es_correcta;
            }
        }
        SetearPregunta();
        yield return null;
    }

    private void SetearPregunta()
    {
        textoPregunta.text = preguntaTxt[indexPregunta];
        textoDetalles.text = detallesTxt[indexPregunta];

        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI textoBoton = botonesRespuestas[i].GetComponentInChildren<TextMeshProUGUI>();
            textoBoton.text = todasOpciones[indexPregunta, i];

            int opcionIndex = i; // Necesario para la closure en el lambda
            botonesRespuestas[i].GetComponent<Button>().onClick.RemoveAllListeners();
            botonesRespuestas[i].GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(opcionIndex));
        }
    }

    bool responde = false;
    private void VerificarRespuesta(int opcionIndex)
    {
        if (!responde)
        {
            responde = true;
            if (esOpcionCorrecta[indexPregunta, opcionIndex])
            {
                Debug.Log("¡Respuesta correcta!");
                StartCoroutine(RespuestaCorrecta());             
            }
            else
            {
                Debug.Log("Respuesta incorrecta.");
                StartCoroutine(RespuestaIncorrecta());
            }
        }     
    }

    public void SiguientePregunta()
    {
        indexPregunta++;
        if (indexPregunta >= diezPreguntasEvaluacion.Length)
        {
            Debug.Log("PasarAlSiguienteNivel");
        }
        SetearPregunta();
    }

    [SerializeField] TextMeshProUGUI textoSolucion;
    [SerializeField] GameObject panelSolucion;
 
    public void ActivarPanelSolucion()
    {
        textoSolucion.text = explicacionTxt[indexPregunta];
        panelSolucion.SetActive(true);
    }
    public void BotonEntiendo()
    {
        panelSolucion.SetActive(false);
        if (viadaActualJugador <= 0f)
        {
            //Reiniciar o hacer algo si muere
            indexPregunta = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else if (vidaActualEnemigo <= 0 || indexPregunta > 10)
        {
            indexPregunta = 0;
            SceneManager.LoadScene("Resultados");
        }
        else
        {
            SiguientePregunta();
        }
    }
    private IEnumerator RespuestaCorrecta()
    {
        animJugador.SetTrigger("Disparar");
        //Lanzar el matraz si queremos
        yield return new WaitForSeconds(1.5f); //Tiempo mientras se lanza
        //Anim del enemigo reaccionando al lanzamiento
        animEnemigo.SetTrigger("Danio");
        //Bajar vida barra de vida enemigo.
        vidaActualEnemigo--;
        if (vidaActualEnemigo <= 0)
        {
            animEnemigo.SetTrigger("Muerte");
            yield return new WaitForSeconds(3);
            ActivarPanelSolucion();

            responde = false;
        }
        else
        {
            yield return new WaitForSeconds(3);
            ActivarPanelSolucion();

            responde = false;
        }
    }

    private IEnumerator RespuestaIncorrecta()
    {
        //Enemigo disparar
        animEnemigo.SetTrigger("Disparar");
        //Lanzar el matraz si queremos
        yield return new WaitForSeconds(1.5f); //Tiempo mientras se lanza
        animJugador.SetTrigger("Danio");
        //Anim del jugador reaccionando al lanzamiento
        animEnemigo.SetTrigger("Burla");
        //Bajar vida barra de vida del jugador

        viadaActualJugador--;
        if (viadaActualJugador <= 0f)
        {
            animJugador.SetTrigger("Muerte");

            yield return new WaitForSeconds(3);
            ActivarPanelSolucion();

            responde = false;
        }
        else
        {
            yield return new WaitForSeconds(3);
            ActivarPanelSolucion();
            responde = false;
        }   
    }
}
