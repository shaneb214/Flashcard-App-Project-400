using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtMessage;

    private Coroutine disableMessageAfterTimeCoroutine;
    
    public bool IsActive { get { return gameObject.activeSelf; } }

    public void EnableMessage(string message)
    {
        gameObject.SetActive(true);
        txtMessage.text = message;
    }
    public void Disable()
    {
        gameObject.SetActive(false);
        txtMessage.text = string.Empty;
    }

    public void EnableMessage(string message,float timeToDisable) 
    {
        if(disableMessageAfterTimeCoroutine == null)
        {
            EnableMessage(message);
            disableMessageAfterTimeCoroutine = StartCoroutine(DisableAfterTime(timeToDisable,Disable));
        }
    }

    private IEnumerator DisableAfterTime(float time,Action callBackAtEnd)
    {
        yield return new WaitForSeconds(time);
        callBackAtEnd();

        disableMessageAfterTimeCoroutine = null;
    }
}
