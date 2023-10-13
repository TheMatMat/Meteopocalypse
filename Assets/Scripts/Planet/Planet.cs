using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] PlanetData _data;
    public PlanetData Data { get { return _data; } set { _data = value;  } }

    [SerializeField] List<Mission> _missions = new List<Mission>();

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

    void ReceiveSonde()
    {

    }

    void ReceiveSpaceShip()
    {

    }
}

[System.Serializable]
public class PlanetData
{
    public string _name;
    public PlanetCoordinates _planetCoordinates;
    public Sprite _sprite;
    public GameObject _gameObject;
}

public struct PlanetCoordinates
{
    int _latitude;
    int _longitude;
}