using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] protected State previousState;
    [SerializeField] protected State currentState;
}