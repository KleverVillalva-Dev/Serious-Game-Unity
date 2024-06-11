using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cargando : MonoBehaviour
{
    public TextMeshProUGUI texto;
    string nivelACargar;
    private void Start()
    {
        nivelACargar = Carga_Nivel.siguienteNivel;

        StartCoroutine(IniciarCarga(nivelACargar));
    }

    IEnumerator IniciarCarga(string nivel)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                texto.text = "Presiona para continuar...";
                // Muestra un mensaje en la consola para verificar la progresión
                Debug.Log("Nivel cargado al 90%, esperando activación");

                // Espera la entrada del usuario
                while (!Input.anyKey)
                {
                    yield return null;
                }
                // Activar la escena una vez que se detecta la entrada del usuario
                operacion.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
