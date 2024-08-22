using System.Collections;
using System.Collections.Generic;
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
                if (selectedScrew != null) selectedScrew.ChangeAnim("IsScrewIn");
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
            }
            else
            {
                if (screw == null && holeBoard == null)
                {
                    selectedScrew.ChangeAnim("IsScrewIn");
                    return;
                }

                if (screw != null)
                {
                    selectedScrew.ChangeAnim("IsScrewIn");
                    selectedScrew = screw;
                    selectedScrew.ChangeAnim("IsSelect");
                }
                else
                {
                    if (holeBoard.IsHoleFree) return;
                    UndoManager.Ins.SaveState(LevelManager.Ins.currentMap.screws, LevelManager.Ins.currentMap.listBar);
                    selectedScrew.transform.position = holeBoard.transform.position;
                    screwSelected = false;
                    selectedScrew.ChangeAnim("IsScrewIn");
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
            }
            else
            {
                Debug.Log("No screw found to use tool on");
            }
        }
    }

    private void HandleHammerInteraction()
    {

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
