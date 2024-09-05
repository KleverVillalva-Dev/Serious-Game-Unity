using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectorDeNivel : MonoBehaviour
{
    [SerializeField] GameObject candado;
    [SerializeField] GameObject panelInformacion; // Referencia al panel de informaci�n
    [SerializeField] TextMeshProUGUI textoInformativo; // Referencia al texto informativo
    [SerializeField] GameObject botonAyuda; // Referencia al bot�n azul de ayuda

    private string nivelAIniciar;

    private void Start()
    {

        {
            // Verifica si el bot�n de ayuda est� asignado
            if (botonAyuda != null)
            {
                // Desactiva el bot�n de ayuda al inicio en la escena de selecci�n de nivel
                botonAyuda.SetActive(false);
            }

            // Verifica si los objetos est�n asignados en el Inspector
            if (candado == null) Debug.LogError("Candado no est� asignado en el inspector.");
            if (panelInformacion == null) Debug.LogError("Panel de informaci�n no est� asignado en el inspector.");
            if (textoInformativo == null) Debug.LogError("Texto informativo no est� asignado en el inspector.");

            // Verifica si los niveles previos est�n superados
            if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                candado.SetActive(false);
            }

            // Desactiva el panel de informaci�n al inicio
            panelInformacion.SetActive(false);
        }

        if (candado == null) Debug.LogError("Candado no est� asignado en el inspector.");
        if (panelInformacion == null) Debug.LogError("Panel de informaci�n no est� asignado en el inspector.");
        if (textoInformativo == null) Debug.LogError("Texto informativo no est� asignado en el inspector.");

        if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
        {
            candado.SetActive(false);
        }

        // Desactivar el panel de informaci�n al inicio
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
                MostrarInformacionNivel(nivel); // Mostrar informaci�n si no se ha desbloqueado
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
            textoInformativo.text = "Bienvenido! Has tomado la decisi�n correcta al comenzar con este nivel." +
                " Aqu� se ver�n conceptos fundamentales sobre gen�tica mendeliana, los cuales te ayudar�n a resolver los ejercicios del siguiente nivel y" +
                " te preparar�n para el test final de evaluaci�n." +
                "\r\n\r\nObjetivo del Nivel:" +
                "\r\n  - Recolectar 10 pergaminos que se encuentran dispersos en el mapa." +
                "\r\n  - Leer el contenido de cada pergamino." +
                "\r\n  - Eliminar a los enemigos que encuentres en el camino." +
                "\r\n\r\nContar�s con un total de ocho vidas, las cuales se ver�n reflejadas en la barra de vida. \r\n\r\n\r\n�Buena suerte!";
        }
        else if (nivel == "Nivel_2_Ejercicios")
        {
            textoInformativo.text = "Bienvenido al nivel de ejercicios! En este nivel encontrar�s una serie de ejercicios que desafiar�n tus conocimientos sobre Gen�tica Mendeliana." +
                "\r\n\r\nRecomendaci�n:" +
                "\r\nPara comenzar este nivel, sugiero haber terminado el nivel de conceptos, ya que ah� encontrar�s conceptos fundamentales que te ayudar�n a resolver estos ejercicios." +
                " Si ya has pasado el nivel de conceptos, �felicidades! Est�s preparado para este desaf�o.\r\n\r\nObjetivo del Nivel:\r\n  - Responder correctamente los 10 ejercicios que se encuentran en el mapa." +
                "\r\n  - Completar 5 ejercicios de selecci�n m�ltiple.\r\n  - Resolver 5 ejercicios interactivos de la matriz de Punnett.\r\n  - Recuerda no dejarte eliminar por los enemigos. \r\n\r\n�Buena suerte!";
        }
        else if (nivel == "Nivel_3_Evaluacion")
        {
            if (GameManager.instance.nivel_Conceptos_Superado && GameManager.instance.nivel_Ejercicios_Superado)
            {
                textoInformativo.text = "�Felicidades, has llegado al nivel final! Tu objetivo es demostrar lo aprendido en los niveles anteriores (conceptos y ejercicios) respondiendo correctamente las preguntas." +
                    "\r\n\r\nEste nivel es una trivia donde:\r\n\r\nDebes responder correctamente para eliminar al enemigo." +
                    "\r\n  - Si respondes incorrectamente, el enemigo te quita vida." +
                    "\r\n  - Tienes 3 oportunidades de equivocarte. Si fallas m�s de 3 veces, repetir�s el nivel." +
                    "\r\n  - Para superar el nivel, necesitas una nota m�nima de 7." +
                    "\r\n\r\nBuena suerte, �demuestra todo lo que has aprendido!";
            }
            else
            {
                textoInformativo.text = "Lo sentimos, pero no puedes acceder a este nivel a�n. Para desbloquear el nivel de evaluaci�n, primero debes completar los niveles de Conceptos y Ejercicios." +
                    "\r\n\r\n  - El nivel de Conceptos te proporcionar� los conocimientos fundamentales sobre gen�tica mendeliana." +
                    "\r\n  - El nivel de Ejercicios te permitir� aplicar estos conocimientos a trav�s de una serie de desaf�os." +
                    "\r\n\r\nSolo despu�s de superar estos dos niveles estar�s preparado para afrontar la evaluaci�n final. \r\n\r\n�Buena suerte!";
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