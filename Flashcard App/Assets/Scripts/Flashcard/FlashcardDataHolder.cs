using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To do - 
//Read from json to get flashcards already created.

public class FlashcardDataHolder : MonoBehaviour
{
    [SerializeField] private List<Flashcard> FlashcardList;

    private void Awake()
    {
        Flashcard.FlashcardCreatedEvent += OnFlashcardCreated;
    }

    private void Start()
    {
       //Flashcard someFlashcard = new Flashcard("Hi", "Привет", null, Color.black, null);
    }

    private void OnFlashcardCreated(Flashcard flashcardCreated)
    {
        FlashcardList.Add(flashcardCreated);
    }

    private void OnDestroy()
    {
        Flashcard.FlashcardCreatedEvent -= OnFlashcardCreated;
    }
}
