using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EventsDispatcher : MonoBehaviour
{
    public UnityEvent OnReceiveNotification;
    public UnityEvent OnOpenWindow;
    public UnityEvent OnExitWindow;
    public UnityEvent OnChangeModule;
    public UnityEvent OnReturn;
    public UnityEvent OnSelectModule;
    public UnityEvent OnDayGenerate;
    public UnityEvent OnFailedShipSend;
    public UnityEvent OnShipSend;
    public UnityEvent OnGameOver;

    public InputActionProperty[] inputs = new InputActionProperty[2];


    private static EventsDispatcher _instance;

    public static EventsDispatcher Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        inputs[1].action.started += OnSelectModuleInput;
    }

    private void OnDestroy()
    {
        inputs[1].action.started -= OnSelectModuleInput;
    }

    public void ReceiveNotification() => OnReceiveNotification?.Invoke();
    public void OpenWindow() => OnOpenWindow?.Invoke();
    public void ExitWindow() => OnExitWindow?.Invoke();
    public void Return() => OnReturn?.Invoke();
    public void ChangeModule() => OnChangeModule?.Invoke();
    public void SelectModule() => OnSelectModule?.Invoke();
    public void DayGenerate() => OnDayGenerate?.Invoke();
    public void FailedShipSend() => OnFailedShipSend?.Invoke();
    public void ShipSend() => OnShipSend?.Invoke();
    public void GameOver() => OnGameOver?.Invoke();



    public void OnSelectModuleInput(InputAction.CallbackContext e)
    {
        SelectModule();
    }
}
