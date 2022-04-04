using System;
using System.Collections;
using System.Collections.Generic;
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

    //List<Language> myLanguages = new List<Language>(); 
    //private IEnumerator Start()
    //{
    //    yield return GetLanguages(listFromAPI => myLanguages = listFromAPI);
    //}

    public void AttemptToRegister(string email, string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback) => StartCoroutine(Register(email, username, password, confirmPassword, successCallback, failedCallback));
    public void AttemptToLogin(string email, string password, Action<Token> successCallback, Action<string> failedCallback) => StartCoroutine(Login(email, password, successCallback, failedCallback));
    private IEnumerator Register(string email, string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback)
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
                successCallback.Invoke();
                UserRegisteredEvent?.Invoke();
            }
            else
            {
                failedCallback.Invoke(request.downloadHandler.text);
            }
        }
    }
    private IEnumerator Login(string username,string password, Action<Token> successCallback, Action<string> failedCallback)
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
                successCallback.Invoke(userToken);
                UserLoggedInEvent?.Invoke(userToken);
            }
            else
            {
                failedCallback.Invoke(request.downloadHandler.text);
            }
        }
    }


    //Get data.
    public IEnumerator GetLanguages(Action<List<Language>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + "/api/Languages"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Language> languages = JSONHelper.FromJson<Language>(json);

                returnCallback(languages);
            }
            else
            {
                returnCallback(null);
            }
        }
    }
    public IEnumerator GetLanguageProfilesOfUser(string userID,Action<List<LanguageProfile>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/LanguageProfiles?userID={userID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<LanguageProfile> languageProfilesOfUser = JSONHelper.FromJson<LanguageProfile>(json);

                returnCallback?.Invoke(languageProfilesOfUser);
            }
            else
            {
                returnCallback?.Invoke(null);
            }
        }
    }
    public IEnumerator GetSetsOfLanguageProfile(string languageProfileID)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/Sets?languageProfileID={languageProfileID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Set> setsOfLanguageProfile = JSONHelper.FromJson<Set>(json);

            }
            else
            {

            }
        }
    }
    public IEnumerator GetFlashcardsOfLanguageProfile(string languageProfileID)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/Flashcards?languageProfileID={languageProfileID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Flashcard> flashcardsOfLanguageProfile = JSONHelper.FromJson<Flashcard>(json);

            }
            else
            {

            }
        }
    }

    //Post data.
    public IEnumerator PostNewLanguageProfile(string userID,LanguageProfile languageProfile)
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
                print("New language profile posted.");
            }
            else
            {
                print("Lanugage profile post Unsuccessful.");
            }
        }
    }

    public IEnumerator PostNewSet(Set set)
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
                print("New set posted.");
            }
            else
            {
                print("New set post Unsuccessful.");
            }
        }
    }

}

[Serializable]
public class Token
{
    public string access_token;
    public string userID;
    public int expires_in;
}