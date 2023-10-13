using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlanetManager))]
public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private float safeRadius = 3.5f;

    [SerializeField] private PlanetManager pm;

    [SerializeField] GameObject planetTemp;
    [SerializeField] GameObject SunTemp;

    private GameObject sun;


    private void Start()
    {
        if (pm == null)
            Debug.LogError("MISSING PLANET MANAGER");
    }

    [Button("SPAWN A Galaxy !")]
    private void SpawnGalaxy()
    {
        sun = Instantiate(SunTemp, new Vector3(0,0,0), Quaternion.identity, transform);

        int planetCount = Random.Range(pm.PlanetMinNB, pm.PlanetMaxNB);

        //Create Planets
        for (int i = 0; i < planetCount; i++)
        {
            int _x = Random.Range(-50, 50);
            int _z = Random.Range(-50, 50);

            _x = _x > -safeRadius && _x < safeRadius ? Random.Range(-50, 50) : _x;
            _z = _z > -safeRadius && _x < safeRadius ? Random.Range(-50, 50) : _z;

            GameObject planetPrefab = Instantiate(planetTemp, new Vector3(_x, 0, _z), Quaternion.identity, transform);

            //Assign a data
            Planet planet = planetPrefab.GetComponent<Planet>();
            SetUpPlanet(planet);

            //planet.Data = _planetDataBase.Data[i];
            //planet.gameObject.name = planet.Data._name;

            //Add in the List
            pm.Planets.Add(planet);
        }
    }

    private void SetUpPlanet(Planet _planetToSetUp)
    {
        _planetToSetUp.Data._name = Time.time.ToString();
        _planetToSetUp.SpinSpeed = Random.Range(5,25);
    }

    [Button("Reset the Galaxy !")]
    private void ResetGalaxy()
    {
        for (int i = 0; i < pm.Planets.Count; i++)
        {
            Destroy(pm.Planets[i].gameObject);
        }
        pm.Planets.Clear();
        Destroy(sun);
    }
}
