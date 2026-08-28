using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] private int hintsRemaining = 3;
    [SerializeField] private bool isAdRemovalPurchased = false;

    private List<LevelData> levels;
    private SaveSystem saveSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        saveSystem = GetComponent<SaveSystem>();
        if (saveSystem == null)
        {
            saveSystem = gameObject.AddComponent<SaveSystem>();
        }

        LoadGameData();
        LoadLevels();
    }

    public void LoadGameData()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        hintsRemaining = PlayerPrefs.GetInt("HintsRemaining", 3);
        isAdRemovalPurchased = PlayerPrefs.GetInt("IsAdRemovalPurchased", 0) == 1;
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
        PlayerPrefs.SetInt("HintsRemaining", hintsRemaining);
        PlayerPrefs.SetInt("IsAdRemovalPurchased", isAdRemovalPurchased ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadLevels()
    {
        levels = new List<LevelData>();
        string levelsPath = Path.Combine(Application.streamingAssetsPath, "Levels");

        if (Directory.Exists(levelsPath))
        {
            string[] levelFiles = Directory.GetFiles(levelsPath, "*.json");
            foreach (string file in levelFiles)
            {
                string json = File.ReadAllText(file);
                LevelData level = JsonUtility.FromJson<LevelData>(json);
                levels.Add(level);
            }
        }
    }

    public LevelData GetCurrentLevel()
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < levels.Count)
        {
            return levels[currentLevelIndex];
        }
        return null;
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        SaveGameData();
    }

    public void UseHint()
    {
        if (hintsRemaining > 0)
        {
            hintsRemaining--;
            SaveGameData();
        }
    }

    public void PurchaseAdRemoval()
    {
        isAdRemovalPurchased = true;
        SaveGameData();
    }

    public int GetHintsRemaining() => hintsRemaining;
    public bool IsAdRemovalActive() => isAdRemovalPurchased;
    public int GetCurrentLevelIndex() => currentLevelIndex;
}
