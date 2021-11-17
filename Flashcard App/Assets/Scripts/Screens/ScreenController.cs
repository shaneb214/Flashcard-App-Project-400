using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;


    public static readonly BlitzyUI.Screen.Id ScreenId_Screen1 = new BlitzyUI.Screen.Id("Screen 1");
    public static readonly BlitzyUI.Screen.Id ScreenId_Screen2 = new BlitzyUI.Screen.Id("Screen 2");

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.QueuePush(ScreenId_Screen1, null, "Screen1", null);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }
}
