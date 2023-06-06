using UnityEngine;

/// <summary>
/// Class for the OptionsMenu
/// </summary>

public class OptionsMenu : MonoBehaviour
{
    private bool isFullScreen = false;

    private void Start()
    {
  
        isFullScreen = Screen.fullScreenMode == FullScreenMode.FullScreenWindow ? true : false;
 
    }
    /// <summary>
    /// Toggle Screen mode
    /// </summary>
    public void ChangeScreenMode()
    {
        SoundManager.Instance.PlayButtonSound();
        isFullScreen = !isFullScreen;
        var fullsScreenMode = isFullScreen? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        //TODO: TP2 - Syntax - Fix formatting
        Screen.SetResolution(1920, 1080,fullsScreenMode );
    }
    /// <summary>
    /// Toggle SFX mute state
    /// </summary>
    public void ChangeSFX()
    {
        //BUG: Could it be that the button sound will never play because it'll be muted immediately?
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
