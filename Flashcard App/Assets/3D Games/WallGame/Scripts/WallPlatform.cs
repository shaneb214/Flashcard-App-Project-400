using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatform : MonoBehaviour
{
    [Header("Wall Spawn Positions")]
    [SerializeField] private Transform[] wallSpawnPositions;

    [SerializeField] private Transform nextPlatformSpawnPos;
    [SerializeField] private SpawnNextPlatformTrigger myTrigger;
    [SerializeField] private WallPlatform testPrefab;

    [SerializeField] private FakeWall fakeWallPrefab;
    [SerializeField] private RealWall realWallPrefab;


    private void Start()
    {
        myTrigger.PlayerHitTriggerEvent += OnPlayerHitLoadNextPlatformTrigger;
    }

    private void OnPlayerHitLoadNextPlatformTrigger()
    {
        //Instantiate(testPrefab.gameObject, nextPlatformSpawnPos);
    }

    private void OnDestroy()
    {
        myTrigger.PlayerHitTriggerEvent -= OnPlayerHitLoadNextPlatformTrigger;
    }

    public void SpawnWalls(string correctFlashcardText,string[] incorrectFlashcardText)
    {
        List<int> wallPositionIndexesList = new List<int>();
        for (int i = 0; i < wallSpawnPositions.Length; i++)
        {
            wallPositionIndexesList.Add(i);
        }

        //Spawn fake wall - correct answer.
        int falseWallPositionIndex = Random.Range(0, wallPositionIndexesList.Count);
        wallPositionIndexesList.RemoveAt(falseWallPositionIndex);

        SpawnWall(fakeWallPrefab, wallSpawnPositions[falseWallPositionIndex],correctFlashcardText);

        for (int i = 0; i < incorrectFlashcardText.Length; i++)
        {
            int randomRealWallPositionIndex = Random.Range(0, wallPositionIndexesList.Count);
            SpawnWall(realWallPrefab, wallSpawnPositions[wallPositionIndexesList[randomRealWallPositionIndex]], incorrectFlashcardText[i]);
            wallPositionIndexesList.RemoveAt(randomRealWallPositionIndex);
        }
    }

    private void SpawnWall(Wall wall,Transform transform,string text)
    {
        Wall spawnedWall = Instantiate(wall, transform.position,Quaternion.identity);
        spawnedWall.SetText(text);
    }
}