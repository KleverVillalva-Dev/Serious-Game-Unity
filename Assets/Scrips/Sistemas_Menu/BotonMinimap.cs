using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonMinimap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool botonAPretado = false;
    [SerializeField] GameObject minimap;

    private void Update()
    {
        if (botonAPretado)
        {
            minimap.SetActive(true);
        }
        else
        {
            minimap.SetActive(false);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        botonAPretado = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        botonAPretado = false;
    }
}
