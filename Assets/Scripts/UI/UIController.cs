using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private CanvasGroup InGameUi;
    [SerializeField] private CanvasGroup pauseUI;
    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }



    public void SetPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        isPaused = !isPaused;
        if (isPaused)
        {
            UpdateCanvas();
            pauseUI.GetComponent<SlideMenu>().OpenSlide();
            Invoke(nameof(ChangeTimeScale), 0.7f);
        }
        else
        {
            ChangeTimeScale();
            pauseUI.GetComponent<SlideMenu>().ReturnToGame();
            Invoke(nameof(UpdateCanvas), 0.7f);

        }

    } 
    public void SetPause()
    {
       
        isPaused = !isPaused;
        if (isPaused)
        {
            UpdateCanvas();
            pauseUI.GetComponent<SlideMenu>().OpenSlide();
            Invoke(nameof(ChangeTimeScale), 0.7f);
        }
        else
        {
            ChangeTimeScale();
            pauseUI.GetComponent<SlideMenu>().ReturnToGame();
            Invoke(nameof(UpdateCanvas), 0.7f);

        }

    }


    public void ChangeTimeScale()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void UpdateCanvas()
    {
        pauseUI.alpha = isPaused ? 1 : 0;
        pauseUI.blocksRaycasts = isPaused;
        pauseUI.interactable = isPaused;

        InGameUi.alpha = !isPaused ? 1 : 0;
        InGameUi.blocksRaycasts = !isPaused;
        InGameUi.interactable = !isPaused;
    }
    public void EndScreenSecuence()
    {
        InGameUi.alpha = 0;
        InGameUi.blocksRaycasts = false;
        InGameUi.interactable = false;
        isPaused = false;
        pauseUI.alpha = 0;
        pauseUI.blocksRaycasts = false;
        pauseUI.interactable = false;
    }

}
