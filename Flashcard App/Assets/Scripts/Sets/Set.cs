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
    public string ParentSetID;
    private bool IsParentSet { get { return ParentSetID == string.Empty || ParentSetID == null; } }

    public bool IsDefaultSet;

    public Set(string Name,string parentSetID,bool isDefaultSet)
    {
        ID = Guid.NewGuid().ToString();
        LanguageProfileID = LanguageProfileController.Instance.CurrentLanguageProfile.ID;
        ParentSetID = parentSetID;
        this.Name = Name;
        IsDefaultSet = isDefaultSet;

        SetCreatedEvent?.Invoke(this);
        Debug.Log($"Set created: {this}");
    }

    public Set(string Name, string parentSetID, bool isDefaultSet,string languageProfileID)
    {
        ID = Guid.NewGuid().ToString();
        ParentSetID = parentSetID;
        this.Name = Name;
        IsDefaultSet = isDefaultSet;
        LanguageProfileID = languageProfileID;

        SetCreatedEvent?.Invoke(this);
        Debug.Log($"Set created: {this}");
    }


    public override string ToString()
    {
        return $"Set: {Name}";
    }
}