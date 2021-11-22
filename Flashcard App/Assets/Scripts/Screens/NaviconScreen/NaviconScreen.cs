using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlitzyUI;
using UnityEngine.UI;

//Keep this screen cached I think.
public class NaviconScreen : BlitzyUI.Screen
{
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

    public override bool AllowPopScreenOnPressingBack 
    { 
        get => !LeanTween.isTweening(naviconObject); 
        set => base.AllowPopScreenOnPressingBack = value;
    }

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
    public override void OnBackButtonPressed() => MoveNaviconOffScreen();
    private void OnHideNaviconButtonPressed() => MoveNaviconOffScreen();

    private void MoveNaviconOffScreen()
    {
        btnHideNavicon.interactable = false;

        LeanTween.move(naviconObject, targetPosition, naviconTimeToMoveToTarget);
        LeanTween.alpha(btnHideNaviconImage.rectTransform, currentAlphaTarget, naviconTimeToMoveToTarget).setOnComplete(() =>
        {
            currentAlphaTarget = darkAlphaTarget;
            targetPosition = posOnScreen;

           UIManager.Instance.QueuePop(null);
        });
    }

    public override void OnFocus() { }
    public override void OnFocusLost() { }
    private void OnDestroy() => btnHideNavicon.onClick.RemoveListener(OnHideNaviconButtonPressed);
}