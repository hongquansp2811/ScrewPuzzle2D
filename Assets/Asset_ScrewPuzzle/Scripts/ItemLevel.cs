using TMPro;
using UnityEngine;

public class ItemLevel : MonoBehaviour
{
    public TextMeshProUGUI lvlTxt;
    public RectTransform imgLevel;

    private void Start()
    {
        //SetUpItemLevel();
    }

    public void SetUpItemLevel(int level, float posX)
    {
        lvlTxt.text = level.ToString();
        RectTransform rectTransform = imgLevel.GetComponent<RectTransform>();
        imgLevel.anchoredPosition = new Vector3(posX, 0, 0);
    }
}
