using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPosition : MonoBehaviour
{

    [SerializeField] Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        
    }
}
