using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginRegisterScreenController : MonoBehaviour
{
    [Header("Login Input Fields")]
    [SerializeField] private CustomInputField usernameInputField;
    [SerializeField] private CustomInputField passwordInputField;

    [Header("Buttons")]
    [SerializeField] private Button btnLogin;
    [SerializeField] private Button btnGoToRegisterScreen;

    [Header("Error message")]
    [SerializeField] private ErrorMessage errorMessage;
    private const string inputFieldsEmptyErrorMessage = "All input fields must be filled!";

    [Header("Progress Bar")]
    [SerializeField] private UIManagerProgressBarLoop progressBarLoop;

    [SerializeField] private bool InProcessOfAttemptingToLogin;
    private Coroutine attemptToLoginCoroutine;

    [SerializeField] private ScreenPushData homeScreenPushData;

    //Testing.
    [SerializeField] private bool AutoLogin;
    [SerializeField] private string autoLoginUsername;
    [SerializeField] private string autoLoginPassword;

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
        btnGoToRegisterScreen.onClick.AddListener(OnRegisterButtonSelected);
    }

    private void Start()
    {
        if(AutoLogin)
        {
            usernameInputField.Text = autoLoginUsername;
            passwordInputField.Text = autoLoginPassword;
            OnLoginButtonSelected();
        }
    }

    private void OnLoginButtonSelected()
    {
        if (!CanAttemptToLogin)
        {
            errorMessage.EnableMessage(inputFieldsEmptyErrorMessage);
            return;
        }

        if (errorMessage.IsActive)
            errorMessage.Disable();

        SetVisualLoggingInProcess(true);
        StartLoginAttemptCoroutine();
    }


    private void OnRegisterButtonSelected()
    {
        if(InProcessOfAttemptingToLogin)
        {
            StopLoginAttemptCoroutine();
            SetVisualLoggingInProcess(false);
        }

        //Goes to register screen in another script.
    }

    private void OnLoginSuccessful(Token token)
    {
        print($"User with id: {token.userID} logged in.");
        print($"Token expires in {token.expires_in}");

        //StartCoroutine(APIUtilities.Instance.GetUser(token.userID, UserDataHolder.Instance.SetCurrentUser));
        //PlayerPrefs.SetString("User_ID", token.userID);
        //PlayerPrefs.SetInt("Token_Expires", token.expires_in);

        StartCoroutine(StartUserDataRetrievalCoroutine());
    }

    private IEnumerator StartUserDataRetrievalCoroutine()
    {
        string loggedInUserID = PlayerPrefs.GetString("User_ID");

        //Load player data.
        yield return StartCoroutine(APIUtilities.Instance.IEnumerator_GetLanguages(LanguageDataHolder.Instance.UpdateLanguagesList));
        yield return StartCoroutine(APIUtilities.Instance.IEnumerator_GetUser(loggedInUserID, UserDataHolder.Instance.SetCurrentUser));
        yield return StartCoroutine(APIUtilities.Instance.IEnumerator_GetLanguageProfilesOfUser(loggedInUserID, LanguageProfileController.Instance.UpdateLanguageProfilesData));
        yield return StartCoroutine(APIUtilities.Instance.IEnumerator_GetSetsOfLanguageProfile(LanguageProfileController.Instance.currentLanguageProfile.ID, SetsDataHolder.Instance.UpdateSetsData));
        yield return StartCoroutine(APIUtilities.Instance.IEnumerator_GetFlashcardsOfLanguageProfile(LanguageProfileController.Instance.currentLanguageProfile.ID, FlashcardDataHolder.Instance.UpdateFlashcardList));
        

        SetVisualLoggingInProcess(false);

        SceneManager.LoadScene("MainScene");
        //BlitzyUI.UIManager.Instance.QueuePop();
        //BlitzyUI.UIManager.Instance.QueuePush(homeScreenPushData.ID);
    }

    private void OnLoginUnsuccessful(string errorJson)
    {
        print("Login was not successful.");
        errorMessage.EnableMessage(errorJson);

        SetVisualLoggingInProcess(false);
    }

    private void SetVisualLoggingInProcess(bool enable)
    {
        if (enable)
        {
            btnLogin.interactable = false;
            InProcessOfAttemptingToLogin = true;
            progressBarLoop.gameObject.SetActive(enable);
        }
        else
        {
            btnLogin.interactable = true;
            InProcessOfAttemptingToLogin = false;
            progressBarLoop.gameObject.SetActive(enable);
        }
    }

    private void StartLoginAttemptCoroutine() => attemptToLoginCoroutine = StartCoroutine(APIUtilities.Instance.IEnumerator_Login(usernameInputField.Text, passwordInputField.Text, OnLoginSuccessful, OnLoginUnsuccessful));
    private void StopLoginAttemptCoroutine()
    {
        StopCoroutine(attemptToLoginCoroutine);
        attemptToLoginCoroutine = null;
    }
}
