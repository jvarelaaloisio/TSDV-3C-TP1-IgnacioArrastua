using UnityEngine;
/// <summary>
/// Class for the OpenLink
/// </summary>
public class OpenLink : MonoBehaviour
{
    /// <summary>
    /// Open an Link
    /// </summary>
    /// <param name="Link">WebLink to open</param>
    public void OpenUrl(string Link)
    {
        SoundManager.Instance.PlayButtonSound();
        Application.OpenURL(Link);
    }
}
