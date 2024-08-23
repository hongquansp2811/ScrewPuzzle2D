using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : UICanvas
{
    public Button ResumeBtn;
    public Button ReplayBtn;
    public Button HomeBtn;

    private void Awake()
    {
        ResumeBtn.onClick.AddListener(OnClickResumeButton);
        ReplayBtn.onClick.AddListener (OnClickReplayButton);
        HomeBtn.onClick.AddListener (OnClickHomeButton);    
    }

    private void OnClickResumeButton() 
    {
        Close();
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    private void OnClickReplayButton() 
    {
        Close();
        LevelManager.Ins.OnReplay();
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    private void OnClickHomeButton() 
    {
        Close();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
