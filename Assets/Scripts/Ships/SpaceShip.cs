using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{ 

    [SerializeField] private GameObject reachPlanet;

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


    private void Start()
    {
        _spacesipUI = GetComponent<Image>();
        _spaceShipsEvent = spaceShipsEventRef.Instance;

        _timeToReachPlanet = distance / shipData.ShipSpeed;
    }
    private void Update()
    {
        if (_hasBeenSend)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed >= _timeToReachPlanet)
            {
                _hasBeenSend = false;
                Arrive();
            }
            
        }
    }

    public void Send()
    {
        Debug.Log("send");
        _hasBeenSend = true; 
        _spacesipUI.color = new Color(_spacesipUI.color.r, _spacesipUI.color.g, _spacesipUI.color.b, 0.3f);
        spaceShipsEventRef.Instance.SendSpaceShip(null);
        onSendSpaceShip?.Invoke();
    }

    public void Arrive()
    {
        Debug.Log("arrive");
        _spaceShipsEvent.ArriveSpaceShip(null);
        onArriveSpaceShip?.Invoke(); 
    }

    public void Return()
    {
        _spaceShipsEvent.ReturnSpaceShip(null);
        onReturnSpaceShip?.Invoke();
    }
}
