using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class CareerStatisctics
    {
        public int TotalFightsFought;
        public int TotalWins;
        public int TotalLoss;
        public int TotalDraws;
    }

    public static GameManager Instance;

    public CareerStatisctics CareerStatistics { get; set; }
    public FightManager.Fighter CurrentFighter { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("GameManager already exists");
    }
    private void Start()
    {
        SaveManager.Instance.Load();
    }
}
