using BlitzyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPopUpScreen : BlitzyUI.Screen
{
    RectTransform rectTransform;
    [SerializeField] Button btnPopMe;

    public override bool AllowStartPoppingSequence { get => !LeanTween.isTweening(gameObject); set => base.AllowStartPoppingSequence = value; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        btnPopMe.onClick.AddListener(() => StartPoppingSequence());
    }

    private void Start()
    {
        rectTransform.localScale = Vector3.zero;

        LeanTween.scale(rectTransform, Vector3.one, 0.34f).setEaseOutBack();

    }

    public override void OnFocus()
    {
        
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



    public override void StartPoppingSequence(Action callback = null)
    {
        //So that pressing button while tweening does nothing.
        if(AllowStartPoppingSequence)
        {
            LeanTween.scale(rectTransform, Vector3.zero, 0.34f).setOnComplete(()=> UIManager.Instance.QueuePop(null));

        }
    }
}
