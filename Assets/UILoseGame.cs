using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoseGame : UICanvas
{
    public Button HomeBtn;
    public Button FreeAdsBtn;

    private void Awake()
    {
        HomeBtn.onClick.AddListener(OnClickHomeButton);
        FreeAdsBtn.onClick.AddListener(OnClickFreeAdsButton);
    }

    private void OnClickHomeButton() 
    {
        Close();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    private void OnClickFreeAdsButton() 
    { 
        // Code xem video QC
        Close();
    }
}
