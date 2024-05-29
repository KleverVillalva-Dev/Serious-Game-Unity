using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    [SerializeField] public GameObject pergaminoUi;
    [SerializeField] public TextMeshProUGUI pergaminoTitulo;
    [SerializeField] public TextMeshProUGUI pergaminoDescripcion;

    [SerializeField] private GameObject pergaminoPrefab;
    [SerializeField] private Transform[] posPergaminos;

    public Conceptos[] diezConceptosNivel1 = new Conceptos[10]; //Estos se deben mantener para la evaluacion.

    private string[] tituloPapiros = new string[10];
    private string[] descripcionPapiros = new string[10];

    public int pergaminosRecolectados;
    [SerializeField] TextMeshProUGUI tmpPergaminos;

    private void Start()
    {       
        StartCoroutine(SetearDescripcionPapiros());
    }

    private void Update()
    {
        tmpPergaminos.text = pergaminosRecolectados.ToString() + " /10";
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


    public void BotonVolverUIPergamino()
    {
        pergaminoUi.SetActive(false);
    }
}
