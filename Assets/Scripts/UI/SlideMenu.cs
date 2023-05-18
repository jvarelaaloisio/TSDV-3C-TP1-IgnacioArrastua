using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the SlideMenu
/// </summary>
public class SlideMenu : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private GameObject currentbutton;
    [SerializeField] private PopUpText screen;
    private int playerScore;
    [SerializeField] private CanvasGroup screenCanvas;
    private bool isActive;
    private static MoveCrosshair cross;
    [SerializeField] private AudioClip openSlideSound;
    private void Awake()
    {
        screenCanvas = GetComponent<CanvasGroup>();
        cross = GameObject.Find("CrossHair").GetComponent<MoveCrosshair>();
    }

    void Start()
    {
        screenCanvas.interactable = false;
        screenCanvas.blocksRaycasts = false;
        isActive = false;
    }


    /// <summary>
    /// Activates the PopUpText and activates the CanvasGroup
    /// </summary>
    public void OpenSlide()
    {
        if (isActive) return;
        if (this.CompareTag("EndScreen"))
        {
            GetComponentInParent<UIController>().EndScreenSecuence();
        }
        SoundManager.Instance.PlaySound(openSlideSound);
        isActive = true;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(currentbutton);
        screen.ActiveBox();
        playerScore = PlayerController.Score;
        if (textMesh)
        {
            textMesh.text = "Score:" + playerScore;
        }
        screenCanvas.interactable = true;
        screenCanvas.alpha = 1;
        screenCanvas.blocksRaycasts = true;
    }
    /// <summary>
    /// Loads the menu scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void GoBackToMenu()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(LoadMenu), 0.5f);
        cross.ResetCrosshair();
    }
    /// <summary>
    /// Resets the current scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void ResetGame()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(ResetScene), 0.5f);
        cross.ResetCrosshair();
    }
    /// <summary>
    /// Loads the next scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void ContinueToNextGame()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(LoadNextScene), 0.5f);
        cross.ResetCrosshair();
    }
    /// <summary>
    /// Close this screen
    /// </summary>
    public void ReturnToGame()
    {
        isActive = false;
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        Debug.Log("Return");
        screen.DeactivateBox();
    }
    /// <summary>
    /// Loads MenuScene
    /// </summary>
    private void LoadMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene("MainMenu");
        cross.ResetCrosshair();
    }
    /// <summary>
    /// Loads next Scene
    /// </summary>
    private void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(nextScene);
        cross.ResetCrosshair();
    }
    /// <summary>
    /// Reset CurrentScene
    /// </summary>
    private void ResetScene()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cross.ResetCrosshair();
    }
}
