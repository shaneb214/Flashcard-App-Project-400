using BlitzyUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnCreateSet : BtnPopScreen
{
    [Header("Other Components")]
    [SerializeField] private TMP_InputField setNameInputField;
    [SerializeField] private Toggle setCurrentSetToggle;

    private bool CanCreateSet { get { return setNameInputField.text != string.Empty; } }

    public override void OnButtonClick()
    {
        if (CanCreateSet == false)
            return;

        string setName = setNameInputField.text;
        bool setDefaultSet = setCurrentSetToggle.isOn;


        //Create new set. 
        //Set as default where???
        Set newSet = new Set(setName);

        //Profile Created - Pop screen.
        BlitzyUI.Screen topScreen = UIManager.Instance.GetTopScreen();

        if (CallPopSequence && topScreen.AllowStartPoppingSequence)
            topScreen.StartPoppingSequence(null);
        else
            UIManager.Instance.QueuePop(null);

        //So user can't select button twice. 
        //Improve this some other time.
        myButton.interactable = false;
    }
}
