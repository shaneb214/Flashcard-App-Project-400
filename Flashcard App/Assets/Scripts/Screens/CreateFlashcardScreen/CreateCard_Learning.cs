using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard_Learning : CreateCard
{
    private void Start()
    {
        LanguageProfile currentProfile = LanguageProfileController.Instance.userCurrentLanguageProfile;

        Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentProfile.learningLanguage.ISO}");
        string placeholderText = $"Type in {currentProfile.learningLanguage._name}..";

        UpdateDisplay(nativeFlagSprite, placeholderText);
    }
}
