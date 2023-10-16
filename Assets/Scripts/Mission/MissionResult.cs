using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MissionResult
{

    public int _id;
    public MISSION_TYPE _type;
    public MISSION_SUBTYPE _subtype;
    public Planet _planet;
    public bool isSuccess;

    public MissionResult(Mission m, bool isSuccess)
    {
        _id = m.ID;
        _type = m.Type;
        _subtype = m.Subtype;
        _planet = m.Planet;
        this.isSuccess = isSuccess;
    }
}
