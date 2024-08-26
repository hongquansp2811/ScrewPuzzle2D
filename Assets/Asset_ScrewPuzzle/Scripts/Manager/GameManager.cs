using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    MainMenu, Gameplay, Pause, Win, Undo,
    Setting, NextLevel, Revive,LoseGame
}

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    private List<Screw> screws = new List<Screw>();
    private List<Bar> listBar = new List<Bar>();
    private UndoState undoState;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //Init data
       // UserData.Ins.OnInitData();
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<Loading>();
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
        switch (this.gameState)
        {
            case GameState.MainMenu:
                HandleMainMenuStage();
                break;
            case GameState.Gameplay:
                HandleGamePlayStage();
                break;
            case GameState.Pause:
                HandlePauseStage();
                break;
            case GameState.Win:
                HandleWinStage();
                break;
            case GameState.Undo:
                HandleUndoStage();
                break;
            case GameState.Setting:
                HandleSettingtage();
                break;
            case GameState.NextLevel:
                HandleNextLeveltage();
                break;
            case GameState.LoseGame:
                HandleLoseGametage();
                break;
        }
    }

    private void HandleSettingtage()
    {
        UIManager.Ins.CloseUI<UIMainMenu>();
        //UIManager.Ins.CanvasParentTF.GetComponent<Canvas>().sortingLayerName = "UI";
        UIManager.Ins.OpenUI<UISetting>().GetComponent<Canvas>().sortingLayerName = "UI"; ;
    }

    private void HandleUndoStage()
    {
        undoState = UndoManager.Ins.Undo();
        if (undoState == null) return;
        screws = LevelManager.Ins.currentMap.screws;
        listBar = LevelManager.Ins.currentMap.listBar;
        if (listBar.Count <= 0) return;
        if (screws.Count <= 0) return;
        for (int i = 0; i < screws.Count; i++)
        {
            screws[i].transform.position = undoState.ScrewPossiions[i];
        }
        for (int i = 0; i < undoState.barStates.Count; i++) 
        {
            listBar[i].transform.position = undoState.barStates[i].position;
            listBar[i].transform.rotation = undoState.barStates[i].rotation;
            listBar[i].HasFallen = undoState.barStates[i].hasFallen;
        }
        LevelManager.Ins.currentMap.SetBarCount(undoState.barCount);
    }

    private void HandleNextLeveltage()
    {
        LevelManager.Ins.OnNextLevel();
        ChangeState(GameState.Gameplay);
    }

    private void HandleWinStage()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.CanvasParentTF.GetComponent<Canvas>().sortingLayerName = "UI";
        UIManager.Ins.OpenUI<UIVictory>();
        SoundManager.Ins.PlaySound(SoundManager.Ins.win);
    }

    private void HandlePauseStage()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.CanvasParentTF.GetComponent<Canvas>().sortingLayerName = "UI";
        UIManager.Ins.OpenUI<UIPause>();
    }

    private void HandleGamePlayStage()
    {
        UIManager.Ins.CanvasParentTF.GetComponent<Canvas>().sortingLayerName = "Default";
        UIManager.Ins.OpenUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UIGamePlay>().SetTextLevel(LevelManager.Ins.LevelIndex());
    }

    private void HandleMainMenuStage()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UIMainMenu>().scrollViewMainMenu.FocusOnHighestLevel();
    }

    private void HandleLoseGametage() 
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UILoseGame>();
        UIManager.Ins.OpenUI<UILoseGame>().GetComponent<Canvas>().sortingLayerName = "UI"; ;
        SoundManager.Ins.PlaySound(SoundManager.Ins.lose);
    }
}
