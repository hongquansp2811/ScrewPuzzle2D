using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class UIRevive : UICanvas
{
    public Button CloseBtn;
    public Button ReviveBtn;

    [SerializeField] TextMeshProUGUI counterTxt;
    private float counter;

    private void Awake()
    {
        CloseBtn.onClick.AddListener(CloseButton);
        ReviveBtn.onClick.AddListener(ReviveButton);
    }
    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            counterTxt.SetText(counter.ToString("F0"));

            if (counter <= 0)
            {
                CloseButton();
            }
        }
    }

    public void OnInit()
    {
        counter = 5;
    }

    public void ReviveButton()
    {

    }

    public void CloseButton()
    {
        Close();
    }
}
