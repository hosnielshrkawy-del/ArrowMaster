using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;

    [SerializeField] private AudioClip levelCompleteClip;
    [SerializeField] private AudioClip arrowMoveClip;
    [SerializeField] private AudioClip errorClip;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent<AudioManager>();
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
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (SettingsManager.Instance.IsSoundEnabled() && clip != null)
        {
            audioSource.PlayOneShot(clip, SettingsManager.Instance.GetMasterVolume());
        }
    }

    public void PlayLevelCompleteSound()
    {
        PlaySoundEffect(levelCompleteClip);
    }

    public void PlayArrowMoveSound()
    {
        PlaySoundEffect(arrowMoveClip);
    }

    public void PlayErrorSound()
    {
        PlaySoundEffect(errorClip);
    }
}
