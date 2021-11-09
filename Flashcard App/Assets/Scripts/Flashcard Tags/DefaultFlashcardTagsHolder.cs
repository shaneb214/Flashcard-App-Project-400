using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Def_Tag_Holder", menuName = "ScriptableObjects/DefaultFlashcardTagHolder",order = 2)]
public class DefaultFlashcardTagsHolder : ScriptableObject
{
    public List<FlashcardTag> defaultFlashcardTagList;

    public FlashcardTag FindDefaultFlashcardTag(string name) => defaultFlashcardTagList.Find(tag => tag.name == name);
}