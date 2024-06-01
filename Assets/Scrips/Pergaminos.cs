using UnityEngine;

public class Pergaminos : MonoBehaviour
{
    public string titulo;
    public string descripcion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NivelManager.Instance.pergaminoUi.SetActive(true);
            NivelManager.Instance.pergaminoTitulo.text = titulo;
            NivelManager.Instance.pergaminoDescripcion.text = descripcion;
            NivelManager.Instance.pergaminosRecolectados++;
            Destroy(this.gameObject);
        }
    }
}
