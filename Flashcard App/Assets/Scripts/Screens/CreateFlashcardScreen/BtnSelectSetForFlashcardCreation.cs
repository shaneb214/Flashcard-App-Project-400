using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnSelectSetForFlashcardCreation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtSetName;
    private Button myButton;

    private void Awake() => myButton = GetComponent<Button>();

    private void OnEnable()
    {
        string currentDefaultSetID = LanguageProfileController.Instance.currentLanguageProfile.defaultSetID;
        Set defaultSet = SetsDataHolder.Instance.FindSetByID(currentDefaultSetID);
        txtSetName.text = defaultSet == null ? "No set selected" : $"Adding to Set: {defaultSet.Name}";
    }

    private void OnDisable()
    {
        
    }

}
