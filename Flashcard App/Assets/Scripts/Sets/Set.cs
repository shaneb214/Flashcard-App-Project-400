using System;
using System.Collections.Generic;
using UnityEngine;

//Set which holds a list of flashcards.

[Serializable]
public class Set
{
    public static event Action<Set> SetCreatedEvent;

    //Default set? Have this in some sort of controller maybe. 
    public string ID;
    public string _name;

    private string languageProfileID;
    private string ParentSetID;
    private bool IsParentSet { get { return ParentSetID == string.Empty; } }

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

        SetCreatedEvent?.Invoke(this);
        Debug.Log($"Set created: {this}");
    }

    public override string ToString()
    {
        return $"Set: {_name}";
    }
}
