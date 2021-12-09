using BlitzyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_CreateSet : BlitzyUI.Screen
{
    [Header("Input Window Scaling")]
    [SerializeField] private RectTransform inputWindowRectTransform;
    [SerializeField] private float inputWindowScaleTime;
    [SerializeField] private LeanTweenType inputWindowTweenEase;

    public override bool AllowStartPoppingSequence { get => !LeanTween.isTweening(inputWindowRectTransform); set => base.AllowStartPoppingSequence = value; }

    public override void OnFocus()
    {
        inputWindowRectTransform.localScale = Vector3.zero;
        LeanTween.scale(inputWindowRectTransform, Vector3.one, inputWindowScaleTime);
    }

    public override void OnFocusLost()
    {
       
    }

    public override void OnPop()
    {

        PopFinished();
    }

    public override void OnPush(ScreenData data)
    {

        PushFinished();
    }

    public override void OnSetup()
    {
        
    }

    public override void StartPoppingSequence(Action callbackOnPopEnd = null)
    {
        if (AllowStartPoppingSequence)
        {
            LeanTween.scale(inputWindowRectTransform, Vector3.zero, inputWindowScaleTime).setEase(inputWindowTweenEase).setOnComplete(() => UIManager.Instance.QueuePop(null));
        }
    }
}
