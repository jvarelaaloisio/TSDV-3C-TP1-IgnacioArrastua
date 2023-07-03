using UnityEngine;
using static UnityEngine.Screen;

/// <summary>
/// Class for the OptionsMenu
/// </summary>

public class OptionsMenu : MonoBehaviour
{
    private bool isFullScreen = false;
    private void Start()
    {

        isFullScreen = fullScreenMode == FullScreenMode.FullScreenWindow;

    }
    /// <summary>
    /// Toggle Screen mode
    /// </summary>
    public void ChangeScreenMode()
    {
        SoundManager.Instance.PlayButtonSound();
        isFullScreen = !isFullScreen;
        var fullsScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        SetResolution(1920, 1080, fullsScreenMode);
    }
    /// <summary>
    /// Toggle SFX mute state
    /// </summary>
    public void ChangeSFX()
    {
        SoundManager.Instance.PlayButtonSound();
        SoundManager.Instance.ToggleEffects();
    }
    /// <summary>
    /// Toggle Music mute state
    /// </summary>
    public void ChangeMusic()
    {
        SoundManager.Instance.PlayButtonSound();
        SoundManager.Instance.ToggleMusic();
    }
}
