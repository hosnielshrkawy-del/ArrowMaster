using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance;

    private bool soundEnabled = true;
    private float masterVolume = 0.8f;
    private string visualTheme = "Modern";

    public static SettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SettingsManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SettingsManager");
                    instance = go.AddComponent<SettingsManager>();
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
            LoadSettings();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void LoadSettings()
    {
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        visualTheme = PlayerPrefs.GetString("VisualTheme", "Modern");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetString("VisualTheme", visualTheme);
        PlayerPrefs.Save();
    }

    public bool IsSoundEnabled() => soundEnabled;
    public void SetSoundEnabled(bool enabled) { soundEnabled = enabled; }

    public float GetMasterVolume() => masterVolume;
    public void SetMasterVolume(float volume) { masterVolume = Mathf.Clamp01(volume); }

    public string GetVisualTheme() => visualTheme;
    public void SetVisualTheme(string theme) { visualTheme = theme; }
}
