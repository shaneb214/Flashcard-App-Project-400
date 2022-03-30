using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatform : MonoBehaviour
{
    [Header("Wall Spawn Positions")]
    [SerializeField] private Transform wallPos1;
    [SerializeField] private Transform wallPos2;
    [SerializeField] private Transform wallPos3;
    [SerializeField] private Transform wallPos4;

    [SerializeField] private Transform nextPlatformSpawnPos;
}
