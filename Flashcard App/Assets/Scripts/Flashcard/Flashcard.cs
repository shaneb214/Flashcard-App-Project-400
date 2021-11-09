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
    [SerializeField] private string firstSide;
    public string FirstSide { get => firstSide; set => firstSide = value; }

    //Learning language.
    [SerializeField] private string secondSide;
    public string SecondSide { get => secondSide; set => secondSide = value; }

    private string optionalComments;
    public string OptionalComments { get => optionalComments; set => optionalComments = value; }

    private Color colour;
    public Color Colour { get => colour; set => colour = value; }

    private AudioClip audioRecording;
    public AudioClip AudioRecording { get => audioRecording; set => audioRecording = value; }

    public Flashcard(string firstSide, string secondSide, string optionalComments, Color colour, AudioClip audioRecording)
    {
        id = Guid.NewGuid(); 
        this.firstSide = firstSide;
        this.secondSide = secondSide;
        this.optionalComments = optionalComments;
        this.colour = colour;
        this.audioRecording = audioRecording;

        FlashcardCreatedEvent?.Invoke(this);
        Debug.Log($"New flashcard created: {this}");
    }

    public override string ToString()
    {
        return string.Format($"{FirstSide} <> {SecondSide}");
    }
}
