using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    public Button SoundBtn;
    public Button VibrateBtn;
    public Button HomeBtn;

    [SerializeField] private Image soundOn;
    [SerializeField] private Image soundOff;
    [SerializeField] private Image vibrateOn;
    [SerializeField] private Image vibrateOff;

    private bool IsSound;
    private bool IsVivrate;

    private void Awake()
    {
        SoundBtn.onClick.AddListener(OnSoundButtonClick);
        VibrateBtn.onClick.AddListener(OnVibrateButtonClick);
        HomeBtn.onClick.AddListener(OnHomeButtonClick);
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        IsSound = true;
        IsVivrate = true;
        soundOn.gameObject.SetActive(true);
        soundOff.gameObject.SetActive(false);
        vibrateOn.gameObject.SetActive(true);
        vibrateOff.gameObject.SetActive(false);
    }

    private void OnSoundButtonClick()
    {
        IsSound = !IsSound;
        soundOn.gameObject.SetActive(IsSound);
        soundOff.gameObject.SetActive(!IsSound);
        //SoundManager.Ins.
    }

    private void OnVibrateButtonClick()
    {
        IsVivrate = !IsVivrate;
        vibrateOn.gameObject.SetActive(IsVivrate);
        vibrateOff.gameObject.SetActive(!IsVivrate);
        //...
    }

    private void OnHomeButtonClick()
    {
        Close();    
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
