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
    private IEnumerator Login(string email,string password, Action<Token> successCallback, Action<string> failedCallback)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("grant_type", "password");
        data.Add("UserName", email);
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

}

[Serializable]
public class Token
{
    public string access_token;
    public string userID;
    public int expires_in;
}