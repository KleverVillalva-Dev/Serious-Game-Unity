using UnityEngine;

public class Pergaminos : MonoBehaviour
{
    public string titulo;
    public string descripcion;


    GetDatos datos;

    private void Start()
    {
        datos = FindObjectOfType<GetDatos>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            #region Anterior
            //int r = Random.Range(0, datos.conceptosArray.Length);
            //titulo = datos.conceptosArray[r].titulo;
            //descripcion = datos.conceptosArray[r].descripcion;
            //Debug.Log(descripcion);

            //Activar en gamemanager pergaminoUI

            ////Pasarle textos descripcion y titulo
            ///
            #endregion

            GameManager.Instance.pergaminoUi.SetActive(true);
            GameManager.Instance.pergaminoTitulo.text = titulo;
            GameManager.Instance.pergaminoDescripcion.text = descripcion;
            GameManager.Instance.pergaminosRecolectados++;
            Destroy(this.gameObject);
        }
    }
}
