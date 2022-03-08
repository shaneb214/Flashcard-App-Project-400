using Michsky.UI.ModernUIPack;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniLang;
using UnityEngine;
using UnityEngine.UI;

//Takes text from one input field, translates it and inserts it into another input field.

public class BtnTranslate : MonoBehaviour
{
    private Button button;

    [SerializeField] private TextMeshProUGUI txtTranslateFrom;
    //[SerializeField] private TMP_InputField inputFieldToAddTranslation;
    [SerializeField] private CustomInputField inputFieldToAddTranslation;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void Start() { }
    private void OnEnable() { }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inputFieldToAddTranslation.AnimateOut(forceAnimate: true);
        }
    }
    private void OnButtonClick()
    {
        //Create animation for arrow.
        //Dont call translate if it's already in process of translating.

        Translator translator = Translator.Create(LanguageProfileController.Instance.userCurrentLanguageProfile.nativeLanguage.ISO, LanguageProfileController.Instance.userCurrentLanguageProfile.learningLanguage.ISO);
        translator.Run(txtTranslateFrom.text, (results) =>
        {
            string translation = string.Empty;

            if (results != null) //Internet.
            {
                foreach (var result in results)
                {
                    translation += result.translated;
                }

                if(translation != string.Empty)
                {
                    print($"translation was {translation}");
                    // inputFieldToAddTranslation.AnimateOut(forceAnimate: true);
                    //inputFieldToAddTranslation.inputField.text = translation;
                    inputFieldToAddTranslation.inputField.SetTextWithoutNotify(translation);
                    inputFieldToAddTranslation.AnimateOut(forceAnimate: true);
                }
                //Debug.Log(result.original + " => " + result.translated);
            }
        });
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}