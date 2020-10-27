using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Serializable]
    public class PrefabObject
    {
        public PrefabType type;
        public GameObject prefab;
    }
    public enum PrefabType
    {
        Card
    }

    public PrefabObject[] Prefabs;

    public static SpawnManager Instance;

    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("SpawnManager already exists");
    }

    public GameObject CreateCardObject(PrefabType type)
    {
        if (Prefabs != null) 
        {
            foreach(var prefab in Prefabs)
            {
                if (prefab.type == type)
                    return Instantiate(prefab.prefab);
            }    
        }
        return null;
    }
}
