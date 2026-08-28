using UnityEngine;

public class MonetizationManager : MonoBehaviour
{
    private static MonetizationManager instance;

    [SerializeField] private int adFrequency = 3;
    [SerializeField] private float adRemovalPrice = 2.99f;

    private int levelsPlayedSinceLastAd = 0;
    private bool isAdRemovalPurchased = false;

    public static MonetizationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonetizationManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("MonetizationManager");
                    instance = go.AddComponent<MonetizationManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMonetizationData();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void LoadMonetizationData()
    {
        isAdRemovalPurchased = PlayerPrefs.GetInt("AdRemovalPurchased", 0) == 1;
        levelsPlayedSinceLastAd = PlayerPrefs.GetInt("LevelsPlayedSinceLastAd", 0);
    }

    public void SaveMonetizationData()
    {
        PlayerPrefs.SetInt("AdRemovalPurchased", isAdRemovalPurchased ? 1 : 0);
        PlayerPrefs.SetInt("LevelsPlayedSinceLastAd", levelsPlayedSinceLastAd);
        PlayerPrefs.Save();
    }

    public bool ShouldShowAd()
    {
        if (isAdRemovalPurchased) return false;
        return levelsPlayedSinceLastAd >= adFrequency;
    }

    public void OnLevelCompleted()
    {
        levelsPlayedSinceLastAd++;
        SaveMonetizationData();
    }

    public void ResetAdCounter()
    {
        levelsPlayedSinceLastAd = 0;
        SaveMonetizationData();
    }

    public void PurchaseAdRemoval()
    {
        isAdRemovalPurchased = true;
        SaveMonetizationData();
        Debug.Log("Ad removal purchased!");
    }

    public bool IsAdRemovalPurchased() => isAdRemovalPurchased;
    public float GetAdRemovalPrice() => adRemovalPrice;
    public int GetAdFrequency() => adFrequency;
}
