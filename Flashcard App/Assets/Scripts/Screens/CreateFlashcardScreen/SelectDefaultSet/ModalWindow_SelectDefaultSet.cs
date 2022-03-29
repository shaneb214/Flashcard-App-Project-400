using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Window that appears when user selects default set button on create flashcard screen (home screen).
//Scroll view manager elsewhere within this modal window object deals with spawning set displays / selections.

public class ModalWindow_SelectDefaultSet : CustomModalWindow
{
    [Header("Components")]
    [SerializeField] private Button btnOk;

    public override void Awake()
    {
        base.Awake();
        btnOk.onClick.AddListener(CloseWindow);
    }

    public override void Start() => base.Start();
}