using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BookData : ScriptableObject
{
    [Header("Discoverable Stats")]
    [SerializeField] private bool m_fleeCount;

    public bool FleeCount { get => m_fleeCount; set => m_fleeCount = value; }
}

