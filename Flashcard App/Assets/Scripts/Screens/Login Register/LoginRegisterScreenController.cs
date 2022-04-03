using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginRegisterScreenController : MonoBehaviour
{
    [Header("Login Input Fields")]
    [SerializeField] private CustomInputField emailInputField;
    [SerializeField] private CustomInputField passwordInputField;

    [Header("Buttons")]
    [SerializeField] private Button btnLogin;
    //[SerializeField] private Button btnGoToRegisterScreen;

    [Header("Error message")]
    [SerializeField] private ErrorMessage errorMessage;
    private const string inputFieldsEmptyErrorMessage = "All input fields must be filled!";

    [Header("Progress Bar")]
    [SerializeField] private UIManagerProgressBarLoop progressBarLoop;

    [SerializeField] private bool IsAttemptingToLogin;

    private bool CanAttemptToLogin 
    { 
        get 
        { 
            return !emailInputField.InputFieldIsEmpty && 
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

        IsAttemptingToLogin = true;
        progressBarLoop.gameObject.SetActive(true);
    }
    //private void OnRegisterButtonSelected() 
    //{ 
    
    //}

    private void OnLoginSuccessful() { }
    private void OnLoginUnsuccessful() { }

}
