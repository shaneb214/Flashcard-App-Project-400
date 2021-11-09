using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Def_Lang_Holder", menuName = "ScriptableObjects/DefaultLanguageHolder", order = 1)]
public class DefaultLanguageHolder : ScriptableObject
{
    public List<Language> defaultLanguageList = new List<Language>();

    public Language FindDefaultLanguage(string name) => defaultLanguageList.Find(lang => lang._name == name);
}