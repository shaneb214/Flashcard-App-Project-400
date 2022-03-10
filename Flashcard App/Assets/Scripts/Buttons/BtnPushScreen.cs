using UnityEngine;
using UnityEngine.UI;
using BlitzyUI;
using System;
using UnityEngine.Events;

//Base button class helps to push a screen when button is pressed.

[RequireComponent(typeof(Button))]
public class BtnPushScreen : MonoBehaviour
{
    protected Button button;

    [Header("Pushing new screen")]
    [SerializeField] private ScreenPushData myScreenToPush;
    [Tooltip("If using base onbuttonclick and this is enabled, it will pop the current screen and not call its pop sequence")]
    [SerializeField] protected bool popCurrentScreenAndSkipPopSequence;

    //Start.
    public virtual void Awake() => button = GetComponent<Button>();
    public virtual void Start() => AddListenerToButtonClick(OnButtonClick);

    public virtual void OnButtonClick()
    {
        if (popCurrentScreenAndSkipPopSequence)
            UIManager.Instance.QueuePop();

        PushMyScreen();
    }

    protected void PushMyScreen() => UIManager.Instance.QueuePush(myScreenToPush.ID, null, null);

    public void AddListenerToButtonClick(UnityAction call) => button.onClick.AddListener(call); 
}
