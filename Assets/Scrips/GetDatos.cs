using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

/// <summary>
/// Resumen del funcionamiento.
/// Este script toma las url de las appis, parsea a json, y de ahi se crean los diferentes objetos
/// para ser utilizados en el proyecto. El el script claseEjercicio se encuentran
/// algunas clases que son creadas a partir de json y la base de datos.
/// Ejercicio es la clase principal, con un array de pciones multiples y una matriz.
///
/// Este codigo tiene varias coorrutinas GetDatosAppi... para cada una de las appis, de ahi distribuimos
/// la informacion a el nivel que sea necesario.
/// Ya que en el caso especifico de este proyecto, se necesitan 10 ejercicios por nivel, tenemos
/// la funcion para adjuntar 10 al gamemanager.
/// </summary>


public class GetDatos : MonoBehaviour
{
    #region Singletone
    public static GetDatos instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private string URLConceptos = "https://backseriousgame.onrender.com/api/getconceptos/";
    //private string urlInteractivos = "https://backseriousgame.onrender.com/api/ejercicios/interactivos";
    private string urlInteractivos = "https://backseriousgame.onrender.com/api/ejercicios/opcionesmultiples";

    //Conceptos
    public Conceptos[] conceptosArray;
    public bool conceptosCargados = false; //Boleano avisando si estamos cargados

    //Ejercicios
    private Ejercicio[] ejerciciosArray;

    private Ejercicio[] ejerciciosArray_Punnett;
    private Ejercicio[] ejerciciosArray_Multiples;

    public bool ejerciciosCargados = false; //Boleano avisando si estamos cargados


    public UnityWebRequest request;

    private void Start()
    {
        StartCoroutine(GetDatosAppiConceptos(URLConceptos));  //No tocar
        StartCoroutine(GetDatosAppiEjercicios(urlInteractivos));
    }


