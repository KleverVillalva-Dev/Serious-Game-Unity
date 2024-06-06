using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorImagenes : MonoBehaviour
{
    [SerializeField] GameObject[] imagen;
    SonidoOnClick sonidoOnclickScript;

    private void Start()
    {
        sonidoOnclickScript = FindObjectOfType<SonidoOnClick>();
    }
    //Para abrir imagenes
    public void BotonImagenes(GameObject imagen)
    {
        sonidoOnclickScript.ReproduciorSonidoOnClick();
        imagen.SetActive(true);
    }
    //Cierra la imagen activa en este momento
    public void BotonCerrarImagen()
    {
        sonidoOnclickScript.ReproduciorSonidoOnClick();
        foreach (var item in imagen)
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
            }
        }
    }
}
