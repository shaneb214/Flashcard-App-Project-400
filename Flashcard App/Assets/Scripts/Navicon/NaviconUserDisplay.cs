using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaviconUserDisplay : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtUserGreeting;

    private void OnEnable()
    {
        txtUserGreeting.text = $"Hello \n {UserDataHolder.Instance.CurrentUser.Username}";
    }

}
