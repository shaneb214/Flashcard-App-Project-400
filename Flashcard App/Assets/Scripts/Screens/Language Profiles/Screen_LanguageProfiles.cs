using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A screen where user can see each of their language profiles
//For each profile they can delete, view stats for that profile, create a new profile.

//This screen listens out for language profile created event which can happen 
//if the user creates a profile through a popup that can be spawned if pressing a button on this screen.
//This screen is not destroyed so it reacts to event and spawns a profile display.

//TODO: Move this spawning of profile displays to a different script?

public class Screen_LanguageProfiles : BlitzyUI.Screen
{

    [SerializeField] private LanguageProfileDisplay languageProfilePrefab;
    [SerializeField] private Transform scrollViewContentTransform;

    public override void OnSetup()
    {
        //Spawn in language profile prefabs for each profile.
        List<LanguageProfile> userLanguageProfiles = LanguageProfileController.Instance.GetUserLanguageProfiles();
        userLanguageProfiles.ForEach(profile => SpawnProfileDisplayInScrollView(profile));
    }

    private void SpawnProfileDisplayInScrollView(LanguageProfile languageProfileToShow)
    {
        Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{languageProfileToShow.nativeLanguage.ISO}");
        Sprite learningFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{languageProfileToShow.learningLanguage.ISO}");
        string headingText = $"{languageProfileToShow.nativeLanguage._name} - {languageProfileToShow.learningLanguage._name}";

        //Spawn prefab + pass in info so it can update its components.
        LanguageProfileDisplay spawnedLanguageProfileDisplay = Instantiate(languageProfilePrefab, scrollViewContentTransform);
        spawnedLanguageProfileDisplay.UpdateDisplay(languageProfileToShow.ID,nativeFlagSprite, learningFlagSprite, headingText);
    }


    public override void OnFocus()
    {

    }

    public override void OnFocusLost()
    {

    }

    public override void OnPop()
    {

        PopFinished();
    }

    public override void OnPush(ScreenData data)
    {


        PushFinished();
    }
    public override void StartPoppingSequence(Action callbackOnPopEnd = null)
    {
        base.StartPoppingSequence(callbackOnPopEnd);
    }

    private void OnEnable() => LanguageProfile.LanguageProfileCreatedEvent += SpawnProfileDisplayInScrollView;
    private void OnDisable() => LanguageProfile.LanguageProfileCreatedEvent -= SpawnProfileDisplayInScrollView;
}
