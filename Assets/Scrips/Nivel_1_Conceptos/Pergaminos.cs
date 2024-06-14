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
            AudioManager.instance.MusicaEspecial(AudioManager.instance.sfx_ejercicio);
            Nivel_Conceptos_Manager.Instance.pergaminoUi.SetActive(true);
            Nivel_Conceptos_Manager.Instance.pergaminoTitulo.text = titulo;
            Nivel_Conceptos_Manager.Instance.pergaminoDescripcion.text = descripcion;


            Nivel_Conceptos_Manager.Instance.pergaminosRecolectados++;

            
            Destroy(this.gameObject);
        }
    }
}
