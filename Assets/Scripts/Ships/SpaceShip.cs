using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpaceShip : CoroutineSystem
{

    [SerializeField] private PlanetSpawner spawner;
    [SerializeField] private SpaceShipSpawner shipSpawner;
    
    [SerializeField] private Planet reachPlanet;

    [SerializeField] private SpaceShipData shipData;
    
    public SpaceShipData ShipData
    {
        get => shipData;
    }
    
    [SerializeField] private SpaceShipsEventRef spaceShipsEventRef;

    [SerializeField] private GameObject spaceShipModel;
    
    private SpaceShipsEvents _spaceShipsEvent;

    private Image _spacesipUI;

    public UnityEvent onSendSpaceShip;
    public UnityEvent onArriveSpaceShip;
    public UnityEvent onReturnSpaceShip;

    private float _timeToReachPlanet;
    private float _timePassed;
    private bool _hasBeenSend;

    public bool HasBeenSend
    {
        get => _hasBeenSend;
        set => _hasBeenSend = value;
    }

    private float timeToAchieveTask;

    public float TimeToAchieveTask
    {
        get => timeToAchieveTask;
    }

    private List<MISSION_SUBTYPE> modules = new List<MISSION_SUBTYPE>();

    public List<MISSION_SUBTYPE> Modules
    {
        get => modules;
    }

    public Planet ReachPlanet
    {
        get => reachPlanet;
        set => reachPlanet = value;
    }

    private SpaceShipMovement _shipMovement;
    
    
    private void Start()
    {
        _spacesipUI = GetComponent<Image>();
        _spaceShipsEvent = spaceShipsEventRef.Instance;

        timeToAchieveTask = UnityEngine.Random.Range(shipData.MinTimeToAchieveTask, shipData.MaxTimeToAchieveTask);

    }

    
    public void Send(Planet planet)
    {
        reachPlanet = planet;
        
        Debug.Log("send");
        _hasBeenSend = true; 
        _spacesipUI.color = new Color(_spacesipUI.color.r, _spacesipUI.color.g, _spacesipUI.color.b, 0.3f);

        if (_shipMovement == null)
        {
            _shipMovement = shipSpawner.SpawnSpaceShip(this);
        }
        else
        {
            ActualizeDestination(reachPlanet.transform);
        }
        
        spaceShipsEventRef.Instance.SendSpaceShip(this);
        onSendSpaceShip?.Invoke();
    }

    public void ArriveOnPlanet()
    {
        Debug.Log("arrive");

        if (modules.Count == 0)
        {
            reachPlanet.ReceiveSonde();
        }
        else
        {
            reachPlanet.ReceiveSpaceShip(this);
        }
        
        _spaceShipsEvent.ArriveSpaceShip(this);
        onArriveSpaceShip?.Invoke();

        RunDelayed(timeToAchieveTask, () =>
        {
            SendToStation();
        });
    }

    private void SendToStation()
    {
        Debug.Log("send to station");
        ActualizeDestination(spawner.Station.transform);
    }
    
    public void ArriveOnStation()
    {
        Debug.Log("arrive on station");
        _hasBeenSend = false;
        _spaceShipsEvent.ReturnSpaceShip(this);
        onReturnSpaceShip?.Invoke();
    }
    
    public void AddModule(MISSION_SUBTYPE module)
    {
        modules.Add(module);
        Debug.Log("add module " + module + " to space ship");
    }

    private void ActualizeDestination(Transform destination)
    {
        _shipMovement.gameObject.transform.parent = destination;
        _shipMovement.TimeToGo = Vector3.Distance(_shipMovement.transform.position,destination.position) / 2;
        _shipMovement.GoToPlanet();
    }
}
