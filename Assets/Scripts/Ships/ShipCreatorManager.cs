using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ShipCreatorManager : MonoBehaviour
{

    private SpaceShipData _targetShip;

    public SpaceShipData TargetShip
    {
        get => _targetShip;
        set => _targetShip = value;
    }

    [SerializeField] private GameObject moduleParent;
    [SerializeField] private GameObject modulePrefab;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private TypeManagerRef typeManagerRef;
    [SerializeField] private NavigationSystem currentNavigation;

    [SerializeField] private InputActionProperty back;
    private NavigationSystem CurrentNavigation
    {
        get => currentNavigation;

        set
        {
            if (currentNavigation != null)
            {
                currentNavigation.enabled = false;
            }
            
            currentNavigation = value;
            currentNavigation.enabled = true;
        }
    }

    private TypeManager _typeManager;

    private int _action;



    private void Start()
    {
        _typeManager = typeManagerRef.Instance;
        back.action.started += OnBack;
    }

    private void OnDestroy()
    {
        back.action.started -= OnBack;
    }

    public void EnableMenu(GameObject clickedShip)
    {
        _targetShip = clickedShip.GetComponent<SpaceShip>().ShipData;

        for (int i = 0; i < _targetShip.MaxModule; i++)
        {
            GameObject moduleInstance = Instantiate(modulePrefab);
            moduleInstance.transform.parent = moduleParent.transform;

            for (int j = 0; j < moduleInstance.transform.childCount; j++)
            {
                if (moduleInstance.transform.GetChild(j).TryGetComponent(out Image image))
                {
                    image.sprite = _typeManager.GetSpriteByType((EType)j);
                }

                if (moduleInstance.transform.GetChild(j).TryGetComponent(out Button button))
                {
                    GameObject targetButtonObj = button.gameObject;
                    button.onClick.AddListener(delegate { OnTypeChoose(targetButtonObj); });

                    if (i == 0 && j == 0)
                    {
                        CurrentNavigation = targetButtonObj.transform.parent.GetComponent<NavigationSystem>();
                        
                        eventSystem.SetSelectedGameObject(targetButtonObj);    
                    }
                    
                    if (i != 0)
                    {
                        button.enabled = false;
                    }
                }
            }
        }
        
    }
    private void OnTypeChoose(GameObject button)
    {
        HorizontalLayoutGroup layoutGroup = button.transform.parent.GetComponent<HorizontalLayoutGroup>();
        
        switch (_action)
        {
            case 0: // Called when choose type 
                for (int i = 0; i < button.transform.parent.childCount; i++)
                {
                    GameObject b = button.transform.parent.GetChild(i).gameObject;

                    if (b != button)
                    {
                        Destroy(b);
                    }
                    else
                    {
                        Button btn = b.GetComponent<Button>();
                        btn.onClick.RemoveAllListeners();
                    }
                }

                layoutGroup.spacing = 100;
                
                EType targetType = _typeManager.GetTypeBySprite(button.GetComponent<Image>().sprite);
                TypeManager.CategoryType category = _typeManager.GetCategoryStructByType(targetType);
                
                CurrentNavigation.NavigationButtons.Clear();

                for (int i = 0; i < category.subTypes.Count; i++)
                {
                    GameObject newButton = Instantiate(button);
                    newButton.transform.parent = button.transform.parent;
                    newButton.GetComponent<Button>().onClick.AddListener(delegate { OnTypeChoose(newButton); });
                    CurrentNavigation.NavigationButtons.Add(newButton.GetComponent<Button>());
                    if (i == 0)
                    {
                        eventSystem.SetSelectedGameObject(newButton);
                    }
                    
                    
                    newButton.GetComponent<Image>().sprite = _typeManager.GetSpriteBySubType(category.subTypes[i]);
                }
                
                _action++;
                break;
            
            case 1: // Called when choose sub type 

                for (int i = 1; i < button.transform.parent.childCount; i++)
                {
                    GameObject b = button.transform.parent.GetChild(i).gameObject;

                    if (b != button)
                    {
                        Destroy(b);
                    }

                    Button btn = b.GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.enabled = false;
                }

                layoutGroup.childAlignment = TextAnchor.MiddleCenter;
                layoutGroup.spacing = 20;
                
                GameObject moduleParent = button.transform.parent.parent.gameObject;

                int index = -1;

                for (int i = 0; i < moduleParent.transform.childCount; i++)
                {
                    if (i - 1 >= 0)
                    {
                        if (moduleParent.transform.GetChild(i - 1).gameObject == button.transform.parent.gameObject)
                        {
                            index = i;
                        }
                    }
                }

                if (index != -1)
                {
                    CurrentNavigation = moduleParent.transform.GetChild(index).GetComponent<NavigationSystem>();

                    for (int i = 0; i < moduleParent.transform.GetChild(index).childCount; i++)
                    {
                        if (moduleParent.transform.GetChild(index).GetChild(i).gameObject.TryGetComponent(out Button btn))
                        {
                            btn.enabled = true;
                            
                            if (i == 1)
                            {
                                eventSystem.SetSelectedGameObject(btn.gameObject);
                            }
                            
                        }
                    }
                }

                _action = 0;
                break;
        }
    }

    private void OnBack(InputAction.CallbackContext e)
    {
        if (_action == 1)
        {
            GameObject module = CurrentNavigation.gameObject;
            HorizontalLayoutGroup horizontalGroup = module.GetComponent<HorizontalLayoutGroup>();

            if (module.transform.childCount > 3)
            {
                for (int i = 3; i < module.transform.childCount; i++)
                {
                    Destroy(module.transform.GetChild(i).gameObject);
                }
            }

            CurrentNavigation.NavigationButtons.Clear();
            for (int i = 0; i < module.transform.childCount; i++)
            {
                CurrentNavigation.NavigationButtons.Add(module.transform.GetChild(i).GetComponent<Button>());
                module.transform.GetChild(i).GetComponent<Image>().sprite = _typeManager.GetSpriteByType((EType)i);
            }

            horizontalGroup.spacing = 200;
            _action = 0;
        }
    }
}
