using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;


public class DataSaveLoader : MonoBehaviour
{

    private void Start()
    {
        PlayerPrefs.SetString("PlayerData", "");
        print("Developing mod active, PlayerPrefs cleared");
    }

    public static void SaveAllData(Dictionary<ResourceID, int> data)
    {
        string s = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString("PlayerData", s);
    }

    public static Dictionary<ResourceID, int> LoadAllData()
    {
        Dictionary<ResourceID, int> resources = new Dictionary<ResourceID, int>();
        if (PlayerPrefs.GetString("PlayerData") != "")
        {
            resources = JsonConvert.DeserializeObject<Dictionary<ResourceID, int>>(PlayerPrefs.GetString("PlayerData"));
            print("we are in load from prefs");
        }
        else
        {
            resources = GetDefaultPlayerData();
        }
        return resources;
    }

    private static Dictionary<ResourceID, int> GetDefaultPlayerData()
    {
        Dictionary<ResourceID, int> resources = new Dictionary<ResourceID, int>();
        foreach (var enumID in Enum.GetValues(typeof(ResourceID)))
        {
            int defaultValue = 0;
            switch ((ResourceID)enumID)
            {
                case ResourceID.InventoryLimit:
                    defaultValue = 20;
                    break;
                case ResourceID.ActiveFeilds:
                    defaultValue = 1;
                    break;
                case ResourceID.FeildUpdatePrice:
                    defaultValue = 500;
                    break;
                case ResourceID.InventoryUpgradePrice:
                    defaultValue = 1000;
                    break;
                case ResourceID.SpeedBoostPrice:
                    defaultValue = 1000;
                    break;
            };

            resources.Add((ResourceID)enumID, defaultValue);

        }
        return resources;
    }

}

