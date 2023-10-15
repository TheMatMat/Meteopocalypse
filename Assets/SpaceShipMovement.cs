using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : CoroutineSystem
{

    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private float timeToGo;

    public float TimeToGo
    {
        get => timeToGo;
        set => timeToGo = value;
    }

    public SpaceShip SpaceShip
    {
        get => spaceShip;
        set => spaceShip = value;
    }

    [Button("GOOOOOOO")]
    public void GoToPlanet()
    {
        transform.DOLocalMove(new Vector3(0,0,0), timeToGo);  
        RunDelayed(timeToGo,() =>
        {
            if (transform.parent.GetComponent<Planet>() != null)
            {
                spaceShip.ArriveOnPlanet();
            }
            else
            {
                spaceShip.ArriveOnStation();
            }
        });
    }

}
