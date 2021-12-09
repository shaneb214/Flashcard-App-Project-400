using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Dropdown which contains all language options in app. Used for selecting native / learning language when creating a language profile.

[RequireComponent(typeof(TMP_Dropdown))]
public class DropDownSelectLanguage : MonoBehaviour
{
    private TMP_Dropdown myDropdown;

    [Header("Languages Available In App")]
    [SerializeField] private DefaultLanguageHolder languageHolder;
    [Header("Placeholder Text")]
    [SerializeField] private string placeholderText;

    private bool OptionSelected { get { return myDropdown.captionText.text != placeholderText; } }
    public Language GetLanguageSelected { get { return languageHolder.defaultLanguageList[myDropdown.value]; } }

    private void Awake() => myDropdown = GetComponent<TMP_Dropdown>();
    private void Start()
    {
        List<Language> availableLanguages = languageHolder.defaultLanguageList;
        List<TMP_Dropdown.OptionData> dropdownOptionData = new List<TMP_Dropdown.OptionData>() { /*new TMP_Dropdown.OptionData(placeholderText)*/ };

        for (int i = 0; i < availableLanguages.Count; i++)
        {
            dropdownOptionData.Add(new TMP_Dropdown.OptionData(availableLanguages[i]._name, Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{availableLanguages[i].ISO}")));
        }

        myDropdown.options.Clear();
        myDropdown.AddOptions(dropdownOptionData);

        //Cant get display placeholder working so doing workaround with ondropdownvaluechanged.
         //myDropdown.captionText.text = placeholderText;
        //txtPlaceholder.text = placeholderText;
        //myDropdown.SetValueWithoutNotify(-1);
        //myDropdown.value = -1;

       // myDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    //private void OnDropdownValueChanged(int newValue)
    //{
    //    if(newValue != 0)
    //    {
    //        myDropdown.options.RemoveAt(0);
    //        myDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    //    }
    //}
}
