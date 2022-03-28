using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetDisplayDefaultSelection : MonoBehaviour
{
    //public static Action<string> SetDisplayPressed;
    private string setIDToRepresent;

    [Header("Components")]
    [SerializeField] private Button btnSelectSet;
    [SerializeField] private TextMeshProUGUI txtSetName;
    [SerializeField] private TextMeshProUGUI txtCardCount;
    [SerializeField] private TextMeshProUGUI txtSubsetCount;


    private void Awake() { }
    private void Start() => btnSelectSet.onClick.AddListener(OnSelectSetButtonPressed);

    private void OnSelectSetButtonPressed()
    {
        //Set default set as one just pressed.
        LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID = setIDToRepresent;
    }

    public void UpdateDisplay(string setID, string setName)
    {
        setIDToRepresent = setID;
        txtSetName.text = setName;
    }
}
