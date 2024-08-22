using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private SpriteRenderer removeEffect;
    private string currentAnim;

    private void Start()
    {
        currentAnim = "IsIdle";
    }

    public void ChangeAnim(string anim)
    {
        if (animator != null && anim != currentAnim)
        {
            animator.SetBool(currentAnim, false);
            currentAnim = anim;
            animator.SetBool(currentAnim, true);
        }     
    }

    public void DisplayRemoveEffect()
    {
        removeEffect.gameObject.SetActive(true);
        removeEffect.DOKill();
        removeEffect.color = Color.white;
        removeEffect.DOColor(Color.red, 0.6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        Debug.Log("Effeeeeeect");
    }

    public void OffDisplayRemoveEffect()
    {
        removeEffect.gameObject.SetActive(false);
    }
}
