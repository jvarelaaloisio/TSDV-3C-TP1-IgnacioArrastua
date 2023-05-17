using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private bool isFullScreen = false;

    private void Start()
    {
        isFullScreen = false;
        Screen.SetResolution(1920,1080, FullScreenMode.Windowed);
    }

    public void ChangeScreenMode()
    {
        SoundManager.Instance.PlayButtonSound();
        isFullScreen = !isFullScreen;
        var fullsScreenMode = isFullScreen? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(1920, 1080,fullsScreenMode );
    }
    public void ChangeSFX()
    {
        SoundManager.Instance.PlayButtonSound();
        SoundManager.Instance.ToggleEffects();
    }
    public void ChangeMusic()
    {
        SoundManager.Instance.PlayButtonSound();
        SoundManager.Instance.ToggleMusic();
    }
}
