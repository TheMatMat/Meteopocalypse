using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using TMPro;
using System;
using UnityEngine.Events;

public class ShipCreatorManager : MonoBehaviour
{

    private SpaceShip _targetShip;

    public SpaceShip TargetShip
    {
        get => _targetShip;
        set => _targetShip = value;
    }

    [SerializeField] private GameObject moduleParent;
    [SerializeField] private GameObject modulePrefab;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private TypeManagerRef typeManagerRef;
    [SerializeField] private NavigationSystem currentNavigation;
    [SerializeField] private PlanetManager planetManager;

    [SerializeField] private InputActionProperty back;
    [SerializeField] private InputActionProperty send;
    [SerializeField] private TMP_InputField coordsField;
    [SerializeField] private GameObject coordsPanel;

    [SerializeField] Transform modulePanelOutPos, modulePanelInPos;
    [SerializeField] Transform coordPanelOutPos, coordPanelInPos;

    [SerializeField] private UnityEvent closeMenu;

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

    private NavigationSystem _startNavigation;

    private bool _isInMenu;

    private void Start()
    {
        _typeManager = typeManagerRef.Instance;
        back.action.started += OnBack;
        send.action.started += OnSendSpaceShip;
        
        
    }

    private void OnDestroy()
    {
        back.action.started -= OnBack;
        back.action.started -= OnSendSpaceShip;
    }

    

    public void EnableMenu(GameObject clickedShip)
    {
        _targetShip = clickedShip.GetComponent<SpaceShip>();

        if (_targetShip.HasBeenSend)
        {
            return;
        }

        coordsPanel.GetComponent<RectTransform>().position = coordPanelOutPos.position;
        coordsPanel.SetActive(true);
        coordsPanel.GetComponent<RectTransform>().DOMove(coordPanelInPos.position, 0.7f);
        
        coordsField.text = "";

        if (_targetShip.ShipData.MaxModule > 0)
        {
            GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<RectTransform>().position = modulePanelOutPos.position;
            this.gameObject.GetComponent<RectTransform>().DOMove(modulePanelInPos.position, 0.5f).OnComplete(() => moduleParent.gameObject.SetActive(true));
        }
        else
        {
            coordsField.Select();
            _startNavigation = currentNavigation;
           // _startNavigation.IsBlocked = true;
           _startNavigation.DisableNavigation();
        }

        _isInMenu = true;

        for (int i = 0; i < moduleParent.transform.childCount; i++)
        {
            Destroy(moduleParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < _targetShip.ShipData.MaxModule; i++)
        {
            GameObject moduleInstance = Instantiate(modulePrefab);
            moduleInstance.transform.parent = moduleParent.transform;

            for (int j = 0; j < moduleInstance.transform.childCount; j++)
            {
                if (moduleInstance.transform.GetChild(j).TryGetComponent(out Image image))
                {
                    image.sprite = _typeManager.GetSpriteByType((MISSION_TYPE)j);
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
                        b.transform.DOScale(new Vector3(0, 0, 0), 0.3f).OnComplete(() => Destroy(b));
                    }
                    else
                    {
                        Button btn = b.GetComponent<Button>();
                        btn.onClick.RemoveAllListeners();
                    }
                }

                layoutGroup.spacing = 100;

                MISSION_TYPE targetType = _typeManager.GetTypeBySprite(button.GetComponent<Image>().sprite);
                TypeManager.CategoryType category = _typeManager.GetCategoryStructByType(targetType);
                
                CurrentNavigation.NavigationButtons.Clear();

                float delay = 0f;
                for (int i = 0; i < category.subTypes.Count; i++)
                {
                    GameObject newButton = Instantiate(button);

                    //Hide it and scale down then up
                    newButton.SetActive(false);
                    newButton.transform.localScale = new Vector3(0, 0, 0);
                    newButton.SetActive(true);

                    newButton.transform.parent = button.transform.parent;
                    newButton.GetComponent<Button>().onClick.AddListener(delegate { OnTypeChoose(newButton); });
                    CurrentNavigation.NavigationButtons.Add(newButton.GetComponent<Button>());
                    if (i == 0)
                    {
                        eventSystem.SetSelectedGameObject(newButton);
                    }
                    
                    
                    newButton.GetComponent<Image>().sprite = _typeManager.GetSpriteBySubType(category.subTypes[i]);

                    newButton.transform.DOScale(new Vector3(1, 1, 1), 0.3f + delay).SetDelay(0.3f);
                    delay += 0.1f;
                }
                
                _action++;
                break;
            
            case 1: // Called when choose sub type 

                for (int i = 1; i < button.transform.parent.childCount; i++)
                {
                    GameObject b = button.transform.parent.GetChild(i).gameObject;

                    if (b != button)
                    {
                        b.transform.DOScale(new Vector3(0, 0, 0), 0.3f).OnComplete(() => Destroy(b));
                    }

                    Button btn = b.GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.enabled = false;
                }

                layoutGroup.childAlignment = TextAnchor.MiddleCenter;
                layoutGroup.spacing = 20;
                
                GameObject moduleParent = button.transform.parent.parent.gameObject;

                MISSION_SUBTYPE missionModule = _typeManager.GetSubTypeBySprite(button.GetComponent<Image>().sprite);
                _targetShip.AddModule(missionModule);

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
                else
                {
                    // Disable tt les boutons et enlever le Select()
                    coordsField.Select();
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
                module.transform.GetChild(i).GetComponent<Image>().sprite = _typeManager.GetSpriteByType((MISSION_TYPE)i);
            }

            horizontalGroup.spacing = 200;
            _action = 0;
        }
        else
        {
            if (_isInMenu)
            {
                coordsPanel.SetActive(false);
                _isInMenu = false;
                GetComponent<Image>().enabled = false;
                moduleParent.gameObject.SetActive(false);
                transform.parent.GetComponentInChildren<NavigationSystem>().EnableNavigation();
            }
        }
        
    }

    private void OnSendSpaceShip(InputAction.CallbackContext e)
    {
        OnTryToSend(coordsField.text);
        CloseMenu();
    }


    public void OnTryToSend(string coordinates)
    {
        foreach (Planet planet in planetManager.Planets)
        {
            if (planet.planetCoordinates.ToString() == coordinates)
            {
                TargetShip.Send(planet);
                
                break;
            }
        }
        
        if (_startNavigation != null)
        {
            _startNavigation.EnableNavigation();
        }
        Debug.Log("renable navigation");
    }

    private void CloseMenu()
    {
        TargetShip = null;
        closeMenu?.Invoke();

        GetComponent<Image>().enabled = false;
        moduleParent.gameObject.SetActive(false);
    }
}
