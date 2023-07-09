using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    GameObject singletonAudioManager = new GameObject(typeof(AudioManager).Name);
                    instance = singletonAudioManager.AddComponent<AudioManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private AudioSource musicSource, soundSource;

    public void PlayMusic(AudioClip music)
    {
        musicSource.PlayOneShot(music);
    }
    
    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
}
