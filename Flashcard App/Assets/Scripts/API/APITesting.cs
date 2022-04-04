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

        StartCoroutine(APIUtilities.Instance.GetSetsOfLanguageProfile("cc3b0a6b-b418-4c1a-92cd-7b9fec687d51"));


        //StartCoroutine(Register("shaneb214@gmail.com","shaneb", "password123", "password123", OnRegisterSuccess,OnRegisterFailed));
        //StartCoroutine(LoginPlayer());
    }

}
