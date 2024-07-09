using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GetDatos;

public class Nivel_Ejercicio_Manager : MonoBehaviour
{
    #region Singletone
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
    //Respetar pos 0 mujer, pos 1 hombre
    [SerializeField] GameObject[] personajePrefab; // Instanciado desde awake
    [SerializeField] Transform posInstanciarJugador;

    [Header("Referencias para anims de personajes UI")]
    [SerializeField] GameObject personajeFemenino;
    [SerializeField] GameObject personajeMasculino;

    [Header("Seccion para el UI en Seleccion Multiple")]
    [SerializeField] public GameObject preguntaUi_SeleccionMultiple; // Se activa para mostrar info, desde el objeto al tocarlo
    [SerializeField] public GameObject preguntaUi_Punnett;
    [SerializeField] public TextMeshProUGUI tmpPregunta_SeleccionMultiple;
    [SerializeField] public TextMeshProUGUI tmpDetalles_SeleccionMultiple;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_opciones;

    [SerializeField] public GameObject[] botonesRespuestas_OpcionesMultiples; //Seteada la funcion correcta desde el objeto recolectable
    public string opcionCorrectaEnBoton; /// XXXXXXX texto para comparar opcion correcta entre los botones.

    public string opcionCorrectaEnBoton_punnet; /// XXXXXXX texto para comparar opcion correcta entre los botones.

    [SerializeField] public GameObject boton5050;
    [SerializeField] public GameObject boton5050_punnet;

    [Header("Seleccion para el Ui en Punnet")]
    [SerializeField] public TextMeshProUGUI tmpPregunta_Punnett;
    [SerializeField] public TextMeshProUGUI tmpDetalles_Punnettt;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_Punnett;

    [SerializeField] public GameObject[] botonesRespuestas_OpcionesPunnet; //Seteada la funcion correcta desde el objeto recolectable

    [SerializeField] public TextMeshProUGUI[] posMatriz;

    [Header("UI pergaminos y virus")]
    public int objetoRecolectableAgarrado;
    public int virusMatados;

    /// ----------------------------------------------------------------------------

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

    /// <summary>
    /// Variables locales utilidad
    /// </summary>
    /// 
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

            // MODIF
            tipoTxt[i] = diezEjercicios[i].tipo;
            preguntaTxt[i] = diezEjercicios[i].pregunta;
            detallesTxt[i] = diezEjercicios[i].detalles;
            informacion[i] = diezEjercicios[i].explicacion_solucion;

            // Preparar un string para almacenar todas las opciones
            string opcionesDebug = $"Opciones para diezEjercicios[{i}]: ";

            // Añadir comprobación de límites
            for (int j = 0; j < cuatroOpciones.Length; j++)
            {
                if (j < diezEjercicios[i].opcionesMultiples.Length)
                {
                    cuatroOpciones[j] = diezEjercicios[i].opcionesMultiples[j].texto_opcion;
                }
                else
                {
                    Debug.LogWarning($"diezEjercicios[{i}].opcionesMultiples tiene menos de 4 opciones.");
                    cuatroOpciones[j] = "Opción no disponible"; // o algún valor por defecto
                }

                // Añadir cada opción al string de depuración
                opcionesDebug += $"[{j}] {cuatroOpciones[j]} ";
            }

