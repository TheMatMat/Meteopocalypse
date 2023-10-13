using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(PlanetManager))]
public class PlanetSpawner : MonoBehaviour
{
    [Header("Spawn Prameters")]
    [SerializeField] private float safeRadius = 3.5f;
    [SerializeField] private float maxSpawnDistance = 50;
    [Range(0.5f, 10)]
    [SerializeField] private float minDistBtwPlanet = .5f;

    [Header("Components")]
    [SerializeField] private PlanetManager pm;

    [SerializeField] GameObject planetPrefab;
    [SerializeField] GameObject planetTemp;
    [SerializeField] GameObject SunTemp;
    [SerializeField] private GameObject station;

    private GameObject sun;

    private GameObject stationInstance;

    public GameObject Station
    {
        get => stationInstance;
    }


    private void Start()
    {
        if (pm == null)
            Debug.LogError("MISSING PLANET MANAGER");
        
        SpawnGalaxy();
    }

    [Button("SPAWN A Galaxy !")]
    public void SpawnGalaxy()
    {
        sun = Instantiate(SunTemp, new Vector3(0,0,0), Quaternion.identity, transform);

        int planetCount = Random.Range(pm.PlanetMinNB, pm.PlanetMaxNB);

        //Create Planets
        for (int i = 0; i < planetCount; i++)
        {
            float _x = Random.Range(safeRadius, maxSpawnDistance);
            _x = _x > -safeRadius && _x < safeRadius ? Random.Range(safeRadius, maxSpawnDistance) : _x;

            GameObject planetPrefab = Instantiate(planetTemp, new Vector3(_x, 0, 0), Quaternion.identity, transform);

            SphereCollider col = planetPrefab.AddComponent<SphereCollider>();
            col.radius = minDistBtwPlanet;
            col.isTrigger = true;

            //Assign a data
            Planet planet = planetPrefab.GetComponent<Planet>();
            //SetUpPlanet(planet);

            planet.Data = pm.PlanetDataBase.Data[i];
            planet.gameObject.name = planet.Data._name;

            //Add in the List
            pm.Planets.Add(planet);

            Destroy(col);
        }

        //Randomize Planet Position
        StartCoroutine(RandomizePosition());

        float randomDistance = Random.Range(sun.transform.position.x,pm.Planets[pm.Planets.Count - 1].transform.position.x);
        float randomSideDistance = Random.Range(-randomDistance, randomDistance);
        
        Vector3 stationPosition = sun.transform.position;
        stationPosition.x += randomSideDistance;
        stationPosition.y += 20;

        stationInstance = Instantiate(station, stationPosition, Quaternion.identity, transform);
                
    }

    IEnumerator RandomizePosition()
    {
        yield return new WaitForSeconds(1f);
    
        foreach (Planet planet in pm.Planets)
        {

            float theta = Random.Range(0, 360);

            //CX + (r * cos(theta))
            float x = sun.transform.position.x + (Vector3.Distance(sun.transform.position, planet.transform.position) * Mathf.Cos(theta));
            float y = sun.transform.position.y;
            //Cy + (r * Sin(theta))
            float z = sun.transform.position.y + (Vector3.Distance(sun.transform.position, planet.transform.position) * Mathf.Sin(theta));


            Vector3 _pos = new Vector3(x, y, z);
            planet.transform.position = _pos;
        }
        Debug.Log("Radomize Position");
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

    [Button("SPIN ME ROUND")]
    private void SpinPlanets()
    {
        foreach (Planet planet in pm.Planets)
        {
           planet.GetComponent<PlanetsPhysics>().startMoving = true;
        }
    }
}
