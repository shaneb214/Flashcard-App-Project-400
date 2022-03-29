using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsDataHolder : MonoBehaviour
{
    public static SetsDataHolder Instance;

    [Header("All sets")]
    [SerializeField] private List<Set> SetList;
    public bool UserHasSetsCreated { get { return SetList.Count > 0; } }

    [Header("Storing JSON Location - Unity Editor & PC")]
    [SerializeField] private string setsListJSONPathPC;
    [Header("Storing JSON Location - Mobile")]
    [SerializeField] private string setsListJSONPathMobile;

    string jsonPath;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Set.SetCreatedEvent += OnSetCreated;
    }
    private void Start() 
    {
#if UNITY_EDITOR
        jsonPath = Application.dataPath + setsListJSONPathPC;
#elif UNITY_ANDROID
        jsonPath = Application.persistentDataPath + setsListJSONPathMobile;
#endif

        //Read from json.
        SetList = JSONHelper.ReadListDataFromJSONFile<Set>(jsonPath);
    }

    private void OnSetCreated(Set setCreated) => SetList.Add(setCreated);

    //Data retrieval.
    public List<Set> FindSetsByLangProfileID(string langProfileID) => SetList.FindAll(s => s.LanguageProfileID == langProfileID);
    public List<Set> FindSetsByParentID(string setParentID) => SetList.FindAll(s => s.ParentSetID == setParentID);
    public List<Set> FindSetsByParentID(string setParentID, string langProfileID) => SetList.FindAll(set => set.LanguageProfileID == langProfileID).FindAll(set => set.ParentSetID == setParentID);
    public List<Set> FindSetsOfCurrentLanguageProfileByParentID(string setParentID) => SetList.FindAll(set => set.LanguageProfileID == LanguageProfileController.Instance.currentLanguageProfile.ID).FindAll(set => set.ParentSetID == setParentID);
    public Set FindSetByID(string ID) => SetList.Find(s => s.ID == ID);

    //Object end.
    private void OnDestroy() => Set.SetCreatedEvent -= OnSetCreated;
    private void OnApplicationQuit() => JSONHelper.SaveListDataToJSON(SetList, jsonPath);
}
