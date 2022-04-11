using UnityEngine;
using BlitzyUI;
using System.Collections.Generic;

//To do: 
//Make a stack on screens so that when you press back it loads the next screen on that stack.  

public class ScreenPushOnSceneLoad : MonoBehaviour
{
    [Header("Screens To Push At Start")]
    [SerializeField] private List<ScreenPushData> screensToPush;
    [SerializeField] private bool PushScreens;

    private void Start()
    {
        if(PushScreens)
            screensToPush.ForEach(screenData => UIManager.Instance.QueuePush(screenData.ID, null, null, null));
    }

    //private void Update() //Put this in UI Manager? 
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        BlitzyUI.Screen topScreen = UIManager.Instance.GetTopScreen(); //If I set up custom stack I don't have to find the top screen. Do this later.

    //        if(topScreen.AllowStartPoppingSequence)
    //        {
    //            //If last screen - minimise application.

    //            //Else notify screen that back button was pressed and start the popping sequence.
    //            topScreen.StartPoppingSequence();
    //        }
    //    }
    //}
}

//Code for minimising on android:
//#if UNITY_ANDROID
//                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
//                activity.Call<bool>("moveTaskToBack", true);
//#endif
