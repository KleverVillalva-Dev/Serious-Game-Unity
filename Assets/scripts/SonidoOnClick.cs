using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoOnClick : MonoBehaviour
{
    [SerializeField] AudioClip onClik;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ReproduciorSonidoOnClick()
    {
        audioSource.PlayOneShot(onClik);
    }


}
