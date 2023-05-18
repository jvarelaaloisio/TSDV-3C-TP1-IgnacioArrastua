using Cinemachine;
using UnityEngine;

/// <summary>
/// Class for the EnemyWaveManager
/// </summary>
public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private EnemyMovementPattern[] pattern;
    [SerializeField] private float[] activationPoint;
     private int nextPoint = 0;


    [SerializeField] private CinemachineDollyCart dollyCart;
    private float currentPosition;

 

    void Update()
    {
        ActivatePattern();
    }
    /// <summary>
    /// Activates the enemy pattern according to the dollyCart position
    /// </summary>
    private void ActivatePattern()
    {
        currentPosition = dollyCart.m_Position;
        if (nextPoint < activationPoint.Length && currentPosition >= activationPoint[nextPoint])
        {
            pattern[nextPoint].StartPattern();
            nextPoint++;
        }
    }
}
