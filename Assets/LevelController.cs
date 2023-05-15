using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private bool isLevelComplete;
    [SerializeField] private CinemachineDollyCart levelDolly;
    [SerializeField] private CinemachinePathBase path;
    [SerializeField] private string scene;

    private void Awake()
    {
        isLevelComplete = false;
        levelDolly = GetComponent<CinemachineDollyCart>();
      
    }

    // Update is called once per frame
    void Update()
    {
       if (levelDolly.m_Position >= path.MaxPos)
        {
            isLevelComplete=true;
            SceneManager.LoadScene(scene);
            Debug.Log("Level Complete");
        }
    }
}
