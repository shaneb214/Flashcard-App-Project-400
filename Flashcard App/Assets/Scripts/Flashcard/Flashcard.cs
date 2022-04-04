using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Flashcard
{
    public static event Action<Flashcard> FlashcardCreatedEvent;

    public string Id;
    public string nativeSide;
    public string learningSide;
    public string notes;
    public string setID;

    public Flashcard() { }
    public Flashcard(string nativeSide, string learningSide)
    {
        Id = Guid.NewGuid().ToString();
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }
    public Flashcard(string nativeSide, string learningSide,string notes,string setID)
    {
        Id = Guid.NewGuid().ToString();
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;
        this.notes = notes;
        this.setID = setID;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }


    public Flashcard(string nativeSide, string learningSide, string notes, Color colour, AudioClip audioRecording)
    {
        Id = Guid.NewGuid().ToString(); 
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;
        this.notes = notes;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }


    public override string ToString()
    {
        return string.Format($"{nativeSide} <> {learningSide}");
    }
}
