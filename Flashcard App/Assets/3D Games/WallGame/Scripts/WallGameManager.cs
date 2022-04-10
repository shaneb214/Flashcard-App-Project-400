using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PromptSetting { Learning, Native }

public class WallGameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static WallGameManager Instance;
    [Header("Events")]
    public static Action<WallGamePlatformData> PlatformDataDequeudEvent;
    public static Action GameStartedEvent;
    public static Action GameEndedEvent;
    [Header("Player")]
    [SerializeField] private WallGamePlayer playerPrefab;
    public WallGamePlayer spawnedPlayer;
    [Header("Delegates for Data Retrieval based on settings")]
    private Func<Flashcard, string> GetPromptedBasedOnSetting;
    private Func<Flashcard, string> GetAnswerBasedOnSetting;
    [Header("Flashcard game data")]
    [SerializeField] private List<Flashcard> flashcardsToGoThrough = new List<Flashcard>();
    private Queue<WallGamePlatformData> gameDataQueue = new Queue<WallGamePlatformData>();
    [Header("Prefabs")]
    [SerializeField] private WallPlatform wallPlatformPrefab;
    [SerializeField] private EndPlatform endPlatformPrefab;
    [Header("Spawned Platforms")]
    private WallPlatform currentWallPlatform;
    private EndPlatform spawnedEndPlatform;
    [Header("Settings")]
    [SerializeField] private WallGameSettings wallGameSettings;

    private WallGameFlashcardDataSlinger wallGameFlashcardDataSlinger;

    //Start.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;

        wallGameFlashcardDataSlinger = FindObjectOfType<WallGameFlashcardDataSlinger>();
        flashcardsToGoThrough.AddRange(wallGameFlashcardDataSlinger.flashcardData);

        //flashcardsToGoThrough.AddRange(new List<Flashcard>()
        //{
        //    new Flashcard("Hello", "привет"),
        //    new Flashcard("Good day", "добрый день"),
        //    new Flashcard("Goodbye", "до свидания"),
        //    new Flashcard("Bye", "пока"),
        //    new Flashcard("My name is", "меня зовут"),
        //    //new Flashcard("I want", "я хочу"),
        //    //new Flashcard("Could you..", "не могли бы вы.."),
        //    //new Flashcard("Tell me", "скажи мне"),
        //    //new Flashcard("I understand", "я понимаю"),
        //});
    }

    public void StartGame()
    {
        GameStartedEvent?.Invoke();

        GenerateDataForGame();

        currentWallPlatform = Instantiate(wallPlatformPrefab, Vector3.zero,Quaternion.identity);
        spawnedPlayer = Instantiate(playerPrefab, currentWallPlatform.playerSpawnPos.position, Quaternion.identity);
        spawnedPlayer.HitFakeWallEvent += OnPlayerHitFakeWall;
        SetUpCurrentPlatform();
    }

    public void OnPlayerHitFakeWall()
    {
        //Not at end of queue.
        if (gameDataQueue.Count > 0)
        {
            Destroy(currentWallPlatform.gameObject, wallGameSettings.TimeToCleanUpSpawnedObjects);

            currentWallPlatform = currentWallPlatform.SpawnNextPlatform(wallPlatformPrefab);
            SetUpCurrentPlatform();
        }
        else
        {
            Destroy(currentWallPlatform.gameObject, wallGameSettings.TimeToCleanUpSpawnedObjects);

            spawnedEndPlatform = SpawnEndPlatform();
            spawnedEndPlatform.endPlatformTrigger.PlayerHitTrigger += OnPlayerHitEndGameTrigger;
        }
    }

    private void OnPlayerHitEndGameTrigger()
    {
        gameDataQueue.Clear();
        GameEndedEvent?.Invoke();
        Destroy(spawnedEndPlatform.gameObject);
        Destroy(spawnedPlayer.gameObject);
    }

    public void SetPlayerPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        spawnedPlayer.transform.position = position;
        spawnedPlayer.transform.rotation = rotation;
    }

    private EndPlatform SpawnEndPlatform() => Instantiate(endPlatformPrefab, currentWallPlatform.endPlatformSpawnPos.position, Quaternion.identity);

    private void SetUpCurrentPlatform()
    {
        WallGamePlatformData currentPlatformData = gameDataQueue.Dequeue();
        PlatformDataDequeudEvent?.Invoke(currentPlatformData);
        print(currentPlatformData.prompted);
        print(gameDataQueue.Count);

        currentWallPlatform.SpawnWalls(currentPlatformData);
    }

    private void GenerateDataForGame()
    {
        switch (wallGameSettings.promptSetting)
        {
            case PromptSetting.Learning:
                GetPromptedBasedOnSetting = GetLearningSideFromFlashcard;
                GetAnswerBasedOnSetting = GetNativeSideFromFlashcard;
                break;
            case PromptSetting.Native:
                GetPromptedBasedOnSetting = GetNativeSideFromFlashcard;
                GetAnswerBasedOnSetting = GetLearningSideFromFlashcard;
                break;
        }

        string prompted,corrrectAnswer;

        for (int r = 0; r < wallGameSettings.repeatCardAmount; r++)
        {
            flashcardsToGoThrough.Shuffle();

            for (int i = 0; i < flashcardsToGoThrough.Count; i++)
            {
                prompted = GetPromptedBasedOnSetting(flashcardsToGoThrough[i]);
                corrrectAnswer = GetAnswerBasedOnSetting(flashcardsToGoThrough[i]);

                List<Flashcard> wrongAnswersFlashcardList = flashcardsToGoThrough.GetItemsFromListIgnoringIndex(i, wallGameSettings.numWrongAnswersToShow);
                List<string> wrongAnswersListBasedOnSetting = new List<string>();
                for (int j = 0; j < wrongAnswersFlashcardList.Count; j++)
                {
                    wrongAnswersListBasedOnSetting.Add(GetAnswerBasedOnSetting(wrongAnswersFlashcardList[j]));
                }

                WallGamePlatformData wallGameStruct = new WallGamePlatformData(prompted, corrrectAnswer, wrongAnswersListBasedOnSetting);
                gameDataQueue.Enqueue(wallGameStruct);
            }
        }        
    }
    private string GetNativeSideFromFlashcard(Flashcard flashcard) => flashcard.nativeSide;
    private string GetLearningSideFromFlashcard(Flashcard flashcard) => flashcard.learningSide;
    private string GetRandomSideFromFlashcard(Flashcard flashcard) 
    {
        System.Random rng = new System.Random();
        int randomNumber = rng.Next(0, 2);
        return randomNumber == 0 ? flashcard.nativeSide : flashcard.learningSide;
    }

    private void OnDestroy()
    {
        wallGameFlashcardDataSlinger.Destroy();
    }
}

public struct WallGamePlatformData
{
    public string prompted;
    public string correctAnswer;
    public List<string> wrongAnswers;

    public WallGamePlatformData(string prompted, string correctAnswer, List<string> wrongAnswers)
    {
        this.prompted = prompted;
        this.correctAnswer = correctAnswer;
        this.wrongAnswers = wrongAnswers;
    }
}