using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matraz : MonoBehaviour
{
    [SerializeField] GameObject explocionParticulas;
    private bool exploto = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!exploto)
        {
            exploto = true;
            Instantiate(explocionParticulas, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
