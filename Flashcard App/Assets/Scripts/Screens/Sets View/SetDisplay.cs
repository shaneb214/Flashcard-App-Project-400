using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Attached to prefab to represent a set.
//Shows set name, clicking on this will open the set and user can see the flashcards within.

[RequireComponent(typeof(Button))]
public class SetDisplay : MonoBehaviour
{
    private Button myButton;
    [SerializeField] private TextMeshProUGUI txtSetName;

    //Start.
    private void Awake() => myButton = GetComponent<Button>();
    private void Start() => myButton.onClick.AddListener(OnMyButtonClick);

    private void OnMyButtonClick()
    {
        //Switch to flashcard display screen.
    }

    public void UpdateDisplay(string setName)
    {
        txtSetName.text = setName;
    }
}