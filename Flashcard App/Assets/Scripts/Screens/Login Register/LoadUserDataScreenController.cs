using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUserDataScreenController : MonoBehaviour
{
    [SerializeField] private ScreenPushData homeScreen;

    private IEnumerator Start()
    {
        string loggedInUserID = UserDataHolder.Instance.CurrentUser.ID;

        //Load player data.
        yield return StartCoroutine(APIUtilities.Instance.GetLanguageProfilesOfUser(loggedInUserID, LanguageProfileController.Instance.UpdateLanguageProfilesData));

        yield return null;

        BlitzyUI.UIManager.Instance.QueuePop();
        BlitzyUI.UIManager.Instance.QueuePush(homeScreen.ID);

    }
}
