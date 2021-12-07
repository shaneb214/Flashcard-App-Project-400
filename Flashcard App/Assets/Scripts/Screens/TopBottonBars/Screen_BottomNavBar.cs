using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_BottomNavBar : BlitzyUI.Screen
{

    public override bool AllowStartPoppingSequence { get => false; set => base.AllowStartPoppingSequence = value; }


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

    //Probably dont need this.
    public override void StartPoppingSequence(Action callbackOnPopEnd = null)
    {
        base.StartPoppingSequence(callbackOnPopEnd);
    }
}
