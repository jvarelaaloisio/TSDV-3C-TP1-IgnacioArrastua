using UnityEngine;

public class PlayerPauseState : PlayerState
{
    public override void OnEnter()
    {
        playerHealthSystem.enabled = false;
        playerMovement.enabled = false;
        playerShooting.enabled = false;
        boxCollider.enabled = false;
    }
    public override void OnExit()
    {
        playerHealthSystem.enabled = true;
        playerMovement.enabled = true;
        playerShooting.enabled = true;
        boxCollider.enabled = true;
    }

    public PlayerPauseState(PlayerMovement playerMovement, PlayerHealthSystem playerHealthSystem, PlayerShooting playerShooting, string name, StateMachine stateMachine,BoxCollider boxCollider) : base(playerMovement, playerHealthSystem, playerShooting, name, stateMachine,boxCollider)
    {
    }
}