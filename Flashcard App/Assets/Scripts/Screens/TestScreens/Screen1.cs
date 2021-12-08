using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;

//Order methods are called when screen is pushed -
//Awake, On Setup, On Push, On Focus, Start.


public class Screen1 : BlitzyUI.Screen
{
    [SerializeField] private Button btnGoToScreen2;

    private void OnClickScreen2Button()
    {
        // Way to pop then push?
        //UIManager.Instance.QueuePop(null);
        //UIManager.Instance.QueuePush(ScreenController.ScreenId_Screen2, null, "Screen2", null);

        //UIManager.Instance.QueuePopTo(ScreenController.ScreenId_Screen2, true, null);
    }

    public override void OnFocus()
    {
        //Called when screen is pushed.
        //OR
        //Called when another screen was popped and this screen is now on top or in focus.
    }

    public override void OnFocusLost()
    {
       // Called just before this screen is popped.
       // OR
       // Called just before a new screen is being pushed.
    }

    public override void OnPop()
    {
        //On pop is called.
        //Call Popfinished at end which raises event ui manager has subscribed to - then it disables / destroys screen.
        


        PopFinished(); // Make sure to call last. Raises pop finished event.
    }

    public override void OnPush(ScreenData data)
    {


        PushFinished(); // Make sure to call last. Raises push finished event.
    }

    public override void OnSetup()
    {
        // Called just after screen is instantiated.
        // Run one-time setup operations here.

        btnGoToScreen2.onClick.AddListener(OnClickScreen2Button);

    }

    private void OnDestroy()
    {
        btnGoToScreen2.onClick.RemoveListener(OnClickScreen2Button);
     
    }
}
