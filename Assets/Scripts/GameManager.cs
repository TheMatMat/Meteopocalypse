using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : CoroutineSystem
{

    private bool _isGameEnd;

    public bool IsGameEnd
    {
        get => _isGameEnd;
    }
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private DayManager dayManager;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Death()
    {
        _isGameEnd = true;
        EventsDispatcher.Instance.GameOver();
        deathMenu.SetActive(true);
        deathMenu.transform.GetChild(2).GetComponent<NumberSpriteCreator>().Number = dayManager.DayCount + 1;
        RunDelayedInput(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
