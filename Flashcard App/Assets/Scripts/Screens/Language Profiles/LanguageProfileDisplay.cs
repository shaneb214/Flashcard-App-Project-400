using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Prefab which helps to display a language profile and clicking on this sets it as the current profile.
//Script is given ID of language profile to determine which profile they are representing.
//So far it shows names of languages and their flags.

//TODO: Diplay stats - no. of cards, no. of sets, date created etc.

[RequireComponent(typeof(Button))]
public class LanguageProfileDisplay : MonoBehaviour
{
    private string myLanguageProfileID;

    [SerializeField] private Image imgNativeFlag;
    [SerializeField] private Image imgLearningFlag;
    [SerializeField] private Image imgCurrentProfile;
    [SerializeField] private TextMeshProUGUI txtProfileLanguages;
    [SerializeField] private TextMeshProUGUI txtProfileCardSetCount;
    private Button btnSelectProfile;

    //Start.
    private void Awake() => btnSelectProfile = GetComponent<Button>();
    private void Start() => btnSelectProfile.onClick.AddListener(OnMyButtonClick);

    private void OnMyButtonClick()
    {
        //Enable icon.
        MarkAsCurrentLanguageProfile();
    }

    public void UpdateDisplay(LanguageProfile languageProfile)
    {
        myLanguageProfileID = languageProfile.ID;

        Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{languageProfile.NativeLanguage.ISO}");
        Sprite learningFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{languageProfile.LearningLanguage.ISO}");

        this.imgNativeFlag.sprite = nativeFlagSprite;
        this.imgLearningFlag.sprite = learningFlagSprite;
        txtProfileLanguages.text = languageProfile.ToString();

        int setCountOfProfile = SetsDataHolder.Instance.GetSetCountOfLanguageProfile(myLanguageProfileID);
        int flashcardCountOfProfile = SetsDataHolder.Instance.GetFlashcardCountOfLanguageProfile(myLanguageProfileID);
        txtProfileCardSetCount.text = $"{setCountOfProfile} Sets - {flashcardCountOfProfile} Cards";

        imgCurrentProfile.enabled = languageProfile.IsCurrentProfile;
    }

    public void MarkAsCurrentLanguageProfile()
    {
        SetCurrentProfileIconImage(true);
        LanguageProfileController.Instance.SelectNewProfile(myLanguageProfileID);
        LanguageProfileController.Instance.UserSelectedNewProfileEvent += OnNewLanguageProfileSelected;
    }
    public void MarkAsNotCurrentLanguageProfile()
    {
        SetCurrentProfileIconImage(false);
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnNewLanguageProfileSelected;
    }

    public void SetCurrentProfileIconImage(bool enabled) => imgCurrentProfile.enabled = enabled;

    public void OnNewLanguageProfileSelected(LanguageProfile newLanguageProfile)
    {
        MarkAsNotCurrentLanguageProfile();
    }

    private void OnDisable()
    {
        LanguageProfileController.Instance.UserSelectedNewProfileEvent -= OnNewLanguageProfileSelected;
    }
}