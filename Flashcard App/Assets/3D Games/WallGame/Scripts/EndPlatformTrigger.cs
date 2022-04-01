using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatformTrigger : MonoBehaviour
{
    public Action PlayerHitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHitTrigger?.Invoke();
        print("PLAYER");
    }
}
