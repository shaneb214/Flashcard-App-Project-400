using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryDisplayController : MonoBehaviour
{
    public static LibraryDisplayController Instance;

    public enum DisplayScreen { None, Home, SetView }
    public DisplayScreen CurrentDisplayScreen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SetDisplay.SetDisplayPressed += OnSetDisplayPressed;

    }
    private void Start()
    {
        CurrentDisplayScreen = DisplayScreen.None;
    }

    private void OnSetDisplayPressed(string setIDPressed)
    {
        switch (CurrentDisplayScreen)
        {
            case DisplayScreen.None:
                break;
            case DisplayScreen.Home:
                break;
            case DisplayScreen.SetView:
                break;
            default:
                break;
        }
    }


    private void OnDestroy()
    {
        SetDisplay.SetDisplayPressed -= OnSetDisplayPressed;

    }
}
