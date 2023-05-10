using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private EnemyMovementPattern[] pattern;
    [SerializeField] private float[] activationPoint;
    [SerializeField] private int nextPoint = 0;


    [SerializeField] private CinemachineDollyCart dollyCart;
    private float currentPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         currentPosition= dollyCart.m_Position;
        if (currentPosition >= activationPoint[nextPoint] && nextPoint < activationPoint.Length-1) 
        {
            pattern[nextPoint].StartPattern();
            nextPoint++;
        }
    }

    private void ActivatePattern()
    {

    }
}
