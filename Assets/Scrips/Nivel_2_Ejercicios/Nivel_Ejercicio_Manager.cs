using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    [Header("Seleccion para el Ui en Punnet")]
    [SerializeField] public TextMeshProUGUI tmpPregunta_Punnett;
    [SerializeField] public TextMeshProUGUI tmpDetalles_Punnettt;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_Punnett;

    [SerializeField] public GameObject[] botonesRespuestas_OpcionesPunnet; //Seteada la funcion correcta desde el objeto recolectable


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

    string[] cuatroOpciones = new string[4];

    private void Start()
    {
        //Activar personaje para UI dependiendo de con cual se este jugando

        bool esFemenino = (GameManager.instance.personajeSeleccionado == 0);
        personajeFemenino.SetActive(esFemenino);
        personajeMasculino.SetActive(!esFemenino);

        StartCoroutine(SetearInformacionObjetoLVL2());
    }
    private void Update()
    {
        tmpPergaminos.text = objetoRecolectableAgarrado.ToString() + " /10";
        tmpVirus.text = virusMatados.ToString();
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

            // cambiamos string "tipo" por entero id_tipo, 1 para multiple 2 para punnet.
            if (diezEjercicios[i].tipo == 1)// 1 "Selección Múltiple"
            {
                for (int j = 0; j < cuatroOpciones.Length; j++)
                {
                    cuatroOpciones[j] = diezEjercicios[i].opcionesMultiples[j].texto_opcion;
                }
            }
            else
            {
                //  otros tipos de ejercicios aquí 
            }
        }

        InstanciarObjeto();
    }

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

            // Asignar las opciones para cada instancia
            for (int j = 0; j < cuatroOpciones.Length; j++)
            {
                objetoNivel2.opciones[j] = diezEjercicios[i].opcionesMultiples[j].texto_opcion;

                //Enviarle info al objeto de cual es la correcta
                if (diezEjercicios[i].opcionesMultiples[j].es_correcta)
                {
                    objetoNivel2.opcionCorrecta = j;
                }
            }

            // Falta decirle cual es el correcto via codigo
            //Tambien distribuir los 4 textos random en las 4 opciones de respuesta.
        }
    }

    //Esta configuracion de botones se llamara y seteara el correcto desde el objeto recolectado
   
    //Variable para que no se precione mas de 1 boton
    bool botonPrecionado = false;

    public void RespuestaCorrecta()
    {
        if (!botonPrecionado)
        {
            botonPrecionado = true;

            Debug.Log("RespuestaCorrecta");

            //Dar feedback al jugador y sumar puntos
            StartCoroutine(RCorrecta_Corrutina());
            RevisarSiAgarre10();
        }
    }

    public void RespuestaIncorrecta()
    {
        if (!botonPrecionado)
        {
            botonPrecionado = true;
            Debug.Log("Respuesta Incorrecta");
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

        //Tiempo para apreciar el feedback
        yield return new WaitForSeconds(4);
        //Cerrar panel abierto
        preguntaUi_Punnett.SetActive(false);
        preguntaUi_SeleccionMultiple.SetActive(false);

        //Restablecer variable
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

        //Restablecer variable
        botonPrecionado = false;
    }
    public void RevisarSiAgarre10()
    {
        if (objetoRecolectableAgarrado == 10)
        {
            //Logica para pasar de nivel
        }
    }

    IEnumerator NivelEjerciciosSuperado()
    {
        Debug.Log("10 objetos recolectados pasando de nivel");
        //Esperar 5 segundos o el tiempo necesario antes de pasar a la siguiente escena.
        //Si hay musica reproducir
        GameManager.instance.JuegoEnPausa = true; //Pausar juego para que no lo elimine un enemigo
        yield return new WaitForSeconds(5);

        //Aqui puedo implementar logica para guardar los datos del nivel si es necesario.

        GameManager.instance.JuegoEnPausa = false; //Despausar y continuar.
        Carga_Nivel.Nivel_A_Cargar("SC_Antagonista");
    }
}
