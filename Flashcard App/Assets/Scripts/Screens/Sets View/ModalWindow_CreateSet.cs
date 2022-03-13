using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindow_CreateSet : CustomModalWindow
{
    [Header("Window Components")]
    [SerializeField] private CustomInputField setNameInputField;
    [SerializeField] private SwitchManager defaultSetSwitch;
    [SerializeField] private Button btnCancelCreation;
    [SerializeField] private Button btnCreateSet;

    private string parentID;
    public string ParentID { get { return parentID; } set { parentID = value; } } 

    private bool CanCreateSet { get { return setNameInputField.inputField.text != string.Empty; } }

    public override void Awake() => base.Awake();


    public override void Start()
    {
        btnCancelCreation.onClick.AddListener(CloseAndResetInputField);
        btnCreateSet.onClick.AddListener(OnCreateSetButtonPressed);
    }

    private void CloseAndResetInputField() 
    {
        setNameInputField.ClearText(updateInstant: true);
        CloseWindow();
    }
    private void OnCreateSetButtonPressed() 
    {
        if(CanCreateSet)
        {
            //Create set.
            string setName = setNameInputField.inputField.text;
            bool setDefaultSet = defaultSetSwitch.isOn;

            //Create new set. 
            //Set as default where???
            Set newSet = new Set(setName,LibraryViewController.SetIDCurrentlyShowing);


            CloseAndResetInputField();
        }
        else
        {
            //Display error? or something
        } 
    }
}
