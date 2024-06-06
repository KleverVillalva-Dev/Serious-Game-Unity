using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorImagenes : MonoBehaviour
{
    [SerializeField] Sprite[] imagenes;
    [SerializeField] GameObject flechaSiguiente;
    [SerializeField] GameObject flechaAnterior;

    private int indice = 0;

    private void Start()
    {
        ActualizarImagen();
    }

    public void ImagenSiguiente()
    {
        if (indice < imagenes.Length -1)
        {
            indice++;
        }
        ActualizarImagen();


    }

    public void ImagenAnterior()
    {
        if (indice > 0)
        {
            indice--;
        }

        ActualizarImagen();
    }

    private void ActualizarImagen()
    {
        Image imagen = gameObject.GetComponent<Image>();

        if (indice >= 0 && indice < imagenes.Length)
        {
            imagen.sprite = imagenes[indice];
        }
        else
        {
            Debug.LogError("Índice de imagen fuera de rango.");
        }

        flechaAnterior.SetActive(indice > 0);
        flechaSiguiente.SetActive(indice < imagenes.Length - 1);
    }
}
