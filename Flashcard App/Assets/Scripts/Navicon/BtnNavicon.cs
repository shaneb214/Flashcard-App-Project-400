using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitzyUI;

//Not used by navicon button prefab - can probably delete this unless I plan to do something extra with the button.
//Navicon button prefab just uses BtnPushScreen as its script.

public class BtnNavicon : BtnPushScreen
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void Start()
    {
        base.Start();
    }
}
