using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pergaminos : MonoBehaviour
{
    string titulo;
    string descripcion;

    GetDatos datos;

    private void Start()
    {
        datos = FindObjectOfType<GetDatos>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int r = Random.Range(0, datos.conceptosArray.Length);
            titulo = datos.conceptosArray[r].titulo;
            descripcion = datos.conceptosArray[r].descripcion;
            Debug.Log(descripcion);

            //Activar en gamemanager pergaminoUI
            GameManager.Instance.pergaminoUi.SetActive(true);
            GameManager.Instance.pergaminoTitulo.text = titulo;
            GameManager.Instance.pergaminoDescripcion.text = descripcion;

            Destroy(gameObject);
            //Pasarle textos descripcion y titulo
        }   
    }
}
