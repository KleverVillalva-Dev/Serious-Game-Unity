using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] GameObject canvasSeleccionPersonaje;

    
    //Activa o desactiva selector
    public void SelectorPersonaje()
    {
        if (!canvasSeleccionPersonaje.activeSelf)
        {
            canvasSeleccionPersonaje.SetActive(true);
        }else
        {
            canvasSeleccionPersonaje.SetActive(false);
        }
    }



    public void ActivarTick(GameObject check)
    {
        check.SetActive(true);
        // Esta porcion de codigo es unicamente para desactivar el otro check cuando seleccionamos uno

        // Obtener todos los objetos en la escena con el mismo tag
        #region Desactivar el otro check
        GameObject[] objetosConMismoNombre = GameObject.FindGameObjectsWithTag(check.tag);
     
        int contador = 0;

        // Recorrer todos los objetos encontrados
        foreach (GameObject obj in objetosConMismoNombre)
        {
            if (obj != check)
            {
                obj.SetActive(false); // Desactivar el objeto
            }
            else
            {
                contador++;
                if (contador > 1)
                {
                    check.SetActive(false);
                }
            }
        }
        #endregion
    }

    public void SeleccionarGeneroInt(int genero)
    {
        GameManager.instance.personajeSeleccionado = genero;
    }


    public void BotonJugar()
    {
        //SceneManager.LoadScene("Nivel_1_Conceptos");
        SceneManager.LoadScene("Nivel_2_Ejercicios");
    }

    public void BotonCerrarJuego()
    {
        Debug.Log("El boton funciona");
        Application.Quit();
    }

}
