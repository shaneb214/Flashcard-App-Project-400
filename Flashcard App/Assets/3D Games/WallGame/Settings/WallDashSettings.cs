using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallDashSettings", menuName = "ScriptableObjects/WallDashSettings", order = 1)]
public class WallDashSettings : GameSettings
{
    public const int minRepeatCardCount = 1;
    public const int maxRepeatCardCount = 5;
    public int numWrongAnswersToShow = 3;
    public float TimeToCleanUpSpawnedObjects = 1.5f;

    [Range(minRepeatCardCount,maxRepeatCardCount)]
    public int repeatCardAmount;
    public PromptSetting promptSetting;
}
