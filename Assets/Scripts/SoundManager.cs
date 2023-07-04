using UnityEngine;

/// <summary>
/// Class for the SoundManager
/// </summary>
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
    /// <summary>
    /// Plays a <paramref name="clip"/> with PlayOneShot
    /// </summary>
    /// <param name="clip">The audio clip that will play.</param>
    /// <param name="volume">Sets the volume of the clip between 0-1f, if null will play at max volume</param>
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        effectSource.PlayOneShot(clip, volume);
    }
    /// <summary>
    /// Plays the button sound
    /// </summary>
    public void PlayButtonSound()
    {
        effectSource.PlayOneShot(button);
    }
    /// <summary>
    /// Plays the score sound
    /// Value can be change in Object
    /// </summary>
    public void PlaySoundScore()
    {
        effectSource.PlayOneShot(scoreSound, scoreVolume);
    }
    /// <summary>
    /// Gets the Music AudioSource
    /// </summary>
    public AudioSource GetMusicSource()
    {
        return musicSource;
    }
    /// <summary>
    /// Toggle the mute in the Effects AudioSource
    /// </summary>
    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }
    /// <summary>
    /// Toggle the mute in the Music AudioSource
    /// </summary>
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

}