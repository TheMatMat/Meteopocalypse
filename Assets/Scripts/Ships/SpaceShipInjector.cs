using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInjector : MonoBehaviour
{
    [SerializeField] private SpaceShipsEvents _e;
    [SerializeField] private SpaceShipsEventRef _ref;
    
    ISet<SpaceShipsEvents> RealRef => _ref;


    void Awake()
    {
        RealRef.Set(_e);
    }
}
