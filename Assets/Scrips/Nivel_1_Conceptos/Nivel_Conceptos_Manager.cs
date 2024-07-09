using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GetDatos;

public class Nivel_Conceptos_Manager : MonoBehaviour
{
    #region Singletone
    public static Nivel_Conceptos_Manager Instance;
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

    [SerializeField] public GameObject pergaminoUi; //Se activa para mostrar los pergaminos
    [SerializeField] public TextMeshProUGUI pergaminoTitulo;
    [SerializeField] public TextMeshProUGUI pergaminoDescripcion;

    [SerializeField] private GameObject pergaminoPrefab;
    [SerializeField] private Transform[] posPergaminos;

    public Conceptos[] diezConceptos = new Conceptos[10]; //Estos se deben mantener para la evaluacion.

    private string[] tituloPapiros = new string[10];
    private string[] descripcionPapiros = new string[10];

    public int pergaminosRecolectados;
    public int virusMatados;

    //Variables para reiniciar build test
    [SerializeField] public GameObject cartelBuildTest;
    [SerializeField] public GameObject botonReiniciarBuildTest;

    [SerializeField] TextMeshProUGUI tmpPergaminos;
    [SerializeField] TextMeshProUGUI tmpVirus;

    GameObject curva;


    private void Start()
    {
        curva = GameObject.Find("Curva");
        StartCoroutine(SetearDescripcionPapiros());
    }

    private void Update()
    {
        tmpPergaminos.text = pergaminosRecolectados.ToString() + " /10";
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

    //Esta coorrunita espera hasta que los conceptos esten cargados para ir a buscar
    // los 10 aleatorios elegidos en GetDatos y preparar la informacion para los papiros.
    IEnumerator SetearDescripcionPapiros()
    {
        while (!GetDatos.instance.conceptosCargados)
        {
            yield return null;
        }
        //Solo luego de estar cargados continuamos.
        for (int i = 0; i < diezConceptos.Length; i++)
        {
            diezConceptos[i] = GameManager.instance.diezConceptosNivel1[i];
        }

        for (int i = 0; i < diezConceptos.Length; i++)
        {
            tituloPapiros[i]      = diezConceptos[i].titulo;
            descripcionPapiros[i] = diezConceptos[i].descripcion;           
        }
        InstanciarPapiro();
    }

    private void InstanciarPapiro()
    {
        for (int i = 0; i < posPergaminos.Length; i++)
        {         
            pergaminoPrefab.GetComponent<Pergaminos>().descripcion = descripcionPapiros[i];
            pergaminoPrefab.GetComponent<Pergaminos>().titulo = tituloPapiros[i];

            Instantiate(pergaminoPrefab, posPergaminos[i].position, Quaternion.identity);
        }
    }

    //Funciones botones
    public void BotonVolverUIPergamino()
    {
        pergaminoUi.SetActive(false);
        GameManager.instance.JuegoEnPausa = false;

        //Sonidos
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        AudioManager.instance.DetenerMusicaEspecial();

        if (pergaminosRecolectados == 10)
        {
            //Corrutina para pasar al siguiente nivel
            StartCoroutine(NivelConceptosSuperado());
        }
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

    IEnumerator NivelConceptosSuperado()
    {
        GameManager.instance.nivel_Conceptos_Superado = true;
        Debug.Log("10 pergaminos recolectados pasando de nivel");
        //Esperar 5 segundos o el tiempo necesario antes de pasar a la siguiente escena.
        //Si hay musica reproducir
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_NivelSuperado);

        GameManager.instance.JuegoEnPausa = true; //Pausar juego para que no lo elimine un enemigo
        yield return new WaitForSeconds(5);

        //Aqui puedo implementar logica para guardar los datos del nivel si es necesario.
        GameManager.instance.concepos_VirusEliminados += virusMatados;
        GameManager.instance.conceptos_Intentos++;


        GameManager.instance.JuegoEnPausa = false; //Despausar y continuar.
        GameManager.instance.indexTextoAntagonista = 0;
        Carga_Nivel.Nivel_A_Cargar("SC_Antagonista");
    }
}
