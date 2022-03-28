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

    public Language nativeLanguage;
    public Language learningLanguage;

    public bool IsCurrentProfile;

    public string userID;
    [SerializeField] private string defaultSetID;
    public string DefaultSetID 
    {
        get => defaultSetID;
        set 
        {
            //New set ID? Update.
            if(defaultSetID != value)
            {
                defaultSetID = value;
                DefaultSetIDUpdated?.Invoke(defaultSetID);
            }
        }  
    }

    public LanguageProfile(Language nativeLanguage,Language learningLanguage, bool IsCurrentProfile)
    {
        ID = Guid.NewGuid().ToString(); //So it saves to json.
        userID = UserDataHolder.Instance.CurrentUser.ID;

        this.nativeLanguage = nativeLanguage;
        this.learningLanguage = learningLanguage;

        this.IsCurrentProfile = IsCurrentProfile;
        defaultSetID = string.Empty;

        LanguageProfileCreatedEvent?.Invoke(this);
        Debug.Log($"New language profile created - {this}");
    }

    public override string ToString()
    {
        return $"{nativeLanguage._name} <> {learningLanguage._name}";
    }
}