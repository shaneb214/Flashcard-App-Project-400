using BlitzyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Button at top left of screen which will return to the previous set view by calling a method from librarysetviewcontroller.
//If the set currently showing doesn't have a parent set, the home library screen is shown instead.
//Parent set id's are stored in a linked list. 

public class BtnViewPreviousSet : MonoBehaviour
{
    [SerializeField] private LibrarySetViewController librarySetViewController;
    [SerializeField] private ScreenPushData libraryHomeScreenPushData;

    private Button btnViewPreviousSet;

    private LinkedList<string> SetHistoryLinkedList = new LinkedList<string>();
    private LinkedListNode<string> SetHistoryLinkedListNode = new LinkedListNode<string>(string.Empty);

    private void Awake()
    {
        btnViewPreviousSet = GetComponent<Button>();
        btnViewPreviousSet.onClick.AddListener(OnViewPreviousSetButtonPress);
    }

    private void OnSetDisplayPressed(string setIDPressed)
    {
        string parentSetID = SetsDataHolder.Instance.FindSetByID(setIDPressed).ParentSetID;

        SetHistoryLinkedList.AddLast(parentSetID);
        SetHistoryLinkedListNode = SetHistoryLinkedList.Last;
    }

    private void OnViewPreviousSetButtonPress()
    {
        if(SetHistoryLinkedListNode == null || SetHistoryLinkedListNode.Value == string.Empty)
        {
            UIManager.Instance.QueuePop();
            UIManager.Instance.QueuePush(libraryHomeScreenPushData.ID, null, null);
        }
        else
        {
            librarySetViewController.DisplaySetContents(SetHistoryLinkedListNode.Value);
            SetHistoryLinkedListNode = SetHistoryLinkedListNode.Previous;
            SetHistoryLinkedList.RemoveLast();
        }
    }

    private void OnEnable()
    {
        SetDisplay.SetDisplayPressed += OnSetDisplayPressed;
    }

    private void OnDisable()
    {
        SetHistoryLinkedList.Clear();
        SetDisplay.SetDisplayPressed -= OnSetDisplayPressed;
    }
}
