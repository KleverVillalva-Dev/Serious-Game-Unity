using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplocionMatraz : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Virus"))
        {
            string sc = SceneManager.GetActiveScene().name;
            if (sc == "Nivel_1_Conceptos")
            {
                Nivel_Conceptos_Manager.Instance.virusMatados++;
            }
            else if( sc == "Nivel_2_Ejercicios")
            {
                Nivel_Ejercicio_Manager.Instance.virusMatados++;
            }
            
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
