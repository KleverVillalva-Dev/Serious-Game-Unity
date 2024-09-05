using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GetDatos;

public class Nivel_Ejercicio_Manager : MonoBehaviour
{
    #region Singleton
    public static Nivel_Ejercicio_Manager Instance;
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

    // Respetar pos 0 mujer, pos 1 hombre
    [SerializeField] GameObject[] personajePrefab; // Instanciado desde awake
    [SerializeField] Transform posInstanciarJugador;

    [Header("Referencias para anims de personajes UI")]
    [SerializeField] GameObject personajeFemenino;
    [SerializeField] GameObject personajeMasculino;

    [Header("Seccion para el UI en Seleccion Multiple")]
    [SerializeField] public GameObject preguntaUi_SeleccionMultiple;
    [SerializeField] public GameObject preguntaUi_Punnett;
    [SerializeField] public TextMeshProUGUI tmpPregunta_SeleccionMultiple;
    [SerializeField] public TextMeshProUGUI tmpDetalles_SeleccionMultiple;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_opciones;

    [SerializeField] public GameObject[] botonesRespuestas_OpcionesMultiples;
    public string opcionCorrectaEnBoton;

    public string opcionCorrectaEnBoton_punnet;

    [SerializeField] public GameObject boton5050;
    [SerializeField] public GameObject boton5050_punnet;

    [Header("Seleccion para el Ui en Punnet")]
    [SerializeField] public TextMeshProUGUI tmpPregunta_Punnett;
    [SerializeField] public TextMeshProUGUI tmpDetalles_Punnettt;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_Punnett;

    [SerializeField] public GameObject[] botonesRespuestas_OpcionesPunnet;
    [SerializeField] public TextMeshProUGUI[] posMatriz;

    [Header("UI pergaminos y virus")]
    public int objetoRecolectableAgarrado;
    public int virusMatados;

    [SerializeField] TextMeshProUGUI tmpPergaminos;
    [SerializeField] TextMeshProUGUI tmpVirus;

    public Ejercicio[] diezEjercicios = new Ejercicio[10];

    [SerializeField] private GameObject objetoInstanciadoPrefab;
    [SerializeField] private Transform[] posObjetos;

    private int[] tipoTxt = new int[10];
    private string[] preguntaTxt = new string[10];
    private string[] detallesTxt = new string[10];
    private string[] informacion = new string[10];

    string[] cuatroOpciones = new string[4];

    bool botonPrecionado = false;
    int respuestasIncorrectas;
    int respuestasIncorrectas3OpcionesYSeReinicia;

    GameObject curva;

    [SerializeField] GameObject panelInfo;
    [SerializeField] public TextMeshProUGUI tmpInformacion;

    private void Start()
    {
        //Activar personaje para UI dependiendo de con cual se este jugando
        curva = GameObject.Find("Curva");

        bool esFemenino = (GameManager.instance.personajeSeleccionado == 0);
        personajeFemenino.SetActive(esFemenino);
        personajeMasculino.SetActive(!esFemenino);

        StartCoroutine(SetearInformacionObjetoLVL2());
        ResetearTextoMatriz();
    }

    private void Update()
    {
        tmpPergaminos.text = objetoRecolectableAgarrado.ToString() + " /10";
        tmpVirus.text = virusMatados.ToString();
        AyudaALApuntar();
    }

    public void AyudaALApuntar()
    {
        if (PanelOpciones.instance.ayudaAlApuntar && curva != null)
        {
            curva.SetActive(true);
        }
        else
        {
            curva.SetActive(false);
        }
    }

    IEnumerator SetearInformacionObjetoLVL2()
    {
        // Asegurarme que estén cargados los datos.
        while (!GetDatos.instance.ejerciciosCargados)
        {
            yield return null;
        }

        // Tomar los 10 ejercicios desde GameManager
        for (int i = 0; i < diezEjercicios.Length; i++)
        {
            diezEjercicios[i] = GameManager.instance.diezEjerciciosNivel2[i];

            tipoTxt[i] = diezEjercicios[i].tipo;
            preguntaTxt[i] = diezEjercicios[i].pregunta;
            detallesTxt[i] = diezEjercicios[i].detalles;
            informacion[i] = diezEjercicios[i].explicacion_solucion;

            string opcionesDebug = $"Opciones para diezEjercicios[{i}]: ";

            for (int j = 0; j < cuatroOpciones.Length; j++)
            {
                if (j < diezEjercicios[i].opcionesMultiples.Length)
                {
                    cuatroOpciones[j] = diezEjercicios[i].opcionesMultiples[j].texto_opcion;
                }
                else
                {
                    Debug.LogWarning($"diezEjercicios[{i}].opcionesMultiples tiene menos de 4 opciones.");
                    cuatroOpciones[j] = "Opción no disponible";
                }

                opcionesDebug += $"[{j}] {cuatroOpciones[j]} ";
            }

            Debug.Log(opcionesDebug);
        }

        InstanciarObjeto();
    }

