using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameDisplayInfo", menuName = "ScriptableObjects/GameDisplayInfo", order = 1)]
public class GameDisplayInfo : ScriptableObject
{
    public string GameName;
    public string MinimumRequirementsDescription;
    public string GameDescription;
    public Sprite gameSprite;
    public GameSettings gameSettings; 
}
