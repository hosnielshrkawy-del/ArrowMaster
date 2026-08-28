using UnityEngine;
using System.Collections.Generic;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private float hintDisplayDuration = 3f;
    [SerializeField] private Color hintHighlightColor = Color.yellow;

    private Queue<int> hintSequence = new Queue<int>();
    private int currentHintIndex = 0;
    private bool isDisplayingHint = false;

    public void GenerateHintsForLevel(LevelData levelData)
    {
        hintSequence.Clear();
        currentHintIndex = 0;

        if (levelData != null && levelData.moves != null && levelData.moves.Length > 0)
        {
            for (int i = 0; i < levelData.moves.Length; i++)
            {
                hintSequence.Enqueue(i);
            }
        }
    }

    public void DisplayNextHint()
    {
        if (hintSequence.Count == 0)
        {
            Debug.LogWarning("No more hints available for this level.");
            return;
        }

        int nextHintArrowId = hintSequence.Dequeue();
        StartCoroutine(ShowHintCoroutine(nextHintArrowId));
    }

    private System.Collections.IEnumerator ShowHintCoroutine(int arrowId)
    {
        isDisplayingHint = true;

        Arrow targetArrow = FindArrowById(arrowId);
        if (targetArrow != null)
        {
            targetArrow.SetHighlight(true, hintHighlightColor);
        }

        yield return new WaitForSeconds(hintDisplayDuration);

        if (targetArrow != null)
        {
            targetArrow.SetHighlight(false, Color.white);
        }

        isDisplayingHint = false;
    }

    private Arrow FindArrowById(int id)
    {
        Arrow[] allArrows = FindObjectsOfType<Arrow>();
        foreach (Arrow arrow in allArrows)
        {
            if (arrow.id == id)
            {
                return arrow;
            }
        }
        return null;
    }

    public int GetRemainingHints() => hintSequence.Count;
    public bool IsDisplayingHint() => isDisplayingHint;
}

[System.Serializable]
public class LevelData
{
    public int levelId;
    public string levelName;
    public int difficulty;
    public int[] moves;
    public int moveCount;
}
