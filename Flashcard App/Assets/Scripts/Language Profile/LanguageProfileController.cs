using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//TO DO - make class that reads / writes json from / to specific folder.


public class LanguageProfileController : MonoBehaviour
{
    [Header("Storing JSON Location")]
    [SerializeField] private string languageProfileJSONResourcePath;
    [SerializeField] private string currentLanguageProfileJSONFileName; 

    [Header("User's language profile info")]
    [SerializeField] private List<LanguageProfile> userLanguageProfilesList;
    [SerializeField] private LanguageProfile userCurrentLanguageProfile;

    private void Start()
    {
        userLanguageProfilesList = new List<LanguageProfile>();

        // Read language profiles from json. Store in list.
        // Find current language. 


        LanguageProfile userCurrentLanuguageProfile = ReadCurrentLanguageProfileFromJSON();

        
    }

    //Testing.
    private void SaveEngRuAsCurrentLanguageProfileToJson()
    {
        LanguageProfile englishRussianProfile = new LanguageProfile("en", "ru", true);

        string profileAsJSON = JsonUtility.ToJson(englishRussianProfile);

        string filePath = Application.dataPath + languageProfileJSONResourcePath + languageProfileJSONResourcePath + "/" + currentLanguageProfileJSONFileName + ".json";

        File.WriteAllText(filePath, profileAsJSON);
    }

    private LanguageProfile ReadCurrentLanguageProfileFromJSON()
    {
        LanguageProfile currentLanguageProfile = JsonUtility.FromJson<LanguageProfile>(File.ReadAllText(Application.dataPath + languageProfileJSONResourcePath + "/" + currentLanguageProfileJSONFileName + ".json"));
        return currentLanguageProfile;
    }
}