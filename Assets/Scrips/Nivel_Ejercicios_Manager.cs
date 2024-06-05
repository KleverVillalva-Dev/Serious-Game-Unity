using System.Collections;
using TMPro;
using UnityEngine;
using static GetDatos;

public class Nivel_Ejercicios_Manager : MonoBehaviour
{
    #region Singletone
    public static Nivel_Ejercicios_Manager Instance;
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

    [Header("Seccion para el UI en Seleccion Multiple")]
    [SerializeField] public GameObject preguntaUi_SeleccionMultiple; // Se activa para mostrar info, desde el objeto al tocarlo
    [SerializeField] public GameObject preguntaUi_Punnett;
    [SerializeField] public TextMeshProUGUI tmpPregunta_SeleccionMultiple;
    [SerializeField] public TextMeshProUGUI tmpDetalles_SeleccionMultiple;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_opciones;

    [Header("Seleccion para el Ui en Punnet")]
    [SerializeField] public TextMeshProUGUI tmpPregunta_Punnett;
    [SerializeField] public TextMeshProUGUI tmpDetalles_Punnettt;
    [SerializeField] public TextMeshProUGUI[] textoRespuestas_Punnett;


    public Ejercicio[] diezEjercicios = new Ejercicio[10];

    [SerializeField] private GameObject objetoInstanciadoPrefab;
    [SerializeField] private Transform[] posObjetos;

    private string[] tipoTxt = new string[10];
    private string[] preguntaTxt = new string[10];
    private string[] detallesTxt = new string[10];


    string[] cuatroOpciones = new string[4];

    private void Start()
    {
        StartCoroutine(SetearInformacionObjetoLVL2());
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

                    
            if (diezEjercicios[i].tipo == "Selección Múltiple")
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

}
