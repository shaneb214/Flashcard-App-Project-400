using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnShowAddNotesWindow : MonoBehaviour
{
    private Button myButton;
    [SerializeField] private ModalWindow_CreateFlashcardNote modalWindow_CreateNotes;

    private void Awake() => myButton = GetComponent<Button>();
    private void Start() => myButton.onClick.AddListener(OnMyButtonClick);

    private void OnMyButtonClick()
    {
        modalWindow_CreateNotes.AnimateWindow();
    }
}
