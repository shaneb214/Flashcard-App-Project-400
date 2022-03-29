using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: set up previous button and button to go back to non parented sets page. 

public class TopBar_SetsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTopBarHeading;

    private void OnSetDisplayPressed(SetDisplayLibrary setDisplaySelected)
    {
        txtTopBarHeading.text = $"../{SetsDataHolder.Instance.FindSetByID(setDisplaySelected.setIDToRepresent).Name}";
    }
    private void OnSwitchToNonParentedSetsDisplay()
    {
        txtTopBarHeading.text = "My sets";
    }

    private void OnEnable()
    {
        SetDisplayLibrary.SetDisplaySelectedEvent += OnSetDisplayPressed;
    }

    private void OnDisable()
    {
        SetDisplayLibrary.SetDisplaySelectedEvent -= OnSetDisplayPressed;
    }
}
