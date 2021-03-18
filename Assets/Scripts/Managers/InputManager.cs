﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable] public class UnityEventFloat : UnityEvent<float> { }
[System.Serializable] public class UnityEventBool : UnityEvent<bool> { }

public class InputManager : MonoBehaviour, INewInputSystem
{
    public string _horAxis, _verAxis, _runAxis, _jumpAxis, _crouchAxis, _shootAxis;
    public UnityEventFloat _OnMovePerformed, _OnRunPerformed, _OnJumpPerformed, _OnCrouchPerformed, _OnShootPerformed;

    public void MovePerformed(InputAction.CallbackContext ctx) 
    { 
        if (GameManager.Instance.IsPaused) return;
        if(Input.GetAxis(_horAxis) > 0)
            _OnMovePerformed?.Invoke(Input.GetAxis(_horAxis));
        else
            _OnMovePerformed?.Invoke(ctx.ReadValue<Vector2>().x);
    
    }
    public void RunInputActionPerformed(InputAction.CallbackContext ctx) 
    { 
        if (GameManager.Instance.IsPaused) return; 
        _OnRunPerformed?.Invoke(ctx.ReadValue<float>()); 
    
    }
    public void JumpInputActionPerformed(InputAction.CallbackContext ctx) 
    { 
        if (GameManager.Instance.IsPaused) return; 
        _OnJumpPerformed?.Invoke(ctx.ReadValue<float>()); 
    }

    public void CrouchInputActionPerformed(InputAction.CallbackContext ctx) 
    { 
        if (GameManager.Instance.IsPaused) return; 
        _OnCrouchPerformed?.Invoke(ctx.ReadValue<float>()); 
    }

    public void OnShootPerformed(InputAction.CallbackContext ctx) 
    { 
        if (GameManager.Instance.IsPaused) return; 
        _OnShootPerformed?.Invoke(ctx.ReadValue<float>()); 
    }

    // NOT USED
    public void EventHandlers()
    {
        throw new NotImplementedException();
    }

    public void Enable()
    {
        throw new NotImplementedException();
    }

    public void Disable()
    {
        throw new NotImplementedException();
    }
}
