using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_EnterDetailsForRegistration : BlitzyUI.Screen
{
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

    public override void StartPoppingSequence(Action callbackOnPopEnd = null)
    {
        base.StartPoppingSequence(callbackOnPopEnd);
    }
}
