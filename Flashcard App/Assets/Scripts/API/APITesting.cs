using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APITesting : MonoBehaviour
{
    void Start()
    {
        APIUtilities.Instance.AttemptGetLanguages();

        //StartCoroutine(Register("shaneb214@gmail.com","shaneb", "password123", "password123", OnRegisterSuccess,OnRegisterFailed));
        //StartCoroutine(LoginPlayer());
    }
}
