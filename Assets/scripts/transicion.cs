using Cinemachine;
using UnityEngine;

public class transicion : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    public void Start()
    {
        currentCamera.Priority++;
    }

    // Update is called once per frame
    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;

    }
}
