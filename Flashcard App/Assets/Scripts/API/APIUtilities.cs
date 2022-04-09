//#define PRINT_API_RESULTS

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIUtilities : MonoBehaviour
{
    //Singleton.
    public static APIUtilities Instance;
    //Events.
    public static Action<Token> UserLoggedInEvent;
    public static Action UserRegisteredEvent;


    public const string ApiAddress = "https://localhost:44306";

    //Start.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public void AttemptToRegister(string email, string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback) => StartCoroutine(IEnumerator_Register(email, username, password, confirmPassword, successCallback, failedCallback));
    public void AttemptToLogin(string username, string password, Action<Token> successCallback, Action<string> failedCallback) => StartCoroutine(IEnumerator_Login(username, password, successCallback, failedCallback));
    public IEnumerator IEnumerator_Register(string email, string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("Email", email);
        data.Add("Username", username);
        data.Add("Password", password);
        data.Add("ConfirmPassword", confirmPassword);

        using (UnityWebRequest request = UnityWebRequest.Post(ApiAddress + "/api/Account/Register", data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                successCallback?.Invoke();
                UserRegisteredEvent?.Invoke();

                #if PRINT_API_RESULTS
                print("API: User Registered");
                #endif 
            }
            else
            {
                failedCallback?.Invoke(GetRegisterErrorDescriptionFromAPIError(request.downloadHandler.text));

                #if PRINT_API_RESULTS
                print("API: User Did Not Register");
                #endif
            }
        }
    }
    public IEnumerator IEnumerator_Login(string username,string password, Action<Token> successCallback, Action<string> failedCallback)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("grant_type", "password");
        data.Add("UserName", username);
        data.Add("Password", password);

        using (UnityWebRequest request = UnityWebRequest.Post($"{ApiAddress}/Token", data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Token userToken = JsonUtility.FromJson<Token>(request.downloadHandler.text);

                PlayerPrefs.SetString("User_ID", userToken.userID);
                PlayerPrefs.SetInt("Token_Expires", userToken.expires_in);

                successCallback?.Invoke(userToken);
                UserLoggedInEvent?.Invoke(userToken);

                #if PRINT_API_RESULTS
                print("API: User Logged In.");
                #endif
            }
            else
            {
                failedCallback?.Invoke(GetLoginErrorDescriptionFromAPIError(request.downloadHandler.text));

                #if PRINT_API_RESULTS
                print("API: User Did Not Login");
                #endif
            }
        }
    }
    //Get data.
    public IEnumerator IEnumerator_GetUser(string userID,Action<User> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/CustomUsers/GetUserDTO?id={userID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                //string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                string json = request.downloadHandler.text;
                User user = JsonUtility.FromJson<User>(json);
                returnCallback(user);

                #if PRINT_API_RESULTS
                print("API: Got User.");
                #endif
            }
            else
            {
                #if PRINT_API_RESULTS
                print("API: Did Not Get User.");
                #endif
            }
        }
    }
    public IEnumerator IEnumerator_GetLanguages(Action<List<Language>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + "/api/Languages"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Language> languages = JSONHelper.FromJson<Language>(json);

                returnCallback(languages);

                #if PRINT_API_RESULTS
                print("API: Got Languages.");
                #endif
            }
            else
            {
                returnCallback(null);

                #if PRINT_API_RESULTS
                print("API: Did Not Get Languages.");
                #endif
            }
        }
    }
    public IEnumerator IEnumerator_GetLanguageProfilesOfUser(string userID,Action<List<LanguageProfile>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/LanguageProfiles?userID={userID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<LanguageProfile> languageProfilesOfUser = JSONHelper.FromJson<LanguageProfile>(json);

                returnCallback?.Invoke(languageProfilesOfUser);

                #if PRINT_API_RESULTS
                print("API: Got Language Profiles Of User.");
                #endif
            }
            else
            {
                returnCallback?.Invoke(null);

                #if PRINT_API_RESULTS
                print("API: Did Not Get Language Profiles Of User.");
                #endif
            }
        }
    }
    public IEnumerator IEnumerator_GetSetsOfLanguageProfile(string languageProfileID,Action<List<Set>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/Sets?languageProfileID={languageProfileID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Set> setsOfLanguageProfile = JSONHelper.FromJson<Set>(json);
                returnCallback?.Invoke(setsOfLanguageProfile);

                #if PRINT_API_RESULTS
                print("API: Got Sets Of Language Profile.");
                #endif
            }
            else
            {
                #if PRINT_API_RESULTS
                print("API: Did Not Get Sets Of Language Profile.");
                #endif
            }
        }
    }
    public IEnumerator IEnumerator_GetFlashcardsOfLanguageProfile(string languageProfileID,Action<List<Flashcard>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/Flashcards?languageProfileID={languageProfileID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Flashcard> flashcardsOfLanguageProfile = JSONHelper.FromJson<Flashcard>(json);
                returnCallback?.Invoke(flashcardsOfLanguageProfile);
                #if PRINT_API_RESULTS
                print("API: Got Flashcards Of Language Profile.");
                #endif
            }
            else
            {
                #if PRINT_API_RESULTS
                print("API: Did Not Get Flashcards Of Language Profile.");
                #endif
            }
        }
    }

    //Post data.
    public void PostNewLanguageProfile(string userID, LanguageProfile languageProfile) => StartCoroutine(IEnumerator_PostNewLanguageProfile(userID, languageProfile));
    public IEnumerator IEnumerator_PostNewLanguageProfile(string userID,LanguageProfile languageProfile)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("ID", languageProfile.ID);
        data.Add("userID", userID);
        data.Add("nativeLanguageISO", languageProfile.NativeLanguage.ISO);
        data.Add("learningLanguageISO", languageProfile.LearningLanguage.ISO);
        data.Add("IsCurrentProfile", languageProfile.IsCurrentProfile.ToString());

        using (UnityWebRequest request = UnityWebRequest.Post(ApiAddress + "/api/PostLanguageProfile", data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                #if PRINT_API_RESULTS
                print("API: Posted New Language Profile.");
                #endif
            }
            else
            {
                #if PRINT_API_RESULTS
                print("API: Did Not Post New Language Profile.");
                #endif
            }
        }
    }
    public void PostNewSet(Set set) => StartCoroutine(IEnumerator_PostNewSet(set));
    public IEnumerator IEnumerator_PostNewSet(Set set)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("ID", set.ID);
        data.Add("Name", set.Name);
        data.Add("LanguageProfileID", set.LanguageProfileID);
        data.Add("ParentSetID", set.ParentSetID);
        data.Add("IsDefaultSet", set.IsDefaultSet.ToString());

        using (UnityWebRequest request = UnityWebRequest.Post(ApiAddress + "/api/Sets", data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                #if PRINT_API_RESULTS
                print("API: Posted New Set.");
                #endif
            }
            else
            {
                #if PRINT_API_RESULTS
                print("API: Did Not Post New Set.");
                #endif
            }
        }
    }
    public IEnumerator IEnumerator_PostNewFlashcard(Flashcard flashcard)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("Id", flashcard.Id);
        data.Add("setID", flashcard.setID);
        data.Add("nativeSide", flashcard.nativeSide);
        data.Add("learningSide", flashcard.learningSide);
        data.Add("notes", flashcard.notes);

        using (UnityWebRequest request = UnityWebRequest.Post(ApiAddress + "/api/Flashcards", data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                #if PRINT_API_RESULTS
                print("API: Posted New Flashcard.");
                #endif
            }
            else
            {
                #if PRINT_API_RESULTS
                print("API: Did Not Post New Flashcard.");
                #endif
            }
        }
    }

    //Put data / modify data.
    public IEnumerator IEnumerator_PutLanguageProfile(string langProfileID,LanguageProfile languageProfile,Action successCallback,Action failureCallback)
    {
        string json = JsonUtility.ToJson(languageProfile);

        using (UnityWebRequest request = UnityWebRequest.Put(ApiAddress + $"/api/LanguageProfiles/{langProfileID}",json))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                successCallback?.Invoke();
                #if PRINT_API_RESULTS
                print("API: Put / Modified Language Profile.");
                #endif
            }
            else
            {
                failureCallback?.Invoke();
                #if PRINT_API_RESULTS
                print("API: Did Not Put / Modify Language Profile.");
                #endif
            }
        }
    }

    public void ModifyDefaultSetValue(Set set, Action successCallback = null, Action failureCallback = null)
    {
        StartCoroutine(IEnumerator_ModifyDefaultSet(set, successCallback, failureCallback));
    }
    public IEnumerator IEnumerator_ModifyDefaultSet(Set set, Action successCallback = null, Action failureCallback = null)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("ID", set.ID);
        data.Add("Name", set.Name);
        data.Add("LanguageProfileID", set.LanguageProfileID);
        data.Add("ParentSetID", set.ParentSetID);
        data.Add("IsDefaultSet", set.IsDefaultSet.ToString());

        using (UnityWebRequest request = UnityWebRequest.Post(ApiAddress + $"/api/Sets/ModifyDefaultSetValue",data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                successCallback?.Invoke();
                #if PRINT_API_RESULTS
                print("API: Modified Default Set Bool Value Of Set.");
                #endif
            }
            else
            {
                failureCallback?.Invoke();
                #if PRINT_API_RESULTS
                print("API: Did Not Modify Default Set Bool Value Of Set.");
                #endif
            }
        }
    }

    private string GetLoginErrorDescriptionFromAPIError(string errorJson)
    {
        Dictionary<string, object> errorDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(errorJson);
        return errorDict["error_description"].ToString();
    }
    private string GetRegisterErrorDescriptionFromAPIError(string errorJson)
    {
        Dictionary<string, object> errorDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(errorJson);
        return errorDict["Message"].ToString();
    }
}

[Serializable]
public class Token
{
    public string access_token;
    public string userID;
    public int expires_in;
}