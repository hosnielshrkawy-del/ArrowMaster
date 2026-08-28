using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "gamesave.json");
    }

    public void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }

        return new PlayerData();
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int totalScore = 0;
    public int levelsCompleted = 0;
    public int hintsUsed = 0;
    public bool soundEnabled = true;
    public float masterVolume = 1f;
}
