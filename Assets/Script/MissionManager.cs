using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionManager _missionManager;
    [SerializeField] MissionManagerReference _missionManagerReference;
    ISet<MissionManager> RealRef => _missionManagerReference;

    [SerializeField] int maxMission;
    public int MaxMission { get; set; }

    [SerializeField] List<Mission> _missionInProgress = new List<Mission>();
    public List<Mission> MissionInProgress { get { return _missionInProgress; } set { _missionInProgress = value; } }


    void Awake()
    {
        RealRef.Set(_missionManager);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NewMission();

        //decrement timer for all the missions
        if(MissionInProgress.Count > 0)
        {
            foreach (Mission mission in MissionInProgress.ToList())
            {
                mission.Timer -= Time.deltaTime;
                //remove mission if counter down to 0
                if (mission.Timer <= 0)
                {
                    MissionInProgress.Remove(mission);
                }
            }
        }
    }

    private Mission NewMission()
    {
        if (MissionInProgress.Count >= maxMission)
            return null;

        //pick a random mission type and subtype
        int randomMissionType = Random.Range(0, System.Enum.GetValues(typeof(MISSION_TYPE)).Length);
        int randomSubmissionType = Random.Range(randomMissionType * 3, randomMissionType * 3 + 2);

        //Initialize Mission data
        Mission newMission = new Mission(_missionManagerReference, randomMissionType, randomSubmissionType);
        MissionInProgress.Add(newMission);

        return newMission;
    }

    public void RemoveMission(int id)
    {

    }
}
