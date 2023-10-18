using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipsEvents : MonoBehaviour
{

    public event Action<SpaceShip> OnSendSpaceShip;
    public event Action<SpaceShip> OnSpaceShipArrive;
    public event Action<SpaceShip> OnSpaceShipReturn;

    public event Action<Planet> OnReceivePlanetData;



    public void SendSpaceShip(SpaceShip spaceShip) => OnSendSpaceShip?.Invoke(spaceShip);
    public void ArriveSpaceShip(SpaceShip spaceShip) => OnSpaceShipArrive?.Invoke(spaceShip);
    public void ReturnSpaceShip(SpaceShip spaceShip) => OnSpaceShipReturn?.Invoke(spaceShip);
    public void ReceivePlanetData(Planet planet) => OnReceivePlanetData?.Invoke(planet);
}
