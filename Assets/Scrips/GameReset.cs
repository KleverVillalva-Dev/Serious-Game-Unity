using UnityEngine;

public class GameReset : MonoBehaviour
{
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey("Conceptos");
        PlayerPrefs.DeleteKey("Ejercicios");
        PlayerPrefs.DeleteKey("Evaluacion");
        PlayerPrefs.Save();
    }
}
