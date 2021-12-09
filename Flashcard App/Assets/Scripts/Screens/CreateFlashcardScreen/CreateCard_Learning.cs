using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard_Learning : CreateCard
{
    private string profileIDToRepresent;

    private void Start()
    {
        //LanguageProfile currentProfile = LanguageProfileController.Instance.userCurrentLanguageProfile;
        //UpdateDisplayBasedOnCurrentProfile(currentProfile);
    }

    private void UpdateDisplayBasedOnCurrentProfile(LanguageProfile currentProfile)
    {
        Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentProfile.learningLanguage.ISO}");
        string placeholderText = $"Type in {currentProfile.learningLanguage._name}..";

        UpdateDisplay(nativeFlagSprite, placeholderText);
    }

    //React to new profile being selected by user & update display.
    private void OnNewProfileSelected(LanguageProfile newSelectedProfile)
    {
        print("reacted to new profile selection");

        ClearUserInput();
        UpdateDisplayBasedOnCurrentProfile(newSelectedProfile);
    }

    private void OnEnable()
    {
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnNewProfileSelected;

        LanguageProfile currentProfile = LanguageProfileController.Instance.userCurrentLanguageProfile;
        if(profileIDToRepresent != currentProfile.ID)
            UpdateDisplayBasedOnCurrentProfile(currentProfile);
    }

    private void OnDisable() => LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnNewProfileSelected;
}