using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    public static PanelOpciones instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        musicaSlider.onValueChanged.AddListener(CambiarVolumenMusica);
        sfxSlider.onValueChanged.AddListener(CambiarVolumenSFX);
    }

    [SerializeField] Slider musicaSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] AudioMixer musicaMixer;
    [SerializeField] GameObject panelOpciones;

    [SerializeField] GameObject minimapCiudad;
    [SerializeField] GameObject minimapLab;
    [SerializeField] GameObject botonVolverMenu;

    [SerializeField] GameObject panelConfirmacion;

    //Panel Informacion
    [SerializeField] GameObject panelInformacion;


    //Velocidad virus
    public float velocidadVirus_Lenta = 1.5f;
    public float velocidadVirus_Media = 3f;
    public float velocidadVirus_Rapida = 4f;

    public float velocidadVirus; //Pasar este parametro a el comportamiento de los virus

    [SerializeField] Toggle lenta;
    [SerializeField] Toggle media;
    [SerializeField] Toggle rapida;

    //Ayuda al apuntar

    public bool ayudaAlApuntar = true;

    private void Start()
    {
        velocidadVirus = 3f;
    }
    public void VelocidadVirus(float velocidad)
    {
        velocidadVirus = velocidad;
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
        minimapCiudad.SetActive(SceneManager.GetActiveScene().name == "Nivel_1_Conceptos");
        minimapLab.SetActive(SceneManager.GetActiveScene().name == "Nivel_2_Ejercicios");
        botonVolverMenu.SetActive(SceneManager.GetActiveScene().name != "MenúPrincipal");     

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

    //Abrir panel
    public void BotonVolverMenuAbreYCierra()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        if (panelConfirmacion.gameObject.activeSelf)
        {
            panelConfirmacion.gameObject.SetActive(false);
        }
        else
        {
            panelConfirmacion.SetActive(true);
        }
    }

    //Boton salir al menu principal

    public void BotonSiSalir()
    {
        GameManager.instance.JuegoEnPausa = false;

        GameManager.instance.concepos_VirusEliminados = 0;
        GameManager.instance.conceptos_Intentos = 0;
        GameManager.instance.conceptos_tiempo = 0;

        GameManager.instance.ejercicios_Intentos = 0;
        GameManager.instance.ejercicios_RespuestasIncorrectas = 0;
        GameManager.instance.ejercicios_VirusEliminados = 0;
        GameManager.instance.ejercicios_tiempo = 0;

        GameManager.instance.evaluacion_Intentos = 0;
        GameManager.instance.evaluacion_RespuestasIncorrectas = 0;
        GameManager.instance.evaluacion_Tiempo = 0;

        GameManager.instance.indexTextoAntagonista = 0;

        panelOpciones.gameObject.SetActive(false);
        panelConfirmacion.gameObject.SetActive(false);
        SceneManager.LoadScene("MenúPrincipal");
    }


    [SerializeField] GameObject menuPrincipalExplicacionBotones;
    [SerializeField] GameObject nivelConceptos_Explicacion;
    [SerializeField] GameObject nivelEjercicios_Explicacion;
    [SerializeField] GameObject nivelEvaluacion_Explicacion;
    public void ActivarInformacion()
    {
        AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_BotonMenu);
        if (panelInformacion.gameObject.activeSelf)
        {
            GameManager.instance.JuegoEnPausa = false;
            panelInformacion.gameObject.SetActive(false);
        }
        else
        {
            GameManager.instance.JuegoEnPausa = true;
            panelInformacion.gameObject.SetActive(true);
        }


        menuPrincipalExplicacionBotones.SetActive(SceneManager.GetActiveScene().name == "MenúPrincipal");
        nivelConceptos_Explicacion.SetActive(SceneManager.GetActiveScene().name == "Nivel_1_Conceptos");
        nivelEjercicios_Explicacion.SetActive(SceneManager.GetActiveScene().name == "Nivel_2_Ejercicios");
        nivelEvaluacion_Explicacion.SetActive(SceneManager.GetActiveScene().name == "Nivel_3_Evaluacion");
    }

    public void AyudaAlApuntar()
    {
        ayudaAlApuntar = !ayudaAlApuntar;
    }
}
