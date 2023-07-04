using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public override void OnEnter()
    {
        playerHealthSystem.enabled = true;
        playerMovement.enabled = true;
        playerShooting.enabled = true;
        boxCollider.enabled = true;
    }
    public PlayerMovementState(PlayerMovement playerMovement, PlayerHealthSystem playerHealthSystem, PlayerShooting playerShooting, string name, StateMachine stateMachine,BoxCollider boxCollider) : base(playerMovement, playerHealthSystem, playerShooting, name, stateMachine,boxCollider)
    {
    }
}