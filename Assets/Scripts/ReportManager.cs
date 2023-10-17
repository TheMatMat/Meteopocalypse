using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReportManager : CoroutineSystem
{


    [SerializeField] private SpaceShipsEventRef shipEvents;

    [SerializeField] private GameObject reportGO;
    [SerializeField] private float reportTimer;

    private void Start()
    {
        shipEvents.Instance.OnReceivePlanetData += OnReceiveData;
        reportGO.SetActive(false);
    }

    private void OnDestroy()
    {
        shipEvents.Instance.OnReceivePlanetData -= OnReceiveData;
    }

    private void OnReceiveData(Planet planet)
    {

        if (planet.Missions.Count == 0)
        {
            return;
        }
        
        reportGO.SetActive(true);

        string reportText = "Report of planet : \n" +
                            planet.Data._name +
                            "\n \n Coordinates " +
                            planet.planetCoordinates.ToString() +
                            "\n \n Analysis Results : \n";

        foreach (Mission mission in planet.Missions)
        {
            reportText += " - " + mission.Subtype + " \n ";
        }

        reportGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = reportText;

        RunDelayed(reportTimer, () =>
        {
            reportGO.SetActive(false);
        });
    }
}
