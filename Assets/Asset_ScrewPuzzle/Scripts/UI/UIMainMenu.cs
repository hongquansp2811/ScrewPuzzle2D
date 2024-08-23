using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    public Button SettingBtn;
    public Button ADSBtn;
    public Button PlayBtn;
    public ScrollViewMainMenu scrollViewMainMenu;

    private void Awake()
    {
        SettingBtn.onClick.AddListener(OnClickSettingBtn);
        ADSBtn.onClick.AddListener(OnClickADSBtn);
        PlayBtn.onClick.AddListener(OnClickPlayBtn);
    }

    private void OnClickSettingBtn() 
    {
        GameManager.Ins.ChangeState(GameState.Setting);
    }

    private void OnClickADSBtn() { }

    private void OnClickPlayBtn() 
    {
        Debug.Log("Play Btn");
        Close();
        LevelManager.Ins.OnStartGame();
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
}
