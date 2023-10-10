using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void EnableMenu(GameObject clickedShip)
    {
        _targetShip = clickedShip.GetComponent<SpaceShip>().ShipData;

        for (int i = 0; i < _targetShip.MaxModule; i++)
        {
            GameObject moduleInstance = Instantiate(modulePrefab);
            moduleInstance.transform.parent = moduleParent.transform;
        }
        
    }
}
