using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//Controller to hold a list of all the language profiles the user has created and their current profile they are using on the app.

//TO DO - make class that reads / writes json from / to specific folder.
// I'm writing 3 profiles to JSON at start every time - eventually have a check for if there is no profiles on device - prompt them to make one before they can continue.
//Ensure there's at least one profile saved before allowing user to make cards, do other stuff etc. 
//Check for this in ScreenController? If no profiles - load create profile screen, otherwise load create card screen.

//NOTE: I changed the script execution order for this script so it gets called before others. 

public class LanguageProfileController : MonoBehaviour
{
    public static LanguageProfileController Instance;

    public event Action<LanguageProfile> UserSelectedNewProfileEvent;

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

    #region Start
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

        //Writing these to JSON at start - change later.
        CreateSampleProfilesAndSaveToJSON();

        //Save to memory.
        userLanguageProfilesList = ReadLanguageProfileListFromJSON();
        userCurrentLanguageProfile = userLanguageProfilesList.Find(profile => profile.IsCurrentProfile == true);
    }
    #endregion
    #region New Language Profile Creation / Selection
    private void OnNewLanguageProfileCreated(LanguageProfile newProfile)
    {
        //Has user set the new profile to be the current profile? - Set it as so. 
        if (newProfile.IsCurrentProfile)
            SelectNewProfile(newProfile);

        //Add to memory & save to JSON.
        userLanguageProfilesList.Add(newProfile);
        //SaveListOfLanguageProfilesToJSON(userLanguageProfilesList);
    }

    //Select new Profile passing in languageprofile object.
    public void SelectNewProfile(LanguageProfile newProfile)
    {
        //If user selects same profile, dont have to do anything.
        if (newProfile == userCurrentLanguageProfile)
            return;

        //If there was a current profile, set that to not be the current profile anymore.
        if(userCurrentLanguageProfile != null)
            userCurrentLanguageProfile.IsCurrentProfile = false;

        //Update current profile and raise event to notify certain UI objects.
        userCurrentLanguageProfile = newProfile;
        userCurrentLanguageProfile.IsCurrentProfile = true;

        UserSelectedNewProfileEvent?.Invoke(userCurrentLanguageProfile);
        print($"New Language Profile Was Selected: {newProfile}");
    }

    //Select new Profile passing in ID. Finds profile based on ID.
    public void SelectNewProfile(string ID)
    {
        LanguageProfile newProfile = userLanguageProfilesList.Find(profile => profile.ID == ID);
        SelectNewProfile(newProfile);
    }
    #endregion
    #region New Set Creation
    private void OnNewSetCreated(Set newSet)
    {
        userCurrentLanguageProfile.setList.Add(newSet);
    }
    #endregion
    #region Writing / Saving Data To Json
    private void CreateSampleProfilesAndSaveToJSON()
    {
        List<LanguageProfile> languageProfiles = new List<LanguageProfile>()
        {
            new LanguageProfile(nativeLanguage: new Language("en","English"),learningLanguage: new Language("ru","Russian"),IsCurrentProfile: true),
            new LanguageProfile(nativeLanguage: new Language("en","English"),learningLanguage: new Language("ja","Japanese"),IsCurrentProfile: false),
            new LanguageProfile(nativeLanguage: new Language("en","English"),learningLanguage: new Language("it","Italian"),IsCurrentProfile: false),
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
    #endregion
    #region Event Subscribing / Unsubscribing.
    private void OnEnable()
    {
        LanguageProfile.LanguageProfileCreatedEvent += OnNewLanguageProfileCreated;
        Set.SetCreatedEvent += OnNewSetCreated;
    }

    private void OnDisable()
    {
        LanguageProfile.LanguageProfileCreatedEvent -= OnNewLanguageProfileCreated;
        Set.SetCreatedEvent -= OnNewSetCreated;
    }
    #endregion

    //Application stops: Save all info to JSON?
    private void OnApplicationQuit()
    {
        SaveListOfLanguageProfilesToJSON(userLanguageProfilesList);
    }
}