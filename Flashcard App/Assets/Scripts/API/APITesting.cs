using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APITesting : MonoBehaviour
{
    void Start()
    {
        string userID = "0ae94ef8-ecff-4d6a-a030-f2b573a797fa";
        string languageProfileID = "cc3b0a6b-b418-4c1a-92cd-7b9fec687d51";
        string animalsSetID = "9187b6b3-2602-4267-b7c4-1532a934aa93";

        /////POSTS.
        //Works.
        //StartCoroutine(APIUtilities.Instance.PostNewLanguageProfile(userID, new LanguageProfile(new Language("en", "English"), new Language("it", "Italian"), false)));

        //Works.
        //StartCoroutine(APIUtilities.Instance.PostNewSet(new Set("Birds", "9187b6b3-2602-4267-b7c4-1532a934aa93", false, languageProfileID)));

        StartCoroutine(APIUtilities.Instance.PostNewFlashcard(new Flashcard("Wolf", "волк", string.Empty, animalsSetID)));




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
