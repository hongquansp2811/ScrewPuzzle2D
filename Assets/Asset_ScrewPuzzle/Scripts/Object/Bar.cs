using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public bool HasFallen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleColliderWithDeathZone(collision);
    }

    private void HandleColliderWithDeathZone(Collider2D collision)
    {
        if (!collision.CompareTag(Caching.CACHE_TAG_DEATHZONE)) return;
        if (!HasFallen)
        {
            HasFallen = true;
            LevelManager.Ins.currentMap.RemoveBarInMap();
        };

        if(LevelManager.Ins.currentMap.GetBarCount() == 0)
        {
            GameManager.Ins.ChangeState(GameState.Win);
        }
    }
}
