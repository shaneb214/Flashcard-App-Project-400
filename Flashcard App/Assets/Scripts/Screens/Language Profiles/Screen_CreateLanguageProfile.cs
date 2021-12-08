using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Pop up screen where you can create a language profile. 
//Can be enabled pressing create profile button in language profiles screen.

public class Screen_CreateLanguageProfile : BlitzyUI.Screen
{
    [Header("Input Window Scaling")]
    [SerializeField] private RectTransform inputWindowRectTransform;
    [SerializeField] private float inputWindowScaleTime;
    [SerializeField] private LeanTweenType inputWindowTweenEase;

    public override bool AllowStartPoppingSequence { get => !LeanTween.isTweening(inputWindowRectTransform); set => base.AllowStartPoppingSequence = value; }

    public override void OnSetup()
    {

    }

    public override void OnFocus()
    {
        inputWindowRectTransform.localScale = Vector3.zero;
        LeanTween.scale(inputWindowRectTransform, Vector3.one, inputWindowScaleTime);
        //LeanTween.scale(inputWindowRectTransform, Vector3.one, 2f);
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

    public override void StartPoppingSequence(Action callbackOnPopEnd = null)
    {
        
    }

}
