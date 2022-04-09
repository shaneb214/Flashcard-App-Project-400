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
    }

    private void OnEnable()
    {
        //Spawn in language profile prefabs for each profile.
        List<LanguageProfile> userLanguageProfiles = LanguageProfileController.Instance.GetUserLanguageProfiles();
        userLanguageProfiles.ForEach(profile => SpawnLanguageProfileDisplayInScrollView(profile));
    }

    private void OnDisable()
    {
        ClearScrollViewItems();
    }

}
