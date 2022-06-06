using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGameGameplayUIManager : MonoBehaviour
{
    private CanvasGroup gameplayCanvasGroup;

    private void Awake() => gameplayCanvasGroup = GetComponent<CanvasGroup>();
    private void Start()
    {
        WallGameManager.GameStartedEvent += OnWallGameStarted;
        WallGameManager.GameEndedEvent += OnWallGameEnded;
    }

    private void OnWallGameStarted()
    {
        gameplayCanvasGroup.alpha = 1;
        gameplayCanvasGroup.interactable = true;
    }

    private void OnWallGameEnded()
    {
        gameplayCanvasGroup.alpha = 0;
        gameplayCanvasGroup.interactable = false;
    }

    private void OnDestroy()
    {
        WallGameManager.GameStartedEvent -= OnWallGameStarted;
        WallGameManager.GameEndedEvent -= OnWallGameEnded;
    }
}
