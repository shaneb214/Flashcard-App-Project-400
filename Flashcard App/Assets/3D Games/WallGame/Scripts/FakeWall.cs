using System;
using UnityEngine;

public class FakeWall : Wall
{
    public static Action PlayerHitMeEvent;

    [SerializeField] private GameObject[] destructibleWallPrefabs;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHitMeEvent?.Invoke();

        Destroy(gameObject);
        Instantiate(destructibleWallPrefabs[UnityEngine.Random.Range(0, destructibleWallPrefabs.Length)], transform.position, Quaternion.identity);
    }
}