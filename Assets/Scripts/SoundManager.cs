
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] public AudioClip button;
    [SerializeField] public AudioClip mainMenu;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] [Range(0, 1)] private float scoreVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        effectSource.PlayOneShot(clip,volume);
    }
    public void PlayButtonSound()
    {
        effectSource.PlayOneShot(button);
    }

    public void PlaySoundScore()
    {
        effectSource.PlayOneShot(scoreSound, scoreVolume);
    }
    public AudioSource GetMusicSource()
    {
        return musicSource;
    }
    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

}
