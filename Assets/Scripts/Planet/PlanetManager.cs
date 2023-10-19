using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetManager : MonoBehaviour
{
    [Header("DataBase")]
    [SerializeField] private PlanetDataBase _planetDataBase;
    public PlanetDataBase PlanetDataBase => _planetDataBase;

    [Header("Infos")]
    [SerializeField] GameObject _planetPrefab;
    [SerializeField] private int _planetMinNB = 1;
    public int PlanetMinNB => _planetMinNB;
    [SerializeField] private int _planetMaxNB = 4;
    public int PlanetMaxNB => _planetMaxNB;

    [Header("Runtime")]
    [SerializeField] List<Planet> _planets = new List<Planet>();

    [SerializeField] private int minDistancePlanet;
    [SerializeField] private int maxDistancePlanet;
    
    [SerializeField] GameObject sun;
    
    public List<Planet> Planets { get { return _planets; } }
    
    
    [SerializeField] private GameObject station;   

    private GameObject _stationInstance;

    public GameObject StationInstance
    {
        get => _stationInstance;
    }

    [SerializeField] private GameObject planetOrbit;

    public GameObject sunInstance;

    // Start is called before the first frame update
    void Awake()
    {
        //Security if we don't have enough planet in the database
        if (_planetMinNB > _planetDataBase.Data.Count)
            _planetMinNB = _planetDataBase.Data.Count;

        if (_planetMaxNB > _planetDataBase.Data.Count)
            _planetMaxNB = _planetDataBase.Data.Count;

        NewPlanetSystem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPlanetSystem()
    {
        int planetCount = Random.Range(_planetMinNB, _planetMaxNB);

        sunInstance = Instantiate(sun, new Vector3(0,0,0), Quaternion.identity, transform);
        sunInstance.GetComponent<Planet>().planetCoordinates = new PlanetCoordinates(0000, 0000);
        int lastDistance = 5;
        
        //Create Planets
        for(int i = 0; i < planetCount; i++)
        {
            GameObject planetPrefab = Instantiate(_planetPrefab, this.transform);

            int randomDistance = Random.Range(minDistancePlanet, maxDistancePlanet + 1);
            randomDistance += lastDistance;

            int randomAngle = Random.Range(0, 360);

            Vector2 position = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomDistance;
            planetPrefab.transform.position = new Vector3(position.x, 0, position.y);
            
            Planet planet = planetPrefab.GetComponent<Planet>();
            PlanetsPhysics physics = planetPrefab.GetComponent<PlanetsPhysics>();

            physics.Angle = randomAngle;
            physics.Distance = randomDistance;

            //pick a random planet data
            int planetDataIndex = _planetDataBase.Data[Random.Range(0, _planetDataBase.Data.Count)]._id;

            planet.Data = _planetDataBase.Data[i];
            planet.gameObject.name = planet.Data._name;

            //Add in the List
            _planets.Add(planet);
            
            GameObject orbit = Instantiate(planetOrbit,transform);
            planet.Orbit = orbit;
            orbit.name = "Orbit of " + planet.name;

            PlanetsPhysics orbitPhysics = orbit.GetComponent<PlanetsPhysics>();
            orbitPhysics.Angle = randomAngle;
            orbitPhysics.Distance = randomDistance;
            
           lastDistance = randomDistance;
        }
        
        float randomStationDistance = Random.Range(sunInstance.transform.position.x,lastDistance);
        float randomSideDistance = Random.Range(-randomStationDistance, randomStationDistance);
        
        Vector3 stationPosition = sunInstance.transform.position;
        stationPosition.x += randomSideDistance;
        stationPosition.y += 20;

        if (_stationInstance == null)
        {
            _stationInstance = Instantiate(station, stationPosition, Quaternion.Euler(-90,0,0), transform);  
            _stationInstance.SetActive(true);
        }
        else
        {
            _stationInstance.transform.position = stationPosition;
            station.SetActive(true);
        }
        
        
      
    }

    public void ResetGalaxy()
    {
        for (int i = 0; i < _planets.Count; i++)
        {
            Destroy(_planets[i].Orbit.gameObject);
            Destroy(_planets[i].gameObject);
            _planets.Remove(_planets[i]);
        }
        
        Destroy(sunInstance);
    }
}
