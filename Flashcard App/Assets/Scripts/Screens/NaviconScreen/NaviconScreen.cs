using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;
using System;

// Navicon screen which appears when you press the navicon button (top left of screen).
// As soon as this screen is pushed or "activated" it moves the navicon onto the screen - see OnPush method below.
// Can only start to hide it when it is not moving and fully on screen.
// Can be hidden by pressing back (Screen Controller deals with pressing back input) or by pressing to the side of the navicon (button in background)

//Notes: 
//Keep this screen cached I think.

public class NaviconScreen : BlitzyUI.Screen
{
    [Header("Target positions for movement")]
    private Vector3 posOffScreen;
    private Vector3 posOnScreen;
    private Vector3 targetPosition;

    [Header("Navicon")]
    [SerializeField] private GameObject naviconObject;
    [SerializeField] private float naviconTimeToMoveToTarget;
    [SerializeField] private LeanTweenType moveTweenType;

    [Header("Background Hide Button")]
    [SerializeField] private Button btnHideNavicon; 
    [SerializeField] private Image btnHideNaviconImage;
    [SerializeField] [Range(0, 1)] private float darkAlphaTarget;
    private float currentAlphaTarget;

    //Only allows screen controller to start to pop this screen when its not moving / tweening.
    public override bool AllowStartPoppingSequence 
    { 
        get => !LeanTween.isTweening(naviconObject); 
        set => base.AllowStartPoppingSequence = value;
    }

    //Start / Setup.
    private void Awake() => btnHideNavicon.onClick.AddListener(OnHideNaviconButtonPressed);
    public override void OnSetup()
    {
        currentAlphaTarget = darkAlphaTarget;
        ReadTargetPositionsForNaviconMovement();
    }
    private void ReadTargetPositionsForNaviconMovement()
    {
        posOffScreen = naviconObject.transform.position;
        posOnScreen = new Vector3(0, posOffScreen.y, 0);
        targetPosition = posOnScreen;
    }

    //Pushing - moving navicon on screen.
    public override void OnPush(ScreenData data) => MoveNaviconOnScreen();
    private void MoveNaviconOnScreen()
    {
        btnHideNavicon.interactable = false;

        LeanTween.move(naviconObject, targetPosition, naviconTimeToMoveToTarget);
        LeanTween.alpha(btnHideNaviconImage.rectTransform, currentAlphaTarget, naviconTimeToMoveToTarget).setOnComplete(() =>
        {
            btnHideNavicon.interactable = true;
            targetPosition = posOffScreen;
            currentAlphaTarget = 0f;
            PushFinished();
        });
    }

    //Popping - moving navicon off screen.
    public override void OnPop() => PopFinished();
    public override void StartPoppingSequence(Action callback = null) => MoveNaviconOffScreen(callback);
    private void OnHideNaviconButtonPressed() => MoveNaviconOffScreen(callback: null);

    private void MoveNaviconOffScreen(Action callback)
    {
        btnHideNavicon.interactable = false;

        LeanTween.move(naviconObject, targetPosition, naviconTimeToMoveToTarget);
        LeanTween.alpha(btnHideNaviconImage.rectTransform, currentAlphaTarget, naviconTimeToMoveToTarget).setOnComplete(() =>
        {
            currentAlphaTarget = darkAlphaTarget;
            targetPosition = posOnScreen;

            UIManager.Instance.QueuePop(null);

            callback?.Invoke();
        });
    }

    public override void OnFocus() => btnHideNavicon.interactable = true;
    public override void OnFocusLost() => btnHideNavicon.interactable = false;
}