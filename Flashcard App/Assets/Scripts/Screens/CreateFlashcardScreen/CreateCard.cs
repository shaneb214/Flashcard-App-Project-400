using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateCard : MonoBehaviour
{
    [SerializeField] protected Image imgFlag;
    [SerializeField] protected TextMeshProUGUI txtPlaceholder;
    [SerializeField] protected TextMeshProUGUI txtUserInput;

    public void UpdateDisplay(Sprite flagSprite, string placeholderText)
    {
        imgFlag.sprite = flagSprite;
        txtPlaceholder.text = placeholderText;
    }

    public void ClearUserInput() => txtUserInput.text = string.Empty;
}