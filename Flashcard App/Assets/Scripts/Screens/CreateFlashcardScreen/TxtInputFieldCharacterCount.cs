using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TxtInputFieldCharacterCount : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_InputField inputField;
    private TextMeshProUGUI txtWordCount;

    private int maxWordCountOfInputField;

    private void Awake()
    {
        txtWordCount = GetComponent<TextMeshProUGUI>();
        inputField.onValueChanged.AddListener(UpdateWordCountDisplay);
    }

    private void Start()
    {
        maxWordCountOfInputField = inputField.characterLimit;
        UpdateWordCountDisplay(inputField.text);
    }

    private void UpdateWordCountDisplay(string inputFieldText)
    {
        txtWordCount.text = $"{inputFieldText.Length}/{maxWordCountOfInputField}";
    }
}
