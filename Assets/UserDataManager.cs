using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public void SaveCurrentLevel(int level)
    {
        PlayerPrefs.SetInt(Caching.CACHE_DATA_CURRENTL_LEVEL, level);
        PlayerPrefs.Save();
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(Caching.CACHE_DATA_CURRENTL_LEVEL, 1); 
    }

    public void SaveHighestLevel(int level)
    {
        int highestLevel = GetHighestLevel();
        if (level > highestLevel)
        {
            PlayerPrefs.SetInt(Caching.CACHE_DATA_HIGHTEST_LEVEL, level);
            PlayerPrefs.Save();
        }
    }

    public int GetHighestLevel()
    {
        return PlayerPrefs.GetInt(Caching.CACHE_DATA_HIGHTEST_LEVEL, 1);
    }
}
