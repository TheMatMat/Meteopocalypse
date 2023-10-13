using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    [SerializeField] private PlanetManager pm;
    [SerializeField] private GameObject vaisseau;

    [Button("Spawn")]
    private void spawn()
    {
        //Spawn un vaisseau à la position de la station puis le mets en enfant de la planètes objectif
        GameObject _p = pm.Planets[Random.Range(0, 5)].gameObject;

        GameObject _t = Instantiate(vaisseau, transform);
        _t.transform.parent = _p.transform;

        _t.GetComponent<SpaceShipMovement>().GoToPlanet();
    }
}
