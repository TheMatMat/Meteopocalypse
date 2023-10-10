using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum MISSION_TYPE
{
    MISSION_TYPE_WEATHER,
    MISSION_TYPE_MILITARY,
    MISSION_TYPE_LOGISTICS,
}

public enum MISSION_SUBTYPE
{
    MISSION_TYPE_WEATHER_SANDSTORM,
    MISSION_TYPE_WEATHER_CLIMAT,
    MISSION_TYPE_WEATHER_POLLUTION,


    MISSION_TYPE_MILITARY_ARMY,
    MISSION_TYPE_WEATHER_DIPLOMAT,
    MISSION_TYPE_WEATHER_SPY,

    MISSION_LOGITIC_RAW_MATERIALS,
    MISSION_LOGISTIC_WORKFORCE,
    MISSION_LOGISTIC_INFRASTRUCTURES,
}

[System.Serializable]
public class Mission
{
    [SerializeField] MissionManager _missionManager;

    static int _nbMission;
    [SerializeField] int _missionId;
    [SerializeField] float _timer;
    [SerializeField] Planet _planet;
    [SerializeField] MISSION_TYPE _missionType;
    [SerializeField] MISSION_SUBTYPE _missionSubtype;

    public int MissionId{ get { return _missionId; } private set { _missionId = value;  } }
    public float Timer { get { return _timer; } set { _timer = value;  } }

    public Mission(MissionManagerReference missionManager, int missionType, int missionSubtype)
    {
        //set id
        _missionId = _nbMission;
        _nbMission++;

        //reference to the mission manager
        _missionManager = missionManager.Instance;

        _timer = 5; //s
        _missionType = (MISSION_TYPE)missionType;
        _missionSubtype = (MISSION_SUBTYPE)missionSubtype;
    }
}
