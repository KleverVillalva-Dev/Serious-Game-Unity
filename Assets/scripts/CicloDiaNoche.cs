using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CicloDiaNoche : MonoBehaviour
{
    [SerializeField] private float duracionDiaNoche = 60f;
    [SerializeField] private Light luzDiurna;
    [SerializeField] private GameObject lucesCalle;
    [SerializeField] private GameObject[] lucesCamioneta;
    public bool dia = true;

    private void Start()
    {
        InvokeRepeating("AlternarDiaNoche", duracionDiaNoche, duracionDiaNoche);
    }

    private void AlternarDiaNoche()
    {
        if (dia)
        {
            StartCoroutine(NocheCoroutine());
            AlternarEstadoLuces();
            AlternarEstadoLucesCamioneta();
        }
        else
        {         
            StartCoroutine(DiaCoroutine());
            AlternarEstadoLuces();
            AlternarEstadoLucesCamioneta();
        }
    }
    IEnumerator NocheCoroutine()
    {
        float umbral = 0.01f; 

        while (luzDiurna.intensity > umbral)
        {
            luzDiurna.intensity = Mathf.Lerp(luzDiurna.intensity, 0, Time.deltaTime / 1);
            yield return new WaitForSeconds(0.01f);
        }
        dia = false;
    }
    IEnumerator DiaCoroutine()
    {
        float umbral = 2.4f;

        while (luzDiurna.intensity < umbral)
        {
            luzDiurna.intensity = Mathf.Lerp(luzDiurna.intensity, 2.5f, Time.deltaTime / 1);
            yield return new WaitForSeconds(0.01f);
        }
        dia = true;
    }


    private void AlternarEstadoLuces()
    {
        StartCoroutine(AlternarELCo());
    }
    IEnumerator AlternarELCo()
    {
        yield return new WaitForSeconds(2f);
        lucesCalle.SetActive(!lucesCalle.activeSelf);
    }

    private void AlternarEstadoLucesCamioneta()
    {
        for (int i = 0; i < lucesCamioneta.Length; i++)
        {
            lucesCamioneta[i].SetActive(!lucesCamioneta[i].activeSelf);
        }
    }
}
