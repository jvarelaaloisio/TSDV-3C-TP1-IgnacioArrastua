using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the SlideMenu
/// </summary>
public class SlideMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private GameObject currentbutton;
    [SerializeField] private PopUpText screen;
    private int playerScore;
    [SerializeField] private CanvasGroup screenCanvas;
    private bool isActive;
    [SerializeField] private AudioClip openSlideSound;
    [SerializeField] private  float timeUntilNextScene = 0.5f;
    [SerializeField] private string menuSceneName = "MainMenu";
    private void Awake()
    {
        screenCanvas = GetComponent<CanvasGroup>();
    }

    private void Start()
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
        playerScore = LevelController.score;
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
    }
    /// <summary>
    /// Resets the current scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void ResetGame()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(ResetScene), timeUntilNextScene);
    }
    /// <summary>
    /// Loads the next scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void ContinueToNextGame()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(LoadNextScene), timeUntilNextScene);
    }
    /// <summary>
    /// Close this screen
    /// </summary>
    public void ReturnToGame()
    {
        isActive = false;
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        screen.DeactivateBox();
    }
    /// <summary>
    /// Loads MenuScene
    /// </summary>
    private void LoadMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(menuSceneName);
    }
    /// <summary>
    /// Loads next Scene
    /// </summary>
    private void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(nextScene);
    }
    /// <summary>
    /// Reset CurrentScene
    /// </summary>
    private void ResetScene()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
