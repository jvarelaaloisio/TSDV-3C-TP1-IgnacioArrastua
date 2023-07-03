using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Class for the MenuController
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup MainMenu;
    [SerializeField] private CanvasGroup Options;
    [SerializeField] private CanvasGroup Credits;

    [SerializeField] private string firstSceneName = "Level1";
    [SerializeField] private string tutorialSceneName = "Tutorial";

    private CanvasGroup currentActiveCanvas;

    private void Start()
    {
        currentActiveCanvas = MainMenu;

        SetCanvasState(MainMenu, true);
        SetCanvasState(Options, false);
        SetCanvasState(Credits, false);
        SoundManager.Instance.GetMusicSource().Stop();
        SoundManager.Instance.GetMusicSource().clip = SoundManager.Instance.mainMenu;
        SoundManager.Instance.GetMusicSource().Play();
    }

    /// <summary>
    /// Loads First GameScene
    /// </summary>
    public void GoToGame()
    {
        SceneManager.LoadScene(firstSceneName);
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Loads TutorialScene
    /// </summary>
    public void GoToTutorial()
    {
        SceneManager.LoadScene(tutorialSceneName);
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Activate OptionsCanvas
    /// </summary>
    public void GoToOptions()
    {
        SetCanvasState(currentActiveCanvas, false);
        SetCanvasState(Options, true);
        currentActiveCanvas = Options;
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Activate CreditsCanvas
    /// </summary>
    public void GoToCredits()
    {
        SetCanvasState(currentActiveCanvas, false);
        SetCanvasState(Credits, true);
        currentActiveCanvas = Credits;
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Deactivate currentCanvas and goes Back To Menu
    /// </summary>
    public void GoBackToMenu()
    {
        SetCanvasState(currentActiveCanvas, false);
        SetCanvasState(MainMenu, true);
        currentActiveCanvas = MainMenu;
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Exits the game
    /// If activate in Unity Editor exits playmode
    /// </summary>
    public void ExitAplication()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
   	    Application.Quit();
#endif
    }
    /// <summary>
    /// Set the canvas interactable, block raycas and alpha according to the bool
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="state"></param>
    private void SetCanvasState(CanvasGroup canvas, bool state)
    {
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
        canvas.alpha = state ? 1 : 0;
    }
}
