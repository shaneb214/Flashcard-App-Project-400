
using UnityEngine;

public class FakeWall : Wall
{
    [SerializeField] private GameObject[] destructibleWallPrefabs;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHitMeEvent?.Invoke();

        Destroy(gameObject);
        Instantiate(destructibleWallPrefabs[Random.Range(0, destructibleWallPrefabs.Length)], transform.position, Quaternion.identity);
    }
}