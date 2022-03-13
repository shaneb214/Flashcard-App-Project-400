using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Attached to prefab to represent a set.
//Shows set name, clicking on this will open the set and user can see the flashcards within.

public class SetDisplay : MonoBehaviour
{
    public static Action<string> SetDisplayPressed;
    private string setIDToRepresent;

    [Header("Components")]
    [SerializeField] private Button btnSelectSet;
    [SerializeField] private TextMeshProUGUI txtSetName;
    [SerializeField] private TextMeshProUGUI txtCardCount;
    [SerializeField] private TextMeshProUGUI txtSubsetCount;

    //Start.
    private void Awake() { }
    private void Start() => btnSelectSet.onClick.AddListener(OnSelectSetButtonPressed);

    private void OnSelectSetButtonPressed()
    {
        //Enter set and display subsets and any flashcards within.
        SetDisplayPressed?.Invoke(setIDToRepresent);
    }

    public void UpdateDisplay(string setID,string setName)
    {
        setIDToRepresent = setID;
        txtSetName.text = setName;
    }
}