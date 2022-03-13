using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Attached to prefab which displays a flashcard on screen.
public class FlashcardDisplay : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtNative;
    [SerializeField] private TextMeshProUGUI txtLearning;
    [SerializeField] private Image imgCardTagColour;
    private Button btnOpen;

    private void Awake() => btnOpen = GetComponent<Button>();

    public void UpdateDisplay(Flashcard flashcard)
    {
        txtNative.text = flashcard.NativeSide;
        txtLearning.text = flashcard.LearningSide;

        //What to do here.
        //imgCardTagColour.color = flashcard.Colour;
    }
}
