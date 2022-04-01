using System;
using UnityEngine;

public class FakeWall : Wall
{
    [SerializeField] private GameObject[] destructibleWallPrefabs;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(Instantiate(destructibleWallPrefabs[UnityEngine.Random.Range(0, destructibleWallPrefabs.Length)], transform.position, Quaternion.identity),WallGameSettingss.TimeToCleanUpSpawnedObjects);
    }
}