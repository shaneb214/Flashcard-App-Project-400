using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibraryViewController : ScrollViewItemManager
{
    public static string SetIDCurrentlyShowing;

    [SerializeField] protected SetDisplayLibrary setDisplayPrefab;
    [SerializeField] protected TextMeshProUGUI txtNoSetsWarning;

    //protected void SpawnSetDisplayInScrollView(Set setToSpawn)
    //{
    //    //Spawn prefab + pass in info so it can update its components.
    //    SetDisplayLibrary spawnedSetDisplay = SpawnItemInScrollView(setDisplayPrefab);
    //    spawnedSetDisplay.UpdateDisplay(setToSpawn.ID, setToSpawn.Name);
    //    spawnedSetDisplayList.Add(spawnedSetDisplay);
    //    spawnedSetDisplay.SetDisplaySelectedEvent += OnSetDisplaySelected;
    //}

    //protected void ClearSpawnedSetDisplayList() => spawnedSetDisplayList.Clear();

    //protected void SpawnFlashcardDisplayInScrollView(Flashcard flashcardToSpawn)
    //{
    //    FlashcardDisplay spawnedFlashcardDisplay = SpawnItemInScrollView(flashcardDisplayPrefab);
    //    spawnedFlashcardDisplay.UpdateDisplay(flashcardToSpawn);
    //}

    //private void OnSetDisplaySelected(SetDisplayLibrary setDisplaySelected)
    //{

    //}

    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
}
