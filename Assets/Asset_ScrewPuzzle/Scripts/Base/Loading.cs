using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Loading : UICanvas
{
    public Image progressBar;
    public float loadingTime = 3f;

    // load type 2
    private void Start()
    {
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        float elapsedTime = 0f;

        // Thanh progress chạy từ 0 đến 1 trong thời gian loadingTime
        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            progressBar.fillAmount = Mathf.Clamp01(elapsedTime / loadingTime);
            yield return null;
        }
        Close();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}