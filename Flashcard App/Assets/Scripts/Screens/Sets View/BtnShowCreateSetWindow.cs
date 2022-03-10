using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnShowCreateSetWindow : MonoBehaviour
{
    private Button btnShowCreateSetWindow;

    [Header("Prefab To Spawn")]
    [SerializeField] private ModalWindow_CreateSet modalWindow_CreateSet;

    private void Awake() => btnShowCreateSetWindow = GetComponent<Button>();
    private void Start() => btnShowCreateSetWindow.onClick.AddListener(OnShowCreateSetWindowButtonPressed);

    private void OnShowCreateSetWindowButtonPressed()
    {
        //Enable modal window.
        modalWindow_CreateSet.AnimateWindow();
    }
}
