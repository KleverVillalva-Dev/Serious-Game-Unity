using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] public GameObject pergaminoUi;
    [SerializeField] public TextMeshProUGUI pergaminoTitulo;
    [SerializeField] public TextMeshProUGUI pergaminoDescripcion;
}
