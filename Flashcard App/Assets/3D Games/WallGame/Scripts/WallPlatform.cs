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


    [SerializeField] private SpawnNextPlatformTrigger myTrigger;
    [SerializeField] private WallPlatform testPrefab;


    private void Start()
    {
        myTrigger.PlayerHitTriggerEvent += OnPlayerHitLoadNextPlatformTrigger;
    }

    private void OnPlayerHitLoadNextPlatformTrigger()
    {
        Instantiate(testPrefab.gameObject, nextPlatformSpawnPos);
    }

    private void OnDestroy()
    {
        myTrigger.PlayerHitTriggerEvent -= OnPlayerHitLoadNextPlatformTrigger;
    }
}