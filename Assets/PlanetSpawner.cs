using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [Button("SPAWN A PLANET !", ButtonSizes.Gigantic)]
    private void SpawnPlanet()
    {
        Debug.Log("SPAWN PLANET");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
