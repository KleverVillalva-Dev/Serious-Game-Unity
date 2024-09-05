using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Image conceptosCompletedIndicator;
    public Image ejerciciosCompletedIndicator;

    void Start()
    {
        // Actualizar el estado de los indicadores
        bool conceptosCompletados = GameProgress.IsLevelCompleted("Conceptos");
        Debug.Log("Conceptos completados: " + conceptosCompletados);
        conceptosCompletedIndicator.enabled = conceptosCompletados;

        //Actualizar el estado de los indicadores para los ejercicios
        bool ejerciciosCompletados = GameProgress.IsLevelCompleted("Ejercicios");
        Debug.Log("Ejercicios completados: " +  ejerciciosCompletados);
        ejerciciosCompletedIndicator.enabled = ejerciciosCompletados;
    }
}
