using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameToPlayScrollViewManager : ScrollViewItemManager
{
    public string CurrentSetChosenToPlay;

    [Header("Prefab Spawning")]
    [SerializeField] private GameDisplay gameDisplayPrefab;

    //Only one game so not a list.
    [SerializeField] private GameDisplayInfo wallGameDisplayInfo;

    private void OnEnable()
    {
        GameDisplay spawnedGameDisplay = SpawnItemInScrollView(gameDisplayPrefab);
        spawnedGameDisplay.UpdateDisplayInfoForGame(wallGameDisplayInfo);

        //Determine if game is available to play?
        Set setToPlay = SetsDataHolder.Instance.FindSetByID(CurrentSetChosenToPlay);
        bool canPlay = FlashcardDataHolder.Instance.FindFlashcardsBySetID(CurrentSetChosenToPlay).Count >= wallGameDisplayInfo.gameSettings.MinimumCardRequirementToPlay;

        spawnedGameDisplay.UpdateAvailabilityDisplay(canPlay);
    }
    private void OnDisable()
    {
        ClearScrollViewItems();
    }
}
