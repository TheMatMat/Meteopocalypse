using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetsPhysics : MonoBehaviour
{
    [SerializeField] private int rotateSpeed;

    public int RotateSpeed
    {
        get => rotateSpeed;
        set
        {
            int pendingSpeed = value;
            
            while (pendingSpeed == 0)
            {
                pendingSpeed = Random.Range(minRotateSpeed,maxRotateSpeed + 1);
            }

            rotateSpeed = pendingSpeed;
        }
    }

    [SerializeField] private float angle;

    public float Angle
    {
        get => angle;
        set => angle = value;
    }

    [SerializeField] private float _distance;

    public float Distance
    {
        get => _distance;
        set => _distance = value;
    }

    [SerializeField] private int minRotateSpeed;
    [SerializeField] private int maxRotateSpeed;

    [SerializeField] private TrailRenderer orbit;

    private void Start()
    {
        RotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed + 1);
    }

    void Update()
    {
        if (orbit != null)
        {
            orbit.enabled = true;
        }
        
        angle += Time.deltaTime * rotateSpeed;
        
        Vector2 position = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * _distance;
        transform.position = new Vector3(position.x,transform.position.y,position.y);
    }

}
