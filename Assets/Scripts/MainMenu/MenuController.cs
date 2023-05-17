using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup MainMenu;
    [SerializeField] private CanvasGroup Options;
    [SerializeField] private CanvasGroup Credits;
    [SerializeField] private List<CanvasGroup> totalCanvas;
    private CanvasGroup currentCanvas;
    void Start()
    {
        currentCanvas = MainMenu;

        SetCanvasState(MainMenu, true);
        SetCanvasState(Options, false);
        SetCanvasState(Credits, false);
        SoundManager.Instance.GetMusicSource().Stop();

        SoundManager.Instance.GetMusicSource().clip = SoundManager.Instance.mainMenu;
        SoundManager.Instance.GetMusicSource().Play();
    }


    public void GoToGame()
    {
        SceneManager.LoadScene("Level1");
        SoundManager.Instance.PlayButtonSound();
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        SoundManager.Instance.PlayButtonSound();
    }
    public void GoToOptions()
    {
        SetCanvasState(currentCanvas, false);
        SetCanvasState(Options, true);
        currentCanvas = Options;
        SoundManager.Instance.PlayButtonSound();
    }
    public void GoToCredits()
    {
        SetCanvasState(currentCanvas, false);
        SetCanvasState(Credits, true);
        currentCanvas = Credits;
        SoundManager.Instance.PlayButtonSound();
    }
    public void GoBackToMenu()
    {
        SetCanvasState(currentCanvas, false);
        SetCanvasState(MainMenu, true);
        currentCanvas = MainMenu;
        SoundManager.Instance.PlayButtonSound();
    }

    public void ExitAplication()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        Debug.Log("Quit!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
   	    Application.Quit();
#endif
    }
    private void SetCanvasState(CanvasGroup canvas, bool state)
    {
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
        canvas.alpha = state ? 1 : 0;
    }
}
