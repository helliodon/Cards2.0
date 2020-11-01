using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Canvas;

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
        SetScreenResolution();
        SaveManager.Instance.Load();
    }

    private void SetScreenResolution()
    {
        if(Canvas != null)
        {
            CanvasScaler cs = Canvas.GetComponent<CanvasScaler>();
            if(cs != null)
            {
                cs.referenceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
                Debug.Log("Screen resolution set to :" + Screen.currentResolution.width.ToString() + " height, " + Screen.currentResolution.height + " width");
            }
        }
    }
}
