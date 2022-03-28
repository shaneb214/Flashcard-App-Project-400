using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Window that appears when user selects default set button on create flashcard screen (home screen)


public class ModalWindow_SelectDefaultSet : CustomModalWindow
{
    int defaultSetIndex;

    [Header("Components")]
    [SerializeField] private Transform scrollViewContentTransform;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button btnOk;

    [Header("Prefabs to Spawn")]
    [SerializeField] private SetDisplayDefaultSelection setDisplayPrefab;


    public override void Awake()
    {
        base.Awake();
        btnOk.onClick.AddListener(CloseWindow);
    }

    public override void Start() => base.Start();

    private void SpawnSetDisplay(Set setToRepresent,int index)
    {
        SetDisplayDefaultSelection spawnedSetDisplay = Instantiate(setDisplayPrefab, scrollViewContentTransform);
        spawnedSetDisplay.UpdateDisplay(setToRepresent.ID, setToRepresent.Name,index);
    }

    private void OnSetDisplaySelected(int indexOfSelection)
    {
        if(indexOfSelection != defaultSetIndex)
        {
            scrollViewContentTransform.GetChild(defaultSetIndex).GetComponent<SetDisplayDefaultSelection>().SetDefaultIconImage(enabled: false);

            defaultSetIndex = indexOfSelection;
            scrollViewContentTransform.GetChild(indexOfSelection).GetComponent<SetDisplayDefaultSelection>().SetDefaultIconImage(enabled: true);
        }
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
        SetDisplayDefaultSelection.SelectedEvent += OnSetDisplaySelected;

        List<Set> setsToDisplay = SetsDataHolder.Instance.FindSetsByLangProfileID(LanguageProfileController.Instance.currentLanguageProfile.ID);
        int defaultSetIndex = setsToDisplay.FindIndex(set => set.ID == LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID);
        Set defaultSet = setsToDisplay[defaultSetIndex];
        setsToDisplay.RemoveAt(defaultSetIndex);
        setsToDisplay.Insert(0, defaultSet);

        for (int i = 0; i < setsToDisplay.Count; i++)
        {
            SpawnSetDisplay(setsToDisplay[i], i);
        }
        //setsToDisplay.ForEach(set => SpawnSetDisplay(set));

        defaultSetIndex = 0;
        scrollRect.verticalNormalizedPosition = 1f;
    }
    private void OnDisable() 
    {
        DestroySetDisplaysInScrollView();
        SetDisplayDefaultSelection.SelectedEvent -= OnSetDisplaySelected;
    }
}