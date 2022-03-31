using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallGamePromptDisplay : MonoBehaviour
{
    private TextMeshProUGUI txtPrompt;

    private void Awake()
    {
        txtPrompt = GetComponent<TextMeshProUGUI>();
        WallGameManager.PlatformDataDequeudEvent += OnNewPlatformDataDequeued;
    }

    private void OnNewPlatformDataDequeued(WallGamePlatformData nextPlatformData)
    {
        txtPrompt.text = nextPlatformData.prompted;
    }

    private void OnDestroy()
    {
        WallGameManager.PlatformDataDequeudEvent -= OnNewPlatformDataDequeued;
    }
}
