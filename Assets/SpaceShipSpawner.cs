using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    [SerializeField] private PlanetManager pm;

    [Button("Spawn")]
    public SpaceShipMovement SpawnSpaceShip(SpaceShip spaceShip)
    {
        
        GameObject planet = spaceShip.ReachPlanet.gameObject;

        GameObject spaceShipInstance = Instantiate(spaceShip.ShipData.ShipModel);
        spaceShipInstance.transform.position = pm.StationInstance.transform.position;
        spaceShipInstance.transform.localScale = spaceShip.ShipData.ShipModel.transform.localScale;
        spaceShipInstance.transform.rotation = Quaternion.Euler(0,0,0);

        SpaceShipMovement movement = spaceShipInstance.GetComponent<SpaceShipMovement>();

        movement.SpaceShip = spaceShip;
        
        movement.TimeToGo = (Vector3.Distance(movement.transform.position,spaceShip.ReachPlanet.transform.position) / 2) / spaceShip.ShipData.ShipSpeed;
        movement.GoToPlanet(planet);

        return movement;
    }
}
