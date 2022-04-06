using BlitzyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnLogout : MonoBehaviour
{
    public static event Action UserLoggedOutEvent;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonSelected);
    }

    private void OnButtonSelected()
    {
        SceneManager.LoadScene("LoginRegisterScene");
        UserLoggedOutEvent?.Invoke();
    }
}
