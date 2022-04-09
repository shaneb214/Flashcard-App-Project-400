using BlitzyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEnableLanguageProfilesScreen : BtnPushScreen
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick()
    {
        UIManager.Instance.GetTopScreen().StartPoppingSequence(callbackOnPopEnd: delegate()
        {
            UIManager.Instance.QueuePop();
            PushMyScreen();      
        });
    }
}
