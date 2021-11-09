using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageProfile
{
    //Current profile - put in controller probably?
    //User? 
    public Guid ID;
    public Guid UserID;

    public Language basicLanguage;
    public Language learningLanguage;

    public LanguageProfile()
    {
        ID = Guid.NewGuid();
        //Set user ID here - get it from somewhere?
    }
}