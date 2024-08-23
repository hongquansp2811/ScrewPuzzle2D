using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    RemoveScrew, UseTool, UseHammer
}

public class ScrewController : Singleton<ScrewController>
{
    public LayerMask screwLayer; // Layer cho đinh ốc
    public LayerMask holeLayer;  // Layer cho lỗ đinh
    public Screw selectedScrew;

    private PlayerState playerState;
    private bool screwSelected = false;

    void Update()
    {
        switch (this.playerState)
        {
            case PlayerState.RemoveScrew:
                HandleInput();
                break;
            case PlayerState.UseTool:
                HandleToolInteraction();
                break;
            case PlayerState.UseHammer:
                HandleHammerInteraction();
                break;
        }
    }

    public void ChangeState(PlayerState playerState)
    {
        this.playerState = playerState;
        if (this.playerState == PlayerState.UseTool)
        {
            OnToolEffect();
        }
        else
        {
            OffToolEffect();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            EffectManager.Ins.OnPlayVFX(mousePosition);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, 0.4f);

            if (hitColliders.Length <= 0)
            {
                if (selectedScrew != null)
                {
                    SoundManager.Ins.PlaySound(SoundManager.Ins.removeScrew);
                    selectedScrew.ChangeAnim("IsScrewIn");
                    screwSelected = false;
                }
                return;
            }

            HoleBoard holeBoard = null;
            Screw screw = null;

            foreach (Collider2D collider in hitColliders)
            {
                HoleBoard cachedHoleBoard = Caching.GetComponentFromCache<HoleBoard>(collider);
                if (cachedHoleBoard != null)
                {
                    holeBoard = cachedHoleBoard;
                }

                Screw cachedScrew = Caching.GetComponentFromCache<Screw>(collider);
                if (cachedScrew != null)
                {
                    screw = cachedScrew;
                }

                if (holeBoard != null && screw != null)
                {
                    break;
                }
            }

            if (!screwSelected)
            {
                if (screw == null) return;
                screwSelected = true;
                selectedScrew = screw;
                selectedScrew.ChangeAnim("IsSelect");
                SoundManager.Ins.PlaySound(SoundManager.Ins.selectBolt);
            }
            else
            {
                if (screw == null && holeBoard == null)
                {
                    selectedScrew.ChangeAnim("IsScrewIn");
                    SoundManager.Ins.PlaySound(SoundManager.Ins.removeScrew);
                    return;
                }

                if (screw != null)
                {
                    selectedScrew.ChangeAnim("IsScrewIn");
                    selectedScrew = screw;
                    selectedScrew.ChangeAnim("IsSelect");
                    SoundManager.Ins.PlaySound(SoundManager.Ins.selectBolt);
                }
                else
                {
                    if (holeBoard.IsHoleFree) return;
                    UndoManager.Ins.SaveState(LevelManager.Ins.currentMap.screws, LevelManager.Ins.currentMap.listBar);
                    selectedScrew.transform.position = holeBoard.transform.position;
                    screwSelected = false;
                    selectedScrew.ChangeAnim("IsScrewIn");
                    SoundManager.Ins.PlaySound(SoundManager.Ins.removeScrew);
                    Debug.Log("Screw moved to hole");
                }
            }
        }
    }

    private void HandleToolInteraction()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, 0.4f);

            if (hitColliders.Length <= 0) return;

            Screw screw = null;

            foreach (Collider2D collider in hitColliders)
            {
                Screw cachedScrew = Caching.GetComponentFromCache<Screw>(collider);
                if (cachedScrew != null)
                {
                    screw = cachedScrew;
                    break;
                }
            }

            if (screw != null)
            {
                // Destroy the screw and change the state back to RemoveScrew
                LevelManager.Ins.currentMap.screws.Remove(screw);
                Destroy(screw.gameObject);
                ChangeState(PlayerState.RemoveScrew);
                SoundManager.Ins.PlaySound(SoundManager.Ins.removeScrew);
            }
            else
            {
                Debug.Log("No screw found to use tool on");
            }
        }
    }

    private void HandleHammerInteraction()
    {
        EffectManager.Ins.OnUseHammer(this.transform.position);
        SoundManager.Ins.PlaySound(SoundManager.Ins.hammer);
        //Map currentMap = LevelManager.Ins.currentMap;
        Camera.main.transform.DOShakePosition(0.5f, 0.3f, 10, 90, false, true);
        BreakBar();
        ChangeState(PlayerState.RemoveScrew);
    }

    private void BreakBar()
    {
        List<Bar> barsCanBreak = LevelManager.Ins.currentMap.listBar.Where(x => x.HasTotalBolt() <= 1 && !x.HasFallen).ToList();
        if (barsCanBreak.Count <= 0) return;
        int randomIndex = Random.Range(0, barsCanBreak.Count);
        Bar randomBar = barsCanBreak[randomIndex];
        if (randomBar == null) return;
        randomBar.transform.DOMoveY(randomBar.transform.position.y + 1f, 0.3f).SetEase(Ease.OutBounce);
    }

    private void OnToolEffect()
    {
        foreach (Screw item in LevelManager.Ins.currentMap.screws)
        {
            if (item != null)
            {
                item.DisplayRemoveEffect();
            }
        }
    }

    private void OffToolEffect()
    {
        foreach (Screw item in LevelManager.Ins.currentMap.screws)
        {
            if (item != null)
            {
                item.OffDisplayRemoveEffect();
            }
        }
    }
}
