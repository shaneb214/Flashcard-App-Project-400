using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatformTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("PLAYER");
    }
}
