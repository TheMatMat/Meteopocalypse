using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [Header("DataBase")]
    [SerializeField] PlanetDataBase _planetDataBase;

    [Header("Infos")]
    [SerializeField] GameObject _planetPrefab;
    [SerializeField] int _planetMinNB = 1;
    [SerializeField] int _planetMaxNB = 4;

    [Header("Runtime")]
    [SerializeField] List<Planet> _planets = new List<Planet>();
    public List<Planet> Planets { get { return _planets; } }

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

        //Create Planets
        for(int i = 0; i < planetCount; i++)
        {
            GameObject planetPrefab = Instantiate(_planetPrefab, this.transform);
            //Assign a data
            Planet planet = planetPrefab.GetComponent<Planet>();
            planet.Data = _planetDataBase.Data[i];
            planet.gameObject.name = planet.Data._name;
            //Add in the List
            _planets.Add(planet);
        }
    }
}
