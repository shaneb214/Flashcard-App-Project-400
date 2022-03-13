using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: set up previous button and button to go back to non parented sets page. 

public class TopBar_SetsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTopBarHeading;

    private void OnSetDisplayPressed(string setID)
    {
        txtTopBarHeading.text = $"../{SetsDataHolder.Instance.FindSetByID(setID).Name}";
    }
    private void OnSwitchToNonParentedSetsDisplay()
    {
        txtTopBarHeading.text = "My sets";
    }

    private void OnEnable()
    {
        SetDisplay.SetDisplayPressed += OnSetDisplayPressed;
    }

    private void OnDisable()
    {
        SetDisplay.SetDisplayPressed -= OnSetDisplayPressed;
    }
}
