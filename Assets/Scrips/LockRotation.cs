using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Guarda la rotación inicial del objeto hijo
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Mantén la rotación inicial del objeto hijo
        transform.rotation = initialRotation;
    }
}