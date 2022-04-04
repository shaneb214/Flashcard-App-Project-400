using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginRegisterScreenController : MonoBehaviour
{
    [Header("Login Input Fields")]
    [SerializeField] private CustomInputField usernameInputField;
    [SerializeField] private CustomInputField passwordInputField;

    [Header("Buttons")]
    [SerializeField] private Button btnLogin;
    //[SerializeField] private Button btnGoToRegisterScreen;

    [Header("Error message")]
    [SerializeField] private ErrorMessage errorMessage;
    private const string inputFieldsEmptyErrorMessage = "All input fields must be filled!";

    [Header("Progress Bar")]
    [SerializeField] private UIManagerProgressBarLoop progressBarLoop;

    [SerializeField] private bool InProcessOfAttemptingToLogin;

    private bool CanAttemptToLogin 
    { 
        get 
        { 
            return !usernameInputField.InputFieldIsEmpty && 
                   !passwordInputField.InputFieldIsEmpty; 
        } 
    }

    private void Awake()
    {
        btnLogin.onClick.AddListener(OnLoginButtonSelected);
        //btnGoToRegisterScreen.onClick.AddListener(OnRegisterButtonSelected);
    }

    private void OnLoginButtonSelected() 
    {
        if(!CanAttemptToLogin)
        {
            errorMessage.EnableMessage(inputFieldsEmptyErrorMessage);
            return;
        }

        if (errorMessage.IsActive)
            errorMessage.Disable();

        SetVisualRegisteringProcess(true);


        APIUtilities.Instance.AttemptToLogin(usernameInputField.Text, passwordInputField.Text, OnLoginSuccessful, OnLoginUnsuccessful);

    }
    //private void OnRegisterButtonSelected() 
    //{ 
    
    //}

    private void OnLoginSuccessful(Token token)
    {
        print($"User with id: {token.userID} logged in.");
        print($"Token expires in {token.expires_in}");

        User loggedInUser = new User() { ID = token.userID, Name = usernameInputField.Text };
        UserDataHolder.Instance.CurrentUser = loggedInUser;

        SetVisualRegisteringProcess(false);
    }
    private void OnLoginUnsuccessful(string errorJson)
    {
        print("Login was not successful.");
        errorMessage.EnableMessage(errorJson);

        SetVisualRegisteringProcess(false);
    }

    private void SetVisualRegisteringProcess(bool enable)
    {
        if (enable)
        {
            InProcessOfAttemptingToLogin = true;
            progressBarLoop.gameObject.SetActive(enable);
        }
        else
        {
            InProcessOfAttemptingToLogin = false;
            progressBarLoop.gameObject.SetActive(enable);
        }
    }
}
