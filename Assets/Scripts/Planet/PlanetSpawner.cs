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
    [SerializeField] private float safeRadius = 3.5f;

    [SerializeField] private PlanetManager pm;

    [SerializeField] GameObject planetPrefab;
    [SerializeField] GameObject SunTemp;
    [SerializeField] private GameObject station;

    private GameObject sun;


    private void Start()
    {
        if (pm == null)
            Debug.LogError("MISSING PLANET MANAGER");
        
        SpawnGalaxy();
    }

    [Button("SPAWN A Galaxy !")]
    private void SpawnGalaxy()
    {
        sun = Instantiate(SunTemp, new Vector3(0,0,0), Quaternion.identity, transform);

        int planetCount = Random.Range(pm.PlanetMinNB, pm.PlanetMaxNB);

        //Create Planets
        for (int i = 0; i < planetCount; i++)
        {
            float _x = Random.Range(safeRadius, 50);

            _x = _x > -safeRadius && _x < safeRadius ? Random.Range(safeRadius, 50) : _x;

            GameObject planetPrefab = Instantiate(this.planetPrefab, new Vector3(_x, 0, 0), Quaternion.identity, transform);


            //Assign a data
            Planet planet = planetPrefab.GetComponent<Planet>();
            //SetUpPlanet(planet);

            planet.Data = pm.PlanetDataBase.Data[i];
            planet.gameObject.name = planet.Data._name;

            //Add in the List
            pm.Planets.Add(planet);
        }

        //Randomize Planet Position
        StartCoroutine(RandomizePosition());

        float randomDistance = Random.Range(sun.transform.position.x,pm.Planets[pm.Planets.Count - 1].transform.position.x);
        float randomSideDistance = Random.Range(-randomDistance, randomDistance);
        
        Vector3 stationPosition = sun.transform.position;
        stationPosition.x += randomSideDistance;
        stationPosition.y += 20;

        Instantiate(station, stationPosition, Quaternion.identity, transform);
                
    }

    IEnumerator RandomizePosition()
    {
        yield return new WaitForSeconds(1f);
    
        foreach (Planet planet in pm.Planets)
        {
            float i = Random.Range(0, 2 * Mathf.PI);

            float x = Mathf.Cos(i) * Vector3.Distance(sun.transform.position, planet.transform.position) + transform.position.x;
            float y = sun.transform.position.y;
            float z = Mathf.Sin(i) * Vector3.Distance(sun.transform.position, planet.transform.position) + transform.position.z;


            Vector3 _pos = new Vector3(x, y, z);
            //transform.position = _pos;

            int rand = Random.Range(0, 2);
            if (rand == 0)
                planet.transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            
            planet.GetComponent<PlanetsPhysics>().startMoving = true;
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
}
