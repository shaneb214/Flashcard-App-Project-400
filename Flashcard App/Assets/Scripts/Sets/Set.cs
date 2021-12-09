using System;
using System.Collections.Generic;
using UnityEngine;

//Set which holds a list of flashcards.

//TODO: make set be able to hold other sets.

[Serializable]
public class Set
{
    public static event Action<Set> SetCreatedEvent;

    //Default set? Have this in some sort of controller maybe. 
    public string ID;
    public string _name;

    private List<Flashcard> flashcardList = new List<Flashcard>();

    public Set(string name)
    {
        ID = Guid.NewGuid().ToString();

        _name = name;

        SetCreatedEvent?.Invoke(this);
        Debug.Log($"Set created: {this}");
    }

    public Set(string name, List<Flashcard> flashcardList)
    {
        ID = Guid.NewGuid().ToString();

        _name = name;
        this.flashcardList = flashcardList;

        SetCreatedEvent?.Invoke(this);
        Debug.Log($"Set created: {this}");
    }

    public int NumberOfFlashcards { get { return flashcardList.Count; } }

    public override string ToString()
    {
        return $"{_name} <> No. of flashcards - {NumberOfFlashcards}";
    }
}
