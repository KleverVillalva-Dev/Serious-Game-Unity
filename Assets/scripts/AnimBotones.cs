using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimBotones : MonoBehaviour
{
    private string url = "https://www.google.com/maps/d/u/0/edit?mid=1dLER6GthJ0jfWRq-T4N2q76zrhpXZ2c&usp=sharing";
    [SerializeField] GameObject modulo1Textos;
    [SerializeField] GameObject modulo1Botonesir;
    [SerializeField] GameObject modulo1BotonRegresar;

    [SerializeField] GameObject modulo2Textos;
    [SerializeField] GameObject modulo2Botonesir;
    [SerializeField] GameObject modulo2BotonRegresar;

    [SerializeField] GameObject modulo3Textos;
    [SerializeField] GameObject modulo3Botonesir;
    [SerializeField] GameObject modulo3BotonRegresar;

    [SerializeField] AudioSource musica;
    [SerializeField] AudioSource sonidos;
    [SerializeField] GameObject botonMutear;
    [SerializeField] Sprite logoMuted;
    [SerializeField] Sprite logoBotonOriginal;

    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel2;
    [SerializeField] GameObject panel3;

    private float tiempoParaDesactivar = 0f;
    public void ActivarModulo1()
    {
        panel1.SetActive(true);
        modulo1Textos.SetActive(true);
        modulo1Botonesir.SetActive(true);
        modulo1BotonRegresar.SetActive(true); 
    }
    public void ActivarModulo2()
    {
        panel2.SetActive(true);
        modulo2Textos.SetActive(true);
        modulo2Botonesir.SetActive(true);
        modulo2BotonRegresar.SetActive(true);
    }
    public void ActivarModulo3()
    {
        panel3.SetActive(true);
        modulo3Textos.SetActive(true);
        modulo3Botonesir.SetActive(true);
        modulo3BotonRegresar.SetActive(true);
    }

    public void DesactivarModulo1()
    {
        StartCoroutine(DesactivarModulo1Co());
    }
    IEnumerator DesactivarModulo1Co()
    {
        yield return new WaitForSeconds(tiempoParaDesactivar);
        panel1.SetActive(false);
        modulo1Textos.SetActive(false);
        modulo1Botonesir.SetActive(false);
        modulo1BotonRegresar.SetActive(false);
    }

    public void DesactivarModulo2()
    {
        StartCoroutine(DesactivarModulo2Co());
    }
    IEnumerator DesactivarModulo2Co()
    {
        yield return new WaitForSeconds(tiempoParaDesactivar);
        panel2.SetActive(false);
        modulo2Textos.SetActive(false);
        modulo2Botonesir.SetActive(false);
        modulo2BotonRegresar.SetActive(false);
    }

    public void DesactivarModulo3()
    {
        StartCoroutine(DesactivarModulo3Co());
    }
    IEnumerator DesactivarModulo3Co()
    {
        yield return new WaitForSeconds(tiempoParaDesactivar);
        panel3.SetActive(false);
        modulo3Textos.SetActive(false);
        modulo3Botonesir.SetActive(false);
        modulo3BotonRegresar.SetActive(false);
    }

    public void MutearSonidos()
    {
        if (musica.enabled)
        {
            musica.enabled = false;
            sonidos.enabled = false;
            Image componenteImagen = botonMutear.GetComponent<Image>();
            componenteImagen.sprite = logoMuted;
        }
        else
        {
            musica.enabled = true;
            sonidos.enabled = true;
            Image componenteImagen = botonMutear.GetComponent<Image>();
            componenteImagen.sprite = logoBotonOriginal;
        }
    }

    public void CerrarPrograma()
    {
        Application.Quit();
        Debug.Log("No funciona en unity solo en la build");
    }

    public void AbrirUrl()
    {
        Application.OpenURL(url);
    }
}
