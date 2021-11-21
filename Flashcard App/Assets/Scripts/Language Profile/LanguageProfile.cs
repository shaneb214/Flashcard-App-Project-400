using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageProfile
{
    public string ID;
    public bool currentProfile;

    //public Language nativeLanguage;
    //public Language learningLanguage;

    public string nativeISO;
    public string learningISO;

    public LanguageProfile(string nativeISO, string learningISO, bool setCurrentProfile)
    {
        ID = Guid.NewGuid().ToString(); //So it saves to json.

        this.nativeISO = nativeISO;
        this.learningISO = learningISO;

        currentProfile = setCurrentProfile;
    }
}