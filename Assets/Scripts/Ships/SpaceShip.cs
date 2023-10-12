using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{ 

    [SerializeField] private Planet reachPlanet;

    [SerializeField] private SpaceShipData shipData;
    
    public SpaceShipData ShipData
    {
        get => shipData;
    }
    
    [SerializeField] private SpaceShipsEventRef spaceShipsEventRef;

    public float distance; // A EFFACER REMPLACER PAR LA DISTANCE DE LA PLANETE

    private SpaceShipsEvents _spaceShipsEvent;

    private Image _spacesipUI;

    public UnityEvent onSendSpaceShip;
    public UnityEvent onArriveSpaceShip;
    public UnityEvent onReturnSpaceShip;

    private float _timeToReachPlanet;
    private float _timePassed;
    private bool _hasBeenSend;

    private List<MISSION_SUBTYPE> modules = new List<MISSION_SUBTYPE>();


    private void Start()
    {
        _spacesipUI = GetComponent<Image>();
        _spaceShipsEvent = spaceShipsEventRef.Instance;

    }
    private void Update()
    {
        if (_hasBeenSend)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed >= _timeToReachPlanet)
            {
                _hasBeenSend = false;
                _timePassed = 0;
                Arrive();
            }
            
        }
    }

    public void Send(Planet planet)
    {
        reachPlanet = planet;
        
        Debug.Log("send");
        _hasBeenSend = true; 
        _spacesipUI.color = new Color(_spacesipUI.color.r, _spacesipUI.color.g, _spacesipUI.color.b, 0.3f);
        _timeToReachPlanet = reachPlanet.DistanceToStation / shipData.ShipSpeed;

        Debug.Log("time " + _timeToReachPlanet);
        spaceShipsEventRef.Instance.SendSpaceShip(this);
        onSendSpaceShip?.Invoke();
        
    }

    public void Arrive()
    {
        Debug.Log("arrive");

        if (modules.Count == 0)
        {
            reachPlanet.ReceiveSonde(this);
        }
        else
        {
            reachPlanet.ReceiveSpaceShip(modules);
        }

        _spaceShipsEvent.ArriveSpaceShip(this);
        onArriveSpaceShip?.Invoke();
    }

    public void Return()
    {
        _spaceShipsEvent.ReturnSpaceShip(this);
        onReturnSpaceShip?.Invoke();
    }
    
    public void AddModule(MISSION_SUBTYPE module)
    {
        modules.Add(module);
        Debug.Log("add module " + module + " to space ship");
    }
}
