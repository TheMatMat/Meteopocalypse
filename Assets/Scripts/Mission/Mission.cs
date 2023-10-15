using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum MISSION_TYPE
{
    MT_WEATHER,
    MT_MILITARY,
    MT_LOGISTIC,
    NB_VALUES
}

public enum MISSION_SUBTYPE
{
    MS_CLIMAT,
    MS_SANDSTORM,
    MS_POLUTION,

    MS_ARMY,
    MS_SPY,
    MS_DIPLOMAT,

    MS_RAW_MATERIALS,
    MS_INFRASTRUCTURES,
    MS_WORKFORCE,
    NB_VALUES
}

public class Mission : MonoBehaviour
{
    public MissionManager _missionManager;

    static int _missionCount = 0;

    [SerializeField] int _id;
    [SerializeField] string _headerText;
    [SerializeField] string _missionText;
    [SerializeField] float maxTime;
    [SerializeField] float _remainingTime; // in seconds
    [SerializeField] MISSION_TYPE _type;
    [SerializeField] MISSION_SUBTYPE _subtype;
    [SerializeField] Planet _planet;
    [SerializeField] Notification _notification;

    private bool _isFinished;

    public bool IsFinished
    {
        get => _isFinished;
        set => _isFinished = value;
    }

    //Properties
    public int ID { get { return _id; } }
    public string HeaderText { get; }
    public string MissionText { get { return _missionText; } }
    public MISSION_TYPE Type { get { return _type; } }
    public MISSION_SUBTYPE Subtype { get { return _subtype; } }
    public Planet Planet { get { return _planet; } set { _planet = value; } }
    public Notification Notification { get { return _notification; } set { _notification = value; } }

    public float MaxTime { get => maxTime; set => maxTime = value; }
    public float RemainingTime { get => _remainingTime; set => _remainingTime = value; }

    //Events
    public event Action<int> OnMissionDone;
    public event Action<int> OnMissionTimeUp;

    public void InitializeMission(MISSION_TYPE type, MISSION_SUBTYPE subtype, Planet planet,MissionManager missionManager)
    {
        //set mission id
        _id = _missionCount;
        _missionCount++;

        //set timer
        maxTime = Random.Range(40,50); // in seconds
        _remainingTime = maxTime;
        //set mission type and subtype
        _type = type;
        _subtype = subtype;
        _planet = planet;

        _missionManager = missionManager;
        //set the mission text
        _missionText = "Lorem ipsum dolores blabla ceci est une phrase random";
    }

    public void MissionDone()
    {
        CreateMissionResult(true);
        OnMissionDone(_id);
    }

    void Update()
    {
        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
        {
            CreateMissionResult(false);
            OnMissionTimeUp(_id);
            Destroy(this.gameObject);
        }
    }

    private void CreateMissionResult(bool isSucceed)
    {
        MissionResult missionResult = new MissionResult(this,isSucceed);
        _missionManager.DailyMissions.Add(missionResult);
    }
}

