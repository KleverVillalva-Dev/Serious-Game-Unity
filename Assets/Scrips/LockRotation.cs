using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Guarda la rotaci�n inicial del objeto hijo
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Mant�n la rotaci�n inicial del objeto hijo
        transform.rotation = initialRotation;
    }
}