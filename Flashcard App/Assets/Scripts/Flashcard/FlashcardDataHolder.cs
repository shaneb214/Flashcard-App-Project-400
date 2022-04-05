using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    //Start.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        Flashcard.FlashcardCreatedEvent += OnFlashcardCreated;
    }

    private void Start()
    {
#if UNITY_EDITOR
        jsonPath = Application.dataPath + flashcardListJSONPathPC;
#elif UNITY_ANDROID
        jsonPath = Application.persistentDataPath + flashcardListJSONPathMobile;
#endif

        //Read from json.
        //List<Flashcard> flashcardListFromJSON = JSONHelper.ReadListDataFromJSONFile<Flashcard>(jsonPath);
        //FlashcardList = flashcardListFromJSON == null ? new List<Flashcard>() : flashcardListFromJSON;
    }
    public void UpdateFlashcardData(List<Flashcard> flashcards) => FlashcardList = flashcards;

    public List<Flashcard> FindFlashcardsBySetID(string setID) => FlashcardList.FindAll(flashcard => flashcard.setID == setID);
    public int FlashcardCountOfSet(string setID) => FlashcardList.Count(flashcard => flashcard.setID == setID);

    private void OnFlashcardCreated(Flashcard flashcardCreated) => FlashcardList.Add(flashcardCreated);

    //Object end.
    private void OnDestroy() => Flashcard.FlashcardCreatedEvent -= OnFlashcardCreated;
    private void OnApplicationQuit() => JSONHelper.SaveListDataToJSON(FlashcardList, jsonPath);
}
