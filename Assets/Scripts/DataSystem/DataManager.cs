using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static GameData data;

    private void Awake()
    {
        if(data == null)
        {
            data = SaveSystem.Load<GameData>();

            Save();
        }
    }
    public static void Save()
    {
        SaveSystem.Save(data);
    }
}
