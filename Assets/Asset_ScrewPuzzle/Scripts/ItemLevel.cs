using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemLevel : MonoBehaviour
{
    public TextMeshProUGUI lvlTxt;
    public RectTransform imgLevel;
    public Button SelectButton;
    public int level;

    private void Awake()
    {
        SelectButton.onClick.AddListener(OnSelectButtonClick);
    }

    private void OnSelectButtonClick()
    {
        UserDataManager.Ins.SaveCurrentLevel(level);
        UIManager.Ins.CloseUI<UIMainMenu>();
        LevelManager.Ins.OnStartGame();
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void SetUpItemLevel(int level, float posX)
    {
        this.level = level;
        lvlTxt.text = level.ToString();
        imgLevel.anchoredPosition = new Vector3(posX, 0, 0);
    }
}
