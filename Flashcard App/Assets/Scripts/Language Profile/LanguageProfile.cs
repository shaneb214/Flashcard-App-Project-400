using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageProfile 
{
    public static event Action<LanguageProfile> LanguageProfileCreatedEvent;

    public string ID;
    public bool IsCurrentProfile;

    public Language nativeLanguage;
    public Language learningLanguage;

    public LanguageProfile(Language nativeLanguage,Language learningLanguage, bool IsCurrentProfile)
    {
        ID = Guid.NewGuid().ToString(); //So it saves to json.

        this.nativeLanguage = nativeLanguage;
        this.learningLanguage = learningLanguage;

        this.IsCurrentProfile = IsCurrentProfile;

        LanguageProfileCreatedEvent?.Invoke(this);
        Debug.Log($"New language profile created - {this}");
    }

    public override string ToString()
    {
        return $"{nativeLanguage._name} <> {learningLanguage._name}";
    }
}