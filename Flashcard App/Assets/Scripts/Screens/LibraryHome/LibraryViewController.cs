using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibraryViewController : MonoBehaviour
{
    public static string DefaultSet;
    public static string SetIDCurrentlyShowing;

    [SerializeField] protected Transform scrollViewContentTransform;
    [SerializeField] protected SetDisplay setDisplayPrefab;
    [SerializeField] protected FlashcardDisplay flashcardDisplayPrefab;
    [SerializeField] protected TextMeshProUGUI txtNoSetsWarning;

    public enum DisplayScreen { None, Home, SetView }
    public static DisplayScreen CurrentDisplayScreen;

    public bool ScrollViewContainsItems { get { return scrollViewContentTransform.childCount > 0; } }

    protected void DestroyItemsInScrollView()
    {
        for (int i = 0; i < scrollViewContentTransform.childCount; i++)
        {
            Transform child = scrollViewContentTransform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    protected void SpawnSetDisplayInScrollView(Set setToSpawn)
    {
        //Spawn prefab + pass in info so it can update its components.
        SetDisplay spawnedSetDisplay = Instantiate(setDisplayPrefab, scrollViewContentTransform);
        spawnedSetDisplay.UpdateDisplay(setToSpawn.ID, setToSpawn.Name);
    }

    protected void SpawnFlashcardDisplayInScrollView(Flashcard flashcardToSpawn)
    {
        FlashcardDisplay spawnedFlashcardDisplay = Instantiate(flashcardDisplayPrefab, scrollViewContentTransform);
        spawnedFlashcardDisplay.UpdateDisplay(flashcardToSpawn);
    }

    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
}
