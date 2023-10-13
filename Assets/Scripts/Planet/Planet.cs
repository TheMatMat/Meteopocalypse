using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    
    


    public PlanetCoordinates planetCoordinates;


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

    public void ReceiveSonde(SpaceShip spaceShip)
    {
        foreach(Mission mission in _missions)
        {
            Debug.Log(mission.Subtype);
        }

        DoMission(spaceShip);
    }

    public void ReceiveSpaceShip(SpaceShip spaceShip)
    {
        foreach(Mission mission in _missions.ToList())
        {
            foreach(MISSION_SUBTYPE subtype in spaceShip.Modules.ToList())
            {
                if(mission.Subtype == subtype)
                {
                    _missions.Remove(mission);
                    spaceShip.Modules.Remove(subtype);
                    Debug.Log("Mission done");
                    mission.MissionDone();
                }
            }
        }
        
        DoMission(spaceShip);
    }

    private void DoMission(SpaceShip spaceShip)
    {
        RunDelayed(spaceShip.TimeToAchieveTask, () =>
        {
            
        });
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
