using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float _remainingTime; // in seconds
    [SerializeField] MISSION_TYPE _type;
    [SerializeField] MISSION_SUBTYPE _subtype;

    //Properties
    public int ID { get { return _id; } }
    public MISSION_TYPE Type { get { return _type; } }
    public MISSION_SUBTYPE Subtype { get { return _subtype; } }

    //Events
    public event Action<int> OnMissionDone;
    public event Action<int> OnMissionTimeUp;

    public void InitializeMission(MISSION_TYPE type, MISSION_SUBTYPE subtype)
    {
        //set mission id
        _id = _missionCount;
        _missionCount++;

        //set timer
        _remainingTime = UnityEngine.Random.Range(10f, 20f); // in seconds

        //set mission type and subtype
        _type = type;
        _subtype = subtype;
    }

    void Update()
    {
        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
        {
            OnMissionTimeUp(_id);
            Destroy(this.gameObject);
        }
    }
}

