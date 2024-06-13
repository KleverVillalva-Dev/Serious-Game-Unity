using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Desactivar : MonoBehaviour
{
    [SerializeField] private float segundosAntesDeDesactivar;
    void Update()
    {
        StartCoroutine(Desactivando());
    }
    IEnumerator Desactivando()
    {
        yield return new WaitForSeconds(segundosAntesDeDesactivar);
        if (gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
