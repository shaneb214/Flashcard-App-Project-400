using System;
using UnityEngine;
using UnityEngine.UI;
using BlitzyUI;


//Base button class helps to push a screen when button is pressed.

[RequireComponent(typeof(Button))]
public class BtnPushScreen : MonoBehaviour
{
    protected Button button;

    [Header("Pushing new screen")]
    [SerializeField] protected ScreenPushData myScreenToPush;
    [Tooltip("If using base onbuttonclick and this is enabled, it will pop the current screen and not call its pop sequence")]
    [SerializeField] private bool popAndSkipPopSequence; 

    public virtual void Awake()
    {
        button = GetComponent<Button>();
    }

    public virtual void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public virtual void OnButtonClick()
    {
        if (popAndSkipPopSequence)
            UIManager.Instance.QueuePop();

        UIManager.Instance.QueuePush(myScreenToPush.ID, null, null);
    }
}
