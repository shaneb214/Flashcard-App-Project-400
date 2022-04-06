using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindow_SelectGameToPlay : CustomModalWindow
{
    [SerializeField] private SelectGameToPlayScrollViewManager selectGameScrollViewManager;
    [SerializeField] private Button btnBack;

    public override void Awake()
    {
        base.Awake();

        btnBack.onClick.AddListener(OnBackButtonSelected);
    }

    private void OnBackButtonSelected()
    {
        CloseWindow();
    }

    public void InformScrollViewManagerOfWhatSetIsAttemptingToBePlayed(string setID) => selectGameScrollViewManager.CurrentSetIDChosenToPlay = setID;
}
