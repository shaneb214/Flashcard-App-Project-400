using BlitzyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Display warning if user is wanting to create a profile where there already is a profile with the same languages selected.

public class BtnCreateLanguageProfile : BtnPopScreen
{
    [Header("Other Components")]
    [SerializeField] private DropDownSelectLanguage nativeLanguageDropdown;
    [SerializeField] private DropDownSelectLanguage learningLanguageDropdown;
    [SerializeField] private Toggle setCurrentProfileToggle;


    public override void OnButtonClick()
    {
        Language nativeLanguage = nativeLanguageDropdown.GetLanguageSelected;
        Language learningLanguage = learningLanguageDropdown.GetLanguageSelected;
        bool setCurrentProfile = setCurrentProfileToggle.isOn;

        //Create new profile - language profile controller gets notified through event.
        LanguageProfile newProfile = new LanguageProfile(nativeLanguage, learningLanguage, setCurrentProfile);

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