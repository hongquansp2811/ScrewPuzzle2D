using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVictory : UICanvas
{
    public Button nextBtn;

    private void Awake()
    {
        nextBtn.onClick.AddListener(OnNextButtonClick);
    }

    private void OnNextButtonClick()
    {
        Close();
        GameManager.Ins.ChangeState(GameState.NextLevel);
    }
}
