using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public Button UndoBtn;
    public Button ToolBtn;
    public Button HammerBtn;

    private void Awake()
    {
        UndoBtn.onClick.AddListener(OnClickUndoButton);
        ToolBtn.onClick.AddListener(OnClickToolButton);
        HammerBtn.onClick.AddListener(OnClickHammerButton);
    }


    private void OnClickUndoButton()
    {
        GameManager.Ins.ChangeState(GameState.Undo);
    }

    private void OnClickToolButton()
    {
        ScrewController.Ins.ChangeState(PlayerState.UseTool);
    }

    private void OnClickHammerButton()
    {
        ScrewController.Ins.ChangeState(PlayerState.UseHammer);
    }
}
