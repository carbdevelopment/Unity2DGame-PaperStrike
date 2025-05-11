using UnityEngine;

public class AudioClipControl : MonoBehaviour
{
    public GameObject audioSourcePrefab; 
    private bool isMuted = false;
    private AudioSource audioSource;

    void Start()
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        audioSource = audioSourceObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = audioSourceObject.AddComponent<AudioSource>();
        }
    }

    
   public void ToggleMute()
    {
        if (isMuted)
        {
            audioSource.volume = 1f;
            Debug.Log("Audio is unmuted.");
        }
        else
        {
            audioSource.volume = 0f;
            Debug.Log("Audio is muted.");
        }
        isMuted = !isMuted;
    }
}
