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
        deathMenu.SetActive(true);
        deathMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = dayManager.DayCount.ToString();
        RunDelayedInput(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
