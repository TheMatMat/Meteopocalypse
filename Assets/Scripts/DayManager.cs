using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{

    [SerializeField] private DayState state;
    [SerializeField] private int dayCount;
    [SerializeField] private int missionPerDay;

    [SerializeField] private MissionManager missionManager;

    private void Start()
    {
        missionManager.OnMissionFinished += OnMissionFinished;
    }

    private void OnDestroy()
    {
        missionManager.OnMissionFinished -= OnMissionFinished;
    }

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
        if(missionManager.Missions.Count == missionPerDay)
        {
            // Change day
            dayCount++;
            missionManager.ClearMission();
            state = DayState.SUMMARY;
        }
    }

}
