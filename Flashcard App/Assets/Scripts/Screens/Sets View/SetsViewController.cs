using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Active in view sets screen - spawns in prefabs which show user the sets that are in their current language profile.

public class SetsViewController : MonoBehaviour
{
    private string profileIDToShowSetsOf;
    private string parentSetIDCurrentlyShowing;

    [SerializeField] private Transform scrollViewContentTransform;
    [SerializeField] private SetDisplay setDisplayPrefab;
    [SerializeField] private TextMeshProUGUI txtNoSetsWarning;

    private bool ScrollViewContainsSets { get { return scrollViewContentTransform.childCount > 0; } }

    //Spawning Set Display Prefabs.
    private void SpawnSetDisplayInScrollView(Set setToSpawn)
    {
        //Spawn prefab + pass in info so it can update its components.
        SetDisplay spawnedSetDisplay = Instantiate(setDisplayPrefab, scrollViewContentTransform);
        spawnedSetDisplay.UpdateDisplay(setToSpawn._name);
    }
    private void SpawnSetDisplayPrefabsForProfile(LanguageProfile profile)
    {
        //List<Set> setList = profile.setList;
        //setList.ForEach(set => SpawnSetDisplayInScrollView(set));
    }

    //Reacting to new Set being created.
    private void OnNewSetCreated(Set newSet)
    {
        SpawnSetDisplayInScrollView(newSet);
        UpdateNoSetsWarning();
    }
    private void UpdateNoSetsWarning()
    {
        txtNoSetsWarning.gameObject.SetActive(ScrollViewContainsSets == false);
    }

    //Reacting to new profile being selected.
    private void OnUserSelectedNewProfile(LanguageProfile newProfile)
    {
        profileIDToShowSetsOf = newProfile.ID;

        //Clear set displays if any.
        if (ScrollViewContainsSets)
            DestroyItemsInScrollView();

        //Spawn new sets for current profile.
        SpawnSetDisplayPrefabsForProfile(newProfile);
    }

    //Clearing scroll view.
    private void DestroyItemsInScrollView()
    {
        for (int i = 0; i < scrollViewContentTransform.childCount; i++)
        {
            Transform child = scrollViewContentTransform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    //Event subscribing / unsubscribing.
    private void OnEnable()
    {
        Set.SetCreatedEvent += OnNewSetCreated;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;

        //Check if new user was selected while I was disabled.
        LanguageProfile currentProfile = LanguageProfileController.Instance.userCurrentLanguageProfile;
        if (profileIDToShowSetsOf != currentProfile.ID)
            OnUserSelectedNewProfile(currentProfile);

        UpdateNoSetsWarning();
    }

    private void OnDisable()
    {
        Set.SetCreatedEvent -= OnNewSetCreated;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}
