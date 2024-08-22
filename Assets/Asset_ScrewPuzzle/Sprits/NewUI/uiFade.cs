//using DG.Tweening;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class uiFade : SingletonMonoBehaviour<uiFade>
//{
//    [SerializeField] private CanvasGroup canvasGroup;

//    #region fade using white img
//    public void FadeIn(float time = 0.75f)
//    {
//        Debug.Log("fade in");
//        canvasGroup.alpha = 0;
//        canvasGroup.DOFade(1, time)
//            .SetEase(Ease.InQuart);
//        canvasGroup.GetComponentInParent<GraphicRaycaster>().enabled = true;
//    }

//    public void FadeOut(float time = 0.75f)
//    {
//        Debug.Log("fade out");
//        canvasGroup.alpha = 1;
//        var tween = canvasGroup.DOFade(0, time)
//            .SetEase(Ease.OutQuad);
//        tween.OnComplete(() =>
//        {
//            // khi fade out xong mới có thể thao tác trên canvas gameplay
//            canvasGroup.GetComponentInParent<GraphicRaycaster>().enabled = false;
//        });
        
//    }

//    public void FadeInOut(System.Action c, float time = 0.75f)
//    {
//        StartCoroutine(ie_FadeInOut(c, time));
//    }

//    IEnumerator ie_FadeInOut(System.Action c, float time)
//    {
//        FadeIn(time * 0.75f);

//        yield return new WaitForSecondsRealtime(time * 0.75f);
//        c.Invoke();

//        yield return new WaitForSecondsRealtime(0.2f);

//        FadeOut(time);
//    }
//    #endregion

//    #region Fade opacity
    
//    #endregion
//}
