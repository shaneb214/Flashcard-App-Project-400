using UnityEngine;
using BlitzyUI;

//To do: 
//Make a stack on screens so that when you press back it loads the next screen on that stack.  

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;

    [Header("Screen data for pushing")] //Putting a few here for now for testing and stuff.
    [SerializeField] private ScreenPushData screenCreateFlashcardPushData;
    [SerializeField] private ScreenPushData screenNaviconPushData;
    [SerializeField] private ScreenPushData screen1PushData;
    [SerializeField] private ScreenPushData screen2PushData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.QueuePush(screenCreateFlashcardPushData.ID, null, null);
        //UIManager.Instance.QueuePush(screen2PushData.ID, null, null);
    }

    private void Update() //Put this in UI Manager? 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BlitzyUI.Screen topScreen = UIManager.Instance.GetTopScreen(); //If I set up custom stack I don't have to find the top screen. Do this later.

            if(topScreen.AllowStartPoppingSequence)
            {
                //If last screen - minimise application.

                //Else notify screen that back button was pressed and start the popping sequence.
                topScreen.StartPoppingSequence();
            }
        }
    }
}

//Code for minimising on android:
//#if UNITY_ANDROID
//                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
//                activity.Call<bool>("moveTaskToBack", true);
//#endif



//Old code using non scriptable object way of holding screen ID data.

//Put this in UI Manager?
//public static readonly BlitzyUI.Screen.ScreenID ScreenId_CreateFlashcard = new BlitzyUI.Screen.ScreenID("CreateFlashcard");
//public static readonly BlitzyUI.Screen.ScreenID ScreenId_Navicon = new BlitzyUI.Screen.ScreenID("Navicon");

////Not using this in actual app but just there for testing.
//public static readonly BlitzyUI.Screen.ScreenID ScreenId_Screen1 = new BlitzyUI.Screen.ScreenID("Screen 1");
//public static readonly BlitzyUI.Screen.ScreenID ScreenId_Screen2 = new BlitzyUI.Screen.ScreenID("Screen 2");

////string prefabname - screenID
//public static readonly Dictionary<string, BlitzyUI.Screen.ScreenID> dictionary = new Dictionary<string, BlitzyUI.Screen.ScreenID>()
//{
//    { "Screen_CreateFlashcard",new BlitzyUI.Screen.ScreenID("CreateFlashcard") },
//    { "Screen_Navicon",new BlitzyUI.Screen.ScreenID("Navicon") },
//    { "Screen1", new BlitzyUI.Screen.ScreenID("Screen 1") },
//    { "Screen2", new BlitzyUI.Screen.ScreenID("Screen 2")}
//};
