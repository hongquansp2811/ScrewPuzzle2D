using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public GameObject VFXTouch;

    public void OnPlayVFX(Vector2 touchPos)
    {
        if (VFXTouch == null) return;
        GameObject vfxInstance = Instantiate(VFXTouch, touchPos, Quaternion.identity);
        StartCoroutine(DestroyVFXAfterTime(vfxInstance, 0.2f));
    }

    private IEnumerator DestroyVFXAfterTime(GameObject vfxInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(vfxInstance);
    }
}
