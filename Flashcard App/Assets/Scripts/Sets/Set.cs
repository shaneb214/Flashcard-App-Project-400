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
    public string Name;

    public string LanguageProfileID;
    private string ParentSetID;
    private bool IsParentSet { get { return ParentSetID == string.Empty; } }

    public Set(string Name)
    {
        ID = Guid.NewGuid().ToString();
        LanguageProfileID = LanguageProfileController.Instance.currentLanguageProfile.ID;

        this.Name = Name;

        SetCreatedEvent?.Invoke(this);
        Debug.Log($"Set created: {this}");
    }
    public override string ToString()
    {
        return $"Set: {Name}";
    }
}