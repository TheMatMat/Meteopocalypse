using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] PlanetManager _planetManager;

    public GameObject _missionPrefab;
    [SerializeField] List<Mission> _missions = new List<Mission>();

    void Start()
    {

    }

    [Button("Create a Mission")]
    void CreateNewMission()
    {
        // random type and subtype
        int randomType = Random.Range(0, (int)MISSION_TYPE.NB_VALUES);
        int randomSubtype = Random.Range(randomType * 3, randomType * 3 + 2);

        //initalize mission
        GameObject missionPrefab = Instantiate(_missionPrefab, this.transform);
        Mission mission = missionPrefab.GetComponent<Mission>();

        mission.InitializeMission((MISSION_TYPE)randomType, (MISSION_SUBTYPE)randomSubtype);
        _missions.Add(mission);
        //register in the mission events
        mission.OnMissionDone += RemoveMission;
        mission.OnMissionTimeUp += RemoveMission;

        //pick a planet to link the mission
        Planet pickedPlanet = _planetManager.Planets[Random.Range(0, _planetManager.Planets.Count - 1)];
        pickedPlanet.AssignMission(mission);
        //register in the mission events
        mission.OnMissionDone += pickedPlanet.RemoveMission;
        mission.OnMissionTimeUp += pickedPlanet.RemoveMission;

        Debug.Log("New Mission: " + mission.Type + " - " + mission.Subtype + " on " + pickedPlanet.name);
    }

    void RemoveMission(int _id)
    {
        foreach(Mission mission in _missions.ToList())
        {
            if(mission.ID == _id)
                _missions.Remove(mission);
        }
    }
}
