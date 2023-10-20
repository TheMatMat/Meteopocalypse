using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : CoroutineSystem
{

    [SerializeField] private DayState state;
    [SerializeField] private int dayCount;
    [SerializeField] private float missionPerDay;
    [SerializeField] private float dayMissionAmplifier;

    [SerializeField] private MissionManager missionManager;
    [SerializeField] private PlanetManager  planetManager;

    [SerializeField] private GameObject endDayMenu;

    [SerializeField] private Slider satisfactionSlider;
    
    public int DayCount
    {
        get => dayCount;
    }
    
    public DayState State
    {
        get => state;
        set
        {
            state = value;

            switch (state)
            {
                case DayState.PRE_DAY:
                    planetManager.ResetGalaxy();
                    endDayMenu.SetActive(false);
                    planetManager.NewPlanetSystem();
                    missionManager.DailyMissions.Clear();
                    EventsDispatcher.Instance.DayGenerate();
                    State = DayState.IN_DAY;
                    break;
                
                case  DayState.IN_DAY:
                    StartCoroutine(missionManager.WaitNextMission());
                    break;
            }
        }
    }

    public int MissionPerDay
    {
        get => (int)missionPerDay;
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
        if(missionManager.DailyMissions.Count == (int)missionPerDay)
        {
            // Change day
            Debug.Log("change day");
            dayCount++;
            missionPerDay +=  missionPerDay * dayMissionAmplifier;
            endDayMenu.transform.GetChild(2).GetComponent<NumberSpriteCreator>().Number = missionManager.DailyMissions.Where(mission => mission.isSuccess).ToList().Count;
            endDayMenu.transform.GetChild(3).GetComponent<NumberSpriteCreator>().Number =  missionManager.DailyMissions.Where(mission => !mission.isSuccess).ToList().Count;
            endDayMenu.transform.GetChild(4).GetComponent<NumberSpriteCreator>().Number = (int)(satisfactionSlider.value * 100);
            
            missionManager.AllDayMissions.Clear();
            missionManager.ClearMission();
          //  spawner.ResetGalaxy();
            State = DayState.SUMMARY;
            
            endDayMenu.SetActive(true);

            RunDelayedInput(() =>
            {
                State = DayState.PRE_DAY;
            });
        }
    }

}
