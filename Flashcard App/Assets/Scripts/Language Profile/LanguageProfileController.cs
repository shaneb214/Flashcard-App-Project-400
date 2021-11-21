using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//TO DO - make class that reads / writes json from / to specific folder.
public class LanguageProfileController : MonoBehaviour
{
    public static LanguageProfileController Instance;

    [Header("Storing JSON Location")]
    [SerializeField] private string languageProfilesListJSONPath;
    [SerializeField] private string currentProfileJSONPath;


    [Header("User's language profile info")]
    [SerializeField] private List<LanguageProfile> userLanguageProfilesList;
    public LanguageProfile userCurrentLanguageProfile;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        userLanguageProfilesList = new List<LanguageProfile>();

        // Read language profiles from json. Store in list.
        // Find current language. 
        userLanguageProfilesList = ReadLanguageProfileListFromJSON();
        userCurrentLanguageProfile = userLanguageProfilesList.Find(profile => profile.currentProfile == true);

        //LanguageProfile userCurrentLanuguageProfile = ReadCurrentLanguageProfileFromJSON();

        //List<LanguageProfile> languageProfiles = new List<LanguageProfile>()
        //{
        //    new LanguageProfile("en","ru",true),
        //    new LanguageProfile("en","ja",false),
        //    new LanguageProfile("en","it",false),
        //};

        //SaveListOfLanguageProfilesToJSON(languageProfiles);

    }

    //Testing.
    private void SaveListOfLanguageProfilesToJSON(List<LanguageProfile> languageProfilesList)
    {
        string json = JSONHelper.ToJson(languageProfilesList);

        File.WriteAllText(Application.dataPath + languageProfilesListJSONPath, json);
    }
    private List<LanguageProfile> ReadLanguageProfileListFromJSON()
    {
        string json = File.ReadAllText(Application.dataPath + languageProfilesListJSONPath);

        List<LanguageProfile> profiles = JSONHelper.FromJson<LanguageProfile>(json);

        return profiles;
    }
    private void SaveEngRuAsCurrentLanguageProfileToJson()
    {
        LanguageProfile englishRussianProfile = new LanguageProfile("en", "ru", true);

        string profileAsJSON = JsonUtility.ToJson(englishRussianProfile);

        string filePath = Application.dataPath + currentProfileJSONPath;

        File.WriteAllText(filePath, profileAsJSON);
    }
    private LanguageProfile ReadCurrentLanguageProfileFromJSON()
    {
        LanguageProfile currentLanguageProfile = JsonUtility.FromJson<LanguageProfile>(File.ReadAllText(Application.dataPath + currentProfileJSONPath));
        return currentLanguageProfile;
    }
}