using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewItemManager : MonoBehaviour
{
    [Header("Scroll View Components")]
    [SerializeField] protected Transform scrollViewContentTransform;
    [SerializeField] private ScrollRect scrollRect;

    protected int ScrollViewItemCount { get { return scrollViewContentTransform.childCount; } } 
    protected bool ScrollViewContainsItems { get { return scrollViewContentTransform.childCount > 0; } }

    protected T SpawnItemInScrollView<T>(T item) where T : MonoBehaviour  
    {
        return Instantiate(item, scrollViewContentTransform);
    }

    protected void ClearScrollViewItems()
    {
        for (int i = 0; i < scrollViewContentTransform.childCount; i++)
        {
            Destroy(scrollViewContentTransform.GetChild(i).gameObject);
        }
    }

    protected void SetScrollRectPosition(float scrollRectPos) => scrollRect.verticalNormalizedPosition = scrollRectPos;
}