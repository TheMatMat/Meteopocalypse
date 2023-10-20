using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpaceShip : CoroutineSystem
{

    [SerializeField] private PlanetManager spawner;
    [SerializeField] private SpaceShipSpawner shipSpawner;
    
    [SerializeField] private Planet reachPlanet;

    [SerializeField] private SpaceShipData shipData;
    
    public SpaceShipData ShipData
    {
        get => shipData;
    }
    
    [SerializeField] private SpaceShipsEventRef spaceShipsEventRef;
    [SerializeField] private PrintDemo printer;
    
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
    
    [SerializeField] private TypeManagerRef _typeManager;
    
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
        EventsDispatcher.Instance.ShipSend();
        if (_shipMovement == null)
        {
            _shipMovement = shipSpawner.SpawnSpaceShip(this);
        }
        else
        {
            _shipMovement.gameObject.SetActive(true);
            ActualizeDestination(reachPlanet.gameObject);
        }
        
        // Generate Modules UI
        foreach (MISSION_SUBTYPE module in modules)
        {
            Debug.Log("module " + module);
            Debug.Log("type " + _typeManager.Instance);
            Debug.Log("sprite " + _typeManager.Instance.GetSpriteBySubType(module));
            _shipMovement.ElementUIInfo.AddIcon(_typeManager.Instance.GetSpriteBySubType(module));
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
        
        _shipMovement.gameObject.SetActive(false);

        RunDelayed(timeToAchieveTask, () =>
        {
            if (modules.Count > 0)
            {
                printer.Print(reachPlanet);
            }
            _spaceShipsEvent.ReceivePlanetData(reachPlanet);
            _shipMovement.gameObject.SetActive(true);
            SendToStation();
        });
    }

    private void SendToStation()
    {
        Debug.Log("send to station");
        ActualizeDestination(spawner.StationInstance);
    }
    
    public void ArriveOnStation()
    {
        Debug.Log("arrive on station");
        _hasBeenSend = false;
        _spacesipUI.color = new Color(_spacesipUI.color.r, _spacesipUI.color.g, _spacesipUI.color.b, 1f);
        _shipMovement.gameObject.SetActive(false);
        _spaceShipsEvent.ReturnSpaceShip(this);
        onReturnSpaceShip?.Invoke();
    }
    
    public void AddModule(MISSION_SUBTYPE module)
    {
        modules.Add(module);
        Debug.Log("add module " + module + " to space ship");
    }

    private void ActualizeDestination(GameObject destination)
    {
        _shipMovement.TimeToGo = (Vector3.Distance(_shipMovement.transform.position,destination.transform.position) / 2) / shipData.ShipSpeed;
        _shipMovement.GoToPlanet(destination);
    }
}
