using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplocionMatraz : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Virus"))
        {
            Nivel_Conceptos_Manager.Instance.virusMatados ++;
            Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(Desactivar());
    }

    IEnumerator Desactivar()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
