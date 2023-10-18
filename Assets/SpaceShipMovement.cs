using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : CoroutineSystem
{

    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private float timeToGo;

   
    [SerializeField] private UISystemElementInfo elementUIInfo;

    public UISystemElementInfo ElementUIInfo
    {
        get => elementUIInfo;
        set => elementUIInfo = value;
    }

    private float _timer;

    public float TimeToGo
    {
        get => timeToGo;
        set => timeToGo = value;
    }

    public SpaceShip SpaceShip
    {
        get => spaceShip;
        set => spaceShip = value;
    }

    private bool _isMoving;

    void Update()
    {
        if (_isMoving)
        {
        }

        elementUIInfo.SetText(((int)_timer).ToString());
        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0, TimeToGo);
        
    }

    public void GoToPlanet()
    {
        transform.DOLocalMove(new Vector3(0,0,0), timeToGo);
        _timer = timeToGo;
        _isMoving = true;
        
        RunDelayed(timeToGo,() =>
        {
            if (transform.parent.GetComponent<Planet>() != null)
            {
                spaceShip.ArriveOnPlanet();
            }
            else
            {
                spaceShip.ArriveOnStation();
            }

            _isMoving = false;
        });
    }

}
