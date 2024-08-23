using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Map> maps = new List<Map>();
    public Map currentMap;
    private int levelIndex;

    private void Start()
    {
        levelIndex = 1;
    }

    public int LevelIndex() { return levelIndex; }

    public void OnStartGame()
    {
        levelIndex = 1;
        LoadLevel(levelIndex);
    }

    public void OnReplay()
    {
        LoadLevel(levelIndex);
    }

    public void LoadLevel(int index)
    {
        if (currentMap != null)
        {
            // Thêm hiệu ứng mờ dần ra trước khi hủy bản đồ hiện tại
            currentMap.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(currentMap.gameObject);
                LoadNewMap(index);
            });
        }
        else
        {
            LoadNewMap(index);
        }
    }

    private void LoadNewMap(int index)
    {
        UndoManager.Ins.Clear();

        index = (index - 1) % maps.Count + 1;

        if (index > 0 && index <= maps.Count)
        {
            Map newMapObject = Instantiate(maps[index - 1]);
            currentMap = newMapObject;

            //currentMap.transform.localScale = Vector3.zero;
            currentMap.transform.DOScale(Vector3.one, 0.5f);
        }
        else
        {
            Debug.LogError("Invalid level index");
        }
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        //PlayerPrefs.SetInt("Level", levelIndex);
        //PlayerPrefs.Save();
        LoadLevel(levelIndex);
    }
}
