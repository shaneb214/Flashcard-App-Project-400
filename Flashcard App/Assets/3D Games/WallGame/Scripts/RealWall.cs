using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealWall : Wall
{
    public static Action PlayerHitMeEvent;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHitMeEvent?.Invoke();
    }
}
