using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Active in view sets screen - spawns in prefabs which show user the sets that are in their current language profile.

public class SetsViewController : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContentTransform;
    [SerializeField] private SetDisplay setDisplayPrefab;

    private bool ScrollViewContainsItems { get { return scrollViewContentTransform.childCount > 0; } }

    //Start.
    private void Start()
    {
        SpawnSetDisplayPrefabsForProfile(LanguageProfileController.Instance.userCurrentLanguageProfile);
    }

    //Spawning Set Display Prefabs.
    private void SpawnSetDisplayInScrollView(Set setToSpawn)
    {
        string setName = setToSpawn._name;

        //Spawn prefab + pass in info so it can update its components.
        SetDisplay spawnedSetDisplay = Instantiate(setDisplayPrefab, scrollViewContentTransform);
        spawnedSetDisplay.UpdateDisplay(setName);
    }
    private void SpawnSetDisplayPrefabsForProfile(LanguageProfile profile)
    {
        List<Set> setList = profile.setList;
        setList.ForEach(set => SpawnSetDisplayInScrollView(set));
    }

    //Reacting to new Set being created.
    private void OnNewSetCreated(Set newSet)
    {
        SpawnSetDisplayInScrollView(newSet);
    }
    //Reacting to new profile being selected.
    private void OnUserSelectedNewProfile(LanguageProfile newProfile)
    {
        //Clear set displays if any.
        if (ScrollViewContainsItems)
            DestroyItemsInScrollView();

        //Spawn new sets for current profile.
        SpawnSetDisplayPrefabsForProfile(newProfile);
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
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewProfile;
    }

    private void OnDestroy()
    {
        Set.SetCreatedEvent -= OnNewSetCreated;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;     
    }

    private void OnDisable()
    {
        Set.SetCreatedEvent -= OnNewSetCreated;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewProfile;
    }
}
