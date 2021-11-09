using System.Collections.Generic;
using UnityEngine;

//Read users tags from where? 
public class FlashcardTagDataHolder : MonoBehaviour
{
    [Header("Default Tags")]
    [SerializeField] private DefaultFlashcardTagsHolder defaultTagsHolder;

    [Header("User created tags")]
    [SerializeField] private List<FlashcardTag> userCreatedTagList = new List<FlashcardTag>();

    private void Awake()
    {
        FlashcardTag.FlashcardTagCreatedEvent += OnFlashcardTagCreated;
    }

    private void Start()
    {
        for (int i = 0; i < defaultTagsHolder.defaultFlashcardTagList.Count; i++)
        {
            print(defaultTagsHolder.defaultFlashcardTagList[i].name);
        }

        defaultTagsHolder.defaultFlashcardTagList.Add(new FlashcardTag("Test", Color.white));
    }

    private void OnFlashcardTagCreated(FlashcardTag newFlashcardTag)
    {

    }

    private void OnDestroy()
    {
        FlashcardTag.FlashcardTagCreatedEvent -= OnFlashcardTagCreated;
    }
}