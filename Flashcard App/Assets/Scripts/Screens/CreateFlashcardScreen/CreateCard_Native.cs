using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard_Native : CreateCard
{
    private void Start()
    {
        LanguageProfile currentProfile = LanguageProfileController.Instance.userCurrentLanguageProfile;

        Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentProfile.nativeLanguage.ISO}");
        string placeholderText = $"Type in {currentProfile.nativeLanguage._name}..";

        UpdateDisplay(nativeFlagSprite, placeholderText);
    }
}
