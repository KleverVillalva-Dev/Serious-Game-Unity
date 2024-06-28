using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorDeNivel : MonoBehaviour
{
    [SerializeField] GameObject candado;

    private void Start()
    {
        if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
        {
            candado.SetActive(false);
        }
    }


    public void CambiarNivel(string nivel)
    {
        if (nivel == "Nivel_3_Evaluacion")
        {
          if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                Carga_Nivel.Nivel_A_Cargar(nivel);
            }
        }
        else
        {
            Carga_Nivel.Nivel_A_Cargar(nivel);
        }

        if(nivel == "Nivel_1_Conceptos")
        {
            GameManager.instance.indexTextoAntagonista = 1;
        }
    }
}
