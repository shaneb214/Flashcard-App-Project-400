using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Attached to prefab to represent a set.
//Shows set name, clicking on this will open the set and user can see the flashcards within.

public class SetDisplayLibrary : SetDisplay
{
    public static Action<SetDisplayLibrary> SetDisplaySelectedEvent;

    public override void Start() => btnSelectSet.onClick.AddListener(OnSelectSetButtonPressed);

    private void OnSelectSetButtonPressed()
    {
        //Enter set and display subsets and any flashcards within.
        SetDisplaySelectedEvent?.Invoke(this);
    }
}