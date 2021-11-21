using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;

    //Put this in UI Manager?
    public static readonly BlitzyUI.Screen.ScreenID ScreenId_CreateFlashcard = new BlitzyUI.Screen.ScreenID("CreateFlashcard");
    public static readonly BlitzyUI.Screen.ScreenID ScreenId_Navicon = new BlitzyUI.Screen.ScreenID("Navicon");

    public static readonly BlitzyUI.Screen.ScreenID ScreenId_Screen1 = new BlitzyUI.Screen.ScreenID("Screen 1");
    public static readonly BlitzyUI.Screen.ScreenID ScreenId_Screen2 = new BlitzyUI.Screen.ScreenID("Screen 2");

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    private void Start()
    {
        UIManager.Instance.QueuePush(ScreenId_CreateFlashcard, null, "Screen_CreateFlashcard", null);
    }


    private void Update() //Put this in UI Manager? 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.QueuePop(null);




            //If last screen on stack - minimise application - don't pop.
            //            if(UIManager.Instance.CurrentScreenIsLastOnStack())
            //            {
            //                print("minimise");

            //#if UNITY_ANDROID
            //                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            //                activity.Call<bool>("moveTaskToBack", true);
            //#endif
            //            }
            //            else
            //            {
            //                UIManager.Instance.QueuePop(null);
            //            }

        }
    }
}
