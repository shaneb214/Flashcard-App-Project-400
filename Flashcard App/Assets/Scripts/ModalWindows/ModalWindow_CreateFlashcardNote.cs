using Michsky.UI.ModernUIPack;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindow_CreateFlashcardNote : CustomModalWindow
{
    [Header("Create Note Components")]
    [SerializeField] private CustomInputField createNoteInputField;
    [SerializeField] private TMP_InputField learningCardInputField;
    [SerializeField] private Button btnCancel;
    [SerializeField] private Button btnPaste;
    [SerializeField] private Button btnOk;

    public void ClearNoteText(bool updateInstant = false) => createNoteInputField.ClearText(updateInstant);

    public string CurrentNote { get { return createNoteInputField.inputField.text; } set { createNoteInputField.inputField.text = value; } }

    public event Action<string> NoteCreatedEvent;

    public override void Awake() => base.Awake();
    public override void Start()
    {
        btnCancel.onClick.AddListener(OnCancelButtonPressed);
        btnPaste.onClick.AddListener(OnPasteButtonPressed);
        btnOk.onClick.AddListener(OnOkButtonPressed);
    }

    private void OnCancelButtonPressed()
    {
        ClearNoteText();
        CloseWindow();
    }
    private void OnPasteButtonPressed()
    {
        if (learningCardInputField.text == string.Empty)
            return;

        if (CurrentNote == string.Empty)
            createNoteInputField.AnimateInsantIn();

        createNoteInputField.inputField.text += learningCardInputField.text;
    }

    private void OnOkButtonPressed()
    {
        if (CurrentNote == string.Empty)
            createNoteInputField.AnimateOut();
        else
            NoteCreatedEvent?.Invoke(CurrentNote);

        CloseWindow();
    }

    private void OnEnable()
    {
        createNoteInputField.AnimateInsantOut();
    }
}
