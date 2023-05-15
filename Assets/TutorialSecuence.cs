using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSecuence : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<PopUpText> popUpText;
    private int textCounter = -1;
    private int maxCounter = 0;
    [SerializeField] private float timeBetweernMessages;
    private float currentTime;
    void Start()
    {
        textCounter = -1;
        currentTime = timeBetweernMessages;
        maxCounter = popUpText.Count-1;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (!(currentTime > timeBetweernMessages)) return;
        currentTime = 0;
        ShowMessage();
    }

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
            if (!enemy.GetComponent<EnemyBaseStats>().IsAlive())
            {
                Invoke(nameof(GoBackToMenu),2f);
            } 
        }
       
    }

    private void GoBackToMenu()
    {
        SceneManager.LoadScene($"Scenes/MainMenu");
    }
}
