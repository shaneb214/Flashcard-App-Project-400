using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Active in view sets screen - spawns in prefabs which show user the sets that are in their current language profile.

public class LibrarySetViewController : LibraryViewController
{
    [SerializeField] private TextMeshProUGUI txtTopBarHeader; 
    [SerializeField] protected FlashcardDisplay flashcardDisplayPrefab;
    [SerializeField] private List<SetDisplayLibrary> spawnedSetDisplayList = new List<SetDisplayLibrary>();

    //Reacting to new Set being created.
    private void OnNewSetCreated(Set newSet)
    {
        SpawnSetDisplayInScrollView(newSet);
    }
    private void SpawnSetDisplayInScrollView(Set newSet)
    {
        SetDisplayLibrary spawnedSetDisplay = SpawnItemInScrollView(setDisplayPrefab);
        spawnedSetDisplayList.Add(spawnedSetDisplay);

        spawnedSetDisplay.UpdateDisplay(newSet.ID, newSet.Name);
    }

    public void DisplaySetContents(string setID)
    {
        Set setPressed = SetsDataHolder.Instance.FindSetByID(setID);
        SetIDCurrentlyShowing = setID;

        //Display data in set (subsets/flashcards).
        ClearScrollViewItems();
        SetsDataHolder.Instance.FindSetsOfCurrentLanguageProfileByParentID(SetIDCurrentlyShowing).ForEach(set => SpawnSetDisplayInScrollView(set));
        FlashcardDataHolder.Instance.FindFlashcardsBySetID(SetIDCurrentlyShowing).ForEach(flashcard => SpawnFlashcardDisplayInScrollView(flashcard));

        txtTopBarHeader.text = $"../{setPressed.Name}";
    }

    public void DisplaySetContents(SetDisplayLibrary setDisplaySelected)
    {
        DisplaySetContents(setDisplaySelected.setIDToRepresent);    
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

    protected void SpawnFlashcardDisplayInScrollView(Flashcard flashcardToSpawn)
    {
        FlashcardDisplay spawnedFlashcardDisplay = SpawnItemInScrollView(flashcardDisplayPrefab);
        spawnedFlashcardDisplay.UpdateDisplay(flashcardToSpawn);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        Set.SetCreatedEvent += OnNewSetCreated;
        SetDisplayLibrary.SetDisplaySelectedEvent += DisplaySetContents;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;

        SetsDataHolder.Instance.FindSetsOfCurrentLanguageProfileByParentID(SetIDCurrentlyShowing).ForEach(set => SpawnSetDisplayInScrollView(set));
        FlashcardDataHolder.Instance.FindFlashcardsBySetID(SetIDCurrentlyShowing).ForEach(flashcard => SpawnFlashcardDisplayInScrollView(flashcard));

        txtTopBarHeader.text = $"../{SetsDataHolder.Instance.FindSetByID(SetIDCurrentlyShowing).Name}";
    }
    public override void OnDisable()
    {
        base.OnDisable();

        //Destroy all sets/flashcards for now.
        spawnedSetDisplayList.Clear();
        ClearScrollViewItems();

        Set.SetCreatedEvent -= OnNewSetCreated;
        SetDisplayLibrary.SetDisplaySelectedEvent -= DisplaySetContents;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}