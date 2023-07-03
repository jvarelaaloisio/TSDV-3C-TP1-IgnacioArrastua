using System;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealthSystem playerHealthSystem;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private VoidChannelSO pauseChannelSo;
    [SerializeField] private VoidChannelSO rollChannelSo;
    private PlayerPauseState pauseState;
    private PlayerRollingState rollingState;
    private PlayerMovementState movementState;
    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        pauseChannelSo.Subscribe(OnPause);
        rollChannelSo.Subscribe(OnRoll);
        playerHealthSystem = GetComponent<PlayerHealthSystem>();
        playerShooting = GetComponent<PlayerShooting>();
        playerMovement = GetComponent<PlayerMovement>();
        movementState = new PlayerMovementState(playerMovement, playerHealthSystem, playerShooting, "PlayerMovementState", this,boxCollider);
        rollingState = new PlayerRollingState(playerMovement, playerHealthSystem, playerShooting, "PlayerRollingState", this,boxCollider);
        pauseState = new PlayerPauseState(playerMovement, playerHealthSystem, playerShooting, "PlayerPauseState", this,boxCollider);
        previousState = movementState;
        currentState = movementState;
    }

    private void OnDestroy()
    {
        pauseChannelSo.Unsubscribe(OnPause);
        rollChannelSo.Unsubscribe(OnRoll);
    }

    private void OnRoll()
    {
        if (currentState == rollingState) return;
        currentState.OnExit();
        previousState = currentState;
        currentState = rollingState;
        currentState.OnEnter();
        Invoke(nameof(OnMovement),playerMovement.rollTime);
    }
    private void OnPause()
    {
        if (currentState == pauseState)
        {
            pauseState.OnExit();
            currentState = previousState;
            currentState.OnEnter();
        }
        else
        {
            currentState.OnExit();
            previousState = currentState;
            currentState = pauseState;
            currentState.OnEnter();
        }
    }
    private void OnMovement()
    {
        currentState.OnExit();
        previousState = currentState;
        currentState = movementState;
        currentState.OnEnter();
    }
}