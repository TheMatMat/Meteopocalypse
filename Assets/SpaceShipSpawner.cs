using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    [SerializeField] private PlanetSpawner pm;
    [SerializeField] private GameObject vaisseau;

    [Button("Spawn")]
    public SpaceShipMovement SpawnSpaceShip(SpaceShip spaceShip)
    {
        
        GameObject planet = spaceShip.ReachPlanet.gameObject;

        GameObject spaceShipInstance = Instantiate(vaisseau, transform);
        spaceShipInstance.transform.parent = planet.transform;
        spaceShipInstance.transform.position = pm.Station.transform.position;

        SpaceShipMovement movement = spaceShipInstance.GetComponent<SpaceShipMovement>();

        movement.SpaceShip = spaceShip;
        
        movement.TimeToGo = Vector3.Distance(movement.transform.position,planet.transform.position) / 2 ;
        movement.GoToPlanet();

        return movement;
    }
}
