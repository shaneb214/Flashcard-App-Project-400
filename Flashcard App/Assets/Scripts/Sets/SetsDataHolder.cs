using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsDataHolder : MonoBehaviour
{
    [SerializeField] private List<Set> SetList;

    private void Awake()
    {
        Set.SetCreatedEvent += OnSetCreated;
    }

    private void Start()
    {
        
    }

    private void OnSetCreated(Set setCreated)
    {
        SetList.Add(setCreated);
    }

    private void OnDestroy()
    {
        Set.SetCreatedEvent -= OnSetCreated;
    }
}
