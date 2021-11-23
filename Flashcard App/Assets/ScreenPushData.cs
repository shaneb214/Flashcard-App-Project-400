using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Way of storing screen ID's as scriptable objects.
//If some script / object is going to push a screen it can get a reference to the scriptable object and get the screen's ID and prefab name from this.

[CreateAssetMenu(fileName = "Screen_ScriptableObject", menuName = "ScriptableObjects/ScreenPushData", order = 2)]
public class ScreenPushData : ScriptableObject
{
    public BlitzyUI.Screen.ScreenID ID;
}
