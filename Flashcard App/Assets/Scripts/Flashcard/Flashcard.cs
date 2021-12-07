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

    private string optionalComments;
    public string OptionalComments { get => optionalComments; set => optionalComments = value; }

    private Color colour;
    public Color Colour { get => colour; set => colour = value; }

    private AudioClip audioRecording;
    public AudioClip AudioRecording { get => audioRecording; set => audioRecording = value; }

    //Tags? 

    public Flashcard(string nativeSide, string learningSide, string optionalComments, Color colour, AudioClip audioRecording)
    {
        id = Guid.NewGuid(); 
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;
        this.optionalComments = optionalComments;
        this.colour = colour;
        this.audioRecording = audioRecording;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }

    public Flashcard(string nativeSide, string learningSide)
    {
        id = Guid.NewGuid();
        this.nativeSide = nativeSide;
        this.learningSide = learningSide;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }

    public override string ToString()
    {
        return string.Format($"{NativeSide} <> {LearningSide}");
    }
}
