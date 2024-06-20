using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] GameObject canvasSeleccionPersonaje;
    [SerializeField] GameObject[] personajesUtilidad; //0 femenino 1 masculino

    [SerializeField] TextMeshProUGUI nombrePersonajeTMP;
    [SerializeField] TextMeshProUGUI rolPersonajeTMP;
    [SerializeField] TextMeshProUGUI descripcionPersonajeTMP;

    [SerializeField] string[] nombrePersonaje;
    [SerializeField] string[] rolPersonaje;
    [SerializeField] string[] descripcionPersonaje;
    
    //Activa o desactiva selector
    public void SelectorPersonaje()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        if (!canvasSeleccionPersonaje.activeSelf)
        {
            canvasSeleccionPersonaje.SetActive(true);
            SeleccionarGeneroInt(0);
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
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        GameManager.instance.personajeSeleccionado = genero;
        personajesUtilidad[0].SetActive(false);
        personajesUtilidad[1].SetActive(false);
        personajesUtilidad[genero].SetActive(true);

        nombrePersonajeTMP.text = nombrePersonaje[genero];
        descripcionPersonajeTMP.text = descripcionPersonaje[genero];
    }


    public void BotonJugar()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);

        if (GetDatos.instance.ejerciciosCargados && GetDatos.instance.evaluacionCargado && GetDatos.instance.conceptosCargados)
        {
            //Carga_Nivel.Nivel_A_Cargar("Nivel_3_Evaluacion");
            //Carga_Nivel.Nivel_A_Cargar("Nivel_1_Conceptos");
            //Carga_Nivel.Nivel_A_Cargar("Nivel_2_Ejercicios");
            Carga_Nivel.Nivel_A_Cargar("SelectorDeNivel");
            
        }
        else
        {
            Debug.Log("No se pudo obtener ejercicios de la base de datos");
        }

    }

    public void BotonCerrarJuego()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        Debug.Log("El boton funciona");
        Application.Quit();
    }

}
