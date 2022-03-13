using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsDataHolder : MonoBehaviour
{
    public static SetsDataHolder Instance;
    [SerializeField] private List<Set> SetList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Set.SetCreatedEvent += OnSetCreated;
    }
    private void Start() { }

    private void OnSetCreated(Set setCreated)
    {
        SetList.Add(setCreated);
    }

    public List<Set> FindSetsByLangProfileID(string langProfileID) => SetList.FindAll(s => s.LanguageProfileID == langProfileID);
    public List<Set> FindSetsByParentID(string setParentID) => SetList.FindAll(s => s.ParentSetID == setParentID);
    public Set FindSetByID(string ID) => SetList.Find(s => s.ID == ID);
    


    private void OnDestroy()
    {
        Set.SetCreatedEvent -= OnSetCreated;
    }
}
