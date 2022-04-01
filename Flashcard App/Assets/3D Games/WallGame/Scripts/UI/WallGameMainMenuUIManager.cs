using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallGameMainMenuUIManager : MonoBehaviour
{
    private CanvasGroup myCanvasGroup;

    [SerializeField] private Transform menuTargetTransform;
    [SerializeField] private Transform menuOffScreenTransform;

    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject settingsMenuObject;

    [Header("Menu Movement")]
    [SerializeField] private float menuCompleteMoveTime;
    [SerializeField] private LeanTweenType menuMoveType;

    [SerializeField] private GameObject mainMenuCharacter;

    private void Awake()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        WallGameManager.GameEndedEvent += OnWallGameEnded;
    }

    private void OnWallGameEnded()
    {
        EnableMainMenu();
    }

    public void OnSettingsButtonSelected()
    {
        LeanTween.move(mainMenuObject, menuOffScreenTransform.position, menuCompleteMoveTime).setEase(menuMoveType)
            .setOnComplete(() => LeanTween.move(settingsMenuObject,menuTargetTransform.position,menuCompleteMoveTime).setEase(menuMoveType));
    }

    public void OnPlayButtonSelected() 
    {
        WallGameManager.Instance.StartGame();
        DisableMainMenu();
    }
    public void OnExitButtonSelected() 
    {
        SceneManager.LoadScene(1);
    }

    public void OnBackToMainMenuButtonSelected()
    {
        LeanTween.move(settingsMenuObject, menuOffScreenTransform.position, menuCompleteMoveTime).setEase(menuMoveType)
            .setOnComplete(() => LeanTween.move(mainMenuObject, menuTargetTransform.position, menuCompleteMoveTime).setEase(menuMoveType));
    }

    private void DisableMainMenu()
    {
        mainMenuCharacter.SetActive(false);

        myCanvasGroup.alpha = 0;
        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;
    }

    public void EnableMainMenu()
    {
        mainMenuCharacter.SetActive(true);

        myCanvasGroup.alpha = 1;
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
    }

    private void OnDestroy()
    {
        WallGameManager.GameEndedEvent -= OnWallGameEnded;
    }
}