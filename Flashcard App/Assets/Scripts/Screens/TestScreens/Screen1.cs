using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;

public class Screen1 : BlitzyUI.Screen
{
    [SerializeField] private Button btnGoToScreen2;


    private void Start()
    {
        btnGoToScreen2.onClick.AddListener(OnClickScreen2Button);
    }

    private void OnClickScreen2Button()
    {
        UIManager.Instance.QueuePush(ScreenController.ScreenId_Screen2, null, "Screen2", null);
    }

    public override void OnFocus()
    {
        
    }

    public override void OnFocusLost()
    {
       
    }

    public override void OnPop()
    {



        PopFinished(); //Call last.
    }

    public override void OnPush(Data data)
    {


        PushFinished(); //Call last.
    }

    public override void OnSetup()
    {

    }

    private void OnDestroy()
    {
        btnGoToScreen2.onClick.RemoveListener(OnClickScreen2Button);
    }
}
