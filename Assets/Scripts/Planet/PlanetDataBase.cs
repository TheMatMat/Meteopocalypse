using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetDataBase", menuName = "DataBase/PlanetDataBase")]
public class PlanetDataBase : ScriptableObject
{
    [SerializeField] List<PlanetData> _data = new List<PlanetData>();

    public List<PlanetData> Data { get { return _data; } }
}
