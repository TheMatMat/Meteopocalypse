using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class MissionManager : MonoBehaviour
{
    [SerializeField] PlanetManager _planetManager;
    [SerializeField] NotificationManager _notificationManager;

    public GameObject _missionPrefab;
    [SerializeField] List<Mission> _missions = new List<Mission>();
    [SerializeField] private int maxMissionSimultaneously;
    [SerializeField] private float minNewMissionCooldown;
    [SerializeField] private float maxNewMissionCooldown;
    [SerializeField] private float minMissionDuration;
    [SerializeField] private float maxMissionDuration;
    [SerializeField] private List<MissionResult> dailyMissions = new List<MissionResult>();
    [SerializeField] private DayManager dayManager;

    public List<Mission> Missions
    {
        get => _missions;
    }

    public List<MissionResult> DailyMissions
    {
        get => dailyMissions;
    }

    public float MinMissionDuration
    {
        get => minMissionDuration;
    }

    public float MaxMissionDuration
    {
        get => maxMissionDuration;
    }

    public event Action OnMissionFinished;
    public event Action OnMissionFailed;
    public event Action OnMissionSucceed;


    void Start()
    {
        StartCoroutine(WaitNextMission());
    }

    [Button("Create a Mission")]
    void CreateNewMission()
    {
        if (dayManager.State != DayState.IN_DAY)
        {
            return;   
        }
        
        if(_missions.Count > maxMissionSimultaneously)
        {
            return;
        }

        if (_missions.Count > dayManager.MissionPerDay)
        {
            return;
        }
        
        // random type and subtype
        int randomType = UnityEngine.Random.Range(0, (int)MISSION_TYPE.NB_VALUES);
        int randomSubtype = UnityEngine.Random.Range(randomType * 3, randomType * 3 + 2);

        //initalize mission
        GameObject missionPrefab = Instantiate(_missionPrefab, this.transform);
        Mission mission = missionPrefab.GetComponent<Mission>();

        //register in the mission events
        mission.OnMissionDone += RemoveMission;
        mission.OnMissionTimeUp += RemoveMission;

        //pick a planet to link the mission and ensure it has less than 2 missions

        List<Planet> availablePlanets = new List<Planet>(_planetManager.Planets);

        Planet pickedPlanet = availablePlanets[UnityEngine.Random.Range(0, availablePlanets.Count)];

        while(pickedPlanet.Missions.Count >= 2)
        {
            if (availablePlanets.Count == 0)
            {
                return;
            }

            availablePlanets.Remove(pickedPlanet);
            pickedPlanet = availablePlanets[UnityEngine.Random.Range(0, availablePlanets.Count)];
        }

        pickedPlanet.AssignMission(mission);

        //register in the mission events
        mission.OnMissionDone += pickedPlanet.RemoveMission;
        mission.OnMissionTimeUp += pickedPlanet.RemoveMission;

        mission.InitializeMission((MISSION_TYPE)randomType, (MISSION_SUBTYPE)randomSubtype, pickedPlanet,this);
       // mission.MaxTime = UnityEngine.Random.Range(minMissionDuration, maxMissionDuration);

        _missions.Add(mission);

        //assign the notification
        mission.Notification = _notificationManager.NewNotificaiton(mission);

        Debug.Log("New Mission: " + mission.Type + " - " + mission.Subtype + " on " + pickedPlanet.name);

        StartCoroutine(WaitNextMission());
    }


    private IEnumerator WaitNextMission()
    {
        float randomTime = UnityEngine.Random.Range(minNewMissionCooldown, maxNewMissionCooldown);

        yield return new WaitForSeconds(randomTime);

        CreateNewMission();
    }

    void RemoveMission(int _id)
    {
        if (GetMissionById(_id) != null)
        {
            return;
        }
        
        if (GetMissionById(_id).IsFinished)
        {
            OnMissionSucceed?.Invoke();
        }
        else
        {
            OnMissionFailed?.Invoke();
        }
        
        foreach(Mission mission in _missions.ToList())
        {
            if(mission.ID == _id)
                _missions.Remove(mission);
        }
        
        OnMissionFinished?.Invoke();
        
    }

    private Mission GetMissionById(int id)
    {
        foreach (Mission mission in _missions)
        {
            if (mission.ID == id)
            {
                return mission;
            }
        }

        return null;
    }

    public void ClearMission()
    {
        _missions.Clear();
    }
}
