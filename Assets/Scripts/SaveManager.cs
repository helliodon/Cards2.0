using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public enum PlayerPrefsKey
    {
        CurrentFighter,
        CareerStatisctics
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("SaveManager already exists");
    }

    public void Save()
    {
        if (GameManager.Instance.CurrentFighter != null) 
        {
            string serializedFileId = JsonUtility.ToJson(GameManager.Instance.CurrentFighter);

            if(!string.IsNullOrEmpty(serializedFileId))
                PlayerPrefs.SetString(PlayerPrefsKey.CurrentFighter.ToString(), serializedFileId);
            Debug.Log("Current Fighter saved");
            return;
        }
        Debug.Log("Failed to save: Current Fighter is null");
    }

    public FightManager.Fighter CreateNewFighter(string name, int level = 0)
    {
        if (!string.IsNullOrEmpty(name))
        {
            FightManager.Fighter newFighter = new FightManager.Fighter();
            newFighter.Name = name;
            Debug.Log("New fighter with " + name + " name is created");
            return newFighter;
        }
        return null;
    }
    public void Load()
    {
        string serializedFileId = string.Empty;
        if (PlayerPrefs.HasKey(PlayerPrefsKey.CurrentFighter.ToString()))
        {
            serializedFileId = PlayerPrefs.GetString(PlayerPrefsKey.CurrentFighter.ToString());
            if (!string.IsNullOrEmpty(serializedFileId))
            {
                GameManager.Instance.CurrentFighter = JsonUtility.FromJson<FightManager.Fighter>(serializedFileId);
                Debug.Log("Current Fighter with " + GameManager.Instance.CurrentFighter.Name + " name is loaded.");
            }
        }
        else
            Debug.Log("Failed to load: hasn't key : " + PlayerPrefsKey.CurrentFighter.ToString());

        serializedFileId = string.Empty;
        if (PlayerPrefs.HasKey(PlayerPrefsKey.CareerStatisctics.ToString()))
        {
            serializedFileId = PlayerPrefs.GetString(PlayerPrefsKey.CareerStatisctics.ToString());
            if (!string.IsNullOrEmpty(serializedFileId))
            {
                GameManager.Instance.CareerStatistics = JsonUtility.FromJson<GameManager.CareerStatisctics>(serializedFileId);
                Debug.Log("Career Statistics loaded.");
            }
        }
        else
            Debug.Log("Failed to load: hasn't key : " + PlayerPrefsKey.CareerStatisctics.ToString());

        //TODO change this logic to support first boot - name enterance etc
        //GameManager.Instance.CurrentFighter = CreateNewFighter("Helliodon", 1);
        //Save();
    }
}
