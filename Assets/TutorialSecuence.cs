using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSecuence : MonoBehaviour
{
    [SerializeField] private List<PopUpText> popUpText;
    private int textCounter = -1;
    private int maxCounter = 0;
    [SerializeField] private float timeBetweernMessages;
    private float currentTime;
    void Start()
    {
        textCounter = -1;
        maxCounter = popUpText.Count;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeBetweernMessages)
        {
            currentTime = 0;
            ShowMessage();
        }
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
    }
}
