using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Base script which displays a set with its information.
//Default set selection display and library set display inherit from this. 

public class SetDisplay : MonoBehaviour
{
    public string setIDToRepresent;

    [Header("Components")]
    [SerializeField] protected Button btnSelectSet;
    [SerializeField] protected TextMeshProUGUI txtSetName;
    [SerializeField] protected TextMeshProUGUI txtCardCount;
    [SerializeField] protected TextMeshProUGUI txtSubsetCount;
    [SerializeField] protected Image imgDefaultSetIcon;

    //Start.
    public virtual void Awake() { }
    public virtual void Start() { } 

    public virtual void UpdateDisplay(string setID, string setName)
    {
        setIDToRepresent = setID;
        txtSetName.text = setName;

        imgDefaultSetIcon.enabled = SetsDataHolder.Instance.DefaultSetID == setIDToRepresent ? true : false;

        txtCardCount.text = $"{FlashcardDataHolder.Instance.FlashcardCountOfSet(setID)} Cards";
        txtSubsetCount.text = $"{SetsDataHolder.Instance.GetSubsetCountOfSet(setID)} Sets";
    }

    public void SetDefaultIconImage(bool enabled) => imgDefaultSetIcon.enabled = enabled;

}
