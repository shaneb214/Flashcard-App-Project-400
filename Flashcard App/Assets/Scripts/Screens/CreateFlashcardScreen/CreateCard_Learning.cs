using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard_Learning : CreateCard
{
    private void Start()
    {
        UpdateDisplayBasedOnCurrentProfile(LanguageProfileController.Instance.CurrentLanguageProfile);
    }

    private void UpdateDisplayBasedOnCurrentProfile(LanguageProfile currentProfile)
    {
        Sprite learningFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentProfile.LearningLanguage.ISO}");
        string placeholderText = $"Enter word in {currentProfile.LearningLanguage.Name}..";

        UpdateDisplay(learningFlagSprite, placeholderText);
    }

    private void OnEnable()
    {
        LanguageProfile currentProfile = LanguageProfileController.Instance.CurrentLanguageProfile;
        if (profileIDToRepresent != currentProfile.ID)
            UpdateDisplayBasedOnCurrentProfile(currentProfile);
    }
}