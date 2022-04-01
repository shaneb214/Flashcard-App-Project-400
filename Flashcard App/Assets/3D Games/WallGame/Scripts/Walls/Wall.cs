using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //Text for card.
    [SerializeField] private TextMeshPro txtWall;

    public void SetText(string text)
    {
        txtWall.text = text;
    }
}