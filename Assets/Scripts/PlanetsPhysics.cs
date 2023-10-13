using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsPhysics : MonoBehaviour
{
    private int spinSpeed;
    public bool startMoving = false;

    public int speedBounds;

    // Start is called before the first frame update
    void Start()
    {
        spinSpeed = Random.Range(-speedBounds, speedBounds);
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
            transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), spinSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
//        Debug.Log("PLANET ONE ON ANOTHER");

        float _x = transform.position.x;
        _x = Random.Range(3.5f, 50);
        transform.position = new Vector3(_x, transform.position.y, transform.position.z);
    }
}
