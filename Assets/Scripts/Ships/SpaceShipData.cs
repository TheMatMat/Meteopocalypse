using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship")]
public class SpaceShipData : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    // List de type de module 

    [SerializeField] private float shipSpeed;
    [SerializeField] private float maxReachDistance;

    [SerializeField] private int maxModule;

    public Sprite Sprite
    {
        get => sprite;
    }

    public float ShipSpeed
    {
        get => shipSpeed;
    }

    public float MaxReachDistance
    {
        get => maxReachDistance;
    }

    public int MaxModule
    {
        get => maxModule;
    }
}
