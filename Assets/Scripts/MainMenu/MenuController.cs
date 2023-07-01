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
    [SerializeField] private List<CanvasGroup> totalCanvas;
    //TODO: TP2 - Syntax - Consistency in naming convention
    private CanvasGroup currentCanvas;

    private void Start()
    {
        currentCanvas = MainMenu;

        SetCanvasState(MainMenu, true);
        SetCanvasState(Options, false);
        SetCanvasState(Credits, false);
        SoundManager.Instance.GetMusicSource().Stop();

        SoundManager.Instance.GetMusicSource().clip = SoundManager.Instance.mainMenu;
        SoundManager.Instance.GetMusicSource().Play();
    }

    /// <summary>
    /// Loads GameScene
    /// </summary>
    public void GoToGame()
    {
        //TODO - Fix - Hardcoded value - Receive as param
        SceneManager.LoadScene("Level1");
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Loads TutorialScene
    /// </summary>
    public void GoToTutorial()
    {
        //TODO - Fix - Hardcoded value - Receive as param
        SceneManager.LoadScene("Tutorial");
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Activate OptionsCanvas
    /// </summary>
    public void GoToOptions()
    {
        SetCanvasState(currentCanvas, false);
        SetCanvasState(Options, true);
        currentCanvas = Options;
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Activate CreditsCanvas
    /// </summary>
    public void GoToCredits()
    {
        SetCanvasState(currentCanvas, false);
        SetCanvasState(Credits, true);
        currentCanvas = Credits;
        SoundManager.Instance.PlayButtonSound();
    }
    /// <summary>
    /// Deactivate currentCanvas and goes Back To Menu
    /// </summary>
    public void GoBackToMenu()
    {
        SetCanvasState(currentCanvas, false);
        SetCanvasState(MainMenu, true);
        currentCanvas = MainMenu;
        SoundManager.Instance.PlayButtonSound();
    }
    //TODO: TP2 - Syntax - Fix formatting
    /// <summary>
    /// Exits the game
    /// If activate in Unity Editor exits playmode
    /// </summary>

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
