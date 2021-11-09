using System;
using UnityEngine;

//Not giving ID yet - maybe will add later. How to give constant ID to default tags?

[Serializable]
public class FlashcardTag
{
    public static event Action<FlashcardTag> FlashcardTagCreatedEvent;

    public string name;
    public Color colour;

    public FlashcardTag(string name, Color colour)
    {
        this.name = name;
        this.colour = colour;

        FlashcardTagCreatedEvent?.Invoke(this);
    } 
}