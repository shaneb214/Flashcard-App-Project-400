using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;

//Keep this screen cached I think.
public class NaviconScreen : BlitzyUI.Screen
{
    private enum NaviconState { OnScreen, OffScreen }
    private NaviconState currentState;

    private Vector3 posOffScreen;
    private Vector3 posOnScreen;
    private Vector3 targetPosition;

    [Header("Navicon")]
    [SerializeField] private GameObject naviconObject;
    [SerializeField] private float naviconTimeToMoveToTarget;
    [SerializeField] private LeanTweenType moveTweenType;

    [Header("Hide Background Button")]
    [SerializeField] private Button btnHideNavicon; 
    [SerializeField] private Image btnHideNaviconImage;
    [SerializeField] [Range(0, 1)] private float darkAlphaTarget;
    private float currentAlphaTarget;


    private void Awake() => btnHideNavicon.onClick.AddListener(OnHideNaviconButtonPressed);
    private void Start() { }

    public override void OnSetup()
    {
        currentState = NaviconState.OffScreen;
        currentAlphaTarget = darkAlphaTarget;
        ReadTargetPositionsForNaviconMovement();
    }

    public override void OnPop()
    {
        if(currentState == NaviconState.OnScreen) //If back button pressed.
        {
            //Start to move off screen.
            LeanTween.move(naviconObject, targetPosition, naviconTimeToMoveToTarget);
            LeanTween.alpha(btnHideNaviconImage.rectTransform, currentAlphaTarget, naviconTimeToMoveToTarget).setOnComplete(() =>
            {
                currentState = NaviconState.OffScreen;
                currentAlphaTarget = darkAlphaTarget;
                targetPosition = posOnScreen;

                PopFinished();
            });

        }
        else //Off screen.. end pop / delete screen.
        {
            PopFinished();
        }
    }

    public override void OnPush(ScreenData data)
    {
        MoveNaviconOnScreen();
    }

    private void ReadTargetPositionsForNaviconMovement()
    {
        posOffScreen = naviconObject.transform.position;
        posOnScreen = new Vector3(0, posOffScreen.y, 0);
        targetPosition = posOnScreen;
    }

    private void OnHideNaviconButtonPressed() //Can only be pressed when navicon is on screen fully.
    {
        LeanTween.move(naviconObject, targetPosition, naviconTimeToMoveToTarget);
        LeanTween.alpha(btnHideNaviconImage.rectTransform, currentAlphaTarget, naviconTimeToMoveToTarget).setOnComplete(() =>
        {
            currentState = NaviconState.OffScreen;
            currentAlphaTarget = darkAlphaTarget;
            targetPosition = posOnScreen;

            UIManager.Instance.QueuePop(null);
        });
    }

    private void OnDestroy()
    {
        btnHideNavicon.onClick.RemoveListener(OnHideNaviconButtonPressed);
    }
    public override void OnFocus() { }
    public override void OnFocusLost() { }

    private void MoveNaviconOnScreen()
    {
        btnHideNavicon.interactable = false;

        LeanTween.move(naviconObject, targetPosition, naviconTimeToMoveToTarget);
        LeanTween.alpha(btnHideNaviconImage.rectTransform, currentAlphaTarget, naviconTimeToMoveToTarget).setOnComplete(() =>
        {
            currentState = NaviconState.OnScreen;

            btnHideNavicon.interactable = true;
            targetPosition = posOffScreen;
            currentAlphaTarget = 0f;
            PushFinished();
        });
    }
}