using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }

    private InputActionMap CurrentMap;
    private InputAction MoveAction;
    private InputAction LookAction;
    private InputAction RunAction;

    private void Awake()
    {
        CurrentMap = playerInput.currentActionMap;
        MoveAction = CurrentMap.FindAction("Move");
        LookAction = CurrentMap.FindAction("Look");
        RunAction = CurrentMap.FindAction("Run");

        MoveAction.performed += OnMove;
        LookAction.performed += OnLook;
        RunAction.performed += OnRun;

        MoveAction.canceled += OnMove;
        LookAction.canceled += OnLook;
        RunAction.canceled += OnRun;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        Run = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        CurrentMap.Enable();
    }

    private void OnDisable()
    {
        CurrentMap.Disable();
    }
}
