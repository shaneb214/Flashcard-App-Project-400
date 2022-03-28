using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Clicking on this button will open screen allowing user to select a set to make it as the default set. 

[RequireComponent(typeof(Button))]
public class BtnSelectSetForFlashcardCreation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtSetName;
    private Button myButton;

    [Header("Modal Window Objects To Activate")]
    [SerializeField] private ModalWindow_SelectDefaultSet modalWindow_SelectDefaultSet;
    [SerializeField] private ModalWindow_CreateDefaultSet modalWindow_CreateDefaultSet;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnMyButtonPressed);

        LanguageProfile.DefaultSetIDUpdated += UpdateTextDisplay; 
    }

    //When user selects 
    private void UpdateTextDisplay(string newDefaultSetID)
    {
        Set newDefaultSet = SetsDataHolder.Instance.FindSetByID(newDefaultSetID);
        txtSetName.text = newDefaultSet == null ? "Click here to select default set" : $"Adding to Set: \n {newDefaultSet.Name}";
    }

    private void OnMyButtonPressed()
    {
        if (SetsDataHolder.Instance.UserHasSetsCreated)
            modalWindow_SelectDefaultSet.AnimateWindow();
        else
            modalWindow_CreateDefaultSet.AnimateWindow(); 
    }

    private void OnEnable()
    {
        LanguageProfile.DefaultSetIDUpdated += UpdateTextDisplay;

        string currentDefaultSetID = LanguageProfileController.Instance.currentLanguageProfile.DefaultSetID;
        UpdateTextDisplay(currentDefaultSetID);
    }

    private void OnDisable()
    {
        LanguageProfile.DefaultSetIDUpdated -= UpdateTextDisplay;
    }
}