using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    [SerializeField] Slider musicaSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] AudioMixer musicaMixer;
    [SerializeField] GameObject panelOpciones;

    private void Awake()
    {
        musicaSlider.onValueChanged.AddListener(CambiarVolumenMusica);
        sfxSlider.onValueChanged.AddListener(CambiarVolumenSFX);
    }
    public void CambiarVolumenMusica(float v)
    {
        musicaMixer.SetFloat("Musica", v);
    }

    public void CambiarVolumenSFX(float v)
    {
        musicaMixer.SetFloat("SFX", v);
    }

    public void AbrirCerrarPanel()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        if (panelOpciones.gameObject.activeSelf)
        {
            GameManager.instance.JuegoEnPausa = false;
            panelOpciones.gameObject.SetActive(false);
        }
        else
        {
            GameManager.instance.JuegoEnPausa = true;
            panelOpciones.gameObject.SetActive(true);
        }
    }

}
