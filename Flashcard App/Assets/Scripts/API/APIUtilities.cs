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

    public void AttemptToRegister(string email, string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback) => StartCoroutine(Register(email, username, password, confirmPassword, successCallback, failedCallback));
    public void AttemptToLogin(string username, string password, Action<Token> successCallback, Action<string> failedCallback) => StartCoroutine(Login(username, password, successCallback, failedCallback));
    public IEnumerator Register(string email, string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback)
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
                //User newUser = new User(username, email);
            }
            else
            {
                failedCallback?.Invoke(request.downloadHandler.text);
            }
        }
    }
    public IEnumerator Login(string username,string password, Action<Token> successCallback, Action<string> failedCallback)
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
                successCallback?.Invoke(userToken);
                UserLoggedInEvent?.Invoke(userToken);

                PlayerPrefs.SetString("User_ID", userToken.userID);
                PlayerPrefs.SetInt("Token_Expires", userToken.expires_in);
            }
            else
            {
                failedCallback?.Invoke(request.downloadHandler.text);
            }
        }
    }


    //Get data.
    public IEnumerator GetUser(string userID,Action<User> returnCallback)
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
            }
            else
            {

            }
        }
    }
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
    public IEnumerator GetSetsOfLanguageProfile(string languageProfileID,Action<List<Set>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/Sets?languageProfileID={languageProfileID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Set> setsOfLanguageProfile = JSONHelper.FromJson<Set>(json);
                returnCallback?.Invoke(setsOfLanguageProfile);
            }
            else
            {

            }
        }
    }
    public IEnumerator GetFlashcardsOfLanguageProfile(string languageProfileID,Action<List<Flashcard>> returnCallback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiAddress + $"/api/Flashcards?languageProfileID={languageProfileID}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = JSONHelper.ModifyJSONString(request.downloadHandler.text);
                List<Flashcard> flashcardsOfLanguageProfile = JSONHelper.FromJson<Flashcard>(json);
                returnCallback?.Invoke(flashcardsOfLanguageProfile);
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
    public IEnumerator PostNewFlashcard(Flashcard flashcard)
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
                print("New flashcard posted.");
            }
            else
            {
                print("New flashcard post Unsuccessful.");
            }
        }
    }



    //Put data / modify data.
    public IEnumerator PutLanguageProfile(string langProfileID,LanguageProfile languageProfile,Action successCallback,Action failureCallback)
    {
        string json = JsonUtility.ToJson(languageProfile);

        using (UnityWebRequest request = UnityWebRequest.Put(ApiAddress + $"/api/LanguageProfiles/{langProfileID}",json))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                successCallback?.Invoke();
            }
            else
            {
                failureCallback?.Invoke();
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