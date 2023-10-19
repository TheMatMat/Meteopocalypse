using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{

    [SerializeField] private DayState state;
    [SerializeField] private int dayCount;
    [SerializeField] private int missionPerDay;

    [SerializeField] private MissionManager missionManager;
    [SerializeField] private PlanetSpawner spawner;

    public DayState State
    {
        get => state;
    }

    public int MissionPerDay
    {
        get => missionPerDay;
    }

    private void Start()
    {
        missionManager.OnMissionFinished += OnMissionFinished;
        EventsDispatcher.Instance.DayGenerate();
    } 
    private void OnDestroy() => missionManager.OnMissionFinished -= OnMissionFinished;
    

    private void Update()
    {
        switch(state)
        {
            case DayState.PRE_DAY:
                break;

            case DayState.IN_DAY:
                break;

            case DayState.SUMMARY:
                break;
        }
    }

    private void OnMissionFinished()
    {
        if(missionManager.DailyMissions.Count == missionPerDay)
        {
            // Change day
            Debug.Log("change day");
            dayCount++;
            missionManager.ClearMission();
            spawner.ResetGalaxy();
            state = DayState.SUMMARY;
        }
    }

}
