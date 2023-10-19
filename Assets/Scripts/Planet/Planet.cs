using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Planet : CoroutineSystem
{
    [SerializeField] PlanetData _data;
    public PlanetData Data { get { return _data; } set { _data = value;  } }

    [SerializeField] List<Mission> _missions = new List<Mission>();

    [SerializeField] int distanceToStation;

    public int DistanceToStation
    {
        get => distanceToStation;
    }

    public List<Mission> Missions
    {
        get => _missions;
    }

    [SerializeField] private float rotateSpeed;

    [SerializeField] private UISystemElementInfo planetElementUIInfo;

    public PlanetCoordinates planetCoordinates;

    private GameObject _orbit;

    public GameObject Orbit
    {
        get => _orbit;
        set => _orbit = value;
    }

    private void Awake()
    {
        distanceToStation = Random.Range(500,2000);
        PlanetCoordinates coordinates = new PlanetCoordinates(Random.Range(1000,9999),Random.Range(1000,9999));
        planetCoordinates = coordinates;
    }

    private void Start()
    {
        GetComponent<MeshFilter>().mesh = _data._planetMesh;
        GetComponent<MeshRenderer>().material = _data._planetMat;

        if (planetElementUIInfo != null)
        {
            planetElementUIInfo.SetText(_data._name);
        }
    }

    private void Update()
    {
        transform.Rotate(0,rotateSpeed * Time.deltaTime,0);
    }

    public void AssignMission(Mission mission)
    {
        _missions.Add(mission);
    }

    public void RemoveMission(int _id)
    {
        foreach (Mission mission in _missions.ToList())
        {
            if (mission.ID == _id)
                _missions.Remove(mission);
        }
    }

    public List<string> ReceiveSonde()
    {
        List<string> returnResults = new List<string>();
        
        foreach(Mission mission in _missions)
        {
            Debug.Log("sonde " + mission.Subtype);
            returnResults.Add(mission.MissionText);
        }

        return returnResults;
    }

    public void ReceiveSpaceShip(SpaceShip spaceShip)
    {
        foreach(Mission mission in _missions.ToList())
        {
            foreach(MISSION_SUBTYPE subtype in spaceShip.Modules.ToList())
            {
                if(mission.Subtype == subtype && !mission.IsFinished)
                {
                    mission.IsFinished = true;
                    Debug.Log("Mission done");
                    mission.MissionDone();
                }
            }
        }
    }

}

[System.Serializable]
public class PlanetData
{
    public int _id;
    public string _name;
    public Sprite _sprite;
    public Mesh _planetMesh;
    public Material _planetMat;
}

[System.Serializable]
public struct PlanetCoordinates
{
    public int _latitude;
    public int _longitude;

    public PlanetCoordinates(int latitude, int longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public string ToString() => _latitude + "-" + _longitude;
}
