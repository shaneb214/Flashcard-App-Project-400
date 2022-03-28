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
    private void OnSetDisplayPressed(string setIDPressed)
    {
        Set setPressed = SetsDataHolder.Instance.FindSetByID(setIDPressed);
        SetIDCurrentlyShowing = setIDPressed;

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
        Set.SetCreatedEvent += OnNewSetCreated;
        SetDisplay.SetDisplayPressed += OnSetDisplayPressed;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;

        SetIDCurrentlyShowing = string.Empty;
        DestroyItemsInScrollView();

        //Check if new user was selected while I was disabled.
        //LanguageProfile currentProfile = LanguageProfileController.Instance.currentLanguageProfile;
        //if (profileIDToShowSetsOf != currentProfile.ID)
        //OnUserSelectedNewProfile(currentProfile);

        //Spawn sets with no parents.
        SetsDataHolder.Instance.FindSetsByParentID(SetIDCurrentlyShowing).ForEach(set => SpawnSetDisplayInScrollView(set));

        UpdateNoSetsWarning();
    }

    public override void OnDisable()
    {
        Set.SetCreatedEvent -= OnNewSetCreated;
        SetDisplay.SetDisplayPressed -= OnSetDisplayPressed;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}
