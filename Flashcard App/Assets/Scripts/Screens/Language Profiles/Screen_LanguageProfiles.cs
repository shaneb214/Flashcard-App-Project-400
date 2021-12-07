using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A screen where user can see each of their language profiles
//For each profile they can delete, view stats for that profile, create a new profile.

public class Screen_LanguageProfiles : BlitzyUI.Screen
{

    [SerializeField] private LanguageProfileDisplay languageProfilePrefab;
    [SerializeField] private Transform scrollViewContentTransform;

    public override void OnSetup()
    {
        //Spawn in language profile prefabs?

        List<LanguageProfile> userLanguageProfiles = LanguageProfileController.Instance.GetUserLanguageProfiles();

        for (int i = 0; i < userLanguageProfiles.Count; i++)
        {
            //Get/Load info for prefab to to spawned.
            Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{userLanguageProfiles[i].nativeLanguage.ISO}");
            Sprite learningFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{userLanguageProfiles[i].learningLanguage.ISO}");
            string headingText = $"{userLanguageProfiles[i].nativeLanguage._name} - {userLanguageProfiles[i].learningLanguage._name}";

            //Spawn prefab + pass in info so it can update its components.
            LanguageProfileDisplay spawnedLanguageProfileDisplay = Instantiate(languageProfilePrefab, scrollViewContentTransform);
            spawnedLanguageProfileDisplay.UpdateDisplay(nativeFlagSprite,learningFlagSprite,headingText);
        }
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
}
