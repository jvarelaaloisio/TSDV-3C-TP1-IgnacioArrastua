using Cinemachine;
using UnityEngine;

/// <summary>
/// Class for the EnemyWaveManager
/// </summary>
public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private EnemyMovementPattern[] pattern;
    [SerializeField] private float[] activationPoint;
    private int nextActivationPoints = 0;
    private float currentDollyPosition;
    
    private void Update()
    {
        ActivatePattern();
    }
    /// <summary>
    /// Activates the enemy pattern according to the dollyCart position
    /// </summary>
    private void ActivatePattern()
    {
        currentDollyPosition = dollyCart.m_Position;
        if (nextActivationPoints < activationPoint.Length && currentDollyPosition >= activationPoint[nextActivationPoints])
        {
            pattern[nextActivationPoints].StartPattern();
            nextActivationPoints++;
        }
    }
}
