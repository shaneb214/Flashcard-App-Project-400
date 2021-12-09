using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnSelectSetForFlashcardCreation : MonoBehaviour
{
    private Button myButton;

    [SerializeField] private TextMeshProUGUI txtSetName;

    private void Awake() => myButton = GetComponent<Button>();

}
