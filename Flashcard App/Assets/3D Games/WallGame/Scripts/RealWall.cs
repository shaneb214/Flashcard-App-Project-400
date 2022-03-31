using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealWall : Wall
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerHitMeEvent?.Invoke();
    }
}
