using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using System;

public class GetDatos : MonoBehaviour
{
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
                Debug.Log("Los conceptos estan cargados");
                //Debug.Log(ConceptoAleatorio());

                //foreach (Conceptos concepto in conceptosArray)
                //{
                //    Debug.Log("Título: " + concepto.titulo + ", Descripción: " + concepto.descripcion);
                //}
            }
        }
    }

    //public string ConceptoAleatorio()
    //{
    //    int r = UnityEngine.Random.Range(0, conceptosArray.Length);
    //    string concepto = conceptosArray[r].descripcion.ToString();
    //    return concepto;
    //}

}
