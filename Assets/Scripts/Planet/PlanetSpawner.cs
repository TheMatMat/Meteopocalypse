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

    [SerializeField] GameObject planetTemp;
    [SerializeField] GameObject SunTemp;

    private GameObject sun;


    private void Start()
    {
        if (pm == null)
            Debug.LogError("MISSING PLANET MANAGER");
    }

    [Button("SPAWN A Galaxy !")]
    public void SpawnGalaxy()
    {
        sun = Instantiate(SunTemp, new Vector3(0,0,0), Quaternion.identity, transform);

        int planetCount = Random.Range(pm.PlanetMinNB, pm.PlanetMaxNB);

        //Create Planets
        for (int i = 0; i < planetCount; i++)
        {
            float _x = Random.Range(safeRadius, 50);

            _x = _x > -safeRadius && _x < safeRadius ? Random.Range(safeRadius, 50) : _x;

            GameObject planetPrefab = Instantiate(planetTemp, new Vector3(_x, 0, 0), Quaternion.identity, transform);

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
            Debug.Log(planet.gameObject.name + " : " + z);


            Vector3 _pos = new Vector3(x, y, z);
            planet.transform.position = _pos;

            int rand = Random.Range(0, 2);
            //if (rand == 0)
            //    planet.transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            //planet.GetComponent<PlanetsPhysics>().startMoving = true;
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

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = UnityEngine.Color.red;
        foreach (Planet planet in pm.Planets)
        {
            float _pos = Vector3.Distance(sun.transform.position, planet.transform.position);
            Gizmos.DrawSphere(transform.position, _pos);
        }
    }
}
