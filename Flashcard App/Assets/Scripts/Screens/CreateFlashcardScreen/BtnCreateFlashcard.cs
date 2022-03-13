using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Michsky.UI.ModernUIPack;

//Part of create flashcard screen.
//Will create a flashcard when pressed if two input fields (native / learning) have text.

//TO DO:
//Move this to a controller script.

public class BtnCreateFlashcard : MonoBehaviour
{
    private Button myButton;

    [SerializeField] private CustomInputField nativeLangInputField;
    [SerializeField] private CustomInputField learningLangInputField;
    [SerializeField] private ModalWindow_CreateFlashcardNote createNoteModalWindow;
    private string currentFlashcardNote;

    private bool CanCreateFlashcard
    { 
        get 
        {
            return nativeLangInputField.inputField.text != string.Empty &&
                   learningLangInputField.inputField.text != string.Empty;

        } 
    }

    //Start.
    private void Awake() => myButton = GetComponent<Button>();
    private void Start()
    {
        createNoteModalWindow.NoteCreatedEvent += OnFlashcardNoteCreated;
        myButton.onClick.AddListener(OnButtonClick);
    }

    private void OnFlashcardNoteCreated(string note)
    {
        currentFlashcardNote = note;
    }

    private void OnButtonClick()
    {
        if(CanCreateFlashcard)
        {
            //Create.
            Flashcard newFlashcard = new Flashcard(nativeLangInputField.inputField.text, learningLangInputField.inputField.text,currentFlashcardNote,LibraryViewController.DefaultSet);

            //Reset Input Fields.
            createNoteModalWindow.ClearNoteText();
            nativeLangInputField.ClearText();
            learningLangInputField.ClearText();
            currentFlashcardNote = string.Empty;
        }
    }
}