using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

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

    public class Conceptos
    {
        public int concepto_id;
        public string titulo;
        public string descripcion;
        public string imagen;
        public string categoria;
    }

    public Conceptos[] conceptosArray;

    public UnityWebRequest request;

    public bool conceptosCargados = false;
    private void Start()
    {
        StartCoroutine(GetDatosAppis(URLConceptos));
    }


    IEnumerator GetDatosAppis(string url)
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

                //Re estructurando xxxx
                //NivelManager.Instance.diezConceptos[i] = arrayDiezConceptos[i];
                GameManager.instance.diezConceptosNivel1[i] = arrayDiezConceptos[i];
            }

            //Revisar que esten los 10 por consola.

            //foreach (var concepto in arrayDiezConceptos)
            //{
                
            //    Debug.Log("Título: " + concepto.titulo + ", Descripción: " + concepto.descripcion);
            //}
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
}
