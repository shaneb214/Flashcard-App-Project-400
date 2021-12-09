using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Prefab which helps to display a language profile and clicking on this sets it as the current profile.
//So far it shows names of languages and their flags.

//TODO: Diplay stats - no. of cards, no. of sets, date created etc.

[RequireComponent(typeof(Button))]
public class LanguageProfileDisplay : MonoBehaviour
{
    private LanguageProfile myLanguageProfile;

    [SerializeField] private Image imgNativeFlag;
    [SerializeField] private Image imgLearningFlag;
    [SerializeField] private TextMeshProUGUI txtHeading;
    private Button myButton;

    //Start.
    private void Awake() => myButton = GetComponent<Button>();
    private void Start() => myButton.onClick.AddListener(OnMyButtonClick);

    private void OnMyButtonClick()
    {
        LanguageProfileController.Instance.SelectNewProfile(myLanguageProfile);
    }

    public void UpdateDisplay(LanguageProfile languageProfile,Sprite nativeFlagSprite,Sprite learningFlagSprite,string headingText)
    {
        myLanguageProfile = languageProfile;

        this.imgNativeFlag.sprite = nativeFlagSprite;
        this.imgLearningFlag.sprite = learningFlagSprite;
        txtHeading.text = headingText;
    }
}