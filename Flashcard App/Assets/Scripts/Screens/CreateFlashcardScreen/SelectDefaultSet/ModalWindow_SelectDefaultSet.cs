using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Window that appears when user selects default set button on create flashcard screen (home screen)


public class ModalWindow_SelectDefaultSet : CustomModalWindow
{
    [Header("Components")]
    [SerializeField] private Transform scrollViewContentTransform;
    [SerializeField] private Button btnOk;

    [Header("Prefabs to Spawn")]
    [SerializeField] private SetDisplayDefaultSelection setDisplayPrefab;


    public override void Awake()
    {
        base.Awake();
        btnOk.onClick.AddListener(CloseWindow);
    }

    public override void Start() => base.Start();

    private void SpawnSetDisplay(Set setToRepresent)
    {
        SetDisplayDefaultSelection spawnedSetDisplay = Instantiate(setDisplayPrefab, scrollViewContentTransform);
        spawnedSetDisplay.UpdateDisplay(setToRepresent.ID, setToRepresent.Name);
    }

    private void DestroySetDisplaysInScrollView()
    {
        for (int i = 0; i < scrollViewContentTransform.childCount; i++)
        {
            Transform child = scrollViewContentTransform.GetChild(i);
            Destroy(child.gameObject);
        }
    }


    private void OnEnable() 
    {
        SetsDataHolder.Instance.FindSetsByLangProfileID(LanguageProfileController.Instance.currentLanguageProfile.ID).ForEach(set => SpawnSetDisplay(set));
    }
    private void OnDisable() 
    { 

    
    }
}
