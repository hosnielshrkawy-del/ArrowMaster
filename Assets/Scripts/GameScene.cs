using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private HintSystem hintSystem;
    [SerializeField] private InputHandler inputHandler;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (levelManager == null) levelManager = GetComponent<LevelManager>();
        if (uiManager == null) uiManager = GetComponent<UIManager>();
        if (hintSystem == null) hintSystem = GetComponent<HintSystem>();
        if (inputHandler == null) inputHandler = GetComponent<InputHandler>();

        InitializeScene();
    }

    private void InitializeScene()
    {
        int currentLevelIndex = gameManager.GetCurrentLevelIndex();
        levelManager.LoadLevel(currentLevelIndex);

        LevelData levelData = levelManager.GetCurrentLevelData();
        if (levelData != null)
        {
            hintSystem.GenerateHintsForLevel(levelData);
        }

        uiManager.UpdateUI();
        Debug.Log("Game scene initialized for level " + (currentLevelIndex + 1));
    }

    public void OnLevelComplete()
    {
        gameManager.CompleteLevel();
        MonetizationManager.Instance.OnLevelCompleted();

        if (MonetizationManager.Instance.ShouldShowAd())
        {
            ShowInterstitialAd();
            MonetizationManager.Instance.ResetAdCounter();
        }

        AudioManager.Instance.PlayLevelCompleteSound();
        Debug.Log("Level completed!");
    }

    private void ShowInterstitialAd()
    {
        Debug.Log("Showing interstitial ad...");
    }

    public void OnUseHint()
    {
        gameManager.UseHint();
        hintSystem.DisplayNextHint();
        uiManager.UpdateUI();
    }

    public void OnSkipLevel()
    {
        gameManager.SkipLevel();
        Debug.Log("Level skipped!");
    }
}
