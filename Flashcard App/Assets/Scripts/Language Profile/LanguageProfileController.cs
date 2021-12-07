using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//Controller to hold a list of all the language profiles the user has created and their current profile they are using on the app.

//TO DO - make class that reads / writes json from / to specific folder.
// I'm writing 3 profiles to JSON at start every time - eventually have a check for if there is no profiles on device - prompt them to make one before they can continue.
//Ensure there's at least one profile saved before allowing user to make cards, do other stuff etc. 
//Check for this in ScreenController? If no profiles - load create profile screen, otherwise load create card screen.

public class LanguageProfileController : MonoBehaviour
{
    public static LanguageProfileController Instance;

    [Header("Storing JSON Location - Unity Editor & PC")]
    [SerializeField] private string languageProfilesListJSONPathPC;
    [SerializeField] private string currentProfileJSONPathPC;
    [Header("Storing JSON Location - Mobile")]
    [SerializeField] private string languageProfilesListJSONPathMobile;
    [SerializeField] private string currentProfileJSONPathMobile;

    [Header("User's language profile info")]
    [SerializeField] private List<LanguageProfile> userLanguageProfilesList;
    public LanguageProfile userCurrentLanguageProfile;
    public List<LanguageProfile> GetUserLanguageProfiles() => userLanguageProfilesList;

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

        //LanguageProfile userCurrentLanguageProfile = ReadCurrentLanguageProfileFromJSON();

        print(Application.persistentDataPath);
        print(Application.dataPath);

        //Writing these to JSON at start - change later.
        CreateSampleProfilesAndSaveToJSON();

        //Save to memory.
        userLanguageProfilesList = ReadLanguageProfileListFromJSON();
        userCurrentLanguageProfile = userLanguageProfilesList.Find(profile => profile.currentProfile == true);
    }


    //Testing.
    private void CreateSampleProfilesAndSaveToJSON()
    {
        List<LanguageProfile> languageProfiles = new List<LanguageProfile>()
        {
            new LanguageProfile(nativeLanguage: new Language("en","English"),learningLanguage: new Language("ru","Russian"),setCurrentProfile: true),
            new LanguageProfile(nativeLanguage: new Language("en","English"),learningLanguage: new Language("ja","Japanese"),setCurrentProfile: false),
            new LanguageProfile(nativeLanguage: new Language("en","English"),learningLanguage: new Language("it","Italian"),setCurrentProfile: false),
        };
        SaveListOfLanguageProfilesToJSON(languageProfiles);
    }

    private void SaveListOfLanguageProfilesToJSON(List<LanguageProfile> languageProfilesList)
    {
        string json = JSONHelper.ToJson(languageProfilesList);

#if UNITY_EDITOR

        if(File.Exists(Application.dataPath + languageProfilesListJSONPathPC) == false)
            File.Create(Application.dataPath + languageProfilesListJSONPathPC).Dispose();


        //Old way - File.WriteAllText(Application.dataPath + languageProfilesListJSONPathPC, json);
        using (TextWriter writer = new StreamWriter(Application.dataPath + languageProfilesListJSONPathPC, false))
        {
            writer.WriteLine(json);
            writer.Close();
        }

#elif UNITY_ANDROID
        File.WriteAllText(Application.persistentDataPath + languageProfilesListJSONPathMobile, json);
#endif


    }
    private List<LanguageProfile> ReadLanguageProfileListFromJSON()
    {
        string json;

#if UNITY_EDITOR
        json = File.ReadAllText(Application.dataPath + languageProfilesListJSONPathPC);

#elif UNITY_ANDROID
        json = File.ReadAllText(Application.persistentDataPath + languageProfilesListJSONPathMobile);

#endif
        List<LanguageProfile> profiles = JSONHelper.FromJson<LanguageProfile>(json);
        return profiles;

        //string json = File.ReadAllText(Application.dataPath + languageProfilesListJSONPathPC);

        //List<LanguageProfile> profiles = JSONHelper.FromJson<LanguageProfile>(json);

        //return profiles;
    }
    private void SaveEngRuAsCurrentLanguageProfileToJson()
    {
        LanguageProfile englishRussianProfile = new LanguageProfile(new Language("en","English"),new Language("ru","Russian"),true);

        string profileAsJSON = JsonUtility.ToJson(englishRussianProfile);

        string filePath = Application.dataPath + currentProfileJSONPathPC;

        File.WriteAllText(filePath, profileAsJSON);
    }
    private LanguageProfile ReadCurrentLanguageProfileFromJSON()
    {
        LanguageProfile currentLanguageProfile = JsonUtility.FromJson<LanguageProfile>(File.ReadAllText(Application.dataPath + currentProfileJSONPathPC));
        return currentLanguageProfile;
    }
}