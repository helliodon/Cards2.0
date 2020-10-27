using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