    IEnumerator GetDatosAppiConceptos(string url)
    {
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
             
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
                conceptosCargados = false;
            }
            else
            {
                string json = request.downloadHandler.text;
                JSONNode data = JSON.Parse(json);

                // Inicializa el array con el número de objetos en el JSON
                conceptosArray = new Conceptos[data.Count];


                for (int i = 0; i < data.Count; i++)
                {
                    JSONNode item = data[i];

                    Conceptos concepto = new Conceptos
                    {
                        concepto_id = item["concepto_id"].AsInt,
                        titulo = item["titulo"],
                        descripcion = item["descripcion"],
                        imagen = item["imagen"],  // Almacena la URL de la imagen
                        categoria = item["categoria"]
                    };

                    conceptosArray[i] = concepto;

                }

                conceptosCargados = true;
                Debug.Log("Los conceptos estan cargados");

                SetearDiezConceptos();
            }
        }
    }

    private void SetearDiezConceptos()
    {
        if (conceptosArray.Length >= 10 && conceptosCargados)
        {
            Conceptos[] arrayDiezConceptos = new Conceptos[10];

            List<int> indices = new List<int>();

            //Agregar a la lista de indices la cantidad de conceptos en el array
            for (int i = 0; i < conceptosArray.Length; i++)
            {
                indices.Add(i);
            }

            //Mezclar la lista 
            indices = ListaAleatoria(indices);

            //Guardar los 10 primeros
            for (int i = 0; i < 10; i++)
            {
                arrayDiezConceptos[i] = conceptosArray[indices[i]];

                //Enviar los 10 a GameManager. Se distribuiran a sus respectivos niveles.
                GameManager.instance.diezConceptosNivel1[i] = arrayDiezConceptos[i];
            }
        }
    }

    // Método para mezclar la lista
    private List<int> ListaAleatoria(List<int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            int temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
        return list;
    }

    #region Conectar ejercicios Interactivos

    //New Ejercicios interactivos.
    //Variables



    IEnumerator GetDatosAppiEjercicios(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
                ejerciciosCargados = false;
            }
            else
            {
                string json = request.downloadHandler.text;
                JSONNode data = JSON.Parse(json);

                // Inicializa el array con el número de objetos en el JSON
                ejerciciosArray = new Ejercicio[data.Count];

                for (int i = 0; i < data.Count; i++)
                {
                    JSONNode item = data[i];

                    //Cambiamos string "tipo" por entero tipo_id, 1 para multiple 2 para punnet.
                    Ejercicio ejercicio = new Ejercicio
                    {
                        ejercicio_id = item["ejercicio_id"].AsInt,
                        pregunta = item["pregunta"],
                        imagen = item["imagen"],  // Almacena la URL de la imagen o null
                        tipo = item["tipo_id"],
                        detalles = item["detalles"],
                        mostrar_solucion = item["mostrar_solucion"].AsBool,
                        explicacion_solucion = item["explicacion_solucion"]
                    };

                    // Procesa las opciones múltiples
                    JSONNode opciones = item["opciones_multiples"];
                    ejercicio.opcionesMultiples = new Opcion[opciones.Count];
                    for (int j = 0; j < opciones.Count; j++)
                    {
                        JSONNode opcionItem = opciones[j];
                        Opcion opcion = new Opcion
                        {
                            opcion_id = opcionItem["opcion_id"].AsInt,
                            ejercicio_id = opcionItem["ejercicio_id"].AsInt,
                            texto_opcion = opcionItem["texto_opcion"],
                            es_correcta = opcionItem["es_correcta"].AsBool,
                            tipo = opcionItem["tipo"],
                            tipo_interactivo = opcionItem["tipo_interactivo"]
                        };
                        ejercicio.opcionesMultiples[j] = opcion;
                    }

                    // Procesa la matriz de Punnett
                    JSONNode matriz = item["matriz_punnett"];
                    ejercicio.matrizPunnett = new Matriz[matriz.Count];
                    for (int k = 0; k < matriz.Count; k++)
                    {
                        JSONNode matrizItem = matriz[k];
                        Matriz celda = new Matriz
                        {
                            matriz_id = matrizItem["matriz_id"].AsInt,
                            ejercicio_id = matrizItem["ejercicio_id"].AsInt,
                            alelo1 = matrizItem["alelo1"],
                            alelo2 = matrizItem["alelo2"],
                            resultado = matrizItem["resultado"]
                        };
                        ejercicio.matrizPunnett[k] = celda;
                    }

                    ejerciciosArray[i] = ejercicio;
                }

                ejerciciosCargados = true;
                Debug.Log("Los ejercicios están cargados");

                SetearDiezEjercicio();
            }
        }
    }
 
    void DistribuirPorTipo()
    {
        //Llenar las 2 arrays con Ejercicios diferenciado por tipo / tipo = Punnett / Tipo = Seleccion Multiple

        List<Ejercicio> punnettList = new List<Ejercicio>();
        List<Ejercicio> multiplesList = new List<Ejercicio>();

        // cambiamos string "tipo" por entero id_tipo, 1 para multiple 2 para punnet
        for (int i = 1; i < ejerciciosArray.Length; i++)
        {

            if (ejerciciosArray[i].tipo == 2) //  2"Punnett"
            {
                punnettList.Add(ejerciciosArray[i]);
            }
            else if (ejerciciosArray[i].tipo == 1 ) //"Selección Múltiple"
            {

                multiplesList.Add(ejerciciosArray[i]);
            }
        }
        //Convertir los arrays
        ejerciciosArray_Punnett = punnettList.ToArray();
        ejerciciosArray_Multiples = multiplesList.ToArray();
    }

    private void SetearDiezEjercicio()
    {
        if (ejerciciosArray.Length >= 10 && ejerciciosCargados)
        {
            Ejercicio[] arrayDiezEjercicios = new Ejercicio[10];

            List<int> indices = new List<int>();

            for (int i = 0; i < ejerciciosArray.Length; i++)
            {
                indices.Add(i);
            }

            indices = ListaAleatoria(indices);

            //Enviar los 10 a GameManager. Se distribuiran a sus respectivos niveles.
            for (int i = 0; i < 10; i++)
            {
                arrayDiezEjercicios[i] = ejerciciosArray[indices[i]];

                GameManager.instance.diezEjerciciosNivel2[i] = arrayDiezEjercicios[i];
            }
        }
    }
        #endregion
    }


