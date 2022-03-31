using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatform : MonoBehaviour
{
    [Header("Wall Spawn Positions")]
    [SerializeField] private Transform[] wallSpawnPositions;

    [SerializeField] private Transform nextPlatformSpawnPos;
    [SerializeField] public Transform endPlatformSpawnPos;
    [SerializeField] private Transform playerWrongAnswerResetPos;

    [Header("Prefabs")]
    [SerializeField] private FakeWall fakeWallPrefab;
    [SerializeField] private RealWall realWallPrefab;
    [SerializeField] private EndPlatform endPlatformPrefab;

    //Start.
    private void Awake() { }
    private void Start() => WallGameManager.Instance.player.PlayerGettingUpAfterFallingEvent += OnPlayerGettingUpAfterFall;

    //Wall Spawning.
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

    //Resetting player position after fall.
    private void OnPlayerGettingUpAfterFall() => WallGameManager.Instance.SetPlayerPositionAndRotation(playerWrongAnswerResetPos.position, playerWrongAnswerResetPos.transform.rotation);
    private void OnDestroy() => WallGameManager.Instance.player.PlayerGettingUpAfterFallingEvent -= OnPlayerGettingUpAfterFall;
}