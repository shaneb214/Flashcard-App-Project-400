using BlitzyUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Home library page where you can only create sets.
//Sets created here wont have a parent.
//Cards can only be created within sets. Change in future?

public class LibraryHomeViewController : LibraryViewController
{
    [SerializeField] private ScreenPushData setsViewScreen;

    [SerializeField] private List<SetDisplayLibrary> spawnedSetDisplayList = new List<SetDisplayLibrary>();
    [SerializeField] private SetDisplayLibrary spawnedSetDisplayDefault;

    //Reacting to new Set being created.
    private void OnNewSetCreated(Set newSet)
    {
        SpawnSetDisplayInScrollView(newSet);
        UpdateNoSetsWarning();
    }

    private void SpawnSetDisplayInScrollView(Set newSet)
    {
        SetDisplayLibrary spawnedSetDisplay = SpawnItemInScrollView(setDisplayPrefab);
        spawnedSetDisplayList.Add(spawnedSetDisplay);

        spawnedSetDisplay.UpdateDisplay(newSet.ID, newSet.Name);

        if (newSet.IsDefaultSet)
        {
            SetsDataHolder.DefaultSetIDUpdatedEvent += spawnedSetDisplay.OnDefaultSetUpdated;
        }
    }

    private void OnSetDisplaySelected(SetDisplayLibrary setDisplaySelected)
    {
        Set setPressed = SetsDataHolder.Instance.FindSetByID(setDisplaySelected.setIDToRepresent);
        SetIDCurrentlyShowing = setDisplaySelected.setIDToRepresent;

        //Push screen to show set.
        UIManager.Instance.QueuePop();
        UIManager.Instance.QueuePush(setsViewScreen.ID);
    }

    private void UpdateNoSetsWarning()
    {
        txtNoSetsWarning.gameObject.SetActive(ScrollViewContainsItems == false);
    }

    //Reacting to new profile being selected.
    private void OnUserSelectedNewProfile(LanguageProfile newProfile)
    {
        //profileIDToShowSetsOf = newProfile.ID;

        //Clear set displays if any.
        // if (ScrollViewContainsSets)
        //DestroyItemsInScrollView();

        //Spawn new sets for current profile.
        //SpawnSetDisplayPrefabsForProfile(newProfile);
    }

    //Event subscribing / unsubscribing.
    public override void OnEnable()
    {
        base.OnEnable();

        Set.SetCreatedEvent += OnNewSetCreated;
        SetDisplayLibrary.SetDisplaySelectedEvent += OnSetDisplaySelected;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;

        SetIDCurrentlyShowing = string.Empty;

        //Check if new user was selected while I was disabled.
        //LanguageProfile currentProfile = LanguageProfileController.Instance.currentLanguageProfile;
        //if (profileIDToShowSetsOf != currentProfile.ID)
        //OnUserSelectedNewProfile(currentProfile);

        //Spawn sets with no parents.
        List<Set> setsToSpawnList = SetsDataHolder.Instance.FindSetsOfCurrentLanguageProfileByParentID(SetIDCurrentlyShowing);
        for (int i = 0; i < setsToSpawnList.Count; i++)
        {
            SpawnSetDisplayInScrollView(setsToSpawnList[i]);
        }

        UpdateNoSetsWarning();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        spawnedSetDisplayList.Clear();
        ClearScrollViewItems();

        Set.SetCreatedEvent -= OnNewSetCreated;
        SetDisplayLibrary.SetDisplaySelectedEvent -= OnSetDisplaySelected;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}
