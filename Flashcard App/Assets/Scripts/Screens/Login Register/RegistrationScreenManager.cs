using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationScreenManager : MonoBehaviour
{
    //Components.
    [Header("Input Fields")]
    [SerializeField] private CustomInputField usernameInputField;
    [SerializeField] private CustomInputField emailInputField;
    [SerializeField] private CustomInputField passwordInputField;
    [Header("Buttons")]
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnRegister;
    [Header("Progress Bar")]
    [SerializeField] private UIManagerProgressBarLoop progressBarLoop;     

    [Header("Register API Status")]
    [SerializeField] private bool IsInProcessOfRegistering;
    [Header("Error Displaying to User")]
    [SerializeField] private ErrorMessage errorMessage;
    private const string errorMessageInputFieldEmpty = "All input fields must be filled!";

    private bool CanAttemptToRegister 
    { 
        get 
        {
            return !usernameInputField.InputFieldIsEmpty &&
                !emailInputField.InputFieldIsEmpty &&
                !passwordInputField.InputFieldIsEmpty;
        } 
    }

    //Start.
    private void Awake()
    {
        btnBack.onClick.AddListener(OnBackButtonSelected);
        btnRegister.onClick.AddListener(OnRegisterButtonSelected);
    }

    private void OnBackButtonSelected()
    {
        if (IsInProcessOfRegistering)
            SetVisualRegisteringProcess(false);

        //Then loads screen through another script.
    }
    private void OnRegisterButtonSelected()
    {
        if(CanAttemptToRegister)
        {
            if (errorMessage.IsActive)
                errorMessage.Disable();

            SetVisualRegisteringProcess(true);
            btnRegister.enabled = false;
            APIUtilities.Instance.AttemptToRegister(emailInputField.Text, usernameInputField.Text, passwordInputField.Text, passwordInputField.Text, OnRegisterSuccessful, OnRegisterUnsuccessful);
        }  
        else
        {
            errorMessage.EnableMessage(errorMessageInputFieldEmpty);
        }
    }


    private void OnRegisterSuccessful() 
    {
        print("User registered successfully.");
        SetVisualRegisteringProcess(false);  
    }
    private void OnRegisterUnsuccessful(string errorJson)
    {
        print("User did NOT register.");

        btnRegister.enabled = true;
        errorMessage.EnableMessage(errorJson);

        SetVisualRegisteringProcess(false);
    }

    private void SetVisualRegisteringProcess(bool enable)
    {
        if(enable)
        {
            IsInProcessOfRegistering = true;
            progressBarLoop.gameObject.SetActive(enable);
        }
        else
        {
            IsInProcessOfRegistering = false;
            progressBarLoop.gameObject.SetActive(enable);
        }    
    }
}
