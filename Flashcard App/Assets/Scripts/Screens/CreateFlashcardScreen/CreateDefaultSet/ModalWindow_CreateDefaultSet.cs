using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//To do: create base class for a modal window where you create a set.

public class ModalWindow_CreateDefaultSet : CustomModalWindow
{
    [Header("Components")]
    [SerializeField] private CustomInputField setNameInputField;
    [SerializeField] private Button btnCancel;
    [SerializeField] private Button btnCreate;

    private bool CanCreateSet { get { return setNameInputField.inputField.text != string.Empty; } }

    public override void Awake() => base.Awake();
    public override void Start()
    {
        btnCancel.onClick.AddListener(CloseAndResetInputField);
        btnCreate.onClick.AddListener(OnCreateSetButtonPressed);
    }

    private void CloseAndResetInputField() 
    {
        setNameInputField.ClearText(updateInstant: true);
        CloseWindow();
    }

    private void OnCreateSetButtonPressed()
    {
        if (CanCreateSet)
        {
            //Create set.
            string setName = setNameInputField.inputField.text;

            //Create new set. 
            Set newSet = new Set(setName, string.Empty,true,LanguageProfileController.Instance.currentLanguageProfile.ID);
            SetsDataHolder.Instance.DefaultSetID = newSet.ID;

            CloseAndResetInputField();
        }
        else
        {
            //Display error?
        }
    }
}
