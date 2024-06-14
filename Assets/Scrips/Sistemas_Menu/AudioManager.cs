using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singletone
    public static AudioManager instance;

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

    [SerializeField] AudioSource musica_Audiosource;
    [SerializeField] AudioSource sfx_Audiosource;
    [SerializeField] AudioSource audioSourceExtra;

    [SerializeField] public AudioClip entorno_MenuPrincipal;
    [SerializeField] public AudioClip entorno_Conceptos;
    [SerializeField] public AudioClip entorno_Ejercicios;
    [SerializeField] public AudioClip entorno_Evaluacion;

    //Musica extra
    [SerializeField] public AudioClip sfx_ejercicio;
    //Efectos de sonido

    [SerializeField] public AudioClip sfx_Arma;
    [SerializeField] public AudioClip sfx_Caminar;
    [SerializeField] public AudioClip sfx_BotonMenu;
    [SerializeField] public AudioClip sfx_muertePersonaje;
    [SerializeField] public AudioClip sfx_RespuestaCorrecta;
    [SerializeField] public AudioClip sfx_RespuestaIncorrecta;
    [SerializeField] public AudioClip sfx_NivelSuperado;

    //         AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
    public void ReproducirSonido(AudioClip clip)
    {
        sfx_Audiosource.PlayOneShot(clip);
    }

    public void MusicaEspecial(AudioClip clip)
    {
        audioSourceExtra.Stop();
        audioSourceExtra.clip = clip;
        audioSourceExtra.Play();
    }
    public void DetenerMusicaEspecial()
    {
        audioSourceExtra.Stop();
    }

    public void MusicaFondo(AudioClip clip)
    {
        musica_Audiosource.Stop();
        musica_Audiosource.clip = clip;
        musica_Audiosource.Play();
    }
}
