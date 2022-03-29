using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDefaultSetScrollViewManager : ScrollViewItemManager
{
    [Header("Prefabs to Spawn")]
    [SerializeField] private SetDisplayDefaultSelection setDisplayPrefab;

    [SerializeField] private List<SetDisplayDefaultSelection> spawnedSetDisplayList = new List<SetDisplayDefaultSelection>();
    [SerializeField] private SetDisplayDefaultSelection currentDefaultSetSpawnedDisplay;

    private void SpawnSetDisplay(Set setToRepresent)
    {
        SetDisplayDefaultSelection spawnedSetDisplay = SpawnItemInScrollView(setDisplayPrefab);
        spawnedSetDisplay.UpdateDisplay(setToRepresent.ID, setToRepresent.Name);
        spawnedSetDisplayList.Add(spawnedSetDisplay);
        spawnedSetDisplay.SetDisplaySelected += OnSetDisplaySelected;
    }

    private void OnSetDisplaySelected(SetDisplayDefaultSelection setDisplayPressed/*int indexOfSelection*/)
    {
        print("gfgg");

        //If new selection.
        if(setDisplayPressed.setIDToRepresent != currentDefaultSetSpawnedDisplay.setIDToRepresent)
        {
            currentDefaultSetSpawnedDisplay.SetDefaultIconImage(false);
            currentDefaultSetSpawnedDisplay = setDisplayPressed;
            setDisplayPressed.SetDefaultIconImage(true);
        }

        //if(indexOfSelection != defaultSetIndex)
        //{
        //    scrollViewContentTransform.GetChild(defaultSetIndex).GetComponent<SetDisplayDefaultSelection>().SetDefaultIconImage(enabled: false);

        //    defaultSetIndex = indexOfSelection;
        //    scrollViewContentTransform.GetChild(indexOfSelection).GetComponent<SetDisplayDefaultSelection>().SetDefaultIconImage(enabled: true);
        //}
    }

    private void OnEnable()
    {      
        //SetDisplayDefaultSelection.SelectedEvent += OnSetDisplaySelected;

        List<Set> setsToDisplay = SetsDataHolder.Instance.FindSetsByLangProfileID(LanguageProfileController.Instance.currentLanguageProfile.ID);
        //int defaultSetIndex = setsToDisplay.FindIndex(set => set.ID == LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID);
        //Set defaultSet = setsToDisplay[defaultSetIndex];
        //setsToDisplay.RemoveAt(defaultSetIndex);
        //setsToDisplay.Insert(0, defaultSet);

        for (int i = 0; i < setsToDisplay.Count; i++)
        {
            SpawnSetDisplay(setsToDisplay[i]/*, i*/);
        }

        currentDefaultSetSpawnedDisplay = spawnedSetDisplayList.Find(sd => sd.setIDToRepresent == LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID);

        //defaultSetIndex = 0;
        SetScrollRectPosition(1f);
    }
    private void OnDisable()
    {
        ClearScrollViewItems();
        spawnedSetDisplayList.Clear();

        //SetDisplayDefaultSelection.SelectedEvent -= OnSetDisplaySelected;
    }
}