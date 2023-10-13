using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : CoroutineSystem
{

    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private float timeToGo = 5;

    public float TimeToGo
    {
        get => timeToGo;
        set => timeToGo = value;
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3.MoveTowards(transform.position, objectif.transform.position, speed * Time.deltaTime);
    }

    [Button("GOOOOOOO")]
    public void GoToPlanet()
    {
        //le fais aller vers 0,0,0 car c'est la position de la plan�te (local pour aller vers la plan�te qui est son 0,0,0 car il est en enfant)
        transform.DOLocalMove(new Vector3(0,0,0), timeToGo);  
        RunDelayed(timeToGo,() =>
        {
            // Arrive
            spaceShip.ArriveOnPlanet();
        });
    }

}
