using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowContainer;

    private LevelData currentLevelData;
    private Arrow[] currentLevelArrows;

    public void LoadLevel(int levelIndex)
    {
        string levelFilePath = Path.Combine(Application.streamingAssetsPath, "Levels", "level_" + (levelIndex + 1).ToString("D3") + ".json");

        if (!File.Exists(levelFilePath))
        {
            Debug.LogError("Level file not found: " + levelFilePath);
            return;
        }

        string jsonContent = File.ReadAllText(levelFilePath);
        currentLevelData = JsonUtility.FromJson<LevelData>(jsonContent);

        InstantiateArrows();
    }

    private void InstantiateArrows()
    {
        if (arrowContainer != null)
        {
            foreach (Transform child in arrowContainer)
            {
                Destroy(child.gameObject);
            }
        }

        currentLevelArrows = new Arrow[currentLevelData.moves.Length];

        for (int i = 0; i < currentLevelData.moves.Length; i++)
        {
            GameObject arrowGO = Instantiate(arrowPrefab, arrowContainer);
            Arrow arrow = arrowGO.GetComponent<Arrow>();

            Vector2 spawnPos = new Vector2(i * 2f, 0f);
            arrow.Initialize(i, spawnPos, currentLevelData.moves[i]);

            currentLevelArrows[i] = arrow;
        }

        Debug.Log("Level " + currentLevelData.levelId + " loaded with " + currentLevelArrows.Length + " arrows.");
    }

    public LevelData GetCurrentLevelData() => currentLevelData;
    public Arrow[] GetCurrentLevelArrows() => currentLevelArrows;
}
