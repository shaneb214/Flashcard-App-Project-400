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
    [SerializeField] private TextMeshProUGUI txtHeading;
    private Button myButton;

    //Start.
    private void Awake() => myButton = GetComponent<Button>();
    private void Start() => myButton.onClick.AddListener(OnMyButtonClick);

    private void OnMyButtonClick()
    {
        LanguageProfileController.Instance.SelectNewProfile(myLanguageProfileID);
    }

    public void UpdateDisplay(string profileID,Sprite nativeFlagSprite,Sprite learningFlagSprite,string headingText)
    {
        myLanguageProfileID = profileID;

        this.imgNativeFlag.sprite = nativeFlagSprite;
        this.imgLearningFlag.sprite = learningFlagSprite;
        txtHeading.text = headingText;
    }
}