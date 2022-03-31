using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGameManager : MonoBehaviour
{
    public static Action<WallGamePlatformData> PlatformDataDequeudEvent;
    public static WallGameManager Instance;

    public WallGamePlayer player;

    //Settings.
    private const int maxRepeatCardCount = 5;
    private const int realWallsCount = 3;
    public enum PromptSetting { Learning,Native}
    [Header("Settings")]
    public PromptSetting promptSetting;
    [Range(1,maxRepeatCardCount)]
    [SerializeField] private int repeatTimes;
    private Func<Flashcard, string> GetPromptedBasedOnSetting;
    private Func<Flashcard, string> GetAnswerBasedOnSetting;

    [SerializeField] private List<Flashcard> flashcardsToGoThrough = new List<Flashcard>();

    [Header("Prefabs")]
    [SerializeField] private WallPlatform wallPlatformPrefab;

    [SerializeField] private WallPlatform currentWallPlatform;

    private Queue<WallGamePlatformData> gameDataQueue = new Queue<WallGamePlatformData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        FakeWall.PlayerHitMeEvent += OnPlayerHitFakeWall;
    }

    public void OnPlayerHitFakeWall()
    {
        //Not at end of queue.
        if (gameDataQueue.Count > 0)
        {
            Destroy(currentWallPlatform, 2f);

            currentWallPlatform = currentWallPlatform.SpawnNextPlatform(wallPlatformPrefab);
            SetUpCurrentPlatform();
        }
    }

    public void SetPlayerPositionAndRotation(Vector3 position,Quaternion rotation)
    {
        player.transform.position = position;
        player.transform.rotation = rotation;
    }

    private void Start()
    {
        flashcardsToGoThrough.AddRange(new List<Flashcard>()
        {
            new Flashcard("Hello", "привет"),
            new Flashcard("Good day", "добрый день"),
            new Flashcard("Goodbye", "до свидания"),
            new Flashcard("Bye", "пока"),
            new Flashcard("My name is", "меня зовут"),
            new Flashcard("I want", "я хочу"),
            new Flashcard("Could you..", "не могли бы вы.."),
            new Flashcard("Tell me", "скажи мне"),
            new Flashcard("I understand", "я понимаю"),
        });

        GenerateDataForGame();

        SetUpCurrentPlatform();
    }

    private void OnPlayerHitSpawnNextPlatformTrigger()
    {
        //Not at end of queue.
        if(gameDataQueue.Count > 0)
        {
            currentWallPlatform = currentWallPlatform.SpawnNextPlatform(wallPlatformPrefab);
            SetUpCurrentPlatform();
        }
    }

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
        switch (promptSetting)
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

        for (int r = 0; r < repeatTimes; r++)
        {
            flashcardsToGoThrough.Shuffle();

            for (int i = 0; i < flashcardsToGoThrough.Count; i++)
            {
                prompted = GetPromptedBasedOnSetting(flashcardsToGoThrough[i]);
                corrrectAnswer = GetAnswerBasedOnSetting(flashcardsToGoThrough[i]);

                List<Flashcard> wrongAnswersFlashcardList = flashcardsToGoThrough.GetItemsFromListIgnoringIndex(i, realWallsCount);
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

    private string GetNativeSideFromFlashcard(Flashcard flashcard) => flashcard.NativeSide;
    private string GetLearningSideFromFlashcard(Flashcard flashcard) => flashcard.LearningSide;
    private string GetRandomSideFromFlashcard(Flashcard flashcard) 
    {
        System.Random rng = new System.Random();
        int randomNumber = rng.Next(0, 2);
        return randomNumber == 0 ? flashcard.NativeSide : flashcard.LearningSide;
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