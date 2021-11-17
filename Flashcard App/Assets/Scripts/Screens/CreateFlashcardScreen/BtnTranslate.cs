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
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        //Create animation for arrow.

        Translator translator = Translator.Create(LanguageISO.Auto, LanguageISO.Russian); //Hard coded for now.
        translator.Run(txtTranslateFrom.text, (results) =>
        {
            string translation = string.Empty;

            if (results != null) //Internet.
            {
                foreach (var result in results)
                {
                    translation += result.translated;
                }

                inputField.text = translation;
                //Debug.Log(result.original + " => " + result.translated);
            }
        });
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}