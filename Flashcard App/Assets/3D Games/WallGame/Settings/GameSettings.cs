using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Range(0,20)]
    public int MinimumCardRequirementToPlay;
}
