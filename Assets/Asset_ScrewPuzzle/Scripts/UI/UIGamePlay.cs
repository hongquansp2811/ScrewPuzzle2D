using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public Button PauseBtn;
    public Button UndoBtn;
    public Button ToolBtn;
    public Button HammerBtn;
    public Button ReplayBtn;
   
    [SerializeField] private TextMeshProUGUI levelTxt;

    private void Awake()
    {
        UndoBtn.onClick.AddListener(OnClickUndoButton);
        ToolBtn.onClick.AddListener(OnClickToolButton);
        HammerBtn.onClick.AddListener(OnClickHammerButton);
        ReplayBtn.onClick.AddListener(OnClickReplayBtn);
        PauseBtn.onClick.AddListener(OnClickPauseButton);
    }

    public void SetTextLevel(int level)
    {
        levelTxt.text = "Level " + level.ToString();
    }

    private void OnClickUndoButton()
    {
        GameManager.Ins.ChangeState(GameState.Undo);
    }

    private void OnClickToolButton()
    {
        PlayerController.Ins.ChangeState(PlayerState.UseTool);
    }

    private void OnClickHammerButton()
    {
        PlayerController.Ins.ChangeState(PlayerState.UseHammer);
    }

    private void OnClickReplayBtn() 
    {
        LevelManager.Ins.OnReplay();
    }

    private void OnClickPauseButton()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
    }
}
