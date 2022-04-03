using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APITesting : MonoBehaviour
{
    public string ApiAddress = "https://localhost:44306/api";

    void Start()
    {
        //StartCoroutine(Register("shaneb214@gmail.com","shaneb", "password123", "password123", OnRegisterSuccess,OnRegisterFailed));
        //StartCoroutine(LoginPlayer());
    }

    IEnumerator Register(string email,string username, string password, string confirmPassword, Action successCallback, Action<string> failedCallback)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("Email", email);
        data.Add("Username", username);
        data.Add("Password", password);
        data.Add("ConfirmPassword", confirmPassword);

        using (UnityWebRequest request = UnityWebRequest.Post(ApiAddress + "/Account/Register", data))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                successCallback.Invoke();
            }
            else
            {
                failedCallback.Invoke(request.downloadHandler.text);
            }
        }
    }

    IEnumerator LoginPlayer(string email, string password, System.Action<Token> successCallback, Action<string> failedCallback)
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
                //this should work....
                successCallback.Invoke(JsonUtility.FromJson<Token>(request.downloadHandler.text));
            }
            else
            {
                failedCallback.Invoke(request.downloadHandler.text);
            }
        }
    }

    private void OnRegisterSuccess()
    {
        print("User registered");
    }

    private void OnRegisterFailed(string errorText)
    {
        print("User NOT registered");
        print(errorText);
    }
}

[Serializable]
public class Token
{
    public string access_token;
    public string userID;
    public int expires_in;
}
