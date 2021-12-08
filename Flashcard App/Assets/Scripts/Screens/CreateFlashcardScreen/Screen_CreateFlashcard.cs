using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;
using TMPro;

//Screen where user can create a flashcard that gets added to a certain set.

public class Screen_CreateFlashcard : BlitzyUI.Screen
{
    [SerializeField] private Button btnCreateFlashcard;


    public override void OnSetup() { }
    public override void OnFocus() { }
    public override void OnFocusLost() { }

    public override void OnPop()
    {
        PopFinished();
    }

    public override void OnPush(ScreenData data)
    {
        PushFinished();
    }
}