            // Mostrar todas las opciones en un solo mensaje de depuración
            Debug.Log(opcionesDebug);

        }

        InstanciarObjeto();
    }

    #region Instanciar objeto v2
    private void InstanciarObjeto()
    {
        for (int i = 0; i < posObjetos.Length; i++)
        {
            GameObject obj = Instantiate(objetoInstanciadoPrefab, posObjetos[i].position, Quaternion.identity);

            ObjetoNivel2 objetoNivel2 = obj.GetComponent<ObjetoNivel2>();

            // MODIF
            objetoNivel2.tipo = tipoTxt[i];
            objetoNivel2.pregunta = preguntaTxt[i];
            objetoNivel2.detalles = detallesTxt[i];
            objetoNivel2.informacion = informacion[i];

            // Verificación de límites y mensajes de depuración
            if (diezEjercicios[i].opcionesMultiples.Length < 4)
            {
                Debug.LogWarning($"diezEjercicios[{i}].opcionesMultiples tiene menos de 4 opciones.");
            }

            // Asignar las opciones para cada instancia
            for (int j = 0; j < cuatroOpciones.Length; j++)
            {
                if (j < diezEjercicios[i].opcionesMultiples.Length)
                {
                    objetoNivel2.opciones[j] = diezEjercicios[i].opcionesMultiples[j].texto_opcion;

                    // Enviarle info al objeto de cual es la correcta
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

            // Mensaje de depuración para verificar las opciones asignadas
            string opcionesDebug = $"Opciones para objetoNivel2 en posObjetos[{i}]: ";
            for (int j = 0; j < objetoNivel2.opciones.Length; j++)
            {
                opcionesDebug += $"[{j}] {objetoNivel2.opciones[j]} ";
            }
            //Debug.Log(opcionesDebug);
        }
    }
    #endregion


    public void RespuestaCorrecta(Button boton)
    {
        if (!botonPrecionado)
        {
            botonPrecionado = true;

            Debug.Log("RespuestaCorrecta");

            // Cambiar color del botón correcto
            boton.GetComponent<Image>().color = Color.green;

            AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_RespuestaCorrecta);

            // Dar feedback al jugador y sumar puntos
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
            // Cambiar color del botón incorrecto
            // Aqui
            string hexColor = "#C33C3C";//Color seteado incorrecto
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
        //hacer anim de derrota
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

        //Reproducir sonido si hay

        //No sumar puntos, o guardar el error.
        respuestasIncorrectas++;
        respuestasIncorrectas3OpcionesYSeReinicia++;

        if (respuestasIncorrectas3OpcionesYSeReinicia == 3)
        {
            respuestasIncorrectas3OpcionesYSeReinicia = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Tiempo para apreciar el feedback
        yield return new WaitForSeconds(4);
        //Cerrar panel abierto
        preguntaUi_Punnett.SetActive(false);
        preguntaUi_SeleccionMultiple.SetActive(false);
        AudioManager.instance.DetenerMusicaEspecial();
        //Restablecer variable
        ResetearTextoMatriz();
        GameManager.instance.JuegoEnPausa = false;
        botonPrecionado = false;
    }
    IEnumerator RCorrecta_Corrutina()
    {
        //hacer anim de victoria
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

        //Reproducir sonido si hay

        //Sumar puntos

        //Tiempo para apreciar el feedback
        yield return new WaitForSeconds(4);
        //Cerrar panel abierto
        preguntaUi_Punnett.SetActive(false);
        preguntaUi_SeleccionMultiple.SetActive(false);
        AudioManager.instance.DetenerMusicaEspecial();
        //Restablecer variable
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
        GameManager.instance.nivel_Ejercicios_Superado= true;
        Debug.Log("10 objetos recolectados pasando de nivel");
        //Esperar 5 segundos o el tiempo necesario antes de pasar a la siguiente escena.
        //Si hay musica reproducir
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_NivelSuperado);
        GameManager.instance.JuegoEnPausa = true; //Pausar juego para que no lo elimine un enemigo
        yield return new WaitForSeconds(5);

        //Aqui puedo implementar logica para guardar los datos del nivel si es necesario.

        GameManager.instance.ejercicios_VirusEliminados += virusMatados;
        GameManager.instance.ejercicios_RespuestasIncorrectas += respuestasIncorrectas;
        GameManager.instance.ejercicios_Intentos++;

        GameManager.instance.JuegoEnPausa = false; //Despausar y continuar.
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



    //Comodin 50% 


    public List<GameObject> botonesDesactivados = new List<GameObject>(); // Lista para guardar los botones desactivados
    public List<GameObject> botonesDesactivados_punnet = new List<GameObject>(); // Lista para guardar los botones desactivados

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

        // Crear una lista con los botones incorrectos.
        List<GameObject> botonesIncorrectos = new List<GameObject>();
        foreach (GameObject boton in botonesRespuestas_OpcionesMultiples)
        {
            if (boton != botonCorrecto)
            {
                botonesIncorrectos.Add(boton);
            }
        }

        // Desactivar dos botones incorrectos al azar.
        for (int i = 0; i < 2; i++)
        {
            if (botonesIncorrectos.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, botonesIncorrectos.Count);
                GameObject botonIncorrecto = botonesIncorrectos[index];
                botonIncorrecto.SetActive(false);
                botonesDesactivados.Add(botonIncorrecto); // Guardar referencia al botón desactivado
                botonesIncorrectos.RemoveAt(index);
            }
        }

        boton5050.SetActive(false); // Desactivar el botón de comodín
    }

    public void DesactivarDosBotonesIncorrectos_Punnet()
    {
        GameObject botonCorrecto = EncontrarBotonCorrecto_Punnet();
        if (botonCorrecto == null)
        {
            Debug.LogError("No se encontró el botón con la opción correcta.");
            return;
        }

        // Crear una lista con los botones incorrectos.
        List<GameObject> botonesIncorrectos = new List<GameObject>();
        foreach (GameObject boton in botonesRespuestas_OpcionesPunnet)
        {
            if (boton != botonCorrecto)
            {
                botonesIncorrectos.Add(boton);
            }
        }

        // Desactivar dos botones incorrectos al azar.
        for (int i = 0; i < 2; i++)
        {
            if (botonesIncorrectos.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, botonesIncorrectos.Count);
                GameObject botonIncorrecto = botonesIncorrectos[index];
                botonIncorrecto.SetActive(false);
                botonesDesactivados_punnet.Add(botonIncorrecto); // Guardar referencia al botón desactivado
                botonesIncorrectos.RemoveAt(index);
            }
        }

        boton5050_punnet.SetActive(false); // Desactivar el botón de comodín
    }

    public void ReactivarBotonesDesactivados()
    {
        // Reactivar todos los botones guardados en la lista de botones desactivados
        foreach (GameObject boton in botonesDesactivados)
        {
            boton.SetActive(true);
        }
        boton5050.SetActive(true);
        // Limpiar la lista después de reactivar los botones
        botonesDesactivados.Clear();
    }
    public void ReactivarBotonesDesactivados_Punnet()
    {
        // Reactivar todos los botones guardados en la lista de botones desactivados
        foreach (GameObject boton in botonesDesactivados_punnet)
        {
            boton.SetActive(true);
        }
        boton5050_punnet.SetActive(true);
        // Limpiar la lista después de reactivar los botones
        botonesDesactivados_punnet.Clear();
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
}
