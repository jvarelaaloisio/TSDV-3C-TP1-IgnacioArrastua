using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenUrl(string Link)
    {
        SoundManager.Instance.PlayButtonSound();
        Application.OpenURL(Link);
    }
}
