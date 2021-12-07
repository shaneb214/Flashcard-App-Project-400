using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageProfile
{
    public string ID;
    public bool currentProfile;

    public Language nativeLanguage;
    public Language learningLanguage;

    public LanguageProfile(Language nativeLanguage,Language learningLanguage, bool setCurrentProfile)
    {
        ID = Guid.NewGuid().ToString(); //So it saves to json.

        this.nativeLanguage = nativeLanguage;
        this.learningLanguage = learningLanguage;

        currentProfile = setCurrentProfile;
    }
}