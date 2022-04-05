using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APITesting : MonoBehaviour
{
    IEnumerator Start()
    {
        string userID = "0ae94ef8-ecff-4d6a-a030-f2b573a797fa";
        string RussianLanguageProfileID = "cc3b0a6b-b418-4c1a-92cd-7b9fec687d51";
        string ItalianLanguageProfileID = "c061745a-e3d1-4a46-b0f0-93c24fd2f815";
        string animalsSetID = "9187b6b3-2602-4267-b7c4-1532a934aa93";

        yield return null;

        //PUT / MODIFY.
        //yield return StartCoroutine(APIUtilities.Instance.GetLanguageProfilesOfUser(userID,LanguageProfileController.Instance.UpdateLanguageProfilesData));
        //List<LanguageProfile> languageProfiles = LanguageProfileController.Instance.GetUserLanguageProfiles();
        //LanguageProfile russianProfile = languageProfiles[1];
        //russianProfile.IsCurrentProfile = true;
        //LanguageProfile italianProfile = languageProfiles[0];


        yield return StartCoroutine(APIUtilities.Instance.GetSetsOfLanguageProfile(RussianLanguageProfileID,SetsDataHolder.Instance.UpdateSetsData));
        List<Set> sets = SetsDataHolder.Instance.SetList;
        Set animalsSet = sets.Find(set => set.ID == animalsSetID);
        animalsSet.IsDefaultSet = false;

        yield return StartCoroutine(APIUtilities.Instance.PutSet(animalsSetID, animalsSet,null,null));


        //yield return StartCoroutine(APIUtilities.Instance.PutLanguageProfile(russianProfile.ID, russianProfile));


        /////POSTS.
        //Works.
        //StartCoroutine(APIUtilities.Instance.PostNewLanguageProfile(userID, new LanguageProfile(new Language("en", "English"), new Language("it", "Italian"), false)));

        //Works.
        //StartCoroutine(APIUtilities.Instance.PostNewSet(new Set("Birds", "9187b6b3-2602-4267-b7c4-1532a934aa93", false, languageProfileID)));

        //StartCoroutine(APIUtilities.Instance.PostNewFlashcard(new Flashcard("Wolf", "волк", string.Empty, animalsSetID)));

        //StartCoroutine(APIUtilities.Instance.GetUser(userID,null));


        /////GETS.
        //Works.
        //StartCoroutine(APIUtilities.Instance.GetLanguageProfilesOfUser("0ae94ef8-ecff-4d6a-a030-f2b573a797fa",null));

        //Works.
        //StartCoroutine(APIUtilities.Instance.GetSetsOfLanguageProfile("cc3b0a6b-b418-4c1a-92cd-7b9fec687d51"));

        //Works.
        //StartCoroutine(APIUtilities.Instance.GetFlashcardsOfLanguageProfile("cc3b0a6b-b418-4c1a-92cd-7b9fec687d51"));

        //StartCoroutine(Register("shaneb214@gmail.com","shaneb", "password123", "password123", OnRegisterSuccess,OnRegisterFailed));
        //StartCoroutine(LoginPlayer());
    }

}
