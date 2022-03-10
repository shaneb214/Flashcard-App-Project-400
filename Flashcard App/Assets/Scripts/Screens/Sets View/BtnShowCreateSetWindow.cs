using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnShowCreateSetWindow : MonoBehaviour
{
    private Button btnShowCreateSetWindow;

    private void Awake()
    {
        btnShowCreateSetWindow.onClick.AddListener(OnShowCreateSetWindowButtonPressed); 
    }

    private void OnShowCreateSetWindowButtonPressed()
    {
        //Enable modal window.
    }
}
