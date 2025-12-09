using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movement : MonoBehaviour
{
    private InputSystem_Actions _inputs;
    protected Rigidbody Rb;
    protected Vector2 Look;
    protected Vector2 MoveInput;

    private void Awake()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
        _inputs = new InputSystem_Actions();
        _inputs.Player.Enable();
    }

    private void OnEnable() => ActivateActions();
    private void OnDisable() => DeactivateActions();
    private void OnMove(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext context) => MoveInput = Vector2.zero;
    private void OnMouseMove(InputAction.CallbackContext context) => Look = context.ReadValue<Vector2>();
    private void OnMouseCanceled(InputAction.CallbackContext context) => Look = Vector2.zero;
    protected abstract void Jump(InputAction.CallbackContext context);

    private void ActivateActions()
    {
        Action(_inputs.Player.Move, OnMove, OnMoveCanceled);
        Action(_inputs.Player.Look, OnMouseMove, OnMouseCanceled);
        Action(_inputs.Player.Jump, Jump);
    }

    private void DeactivateActions()
    {
        DeAction(_inputs.Player.Move, OnMove, OnMoveCanceled);
        DeAction(_inputs.Player.Look, OnMouseMove, OnMouseCanceled);

        DeAction(_inputs.Player.Jump, Jump);
    }

    private void Action(InputAction input, 
        Action<InputAction.CallbackContext> preformed = null, 
        Action<InputAction.CallbackContext> canceled = null)
    {
        if (preformed != null)
            input.performed += preformed;
        if (canceled != null)
            input.canceled += canceled;
    }

    private void DeAction(InputAction input, 
        Action<InputAction.CallbackContext> preformed = null, 
        Action<InputAction.CallbackContext> canceled = null)
    {
        if (preformed != null)
            input.performed -= preformed;
        if (canceled != null)
            input.canceled -= canceled;
    }
}