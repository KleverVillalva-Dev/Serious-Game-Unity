using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 posInicial;
    string textoObjeto;
    TextMeshProUGUI tmpObjeto;

    private void Awake()
    {
        tmpObjeto = GetComponentInChildren<TextMeshProUGUI>();
        textoObjeto = tmpObjeto.text;
        
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        posInicial = rectTransform.anchoredPosition; // Almacena la posici�n inicial del objeto
    }

    //Funcion evento cuado se comienza a arrastrar.
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    //Funcion "Mientras arrastramos"
    public void OnDrag(PointerEventData eventData)
    {
        // Mueve el objeto de acuerdo al arrastre del puntero
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //Funcion de logica cuando soltamos el objeto
    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaurar la configuraci�n al soltar el objeto
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        // Detectar el objeto bajo el puntero al soltar
        GameObject objectUnderPointer = eventData.pointerCurrentRaycast.gameObject;

        if (objectUnderPointer != null && objectUnderPointer.GetComponent<TextMeshProUGUI>() != null)
        {
            // Si el objeto es un TextMeshPro, cambia su texto
            TextMeshProUGUI textMeshPro = objectUnderPointer.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = textoObjeto;
        }

        // Volver a la posici�n inicial
        rectTransform.anchoredPosition = posInicial;
    }
}

