using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionCounter : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private float satisfaction;

    public float Satisfaction
    {
        get => satisfaction;
        set
        {
            satisfaction = value;
            satisfaction = Mathf.Clamp(satisfaction, 0, maxSatisfaction);
        }
    }
    
    [SerializeField] private float maxSatisfaction;
    
    [Range(0,1)]
    [SerializeField] private float missionSucceedPercentage;
    [Range(0,1)]
    [SerializeField] private float missionFailedPercentage;
    
    [SerializeField] private MissionManager missionManager;
    
    

    private void Start()
    {
        satisfaction = maxSatisfaction;
        missionManager.OnMissionSucceed += OnMissionSucceed;
        missionManager.OnMissionFailed += OnMissionFailed;
        
        Debug.Log("maxSatis " + maxSatisfaction);
    }

    private void OnDestroy()
    {
        missionManager.OnMissionSucceed -= OnMissionSucceed;
        missionManager.OnMissionFailed -= OnMissionFailed;
    }

    private void Update()
    {
        float t = (satisfaction / maxSatisfaction);
        slider.value = t;
    }

    private void OnMissionSucceed() => Satisfaction += maxSatisfaction * missionSucceedPercentage;
    private void OnMissionFailed() => Satisfaction -= maxSatisfaction * missionFailedPercentage;
}
