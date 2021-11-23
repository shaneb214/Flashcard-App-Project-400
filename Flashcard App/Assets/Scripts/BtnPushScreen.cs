using System;
using UnityEngine;
using UnityEngine.UI;
using BlitzyUI;


//Base button class that will push a screen when pressed.

[RequireComponent(typeof(Button))]
public class BtnPushScreen : MonoBehaviour
{
    protected Button button;

    [Header("Pushing new screen")]
    [SerializeField] protected ScreenPushData myScreenToPush;
    [SerializeField] private bool popCurrentScreen;

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
        if (popCurrentScreen)
            UIManager.Instance.QueuePop();

        UIManager.Instance.QueuePush(myScreenToPush.ID, null, null);
    }

    public virtual void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}
