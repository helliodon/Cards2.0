using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public enum PlayerPrefsKey
    {
        CurrentFighter
    }

    public FightManager.Fighter CurrentFighter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("SaveManager already exists");
    }

    public void Save()
    {
        if (CurrentFighter != null) 
        {
            string serializedFileId = JsonUtility.ToJson(CurrentFighter);

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
        if (PlayerPrefs.HasKey(PlayerPrefsKey.CurrentFighter.ToString()))
        {
            string serializedFileId = PlayerPrefs.GetString(PlayerPrefsKey.CurrentFighter.ToString());
            if (!string.IsNullOrEmpty(serializedFileId))
            {
                CurrentFighter = JsonUtility.FromJson<FightManager.Fighter>(serializedFileId);
                Debug.Log("Current Fighter loaded. Name is " + CurrentFighter.Name);
                return;
            }
        }
        Debug.Log("Failed to load: hasn't key :" + PlayerPrefsKey.CurrentFighter.ToString());
        
        //TODO change this logic to sup-port first boot - name enterance etc
        CurrentFighter = CreateNewFighter("Helliodon", 1);
        Save();
    }
}
