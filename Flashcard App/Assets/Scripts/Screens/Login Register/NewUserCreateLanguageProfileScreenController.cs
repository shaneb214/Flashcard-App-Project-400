using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewUserCreateLanguageProfileScreenController : MonoBehaviour
{
    [Header("Dropdowns")]
    [SerializeField] private CustomDropdown nativeLanguageDropdown;
    [SerializeField] private CustomDropdown learningLanguageDropdown;

    [Header("Back To Register Screen")]
    [SerializeField] private Button btnBack;
    [SerializeField] private ScreenPushData registerScreenPushData;

    [SerializeField] private Button btnCreate;

    private void Awake()
    {
        //btnBack.onClick.AddListener(OnBackButtonSelected);
        btnCreate.onClick.AddListener(OnCreateButtonSelected);
    }

    private void Start()
    {
        //Languages read from database.
        List<Language> languages = LanguageDataHolder.Instance.languagesList;

        for (int i = 0; i < languages.Count; i++)
        {
            nativeLanguageDropdown.CreateNewItem(languages[i].Name, Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{languages[i].ISO}"));
            learningLanguageDropdown.CreateNewItem(languages[i].Name, Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{languages[i].ISO}"));
        }
    }

    private void OnBackButtonSelected()
    {

    }
    private void OnCreateButtonSelected()
    {
        Language selectedNativeLanguage = LanguageDataHolder.Instance.languagesList[nativeLanguageDropdown.selectedItemIndex];
        Language selectedLearningLanguage = LanguageDataHolder.Instance.languagesList[learningLanguageDropdown.selectedItemIndex];

        LanguageProfile languageProfile = new LanguageProfile(selectedNativeLanguage, selectedLearningLanguage, true);

        SceneManager.LoadScene("MainScene");
    }
}
