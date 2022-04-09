using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard_Native : CreateCard
{
    private void Start()
    {
        UpdateDisplayBasedOnCurrentProfile(LanguageProfileController.Instance.CurrentLanguageProfile);
    }

    private void UpdateDisplayBasedOnCurrentProfile(LanguageProfile currentProfile)
    {
        Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentProfile.NativeLanguage.ISO}");
        string placeholderText = $"Enter word in {currentProfile.NativeLanguage.Name}..";

        UpdateDisplay(nativeFlagSprite, placeholderText);
    }

    private void OnEnable()
    {
        LanguageProfile currentProfile = LanguageProfileController.Instance.CurrentLanguageProfile;
        if (profileIDToRepresent != currentProfile.ID)
            UpdateDisplayBasedOnCurrentProfile(currentProfile);
    }

}
