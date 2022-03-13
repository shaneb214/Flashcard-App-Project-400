using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Flashcard
{
    public static event Action<Flashcard> FlashcardCreatedEvent;

    private Guid id;
    public Guid ID => id;

    //Native language.
    [SerializeField] private string nativeSide;
    public string NativeSide { get => nativeSide; set => nativeSide = value; }

    //Learning language.
    [SerializeField] private string learningSide;
    public string LearningSide { get => learningSide; set => learningSide = value; }

    private string notes;
    public string Notes { get => notes; set => notes = value; }

    private Color colour;
    public Color Colour { get => colour; set => colour = value; }

    private AudioClip audioRecording;
    public AudioClip AudioRecording { get => audioRecording; set => audioRecording = value; }

    private string setID;
    public string SetID { get => setID; set => setID = value; }

    //Tags? 
    public Flashcard(string nativeSide, string learningSide)
    {
        id = Guid.NewGuid();
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }
    public Flashcard(string nativeSide, string learningSide,string notes,string setID)
    {
        id = Guid.NewGuid();
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;
        this.notes = notes;
        this.setID = setID;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }


    public Flashcard(string nativeSide, string learningSide, string notes, Color colour, AudioClip audioRecording)
    {
        id = Guid.NewGuid(); 
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;
        this.notes = notes;
        this.colour = colour;
        this.audioRecording = audioRecording;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }


    public override string ToString()
    {
        return string.Format($"{NativeSide} <> {LearningSide}");
    }
}
