using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGameFlashcardDataSlinger : MonoBehaviour
{
    public List<Flashcard> flashcardData = new List<Flashcard>();

    private void Awake() => DontDestroyOnLoad(this);
    public void RecieveFlashcardData(List<Flashcard> flashcardList) => flashcardData = flashcardList;
    public void Destroy()
    {
        if(gameObject)
            Destroy(gameObject);
    }
}
