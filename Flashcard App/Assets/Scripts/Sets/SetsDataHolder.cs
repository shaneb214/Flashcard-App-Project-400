using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


//Listen to new profile selected event to download sets for that profile? 


public class SetsDataHolder : MonoBehaviour
{
    public static SetsDataHolder Instance;

    public static event Action<string> DefaultSetIDUpdatedEvent;

    [Header("All sets")]
    public List<Set> SetList;
    public bool UserHasSetsCreated { get { return SetList.Count > 0; } }

    [Header("Storing JSON Location - Unity Editor & PC")]
    [SerializeField] private string setsListJSONPathPC;
    [Header("Storing JSON Location - Mobile")]
    [SerializeField] private string setsListJSONPathMobile;

    [SerializeField] private string defaultSetID;
    public string DefaultSetID 
    { 
        get => defaultSetID;
        set 
        {
            //New set ID? Update.
            if (defaultSetID != value)
            {
                defaultSetID = value;
                DefaultSetIDUpdatedEvent?.Invoke(defaultSetID);
            }
        }
    }
    private string jsonPath;

    [Header("API Settings")]
    [SerializeField] private bool PostNewSetsToAPI;
    private Action<Set> OnSetCreated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        if (PostNewSetsToAPI)
            OnSetCreated += PostSetToAPI;

        OnSetCreated += SaveSetToMemory;
        OnSetCreated += CheckToAssignNewDefaultSetOnSetCreation;

        Set.SetCreatedEvent += OnSetCreated;
        BtnLogout.UserLoggedOutEvent += ClearSetsDataFromMemory;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnUserSelectedNewLanguageProfile;
    }

    private void ClearSetsDataFromMemory()
    {
        defaultSetID = null;
        SetList.Clear();
    }

    private void Start() 
    {
#if UNITY_EDITOR
        jsonPath = Application.dataPath + setsListJSONPathPC;
#elif UNITY_ANDROID
        jsonPath = Application.persistentDataPath + setsListJSONPathMobile;
#endif

        //Read from json.
        //SetList = JSONHelper.ReadListDataFromJSONFile<Set>(jsonPath);
    }

    //On new language profile selection.
    private void OnUserSelectedNewLanguageProfile(LanguageProfile languageProfile) => APIUtilities.Instance.GetSetsOfLanguageProfile(languageProfile.ID, UpdateSetsData);

    public void UpdateSetsData(List<Set> sets)
    {
        SetList = sets;
        Set defaultSet = SetList.SingleOrDefault(Set => Set.IsDefaultSet == true);
        if(defaultSet != null)
            DefaultSetID = defaultSet.ID;
    }
    private void CheckToAssignNewDefaultSetOnSetCreation(Set createdSet)
    {
        //New set not marked as default? Don't need to do anything.
        if(createdSet.IsDefaultSet == false)
            return;

        //If this set is the first one created.. mark its id.
        string previousDefaultSetID = defaultSetID;
        if (previousDefaultSetID == string.Empty || previousDefaultSetID == null)
        {
            DefaultSetID = createdSet.ID;
            return;
        }

        //Otherwise there is another set that is marked as default. Switch around.
        Set previousDefaultSet = SetList.Find(set => set.ID == defaultSetID);
        previousDefaultSet.IsDefaultSet = false;

        DefaultSetID = createdSet.ID;

        //Update previous default set on API side. 
        APIUtilities.Instance.ModifyDefaultSetValue(previousDefaultSet);
    }

    public void SetDefaultSetBasedOnID(string setID)
    {
        Set previousDefaultSet = SetList.SingleOrDefault(Set => Set.IsDefaultSet == true);
        if(previousDefaultSet != null)
        {
            previousDefaultSet.IsDefaultSet = false;
        }

        Set newDefaultSet = SetList.Find(set => set.ID == setID);
        DefaultSetID = newDefaultSet.ID;
        newDefaultSet.IsDefaultSet = true;
    }
    private void SaveSetToMemory(Set createdSet) => SetList.Add(createdSet);
    private void PostSetToAPI(Set createdSet) => APIUtilities.Instance.PostNewSet(createdSet);

    //Data retrieval.
    public List<Set> FindSetsByLangProfileID(string langProfileID) => SetList.FindAll(s => s.LanguageProfileID == langProfileID);
    public List<Set> FindSetsByParentID(string setParentID) => SetList.FindAll(s => s.ParentSetID == setParentID);
    public List<Set> FindSetsByParentID(string setParentID, string langProfileID) => SetList.FindAll(set => set.LanguageProfileID == langProfileID).FindAll(set => set.ParentSetID == setParentID);
    public List<Set> FindSetsOfCurrentLanguageProfileByParentID(string setParentID) => SetList.FindAll(set => set.LanguageProfileID == LanguageProfileController.Instance.CurrentLanguageProfile.ID).FindAll(set => set.ParentSetID == setParentID);
    public int GetSubsetCountOfSet(string setID) => SetList.Count(set => set.ParentSetID == setID);
    public int GetSetCountOfLanguageProfile(string languageProfileID) => SetList.FindAll(set => set.LanguageProfileID == languageProfileID).Count;
    public int GetFlashcardCountOfLanguageProfile(string languageProfileID)
    {
        var profileSets = FindSetsByLangProfileID(languageProfileID);
        int flashcardCount = 0;
        profileSets.ForEach(set => flashcardCount += FlashcardDataHolder.Instance.FlashcardCountOfSet(set.ID));

        return flashcardCount;
    }
    public Set FindSetByID(string ID) => SetList.Find(s => s.ID == ID);


    //Object end.
    private void OnDestroy()
    {
        Set.SetCreatedEvent -= OnSetCreated;
        BtnLogout.UserLoggedOutEvent -= ClearSetsDataFromMemory;
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnUserSelectedNewLanguageProfile;
    }
}