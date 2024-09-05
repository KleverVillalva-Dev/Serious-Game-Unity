using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectorDeNivel : MonoBehaviour
{
    [SerializeField] GameObject candado;
    [SerializeField] GameObject panelInformacion; // Referencia al panel de información
    [SerializeField] TextMeshProUGUI textoInformativo; // Referencia al texto informativo
    [SerializeField] GameObject botonAyuda; // Referencia al botón azul de ayuda

    private string nivelAIniciar;

    private void Start()
    {

        {
            // Verifica si el botón de ayuda está asignado
            if (botonAyuda != null)
            {
                // Desactiva el botón de ayuda al inicio en la escena de selección de nivel
                botonAyuda.SetActive(false);
            }

            // Verifica si los objetos están asignados en el Inspector
            if (candado == null) Debug.LogError("Candado no está asignado en el inspector.");
            if (panelInformacion == null) Debug.LogError("Panel de información no está asignado en el inspector.");
            if (textoInformativo == null) Debug.LogError("Texto informativo no está asignado en el inspector.");

            // Verifica si los niveles previos están superados
            if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                candado.SetActive(false);
            }

            // Desactiva el panel de información al inicio
            panelInformacion.SetActive(false);
        }

        if (candado == null) Debug.LogError("Candado no está asignado en el inspector.");
        if (panelInformacion == null) Debug.LogError("Panel de información no está asignado en el inspector.");
        if (textoInformativo == null) Debug.LogError("Texto informativo no está asignado en el inspector.");

        if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
        {
            candado.SetActive(false);
        }

        // Desactivar el panel de información al inicio
        panelInformacion.SetActive(false);
    }

    public void CambiarNivel(string nivel)
    {
        if (nivel == "Nivel_3_Evaluacion")
        {
            if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                Carga_Nivel.Nivel_A_Cargar(nivel);
            }
            else
            {
                MostrarInformacionNivel(nivel); // Mostrar información si no se ha desbloqueado
            }
        }
        else
        {
            Carga_Nivel.Nivel_A_Cargar(nivel);
        }

        if (nivel == "Nivel_1_Conceptos")
        {
            GameManager.instance.indexTextoAntagonista = 1;
        }
    }

    public void MostrarInformacionNivel(string nivel)
    {
        nivelAIniciar = nivel;

        if (nivel == "Nivel_1_Conceptos")
        {
            textoInformativo.text = "Bienvenido! Has tomado la decisión correcta al comenzar con este nivel." +
                " Aquí se verán conceptos fundamentales sobre genética mendeliana, los cuales te ayudarán a resolver los ejercicios del siguiente nivel y" +
                " te prepararán para el test final de evaluación." +
                "\r\n\r\nObjetivo del Nivel:" +
                "\r\n  - Recolectar 10 pergaminos que se encuentran dispersos en el mapa." +
                "\r\n  - Leer el contenido de cada pergamino." +
                "\r\n  - Eliminar a los enemigos que encuentres en el camino." +
                "\r\n\r\nContarás con un total de ocho vidas, las cuales se verán reflejadas en la barra de vida. \r\n\r\n\r\n¡Buena suerte!";
        }
        else if (nivel == "Nivel_2_Ejercicios")
        {
            textoInformativo.text = "Bienvenido al nivel de ejercicios! En este nivel encontrarás una serie de ejercicios que desafiarán tus conocimientos sobre Genética Mendeliana." +
                "\r\n\r\nRecomendación:" +
                "\r\nPara comenzar este nivel, sugiero haber terminado el nivel de conceptos, ya que ahí encontrarás conceptos fundamentales que te ayudarán a resolver estos ejercicios." +
                " Si ya has pasado el nivel de conceptos, ¡felicidades! Estás preparado para este desafío.\r\n\r\nObjetivo del Nivel:\r\n  - Responder correctamente los 10 ejercicios que se encuentran en el mapa." +
                "\r\n  - Completar 5 ejercicios de selección múltiple.\r\n  - Resolver 5 ejercicios interactivos de la matriz de Punnett.\r\n  - Recuerda no dejarte eliminar por los enemigos. \r\n\r\n¡Buena suerte!";
        }
        else if (nivel == "Nivel_3_Evaluacion")
        {
            if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                textoInformativo.text = "¡Felicidades, has llegado al nivel final! Tu objetivo es demostrar lo aprendido en los niveles anteriores (conceptos y ejercicios) respondiendo correctamente las preguntas." +
                    "\r\n\r\nEste nivel es una trivia donde:\r\n\r\nDebes responder correctamente para eliminar al enemigo." +
                    "\r\n  - Si respondes incorrectamente, el enemigo te quita vida." +
                    "\r\n  - Tienes 3 oportunidades de equivocarte. Si fallas más de 3 veces, repetirás el nivel." +
                    "\r\n  - Para superar el nivel, necesitas una nota mínima de 7." +
                    "\r\n\r\nBuena suerte, ¡demuestra todo lo que has aprendido!";
            }
            else
            {
                textoInformativo.text = "Lo sentimos, pero no puedes acceder a este nivel aún. Para desbloquear el nivel de evaluación, primero debes completar los niveles de Conceptos y Ejercicios." +
                    "\r\n\r\n  - El nivel de Conceptos te proporcionará los conocimientos fundamentales sobre genética mendeliana." +
                    "\r\n  - El nivel de Ejercicios te permitirá aplicar estos conocimientos a través de una serie de desafíos." +
                    "\r\n\r\nSolo después de superar estos dos niveles estarás preparado para afrontar la evaluación final. \r\n\r\n¡Buena suerte!";
            }
        }


        panelInformacion.SetActive(true);
    }

    public void IniciarNivel()
    {
        if (nivelAIniciar == "Nivel_3_Evaluacion")
        {
            if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                CargarNivel(nivelAIniciar);
            }
            else
            {
                Debug.Log("Debes completar los niveles de Conceptos y Ejercicios primero.");
            }
        }
        else
        {
            CargarNivel(nivelAIniciar);
        }
    }

    void CargarNivel(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    public void CerrarPanelInformacion()
    {
        panelInformacion.SetActive(false);
    }
}