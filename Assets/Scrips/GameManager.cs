using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GetDatos;

public class GameManager : MonoBehaviour
{
    #region Singletone
    public static GameManager Instance;
    private void Awake()
    {
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

    public Conceptos[] diezConceptosNivel1 = new Conceptos[10]; //Estos se deben mantener para la evaluacion.

    private string[] tituloPapiros = new string[10];
    private string[] descripcionPapiros = new string[10];

    public int pergaminosRecolectados;
    public int virusMatados;

    [SerializeField] TextMeshProUGUI tmpPergaminos;
    [SerializeField] TextMeshProUGUI tmpVirus;

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

        for (int i = 0; i < diezConceptosNivel1.Length; i++)
        {
            tituloPapiros[i]      = diezConceptosNivel1[i].titulo;
            descripcionPapiros[i] = diezConceptosNivel1[i].descripcion;           
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
