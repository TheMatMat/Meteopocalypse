using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    [SerializeField] private int TimeToGO = 5;

    // Update is called once per frame
    void Update()
    {
        //Vector3.MoveTowards(transform.position, objectif.transform.position, speed * Time.deltaTime);
    }

    [Button("GOOOOOOO")]
    public void GoToPlanet()
    {
        //le fais aller vers 0,0,0 car c'est la position de la planète (local pour aller vers la planète qui est son 0,0,0 car il est en enfant)
        transform.DOLocalMove(new Vector3(0,0,0), TimeToGO);        
    }

}
