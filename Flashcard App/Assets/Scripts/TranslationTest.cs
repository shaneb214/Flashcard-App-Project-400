using TMPro;
using UniLang;
using UnityEngine;

public class TranslationTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmProTranslationDisplay;

    public void OnTextFieldChanged(string textToTranslate)
    {
        Translator translator = Translator.Create(LanguageISO.Auto, LanguageISO.Greek);
        translator.Run(textToTranslate, (results) => 
        {
            string translation = string.Empty;

            if(results != null) //Internet.
            {
                foreach (var result in results)
                {
                    translation += result.translated;
                }

                tmProTranslationDisplay.text = translation;
                //Debug.Log(result.original + " => " + result.translated);
            }
        });
    }

}
