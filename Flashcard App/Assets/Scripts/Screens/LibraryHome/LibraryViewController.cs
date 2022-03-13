using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibraryViewController : MonoBehaviour
{
    public static string SetIDCurrentlyShowing;

    [SerializeField] protected Transform scrollViewContentTransform;
    [SerializeField] protected SetDisplay setDisplayPrefab;
    [SerializeField] protected TextMeshProUGUI txtNoSetsWarning;

    public enum DisplayScreen { None, Home, SetView }
    public static DisplayScreen CurrentDisplayScreen;

    public bool ScrollViewContainsItems { get { return scrollViewContentTransform.childCount > 0; } }

    protected void DestroyItemsInScrollView()
    {
        for (int i = 0; i < scrollViewContentTransform.childCount; i++)
        {
            Transform child = scrollViewContentTransform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
}
