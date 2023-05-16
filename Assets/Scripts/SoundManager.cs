using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] public AudioClip button;


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

    public void PlaySound(AudioClip clip,float volume = 1.0f)
    {
        volume = Mathf.Clamp01(volume);
        effectSource.PlayOneShot(clip,volume);
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }
    public void ToggleAudio()
    {
        effectSource.mute = !effectSource.mute;
        musicSource.mute = !musicSource.mute;
        PlaySound(button);
    }

}
