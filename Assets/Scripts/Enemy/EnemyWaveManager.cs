using Cinemachine;
using UnityEngine;

/// <summary>
/// Class for the EnemyWaveManager
/// </summary>
public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private EnemyMovementPattern[] pattern;
    [SerializeField] private float[] activationPoint;
    private int nextActivationPoints = 0;


     //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private CinemachineDollyCart dollyCart;
    private float currentPosition;

    
    private void Update()
    {
        ActivatePattern();
    }
    /// <summary>
    /// Activates the enemy pattern according to the dollyCart position
    /// </summary>
    private void ActivatePattern()
    {
        currentPosition = dollyCart.m_Position;
        if (nextActivationPoints < activationPoint.Length && currentPosition >= activationPoint[nextActivationPoints])
        {
            pattern[nextActivationPoints].StartPattern();
            nextActivationPoints++;
        }
    }
}