    private void InstanciarObjeto()
    {
        for (int i = 0; i < posObjetos.Length; i++)
        {
            GameObject obj = Instantiate(objetoInstanciadoPrefab, posObjetos[i].position, Quaternion.identity);

            ObjetoNivel2 objetoNivel2 = obj.GetComponent<ObjetoNivel2>();

            objetoNivel2.tipo = tipoTxt[i];
            objetoNivel2.pregunta = preguntaTxt[i];
            objetoNivel2.detalles = detallesTxt[i];
            objetoNivel2.informacion = informacion[i];

            if (diezEjercicios[i].opcionesMultiples.Length < 4)
            {
                Debug.LogWarning($"diezEjercicios[{i}].opcionesMultiples tiene menos de 4 opciones.");
            }

            for (int j = 0; j < cuatroOpciones.Length; j++)
            {
                if (j < diezEjercicios[i].opcionesMultiples.Length)
                {
                    objetoNivel2.opciones[j] = diezEjercicios[i].opcionesMultiples[j].texto_opcion;

                    if (diezEjercicios[i].opcionesMultiples[j].es_correcta)
                    {
                        objetoNivel2.opcionCorrecta = j;
                    }
                }
                else
                {
                    objetoNivel2.opciones[j] = "Opción no disponible";
                }
            }

            string opcionesDebug = $"Opciones para objetoNivel2 en posObjetos[{i}]: ";
            for (int j = 0; j < objetoNivel2.opciones.Length; j++)
            {
                opcionesDebug += $"[{j}] {objetoNivel2.opciones[j]} ";
            }
        }
    }

