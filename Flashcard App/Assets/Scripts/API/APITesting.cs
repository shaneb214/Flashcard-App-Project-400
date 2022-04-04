using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APITesting : MonoBehaviour
{
    void Start()
    {

        //StartCoroutine(APIUtilities.Instance.GetLanguageProfilesOfUser("0ae94ef8-ecff-4d6a-a030-f2b573a797fa",null));

        StartCoroutine(APIUtilities.Instance.GetSetsOfLanguageProfile("9b18fbf4-b158-4fa5-a6c7-860163c87863"));
        //StartCoroutine(Register("shaneb214@gmail.com","shaneb", "password123", "password123", OnRegisterSuccess,OnRegisterFailed));
        //StartCoroutine(LoginPlayer());
    }

}
