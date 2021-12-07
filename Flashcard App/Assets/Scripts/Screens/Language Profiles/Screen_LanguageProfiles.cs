using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_LanguageProfiles : BlitzyUI.Screen
{

    public override void OnSetup()
    {
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
    public override void StartPoppingSequence(Action callbackOnPopEnd = null)
    {
        base.StartPoppingSequence(callbackOnPopEnd);
    }
}
