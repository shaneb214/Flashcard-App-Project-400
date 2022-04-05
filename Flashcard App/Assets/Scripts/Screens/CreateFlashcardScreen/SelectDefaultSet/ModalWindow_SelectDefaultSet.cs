using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Window that appears when user selects default set button on create flashcard screen (home screen).
//Scroll view manager elsewhere within this modal window object deals with spawning set displays / selections.

public class ModalWindow_SelectDefaultSet : CustomModalWindow
{
    [SerializeField] private string defaultSetIDOnEnable;


    [Header("Components")]
    [SerializeField] private Button btnOk;

    public override void Awake()
    {
        base.Awake();
        btnOk.onClick.AddListener(OnOkButtonSelected);
    }

    public override void Start() => base.Start();

    private void OnOkButtonSelected()
    {
        string currentDefaultSetID = SetsDataHolder.Instance.defaultSet.ID;

        //Has user selected new set to be their default set? - Update API.
        if (currentDefaultSetID != defaultSetIDOnEnable)
        {
            Set oldDefaultSet = SetsDataHolder.Instance.FindSetByID(defaultSetIDOnEnable);
            Set newDefaultSet = SetsDataHolder.Instance.defaultSet;

            APIUtilities.Instance.ModifyDefaultSetValue(oldDefaultSet);
            APIUtilities.Instance.ModifyDefaultSetValue(newDefaultSet);
        }

        CloseWindow();
    }

    private void OnEnable()
    {
        defaultSetIDOnEnable = SetsDataHolder.Instance.defaultSet.ID;
    }
    private void OnDisable()
    {
       
    }
}