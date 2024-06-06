using UnityEngine;

public class Pergaminos : MonoBehaviour
{
    public string titulo;
    public string descripcion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Pausar para que no nos maten los virus
            GameManager.instance.JuegoEnPausa = true;

            Nivel_Conceptos_Manager.Instance.pergaminoUi.SetActive(true);
            Nivel_Conceptos_Manager.Instance.pergaminoTitulo.text = titulo;
            Nivel_Conceptos_Manager.Instance.pergaminoDescripcion.text = descripcion;

            // Para build de testeo, mas adelante darle iniciar al siguiente nivel
            if (Nivel_Conceptos_Manager.Instance.pergaminosRecolectados == 9)
            {
                
                Nivel_Conceptos_Manager.Instance.cartelBuildTest.SetActive(true);
                Nivel_Conceptos_Manager.Instance.botonReiniciarBuildTest.SetActive(true);
            }
            else
            {
                Nivel_Conceptos_Manager.Instance.pergaminosRecolectados++;
            }
            
            Destroy(this.gameObject);
        }
    }
}
