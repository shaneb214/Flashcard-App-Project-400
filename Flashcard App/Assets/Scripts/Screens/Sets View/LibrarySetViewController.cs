using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Active in view sets screen - spawns in prefabs which show user the sets that are in their current language profile.

public class LibrarySetViewController : LibraryViewController
{
    [SerializeField] private TextMeshProUGUI txtTopBarHeader; //Make seperate script for this and make it react to event?

    //Reacting to new Set being created.
    private void OnNewSetCreated(Set newSet)
    {
        SpawnSetDisplayInScrollView(newSet);
    }
    private void OnSetDisplayPressed(string setIDPressed)
    {
        Set setPressed = SetsDataHolder.Instance.FindSetByID(setIDPressed);
        SetIDCurrentlyShowing = setIDPressed;

        //Display data in set (subsets/flashcards).
        DestroyItemsInScrollView();
        SetsDataHolder.Instance.FindSetsByParentID(SetIDCurrentlyShowing).ForEach(set => SpawnSetDisplayInScrollView(set));
        FlashcardDataHolder.Instance.FindFlashcardsBySetID(SetIDCurrentlyShowing).ForEach(flashcard => SpawnFlashcardDisplayInScrollView(flashcard));

        txtTopBarHeader.text = $"../{SetsDataHolder.Instance.FindSetByID(SetIDCurrentlyShowing).Name}";
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

    public override void OnEnable()
    {
        Set.SetCreatedEvent += OnNewSetCreated;
        SetDisplay.SetDisplayPressed += OnSetDisplayPressed;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;

        //Destroy all sets for now.
        DestroyItemsInScrollView();
        SetsDataHolder.Instance.FindSetsByParentID(SetIDCurrentlyShowing).ForEach(set => SpawnSetDisplayInScrollView(set));
        FlashcardDataHolder.Instance.FindFlashcardsBySetID(SetIDCurrentlyShowing).ForEach(flashcard => SpawnFlashcardDisplayInScrollView(flashcard));


        txtTopBarHeader.text = $"../{SetsDataHolder.Instance.FindSetByID(SetIDCurrentlyShowing).Name}";
    }
    public override void OnDisable()
    {
        Set.SetCreatedEvent -= OnNewSetCreated;
        SetDisplay.SetDisplayPressed -= OnSetDisplayPressed;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}