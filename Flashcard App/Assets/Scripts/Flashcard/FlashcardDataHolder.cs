using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

//To do - 
//Read from json to get flashcards already created.

public class FlashcardDataHolder : MonoBehaviour
{
    public static FlashcardDataHolder Instance;

    [SerializeField] private List<Flashcard> FlashcardList;

    [Header("Storing JSON Location - Unity Editor & PC")]
    [SerializeField] private string flashcardListJSONPathPC;
    [Header("Storing JSON Location - Mobile")]
    [SerializeField] private string flashcardListJSONPathMobile;

    string jsonPath;

    [Header("API Settings")]
    [SerializeField] private bool PostNewFlashcardsToAPI;
    private Action<Flashcard> OnFlashcardCreated;

    //Start.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        jsonPath = Application.dataPath + flashcardListJSONPathPC;
#elif UNITY_ANDROID
        jsonPath = Application.persistentDataPath + flashcardListJSONPathMobile;
#endif

        OnFlashcardCreated += SaveFlashcardToMemory;

        if (PostNewFlashcardsToAPI)
            OnFlashcardCreated += PostFlashcardToApi;

        Flashcard.FlashcardCreatedEvent += OnFlashcardCreated;
    }

    public void UpdateFlashcardList(List<Flashcard> flashcards) => FlashcardList = flashcards;

    public List<Flashcard> FindFlashcardsBySetID(string setID) => FlashcardList.FindAll(flashcard => flashcard.setID == setID);
    public int FlashcardCountOfSet(string setID) => FlashcardList.Count(flashcard => flashcard.setID == setID);

    //Json.
    private void ReadFlashcardListFromJSON()
    {
        List<Flashcard> flashcardListFromJSON = JSONHelper.ReadListDataFromJSONFile<Flashcard>(jsonPath);
        FlashcardList = flashcardListFromJSON == null ? new List<Flashcard>() : flashcardListFromJSON;
    }

    //Saving to memory / API.
    private void SaveFlashcardToMemory(Flashcard flashcard) => FlashcardList.Add(flashcard);
    private void PostFlashcardToApi(Flashcard flashcardCreated) => StartCoroutine(APIUtilities.Instance.IEnumerator_PostNewFlashcard(flashcardCreated));

    //Object end.
    private void OnDestroy() => Flashcard.FlashcardCreatedEvent -= OnFlashcardCreated;
    private void OnApplicationQuit() => JSONHelper.SaveListDataToJSON(FlashcardList, jsonPath);
}
