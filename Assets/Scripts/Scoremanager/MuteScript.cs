using UnityEngine;

public class MuteUnmuteScript : MonoBehaviour
{
    private static MuteUnmuteScript instance;
    private bool isMuted = false;
    private AudioSource audioSource;

    private const string MuteKey = "IsMuted";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject audioSourceObject = GameObject.FindGameObjectWithTag("GameMusic");

        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = audioSourceObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'GameMusic' found.");
        }
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        ApplyMuteState();
        DontDestroyOnLoad(gameObject);
    }

    void ApplyMuteState()
    {
        if (audioSource != null)
        {
            audioSource.mute = isMuted;
            Debug.Log("Audio " + (isMuted ? "Muted" : "Unmuted"));
        }
    }
    void Start()
    {
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        ApplyMuteState();
    }

    void MuteAudio()
    {
        isMuted = true;
        ApplyMuteState();
        PlayerPrefs.SetInt(MuteKey, 1);
        PlayerPrefs.Save();
    }

    void UnmuteAudio()
    {
        isMuted = false;
        ApplyMuteState();
        PlayerPrefs.SetInt(MuteKey, 0);
        PlayerPrefs.Save();
    }
    public void ToggleMuteUnmute()
    {
        if (isMuted)
            UnmuteAudio();
        else
            MuteAudio();
    }
}
