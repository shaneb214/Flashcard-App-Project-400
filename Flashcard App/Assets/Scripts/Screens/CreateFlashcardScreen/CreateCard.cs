using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateCard : MonoBehaviour
{
    [SerializeField] protected Image imgFlag;
    [SerializeField] protected TextMeshProUGUI txtPlaceholder;

    public void UpdateDisplay(Sprite flagSprite, string placeholderText)
    {
        imgFlag.sprite = flagSprite;
        txtPlaceholder.text = placeholderText;
    }
}