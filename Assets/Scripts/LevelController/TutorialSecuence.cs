using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the TutorialSecuence
/// </summary>
public class TutorialSecuence : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<PopUpText> popUpText;
    [SerializeField] private float timeBetweernMessages;
    //TODO: TP2 - Syntax - Consistency in naming convention
    private int textCounter = -1;
    private int maxCounter = 0;
    private float currentTime;

    private void Start()
    {
        textCounter = -1;
        currentTime = timeBetweernMessages;
        maxCounter = popUpText.Count - 1;

    }

    private void Update()
    {
        //TODO - Fix - Coroutine
        currentTime += Time.deltaTime;
        if (currentTime > timeBetweernMessages)
        {
            currentTime = 0;
            ShowMessage();
        }
    }
    //TODO: TP2 - Syntax - Fix formatting
    /// <summary>
    /// Show a PopUpText according to time
    /// </summary>
    private void ShowMessage()
    {
        if (textCounter < maxCounter)
        {
            if (textCounter > -1)
            {
                popUpText[textCounter].DeactivateBox();
            }
            textCounter++;
            Debug.Log(textCounter);
            popUpText[textCounter].ActiveBox();
        }
        else
        {
            CheckIfGameShouldEnd();
        }

    }
    /// <summary>
    /// Check if the condition to end the tutorial is fullfil and changes scene
    /// </summary>
    private void CheckIfGameShouldEnd()
    {
        if (!enemy.GetComponent<EnemyBaseStats>().IsAlive())
        {
            //TODO - Fix - Hardcoded value
            Invoke(nameof(GoBackToMenu), 2f);
        }
    }
    /// <summary>
    /// Load MenuScene
    /// </summary>
    private void GoBackToMenu()
    {
        //TODO - Fix - Hardcoded value
        SceneManager.LoadScene($"Scenes/MainMenu");
    }
}
