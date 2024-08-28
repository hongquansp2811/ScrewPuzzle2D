using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemLevel : MonoBehaviour
{
    public TextMeshProUGUI lvlTxt;
    public RectTransform imgLevel;
    public Image Img_Log;
    public Image Img_Stick;
    public Button SelectButton;
    public int level;
    private int hightestLevel;

    private void Awake()
    {
        SelectButton.onClick.AddListener(OnSelectButtonClick);
    }

    private void OnSelectButtonClick()
    {
        hightestLevel = UserDataManager.Ins.GetHighestLevel();
        if (level > hightestLevel ) return;
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
        hightestLevel = UserDataManager.Ins.GetHighestLevel();
        if (level == hightestLevel) 
        {
            CloseAllObject();
            Img_Stick.gameObject.SetActive(true);
        }else if (level > hightestLevel)
        {
            CloseAllObject();
            lvlTxt.gameObject.SetActive(true);
            Img_Log.gameObject.SetActive(true);
        }
        else
        {
            CloseAllObject();
            lvlTxt.gameObject.SetActive(true);
        }
    }

    private void CloseAllObject()
    {
        lvlTxt.gameObject.SetActive(false);
        Img_Stick.gameObject.SetActive(false);
        Img_Log.gameObject.SetActive(false);
    }
}
