using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static void SetLevelCompleted(string levelName)
    {
        PlayerPrefs.SetInt(levelName, 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelCompleted(string levelName)
    {
        return PlayerPrefs.GetInt(levelName, 0) == 1;
    }
}
