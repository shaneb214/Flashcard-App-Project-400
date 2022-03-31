using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatform : MonoBehaviour
{
    [Header("Wall Spawn Positions")]
    [SerializeField] private Transform[] wallSpawnPositions;

    [SerializeField] private Transform nextPlatformSpawnPos;
    [SerializeField] private Transform playerWrongAnswerResetPos;
    [SerializeField] public SpawnNextPlatformTrigger myTrigger;
    [SerializeField] private WallPlatform testPrefab;

    [SerializeField] private FakeWall fakeWallPrefab;
    [SerializeField] private RealWall realWallPrefab;


    private void Start()
    {
        //myTrigger.PlayerHitTriggerEvent += OnPlayerHitLoadNextPlatformTrigger;
        RealWall.PlayerHitMeEvent += OnPlayerHitRealWall;
    }

    private void OnPlayerHitLoadNextPlatformTrigger()
    {
        //Instantiate(testPrefab.gameObject, nextPlatformSpawnPos);
    }

    private void OnPlayerHitRealWall()
    {
        WallGameManager.Instance.SetPlayerPositionAndRotation(playerWrongAnswerResetPos.position, playerWrongAnswerResetPos.transform.rotation);
    }

    private void OnDestroy()
    {
        //myTrigger.PlayerHitTriggerEvent -= OnPlayerHitLoadNextPlatformTrigger;
    }

    public WallPlatform SpawnNextPlatform(WallPlatform wallPlatformPrefab)
    {
        WallPlatform spawnedWallPlatform = Instantiate(wallPlatformPrefab, nextPlatformSpawnPos.position, Quaternion.identity);
        return spawnedWallPlatform;
    }

    public void SpawnWalls(WallGamePlatformData wallGameStruct)
    {
        List<int> wallPositionIndexesList = new List<int>();
        for (int i = 0; i < wallSpawnPositions.Length; i++)
        {
            wallPositionIndexesList.Add(i);
        }

        //Spawn fake wall - correct answer.
        int falseWallPositionIndex = Random.Range(0, wallPositionIndexesList.Count);
        wallPositionIndexesList.RemoveAt(falseWallPositionIndex);

        SpawnWall(fakeWallPrefab, wallSpawnPositions[falseWallPositionIndex],wallGameStruct.correctAnswer);

        for (int i = 0; i < wallGameStruct.wrongAnswers.Count; i++)
        {
            int randomRealWallPositionIndex = Random.Range(0, wallPositionIndexesList.Count);
            SpawnWall(realWallPrefab, wallSpawnPositions[wallPositionIndexesList[randomRealWallPositionIndex]], wallGameStruct.wrongAnswers[i]);
            wallPositionIndexesList.RemoveAt(randomRealWallPositionIndex);
        }
    }

    private void SpawnWall(Wall wall,Transform transform,string text)
    {
        Wall spawnedWall = Instantiate(wall, transform.position,Quaternion.identity);
        spawnedWall.SetText(text);
    }
}