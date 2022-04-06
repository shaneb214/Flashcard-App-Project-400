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
    [SerializeField] private Button btnPlaySet;
    public static Action<SetDisplayLibrary> SetDisplaySelectedEvent;
    public static Action<string> SetDisplayPlaySetSelectedEvent;

    public override void Start()
    {
        btnSelectSet.onClick.AddListener(OnSelectSetButtonPressed);
        btnPlaySet.onClick.AddListener(OnPlaySetButtonPressed);
    }

    private void OnSelectSetButtonPressed()
    {
        //Enter set and display subsets and any flashcards within.
        SetDisplaySelectedEvent?.Invoke(this);
    }
    private void OnPlaySetButtonPressed()
    {
        SetDisplayPlaySetSelectedEvent?.Invoke(setIDToRepresent);
    }
}