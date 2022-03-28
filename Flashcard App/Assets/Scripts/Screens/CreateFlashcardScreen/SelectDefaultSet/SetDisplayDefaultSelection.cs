using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetDisplayDefaultSelection : MonoBehaviour
{
    int indexInScrollView;
    public static Action<int> SelectedEvent;

    private string setIDToRepresent;

    [Header("Components")]
    [SerializeField] private Button btnSelectSet;
    [SerializeField] private TextMeshProUGUI txtSetName;
    [SerializeField] private TextMeshProUGUI txtCardCount;
    [SerializeField] private TextMeshProUGUI txtSubsetCount;
    [SerializeField] private Image imgDefaultSetIcon;

    private void Awake() { }
    private void Start() => btnSelectSet.onClick.AddListener(OnSelectSetButtonPressed);

    private void OnSelectSetButtonPressed()
    {
        //Set default set as one just pressed.
        LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID = setIDToRepresent;
        imgDefaultSetIcon.enabled = true;

        SelectedEvent?.Invoke(indexInScrollView);
    }

    public void UpdateDisplay(string setID, string setName,int index)
    {
        setIDToRepresent = setID;
        txtSetName.text = setName;
        indexInScrollView = index;

        imgDefaultSetIcon.enabled = LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID == setIDToRepresent ? true : false;
    }

    public void SetDefaultIconImage(bool enabled)
    {
        imgDefaultSetIcon.enabled = enabled;
    }
}
