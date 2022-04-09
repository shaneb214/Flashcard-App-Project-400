using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibraryViewController : ScrollViewItemManager
{
    public static string SetIDCurrentlyShowing;

    [SerializeField] protected SetDisplayLibrary setDisplayPrefab;
    [SerializeField] protected TextMeshProUGUI txtNoSetsWarning;

    [SerializeField] protected ModalWindow_SelectGameToPlay ModalWindow_SelectGameToPlay;

    private void OnSetDisplayPlaySetSelected(string setID)
    {
        ModalWindow_SelectGameToPlay.InformScrollViewManagerOfWhatSetIsAttemptingToBePlayed(setID);
        ModalWindow_SelectGameToPlay.AnimateWindow();
    }


    public virtual void OnEnable() { SetDisplayLibrary.SetDisplayPlaySetSelectedEvent += OnSetDisplayPlaySetSelected; }
    public virtual void OnDisable() { SetDisplayLibrary.SetDisplayPlaySetSelectedEvent -= OnSetDisplayPlaySetSelected; }
}
