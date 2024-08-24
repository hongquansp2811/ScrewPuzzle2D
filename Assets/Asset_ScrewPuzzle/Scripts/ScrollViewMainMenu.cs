using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewMainMenu : MonoBehaviour
{
    public RectTransform content;
    public ItemLevel itemLevelPrefab;
    private Vector2 currentContentPos;

    private void Awake()
    {
        currentContentPos = content.anchoredPosition;
    }

    private void Start()
    {
        SetUpItemLevel();
    }

    private void SetUpItemLevel()
    {
        float posX = 20f;
        for (int i = 0; i < LevelManager.Ins.maps.Count; i++)
        {
            ItemLevel newItem = Instantiate(itemLevelPrefab, content);
            posX = posX * -1f;
            newItem.SetUpItemLevel(i + 1, posX);
        }
    }

    public void FocusOnHighestLevel()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        Vector2 currentAnchoredPosition = currentContentPos;
        int index = UserDataManager.Ins.GetCurrentLevel();
        float yOffset = 100;
        if (index >= 4)
        {
            float newY = currentAnchoredPosition.y - yOffset * (index - 3);
            content.anchoredPosition = new Vector2(currentAnchoredPosition.x, newY);
        }
        else
        {
            content.anchoredPosition = currentAnchoredPosition;
        }
    }
}
