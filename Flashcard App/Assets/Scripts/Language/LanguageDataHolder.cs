using System.Collections.Generic;
using UnityEngine;

public class LanguageDataHolder : MonoBehaviour
{
    public static LanguageDataHolder Instance;

    [SerializeField] public List<Language> languagesList = new List<Language>();

    [Header("Default Languages")]
    [SerializeField] private DefaultLanguageHolder defaultLanguageHolder;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    public void UpdateLanguagesList(List<Language> languages)
    {
        languagesList = languages;
    }
}