using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject botonIniciar;
    [SerializeField] GameObject fadeOut;
    private float tCambioEscena = 2.3f;

    public void ActivarBoton()
    {
        botonIniciar.SetActive(true);
    }

    public void SiguienteEscena()
    {
        StartCoroutine(SiguienteEscenaCorrutina());
    }
    IEnumerator SiguienteEscenaCorrutina()
    {
        fadeOut.SetActive(true);
        botonIniciar.SetActive(false);
        yield return new WaitForSeconds(tCambioEscena);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
