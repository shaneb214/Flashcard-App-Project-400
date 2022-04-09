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
        spawnedSetDisplay.SetDisplaySelectedEvent += OnSetDisplaySelected;
    }

    private void OnSetDisplaySelected(SetDisplayDefaultSelection setDisplayPressed)
    {
        //If new selection.
        if(setDisplayPressed.setIDToRepresent != currentDefaultSetSpawnedDisplay.setIDToRepresent)
        {
            currentDefaultSetSpawnedDisplay.SetDefaultIconImage(false);
            currentDefaultSetSpawnedDisplay = setDisplayPressed;
            setDisplayPressed.SetDefaultIconImage(true);
        }
    }

    private void OnEnable()
    {      
        List<Set> setsToDisplay = SetsDataHolder.Instance.FindSetsByLangProfileID(LanguageProfileController.Instance.CurrentLanguageProfile.ID);
        //int defaultSetIndex = setsToDisplay.FindIndex(set => set.ID == LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID);
        //Set defaultSet = setsToDisplay[defaultSetIndex];
        //setsToDisplay.RemoveAt(defaultSetIndex);
        //setsToDisplay.Insert(0, defaultSet);

        for (int i = 0; i < setsToDisplay.Count; i++)
        {
            SpawnSetDisplay(setsToDisplay[i]);
        }

        currentDefaultSetSpawnedDisplay = spawnedSetDisplayList.Find(sd => sd.setIDToRepresent == SetsDataHolder.Instance.DefaultSetID);

        SetScrollRectPosition(1f);
    }
    private void OnDisable()
    {
        //Need for this?????

        //for (int i = 0; i < spawnedSetDisplayList.Count; i++)
        //{
        //    spawnedSetDisplayList[i].SetDisplaySelectedEvent -= OnSetDisplaySelected;
        //}

        ClearScrollViewItems();
        spawnedSetDisplayList.Clear();
    }
}