using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To do - 
//Read from json to get flashcards already created.

public class FlashcardDataHolder : MonoBehaviour
{
    [SerializeField] private List<Flashcard> FlashcardList;

    [Header("Storing JSON Location - Unity Editor & PC")]
    [SerializeField] private string flashcardListJSONPathPC;
    [Header("Storing JSON Location - Mobile")]
    [SerializeField] private string flashcardListJSONPathMobile;

    string jsonPath;

    private void Awake()
    {
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
        FlashcardList = JSONHelper.ReadListDataFromJSONFile<Flashcard>(jsonPath);
    }

    private void OnFlashcardCreated(Flashcard flashcardCreated) => FlashcardList.Add(flashcardCreated);

    //Object end.
    private void OnDestroy() => Flashcard.FlashcardCreatedEvent -= OnFlashcardCreated;
    private void OnApplicationQuit() => JSONHelper.SaveListDataToJSON(FlashcardList, jsonPath);
}
