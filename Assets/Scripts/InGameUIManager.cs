using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;

public class InGameUIManager : Singleton<InGameUIManager>
{
    public RectTransform pauseScreen;
    public Vector2 offscreenOffset;
    public Vector2 onScreenOffset;
    public float pauseScreenAnimationDuration;
    public AnimationCurve pauseScreenAnimationCurve;
    
    public GameObject ammoIcon;

    public bool isPaused = false;

    public void HaveAFlare(bool haveFlare)
    {
        ammoIcon.SetActive(haveFlare);
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        if (pause)
        {
            pauseScreen.gameObject.SetActive(true);
            pauseScreen.DOLocalMove(offscreenOffset, pauseScreenAnimationDuration).SetEase(pauseScreenAnimationCurve).SetUpdate(UpdateType.Late, true);;
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.DOLocalMove(onScreenOffset, pauseScreenAnimationDuration).SetEase(pauseScreenAnimationCurve).OnComplete(() => Stop()).SetUpdate(UpdateType.Late, true);;
        }
    }

    public void Stop()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
