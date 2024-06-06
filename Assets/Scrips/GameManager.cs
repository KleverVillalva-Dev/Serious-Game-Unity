using UnityEngine;
using static GetDatos;

public class GameManager : MonoBehaviour
{
    #region Singletone
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    public bool JuegoEnPausa = false;

    // 0 para femenino, 1 para masculino
    public int personajeSeleccionado;

    public Conceptos[] diezConceptosNivel1 = new Conceptos[10]; //Estos se deben mantener para la evaluacion.
    public Ejercicio[] diezEjerciciosNivel2 = new Ejercicio[10];

}
