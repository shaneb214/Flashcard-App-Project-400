using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Active in view sets screen - spawns in prefabs which show user the sets that are in their current language profile.

public class SetsViewController : MonoBehaviour
{
    public static Action EnteredNonParentedSetsDisplayEvent;

    private string profileIDToShowSetsOf;
    [SerializeField] private string setIDCurrentlyShowing;
    [SerializeField] private bool showingNonParentedSets; 

    [SerializeField] private Transform scrollViewContentTransform;
    [SerializeField] private SetDisplay setDisplayPrefab;
    [SerializeField] private TextMeshProUGUI txtNoSetsWarning;
    [SerializeField] private Button btnCreateFlashcard;

    private bool ScrollViewContainsSets { get { return scrollViewContentTransform.childCount > 0; } }

    //Spawning Set Display Prefabs.
    private void SpawnSetDisplayInScrollView(Set setToSpawn)
    {
        //Spawn prefab + pass in info so it can update its components.
        SetDisplay spawnedSetDisplay = Instantiate(setDisplayPrefab, scrollViewContentTransform);
        spawnedSetDisplay.UpdateDisplay(setToSpawn.ID,setToSpawn.Name);
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
    private void OnSetDisplayPressed(string setIDPressed)
    {
        Set setPressed = SetsDataHolder.Instance.FindSetByID(setIDPressed);

        setIDCurrentlyShowing = setIDPressed;
        showingNonParentedSets = false;
        //Go into set. 
        //Change top bar heading text to set name.
        //Enable make flashcard button.
    }

    private void ShowNonParentSetsScreen()
    {
        showingNonParentedSets = true;

        btnCreateFlashcard.gameObject.SetActive(false);
        setIDCurrentlyShowing = string.Empty;

    }

    private void UpdateNoSetsWarning()
    {
        txtNoSetsWarning.gameObject.SetActive(ScrollViewContainsSets == false);
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
        SetDisplay.SetDisplayPressed += OnSetDisplayPressed;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;

        if(showingNonParentedSets)
        {

        }



        //Check if new user was selected while I was disabled.
        //LanguageProfile currentProfile = LanguageProfileController.Instance.currentLanguageProfile;
        //if (profileIDToShowSetsOf != currentProfile.ID)
            //OnUserSelectedNewProfile(currentProfile);

        UpdateNoSetsWarning();
    }

    private void OnDisable()
    {
        Set.SetCreatedEvent -= OnNewSetCreated;
        SetDisplay.SetDisplayPressed -= OnSetDisplayPressed;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}
