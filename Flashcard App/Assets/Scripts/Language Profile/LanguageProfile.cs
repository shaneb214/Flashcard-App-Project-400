using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageProfile 
{
    public static event Action<LanguageProfile> LanguageProfileCreatedEvent;
    public static event Action<string> DefaultSetIDUpdated;

    public string ID;

    public Language NativeLanguage;
    public Language LearningLanguage;

    public bool IsCurrentProfile;

    public string userID;

    public LanguageProfile(Language nativeLanguage,Language learningLanguage, bool IsCurrentProfile)
    {
        ID = Guid.NewGuid().ToString();

        userID = UserDataHolder.Instance.CurrentUser.ID;

        this.NativeLanguage = nativeLanguage;
        this.LearningLanguage = learningLanguage;

        this.IsCurrentProfile = IsCurrentProfile;

        LanguageProfileCreatedEvent?.Invoke(this);
        Debug.Log($"New language profile created - {this}");
    }

    public override string ToString()
    {
        return $"{NativeLanguage.Name} - {LearningLanguage.Name}";
    }
}