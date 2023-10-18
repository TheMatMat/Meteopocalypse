using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NavigationSystem : MonoBehaviour
{
    
    [SerializeField] private InputActionProperty[] navigations = new InputActionProperty[4];
    [SerializeField] private List<Button> navigationButtons;

    public List<Button> NavigationButtons
    {
        get => navigationButtons;
        set => navigationButtons = value;
    }
    
    
    private int _lastDirection;
    
    private int _currentSelectedButton;
    private int CurrentSelectedButton
    {
        get => _currentSelectedButton;
        set
        {
            _currentSelectedButton = value;
            
            if (_currentSelectedButton > navigationButtons.Count - 1 || _currentSelectedButton < 0)
            {
                _currentSelectedButton = 0;
            }

            if (navigationButtons[_currentSelectedButton] != null)
            {
                navigationButtons[_currentSelectedButton].Select();   
            }
        }
    }

    private bool _isBlocked;

    public bool IsBlocked
    {
        get => _isBlocked;
        set => _isBlocked = value;
    }

    private void Start()
    {
        EnableNavigation();
        _lastDirection = 3;
        
        navigationButtons[CurrentSelectedButton].Select();
    }

    private void OnDisable()
    {
        DisableNavigation();
    }

    public void EnableNavigation()
    {
        navigations[0].action.started += OnRight;
        navigations[1].action.started += OnDown;
        navigations[2].action.started += OnLeft;
        navigations[3].action.started += OnTop;
    }

    public void DisableNavigation()
    {
        
        navigations[0].action.started -= OnRight;
        navigations[1].action.started -= OnDown;
        navigations[2].action.started -= OnLeft;
        navigations[3].action.started -= OnTop;    
    }
    
    private void OnRight(InputAction.CallbackContext e)
    { // 0 
        
        if (_isBlocked)
        {
            return;
        }
        
        switch (_lastDirection)
        {
            case 3:
                CurrentSelectedButton = CurrentSelectedButton + 1;
                break;
            
            case 1:
                CurrentSelectedButton = CurrentSelectedButton - 1;
                break;
        }

        _lastDirection = 0;
    }

    private void OnDown(InputAction.CallbackContext e)
    {  
        if (_isBlocked)
        {
            return;
        }
        
        switch (_lastDirection)
        {
            case 0:
                CurrentSelectedButton = CurrentSelectedButton + 1;
                break;
            
            case 2:
                CurrentSelectedButton = CurrentSelectedButton - 1;
                break;
        }
        
        _lastDirection = 1;
    }

    private void OnLeft(InputAction.CallbackContext e)
    {
        if (_isBlocked)
        {
            return;
        }
        
        switch (_lastDirection)
        {
            case 1:
                CurrentSelectedButton = CurrentSelectedButton + 1; 
                break;
            
            case 3:
                CurrentSelectedButton = CurrentSelectedButton - 1;
                break;
        }
        
        _lastDirection = 2;
    }

    private void OnTop(InputAction.CallbackContext e)
    {
        if (_isBlocked)
        {
            return;
        }
        
        switch (_lastDirection)
        {
            case 2:
                CurrentSelectedButton = CurrentSelectedButton + 1;
                break;
            
            case 0:
                CurrentSelectedButton = CurrentSelectedButton - 1;
                break;
        }
        
        _lastDirection = 3;
    }
}
