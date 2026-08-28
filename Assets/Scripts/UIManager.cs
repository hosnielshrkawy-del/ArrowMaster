using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hintsText;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button pauseButton;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        UpdateUI();

        if (hintButton != null) hintButton.onClick.AddListener(OnHintButtonPressed);
        if (skipButton != null) skipButton.onClick.AddListener(OnSkipButtonPressed);
        if (pauseButton != null) pauseButton.onClick.AddListener(OnPauseButtonPressed);
    }

    public void UpdateUI()
    {
        if (gameManager == null) return;

        if (levelText != null)
            levelText.text = "Level " + (gameManager.GetCurrentLevelIndex() + 1);

        if (hintsText != null)
            hintsText.text = "Hints: " + gameManager.GetHintsRemaining();
    }

    private void OnHintButtonPressed()
    {
        if (gameManager.GetHintsRemaining() > 0)
        {
            gameManager.UseHint();
            UpdateUI();
            Debug.Log("Hint used!");
        }
        else
        {
            Debug.Log("No hints remaining.");
        }
    }

    private void OnSkipButtonPressed()
    {
        Debug.Log("Skip button pressed.");
    }

    private void OnPauseButtonPressed()
    {
        Time.timeScale = (Time.timeScale == 1f) ? 0f : 1f;
        Debug.Log("Game paused: " + (Time.timeScale == 0f));
    }
}
