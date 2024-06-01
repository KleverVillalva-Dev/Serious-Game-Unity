using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GetDatos;

public class NivelManager : MonoBehaviour
{
    #region Singletone
    public static NivelManager Instance;
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
    [SerializeField] public GameObject pergaminoUi; //Se activa para
    [SerializeField] public TextMeshProUGUI pergaminoTitulo;
    [SerializeField] public TextMeshProUGUI pergaminoDescripcion;

    [SerializeField] private GameObject pergaminoPrefab;
    [SerializeField] private Transform[] posPergaminos;

    public Conceptos[] diezConceptos = new Conceptos[10]; //Estos se deben mantener para la evaluacion.

    private string[] tituloPapiros = new string[10];
    private string[] descripcionPapiros = new string[10];

    public int pergaminosRecolectados;
    public int virusMatados;

    [SerializeField] TextMeshProUGUI tmpPergaminos;
    [SerializeField] TextMeshProUGUI tmpVirus;

    //Respetar pos 0 mujer, pos 1 hombre
    [SerializeField] GameObject[] personajePrefab;
    [SerializeField] Transform posInstanciarJugador;
    
    private void Start()
    {
        StartCoroutine(SetearDescripcionPapiros());
    }

    private void Update()
    {
        tmpPergaminos.text = pergaminosRecolectados.ToString() + " /10";
        tmpVirus.text = virusMatados.ToString();
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
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
