using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private PlayerInput _inputSystem;
    private InputAction _actionMove;
    private InputAction _actionInteraction;
    private InputAction _actionSwitchMode;
    private InputAction _actionAttack;
    private InputAction _actionDodge;

    private RobotController _robotController;
    private void Awake()
    {
        _inputSystem = GetComponent<PlayerInput>();
        _robotController = GetComponent<RobotController>();

        _actionMove = _inputSystem.actions["Move"];
        _actionInteraction = _inputSystem.actions["Interact"];
        _actionSwitchMode = _inputSystem.actions["SwitchMode"];
        _actionAttack = _inputSystem.actions["Attack"];
        _actionDodge = _inputSystem.actions["Dodge"];
    }

    private void OnEnable()
    {
        _actionMove.Enable();
        _actionInteraction.Enable();
        _actionSwitchMode.Enable();
        _actionAttack.Enable();
        _actionDodge.Enable();
    }

    private void OnDisable()
    {
        _actionMove.Disable();
        _actionInteraction.Disable();
        _actionSwitchMode.Disable();
        _actionAttack.Disable();
        _actionDodge.Disable();
    }

    private void Update()
    {
        ActionMove();
        ActionInteraction();
        ActionSwitchMode();
        ActionAttack();
        ActionDodge();
    }

    private void ActionMove()
    {
        if(_actionMove != null)
        {
            Vector2 inputMoveAction = _actionMove.ReadValue<Vector2>();
            _robotController.MoveInput = inputMoveAction;
        }
    }

    private void ActionInteraction()
    {
        if (_actionInteraction.triggered)
        {
            _robotController.Interact();
        }
        
    }

    private void ActionSwitchMode()
    {
        if (_actionSwitchMode.triggered)
        {
            _robotController.SwitchMode();
        }
    }

    private void ActionAttack()
    {
        if (_actionAttack.triggered)
        {
            _robotController.Attack();
        }
    }

    private void ActionDodge()
    {
        if (_actionDodge.triggered)
        {
            _robotController.Dodge();
        }
    }

}
