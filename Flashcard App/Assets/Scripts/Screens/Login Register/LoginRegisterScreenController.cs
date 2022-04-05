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
    [SerializeField] private Button btnGoToRegisterScreen;

    [Header("Error message")]
    [SerializeField] private ErrorMessage errorMessage;
    private const string inputFieldsEmptyErrorMessage = "All input fields must be filled!";

    [Header("Progress Bar")]
    [SerializeField] private UIManagerProgressBarLoop progressBarLoop;

    [SerializeField] private bool InProcessOfAttemptingToLogin;
    private Coroutine attemptToLoginCoroutine;

    [SerializeField] private ScreenPushData homeScreenPushData;

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

    private void OnLoginButtonSelected()
    {
        if (!CanAttemptToLogin)
        {
            errorMessage.EnableMessage(inputFieldsEmptyErrorMessage);
            return;
        }

        if (errorMessage.IsActive)
            errorMessage.Disable();

        SetVisualRegisteringProcess(true);
        StartLoginAttemptCoroutine();
    }


    private void OnRegisterButtonSelected()
    {
        if(InProcessOfAttemptingToLogin)
        {
            StopLoginAttemptCoroutine();
            SetVisualRegisteringProcess(false);
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
        yield return StartCoroutine(APIUtilities.Instance.GetLanguages(LanguageDataHolder.Instance.UpdateLanguagesList));
        yield return StartCoroutine(APIUtilities.Instance.GetUser(loggedInUserID, UserDataHolder.Instance.SetCurrentUser));
        yield return StartCoroutine(APIUtilities.Instance.GetLanguageProfilesOfUser(loggedInUserID, LanguageProfileController.Instance.UpdateLanguageProfilesData));
        yield return StartCoroutine(APIUtilities.Instance.GetSetsOfLanguageProfile(LanguageProfileController.Instance.currentLanguageProfile.ID, SetsDataHolder.Instance.UpdateSetsData));
        yield return StartCoroutine(APIUtilities.Instance.GetFlashcardsOfLanguageProfile(LanguageProfileController.Instance.currentLanguageProfile.ID, FlashcardDataHolder.Instance.UpdateFlashcardList));
        

        SetVisualRegisteringProcess(false);
        BlitzyUI.UIManager.Instance.QueuePop();
        BlitzyUI.UIManager.Instance.QueuePush(homeScreenPushData.ID);
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

    private void StartLoginAttemptCoroutine() => attemptToLoginCoroutine = StartCoroutine(APIUtilities.Instance.Login(usernameInputField.Text, passwordInputField.Text, OnLoginSuccessful, OnLoginUnsuccessful));
    private void StopLoginAttemptCoroutine()
    {
        StopCoroutine(attemptToLoginCoroutine);
        attemptToLoginCoroutine = null;
    }
}
