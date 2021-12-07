using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

//Part of create flashcard screen.
//Will create a flashcard when pressed if two input fields (native / learning) have text.

//TO DO:
//Move this to a controller script.

public class BtnCreateFlashcard : MonoBehaviour
{
    private Button myButton;

    [Header("test")]
    [SerializeField] private TMP_InputField nativeLangInputField;
    [SerializeField] private TMP_InputField learningLangInputField;

    private bool CanCreateFlashcard
    { 
        get 
        {
            return nativeLangInputField.text != string.Empty &&
                   learningLangInputField.text != string.Empty;

        } 
    }

    //Start.
    private void Awake() => myButton = GetComponent<Button>();
    private void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if(CanCreateFlashcard)
        {
            //Create.
            Flashcard newFlashcard = new Flashcard(nativeLangInputField.text, learningLangInputField.text);

            //Reset Input Fields.
            nativeLangInputField.text = string.Empty;
            learningLangInputField.text = string.Empty;
        }
    }
}