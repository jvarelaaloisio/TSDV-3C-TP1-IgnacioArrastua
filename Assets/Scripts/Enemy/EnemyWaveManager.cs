using Cinemachine;
using UnityEngine;

/// <summary>
/// Class for the EnemyWaveManager
/// </summary>
public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private EnemyMovementPattern[] pattern;
    [SerializeField] private float[] activationPoint;
    //TODO: TP2 - Syntax - Consistency in naming convention
     private int nextPoint = 0;


     //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private CinemachineDollyCart dollyCart;
    private float currentPosition;

 

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
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
