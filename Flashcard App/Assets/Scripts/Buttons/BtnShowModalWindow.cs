using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnShowModalWindow : MonoBehaviour
{
    private Button btnShowCreateSetWindow;

    [Header("Modal Window To Show")]
    [SerializeField] private CustomModalWindow modalWindowToShow;

    public virtual void Awake() => btnShowCreateSetWindow = GetComponent<Button>();
    public virtual void Start() => btnShowCreateSetWindow.onClick.AddListener(OnShowCreateSetWindowButtonPressed);

    public virtual void OnShowCreateSetWindowButtonPressed()
    {
        //Enable modal window.
        modalWindowToShow.AnimateWindow();
    }
}
