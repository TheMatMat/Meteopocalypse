using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class NavigationSystem : MonoBehaviour
{
    
    [SerializeField] private InputActionProperty[] navigations = new InputActionProperty[2];
    [SerializeField] private List<Button> navigationButtons;

    public List<Button> NavigationButtons
    {
        get => navigationButtons;
        set => navigationButtons = value;
    }
    
    
    
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
        
        navigationButtons[CurrentSelectedButton].Select();
    }

    private void OnDisable()
    {
        DisableNavigation();
    }

    public void EnableNavigation()
    {
        navigations[0].action.started += OnRight;
        navigations[1].action.started += OnLeft;
    }

    public void DisableNavigation()
    {
        
        navigations[0].action.started -= OnRight;
        navigations[1].action.started -= OnLeft; 
    }
    
    private void OnRight(InputAction.CallbackContext e)
    { // 0 
        
        if (_isBlocked)
        {
            return;
        }
        
        CurrentSelectedButton = CurrentSelectedButton + 1;
        EventsDispatcher.Instance.ChangeModule();
    }

    

    private void OnLeft(InputAction.CallbackContext e)
    {
        if (_isBlocked)
        {
            return;
        }
        
        CurrentSelectedButton = CurrentSelectedButton - 1;
        EventsDispatcher.Instance.Return();
    }

   
}
