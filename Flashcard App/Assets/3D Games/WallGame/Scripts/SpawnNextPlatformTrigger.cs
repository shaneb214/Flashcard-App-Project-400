using System;
using UnityEngine;

public class SpawnNextPlatformTrigger : MonoBehaviour
{
    public Action PlayerHitTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        //Don't need to check if its the player as it can be the only thing that can trigger this.
        PlayerHitTriggerEvent?.Invoke();
    }
}
