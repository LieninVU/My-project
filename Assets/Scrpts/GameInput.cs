using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions inputSystemActions;
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPlayerAttack;

    private void Awake()
    {
        Instance = this;
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Enable();
        inputSystemActions.Player.Attack.started += PlayerAttackStarted;
    }

    private void PlayerAttackStarted(InputAction.CallbackContext obj)
    {
        if (OnPlayerAttack != null)
        {
            OnPlayerAttack.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementVEctor()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }
}
