using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageProfileScreenController : ScrollViewItemManager
{
    [Header("Prefab To Spawn")]
    [SerializeField] private LanguageProfileDisplay languageProfileDisplayPrefab;

    private void SpawnLanguageProfileDisplayInScrollView(LanguageProfile languageProfile)
    {
        //Spawn prefab + pass in info so it can update its components.
        LanguageProfileDisplay spawnedLanguageProfileDisplay = SpawnItemInScrollView(languageProfileDisplayPrefab);
        spawnedLanguageProfileDisplay.UpdateDisplay(languageProfile);

        if (languageProfile.IsCurrentProfile)
        {
            LanguageProfileController.Instance.UserSelectedNewProfileEvent += spawnedLanguageProfileDisplay.OnNewLanguageProfileSelected;
        }
    }

    private void OnEnable()
    {
        //Spawn in language profile prefabs for each profile.
        List<LanguageProfile> userLanguageProfiles = LanguageProfileController.Instance.GetUserLanguageProfiles();
        userLanguageProfiles.ForEach(profile => SpawnLanguageProfileDisplayInScrollView(profile));

        LanguageProfile.LanguageProfileCreatedEvent += OnNewLanguageProfileCreated;
    }

    private void OnNewLanguageProfileCreated(LanguageProfile newLanguageProfile) => SpawnLanguageProfileDisplayInScrollView(newLanguageProfile);

    private void OnDisable()
    {
        ClearScrollViewItems();
        LanguageProfile.LanguageProfileCreatedEvent -= OnNewLanguageProfileCreated;
    }
}
