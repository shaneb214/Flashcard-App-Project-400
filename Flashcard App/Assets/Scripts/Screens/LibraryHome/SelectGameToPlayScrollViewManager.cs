using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameToPlayScrollViewManager : ScrollViewItemManager
{
    public string CurrentSetIDChosenToPlay;

    [Header("Prefab Spawning")]
    [SerializeField] private GameDisplay gameDisplayPrefab;

    //Only one game so not a list.
    [SerializeField] private GameDisplayInfo wallGameDisplayInfo;

    private void OnEnable()
    {
        GameDisplay spawnedGameDisplay = SpawnItemInScrollView(gameDisplayPrefab);
        spawnedGameDisplay.UpdateDisplayInfoForGame(wallGameDisplayInfo,CurrentSetIDChosenToPlay);

        //Determine if game is available to play?
        Set setToPlay = SetsDataHolder.Instance.FindSetByID(CurrentSetIDChosenToPlay);
        bool canPlay = FlashcardDataHolder.Instance.FindFlashcardsBySetID(CurrentSetIDChosenToPlay).Count >= wallGameDisplayInfo.gameSettings.MinimumCardRequirementToPlay;

        spawnedGameDisplay.UpdateAvailabilityDisplay(canPlay);
    }
    private void OnDisable()
    {
        ClearScrollViewItems();
    }
}
