using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewMainMenu : MonoBehaviour
{
    public Transform content;
    public ItemLevel itemLevelPrefab;

    private void Start()
    {
        SetUpItemLevel();
       // FocusOnHighestLevel();
    }

    private void SetUpItemLevel()
    {
        float posX = 30f;
        for (int i = 0; i < LevelManager.Ins.maps.Count; i++)
        {
            ItemLevel newItem = Instantiate(itemLevelPrefab, content);
            ItemLevel itemLevel = newItem.GetComponent<ItemLevel>();
            posX = posX * -1f;
            itemLevel.SetUpItemLevel(i + 1, posX);
        }
    }
}
