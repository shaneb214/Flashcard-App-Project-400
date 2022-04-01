using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGameGameplayUIManager : MonoBehaviour
{
    private CanvasGroup gameplayCanvasGroup;

    private void Awake() => gameplayCanvasGroup = GetComponent<CanvasGroup>();
    private void Start() => WallGameManager.GameStartedEvent += OnWallGameStarted;

    private void OnWallGameStarted()
    {
        gameplayCanvasGroup.alpha = 1;
        gameplayCanvasGroup.enabled = true;
    }

    private void OnDestroy()
    {
        WallGameManager.GameStartedEvent -= OnWallGameStarted;
    }
}
