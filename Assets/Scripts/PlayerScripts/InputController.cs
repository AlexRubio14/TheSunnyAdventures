using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    InputActionReference moveAction;
    [SerializeField]
    InputActionReference jumpAction;
    [SerializeField]
    InputActionReference dashAction;
    [SerializeField]
    InputActionReference interactAction;
    [SerializeField]
    InputActionReference shootAction;
    [SerializeField]
    InputActionReference pauseAction;

    playerController controller;
    fireBallThrowController shootController;
    Pause pause;
    public float movementInput { get; private set; }


    private void Awake()
    {

        controller = GetComponent<playerController>();
        shootController = GetComponent<fireBallThrowController>();
        pause = FindObjectOfType<Pause>();

        moveAction.action.started += MoveAction;
        moveAction.action.performed += MoveAction;
        moveAction.action.canceled += MoveAction;

        jumpAction.action.started += JumpAction;
        jumpAction.action.canceled += EndJumpAction;

        dashAction.action.started += DashAction;

        interactAction.action.started += InteractAction;

        shootAction.action.started += ShootAction;
        shootAction.action.canceled += ShootAction;

        pauseAction.action.started += PauseAction;
    }
    
    private void PauseAction(InputAction.CallbackContext obj)
    {
        pause.pauseCanvas();
    }

    private void OnDestroy()
    {
        moveAction.action.started -= MoveAction;
        moveAction.action.performed -= MoveAction;
        moveAction.action.canceled -= MoveAction;

        jumpAction.action.started -= JumpAction;
        jumpAction.action.canceled -= EndJumpAction;

        dashAction.action.started -= DashAction;

        interactAction.action.started -= InteractAction;

        shootAction.action.started -= ShootAction;
        shootAction.action.canceled -= ShootAction;

        pauseAction.action.started -= PauseAction;
    }

    private void ShootAction(InputAction.CallbackContext obj)
    {
        shootController.InvertShooting();
    }

    private void InteractAction(InputAction.CallbackContext obj)
    {
        controller.Attack();
    }

    private void DashAction(InputAction.CallbackContext obj)
    {
        controller.Dash();
    }

    private void JumpAction(InputAction.CallbackContext obj)
    {
       controller.Jump();
    }

    private void EndJumpAction(InputAction.CallbackContext obj)
    {
        controller.EndJump();
    }

    private void MoveAction(InputAction.CallbackContext obj)
    {
        movementInput = moveAction.action.ReadValue<float>();
    }
}
