using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageProfileDisplay : MonoBehaviour
{
    [SerializeField] private Image imgNativeFlag;
    [SerializeField] private Image imgLearningFlag;
    [SerializeField] private TextMeshProUGUI txtHeading;

    public void UpdateDisplay(Sprite nativeFlagSprite,Sprite learningFlagSprite,string headingText)
    {
        this.imgNativeFlag.sprite = nativeFlagSprite;
        this.imgLearningFlag.sprite = learningFlagSprite;
        txtHeading.text = headingText;
    }
}