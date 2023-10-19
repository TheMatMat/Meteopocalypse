using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship")]
public class SpaceShipData : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    // List de type de module 

    [SerializeField] private float shipSpeed;
    [SerializeField] private float minTimeToAchieveTask;
    [SerializeField] private float maxTimeToAchieveTask;

    [SerializeField] private int maxModule;

    [SerializeField] private GameObject shipModel;

    public Sprite Sprite
    {
        get => sprite;
    }

    public float ShipSpeed
    {
        get => shipSpeed;

    }

    public int MaxModule
    {
        get => maxModule;
    }

    public float  MinTimeToAchieveTask
    {
        get => minTimeToAchieveTask;
    }
    
    
    public float MaxTimeToAchieveTask
    {
        get => maxTimeToAchieveTask;
    }

    public GameObject ShipModel
    {
        get => shipModel;
    }
}