    public void RespuestaCorrecta(Button boton)
    {
        if (!botonPrecionado)
        {
            botonPrecionado = true;

            Debug.Log("RespuestaCorrecta");

            boton.GetComponent<Image>().color = Color.green;

            AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_RespuestaCorrecta);

            StartCoroutine(RCorrecta_Corrutina());
            RevisarSiAgarre10();
        }
    }

    public void RespuestaIncorrecta(Button boton)
    {
        if (!botonPrecionado)
        {
            botonPrecionado = true;
            Debug.Log("Respuesta Incorrecta");
            AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_RespuestaIncorrecta);

            string hexColor = "#C33C3C";
            Color color;
            if (ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                boton.GetComponent<Image>().color = color;
            }
            else
            {
                Debug.LogError("Error al convertir el color hexadecimal.");
            }

            StartCoroutine(Respuesta_Incorrecta());
            RevisarSiAgarre10();
        }
    }

    IEnumerator Respuesta_Incorrecta()
    {
        if (personajeFemenino.activeSelf)
        {
            Animator animator = personajeFemenino.GetComponent<Animator>();
            animator.SetTrigger("Error");
        }
        else if (personajeMasculino.activeSelf)
        {
            Animator animator = personajeMasculino.GetComponent<Animator>();
            animator.SetTrigger("Error");
        }

        respuestasIncorrectas++;
        respuestasIncorrectas3OpcionesYSeReinicia++;

        if (respuestasIncorrectas3OpcionesYSeReinicia == 3)
        {
            respuestasIncorrectas3OpcionesYSeReinicia = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        yield return new WaitForSeconds(4);
        preguntaUi_Punnett.SetActive(false);
        preguntaUi_SeleccionMultiple.SetActive(false);
        AudioManager.instance.DetenerMusicaEspecial();
        ResetearTextoMatriz();
        GameManager.instance.JuegoEnPausa = false;
        botonPrecionado = false;
    }

    IEnumerator RCorrecta_Corrutina()
    {
        if (personajeFemenino.activeSelf)
        {
            Animator animator = personajeFemenino.GetComponent<Animator>();
            animator.SetTrigger("Bailar");
        }
        else if (personajeMasculino.activeSelf)
        {
            Animator animator = personajeMasculino.GetComponent<Animator>();
            animator.SetTrigger("Bailar");
        }

        yield return new WaitForSeconds(4);
        preguntaUi_Punnett.SetActive(false);
        preguntaUi_SeleccionMultiple.SetActive(false);
        AudioManager.instance.DetenerMusicaEspecial();
        ResetearTextoMatriz();
        GameManager.instance.JuegoEnPausa = false;
        botonPrecionado = false;
    }

    public void RevisarSiAgarre10()
    {
        if (objetoRecolectableAgarrado == 10)
        {
            StartCoroutine(NivelEjerciciosSuperado());
        }
    }

    IEnumerator NivelEjerciciosSuperado()
    {
        GameManager.instance.nivel_Ejercicios_Superado = true;
        Debug.Log("10 objetos recolectados pasando de nivel");
        GameProgress.SetLevelCompleted("Ejercicios");

        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_NivelSuperado);
        GameManager.instance.JuegoEnPausa = true;
        yield return new WaitForSeconds(5);

        GameManager.instance.ejercicios_VirusEliminados += virusMatados;
        GameManager.instance.ejercicios_RespuestasIncorrectas += respuestasIncorrectas;
        GameManager.instance.ejercicios_Intentos++;

        GameManager.instance.JuegoEnPausa = false;
        Carga_Nivel.Nivel_A_Cargar("SC_Antagonista");
        GameManager.instance.indexTextoAntagonista = 1;
    }

    public void ReiniciarEscena()
    {
        StartCoroutine(ReiniciarEscenaCorrutina());
    }

    public IEnumerator ReiniciarEscenaCorrutina()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetearTextoMatriz()
    {
        for (int i = 0; i < posMatriz.Length; i++)
        {
            posMatriz[i].text = "";
        }
    }

    public void PanelInformacion()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        if (panelInfo.activeSelf)
        {
            panelInfo.SetActive(false);
        }
        else
        {
            panelInfo.SetActive(true);
        }
    }

    public List<GameObject> botonesDesactivados = new List<GameObject>();
    public List<GameObject> botonesDesactivados_punnet = new List<GameObject>();

    private GameObject EncontrarBotonCorrecto()
    {
        foreach (GameObject boton in botonesRespuestas_OpcionesMultiples)
        {
            TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
            if (textoBoton != null && textoBoton.text == opcionCorrectaEnBoton)
            {
                return boton;
            }
        }
        return null;
    }

    private GameObject EncontrarBotonCorrecto_Punnet()
    {
        foreach (GameObject boton in botonesRespuestas_OpcionesPunnet)
        {
            TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
            if (textoBoton != null && textoBoton.text == opcionCorrectaEnBoton)
            {
                return boton;
            }
        }
        return null;
    }

    public void DesactivarDosBotonesIncorrectos()
    {
        GameObject botonCorrecto = EncontrarBotonCorrecto();
        if (botonCorrecto == null)
        {
            Debug.LogError("No se encontró el botón con la opción correcta.");
            return;
        }

        List<GameObject> botonesIncorrectos = new List<GameObject>();
        foreach (GameObject boton in botonesRespuestas_OpcionesMultiples)
        {
            if (boton != botonCorrecto)
            {
                botonesIncorrectos.Add(boton);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (botonesIncorrectos.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, botonesIncorrectos.Count);
                GameObject botonIncorrecto = botonesIncorrectos[index];
                botonIncorrecto.SetActive(false);
                botonesDesactivados.Add(botonIncorrecto);
                botonesIncorrectos.RemoveAt(index);
            }
        }

        boton5050.SetActive(false);
    }

    public void DesactivarDosBotonesIncorrectos_Punnet()
    {
        GameObject botonCorrecto = EncontrarBotonCorrecto_Punnet();
        if (botonCorrecto == null)
        {
            Debug.LogError("No se encontró el botón con la opción correcta.");
            return;
        }

        List<GameObject> botonesIncorrectos = new List<GameObject>();
        foreach (GameObject boton in botonesRespuestas_OpcionesPunnet)
        {
            if (boton != botonCorrecto)
            {
                botonesIncorrectos.Add(boton);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (botonesIncorrectos.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, botonesIncorrectos.Count);
                GameObject botonIncorrecto = botonesIncorrectos[index];
                botonIncorrecto.SetActive(false);
                botonesDesactivados_punnet.Add(botonIncorrecto);
                botonesIncorrectos.RemoveAt(index);
            }
        }

        boton5050_punnet.SetActive(false);
    }

    public void ReactivarBotonesDesactivados()
    {
        foreach (GameObject boton in botonesDesactivados)
        {
            boton.SetActive(true);
        }
        boton5050.SetActive(true);
        botonesDesactivados.Clear();
    }

    public void ReactivarBotonesDesactivados_Punnet()
    {
        foreach (GameObject boton in botonesDesactivados_punnet)
        {
            boton.SetActive(true);
        }
        boton5050_punnet.SetActive(true);
        botonesDesactivados_punnet.Clear();
    }
}
