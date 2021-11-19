using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;

public class Screen2 : BlitzyUI.Screen
{

    [SerializeField] Button btnGoScreen1;

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
        btnGoScreen1.onClick.AddListener(GoScreen1);
    }

    void GoScreen1()
    {
        UIManager.Instance.QueuePop(null);
        //UIManager.Instance.QueuePush(ScreenController.ScreenId_Screen1, null, "Screen1", null);
    }
}
