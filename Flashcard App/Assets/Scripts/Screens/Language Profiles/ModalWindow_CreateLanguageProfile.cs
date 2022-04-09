using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindow_CreateLanguageProfile : CustomModalWindow
{
    [Header("Dropdowns")]
    [SerializeField] private CustomDropdown nativeLanguageDropdown;
    [SerializeField] private CustomDropdown learningLanguageDropdown;
    [Header("Current Profile Switch")]
    [SerializeField] private SwitchManager currentProfileSwitch;
    [Header("Back To Register Screen")]
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnCreate;

    public override void Awake()
    {
        base.Awake();

        btnBack.onClick.AddListener(OnBackButtonSelected);
        btnCreate.onClick.AddListener(OnCreateButtonSelected);
    }

    public override void Start()
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
        CloseWindow();
    }
    private void OnCreateButtonSelected()
    {
        Language selectedNativeLanguage = LanguageDataHolder.Instance.languagesList[nativeLanguageDropdown.selectedItemIndex];
        Language selectedLearningLanguage = LanguageDataHolder.Instance.languagesList[learningLanguageDropdown.selectedItemIndex];

        LanguageProfile languageProfile = new LanguageProfile(selectedNativeLanguage, selectedLearningLanguage, currentProfileSwitch.isOn);
        CloseWindow();
    }
}