using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Nivel_Evaluacion_Manager : MonoBehaviour
{
    #region Singletone
    public static Nivel_Evaluacion_Manager Instance;
    private void Awake()
    {
        //Instanciar jugador dependiendo de seleccion
        //personajePrefab[GameManager.instance.personajeSeleccionado].SetActive(true);

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

    private void Start()
    {
        StartCoroutine(ArrayEvaluacionesDatos());
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

    private void VerificarRespuesta(int opcionIndex)
    {
        if (esOpcionCorrecta[indexPregunta, opcionIndex])
        {
            Debug.Log("¡Respuesta correcta!");
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");
        }
    }

    public void Botontest()
    {
        indexPregunta++;
        if (indexPregunta >= diezPreguntasEvaluacion.Length)
        {
            indexPregunta = 0; // Resetea al inicio si se pasa del límite
        }
        SetearPregunta();
    }
}
