using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script which data holders listen to 
public class APIDataSlinger : MonoBehaviour
{
    public Action DataTransferredFromAPIEvent;

    IEnumerator Start()
    {
        yield return APIUtilities.Instance.GetLanguages(LanguageDataHolder.Instance.UpdateLanguagesList);
    }

    public void SlingDataToDataHolders()
    { 
    
    
    
    
    }
}