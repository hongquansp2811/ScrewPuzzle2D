﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public GameObject VFXTouch;
    public GameObject VFXHammer;
    private bool isVibrationEnabled = true;

    public void OnPlayVFX(Vector2 touchPos)
    {
        if (VFXTouch == null) return;
        GameObject vfxInstance = Instantiate(VFXTouch, touchPos, Quaternion.identity);
        StartCoroutine(DestroyVFXAfterTime(vfxInstance, 0.2f));
    }

    public void OnUseHammer(Vector2 touchPos)
    {
        if (VFXHammer == null) return;
        GameObject vfxInstance = GameObject.Instantiate(VFXHammer, touchPos, Quaternion.identity);
        StartCoroutine(DestroyVFXAfterTime(vfxInstance, 0.2f));
    }

    private IEnumerator DestroyVFXAfterTime(GameObject vfxInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(vfxInstance);
    }

    public void Vibrate()
    {
        if (isVibrationEnabled)
        {
            Handheld.Vibrate();
        }
    }

    // Bật rung
    public void EnableVibration()
    {
        isVibrationEnabled = true;
    }

    // Tắt rung
    public void DisableVibration()
    {
        isVibrationEnabled = false;
    }

    // Kiểm tra trạng thái rung
    public bool IsVibrationEnabled()
    {
        return isVibrationEnabled;
    }
}
