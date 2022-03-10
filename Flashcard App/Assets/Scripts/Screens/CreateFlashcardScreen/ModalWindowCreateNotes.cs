using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalWindowCreateNotes : ModalWindowManager
{
    [SerializeField] private CustomInputField noteInputField;
    private string currentNotes;
    public string GetNotes => currentNotes;

    private void Awake()
    {
        noteInputField = GetComponent<CustomInputField>();
    }

    public void ClearNotes()
    {
        noteInputField.inputField.text = string.Empty;
    }
}
