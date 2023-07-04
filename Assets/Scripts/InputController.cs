using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]private Vector2ChannelSO OnMoveChannel;
    [SerializeField]private VoidChannelSO OnRollChannel;
    [SerializeField]private VoidChannelSO OnPauseChannel;
    [SerializeField]private BoolChannelSO OnFocusChannel;
    [SerializeField]private BoolChannelSO OnFireChannel;


    public void OnMove(InputAction.CallbackContext ctx)
    {
        OnMoveChannel.RaiseEvent(ctx.ReadValue<Vector2>());
    }
    public void OnRollInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnRollChannel.RaiseEvent();
        }
    }
    public void OnFocusMode(InputAction.CallbackContext ctx)
    {
        OnFocusChannel.RaiseEvent(ctx.performed);
    } 
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnFireChannel.RaiseEvent(true);
        }
        else if (ctx.canceled)
        {
            OnFireChannel.RaiseEvent(false);
        }
    }

    public void OnPauseMode(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnPauseChannel.RaiseEvent();
        }
    }
}