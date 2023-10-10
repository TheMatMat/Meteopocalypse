using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField] private TypeManagerRef typeManagerRef;

    private TypeManager _typeManager;

    private void Start()
    {
        _typeManager = typeManagerRef.Instance;
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
        Debug.Log("on type choose");
    }
}
