using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the TutorialSecuence
/// </summary>
public class TutorialSecuence : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<PopUpText> popUpText;
    [SerializeField] private float maxTimeBetweenText;
    [SerializeField] private  float timeUntilChangeScene = 2f;
    [SerializeField] private string scenesMainmenu = "Scenes/MainMenu";
    private int currentTextCounter = -1;
    private int maxTextCounter = 0;
    private float currentTimeBetweenText;

    private void Start()
    {
        currentTextCounter = -1;
        currentTimeBetweenText = maxTimeBetweenText;
        maxTextCounter = popUpText.Count - 1;

    }

    private void Update()
    {
        //TODO - Fix - Coroutine
        currentTimeBetweenText += Time.deltaTime;
        if (currentTimeBetweenText > maxTimeBetweenText)
        {
            currentTimeBetweenText = 0;
            ShowMessage();
        }
        CheckIfGameShouldEnd();
    }
    /// <summary>
    /// Show a PopUpText according to time
    /// </summary>
    private void ShowMessage()
    {
        if (currentTextCounter < maxTextCounter)
        {
            if (currentTextCounter > -1)
            {
                popUpText[currentTextCounter].DeactivateBox();
            }
            currentTextCounter++;
            Debug.Log(currentTextCounter);
            popUpText[currentTextCounter].ActiveBox();
        }
      

    }
    /// <summary>
    /// Check if the condition to end the tutorial is fullfil and changes scene
    /// </summary>
    private void CheckIfGameShouldEnd()
    {
        if (!enemy.GetComponent<EnemyBaseStats>().IsAlive())
        {
            Invoke(nameof(GoBackToMenu), timeUntilChangeScene);
        }
    }
    /// <summary>
    /// Load MenuScene
    /// </summary>
    private void GoBackToMenu()
    {
        SceneManager.LoadScene(scenesMainmenu);
    }
}
