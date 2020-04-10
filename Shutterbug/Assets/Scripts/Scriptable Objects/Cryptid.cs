using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Region {Rocks, Shrine, Lake, Hut }

[CreateAssetMenu]
public class Cryptid : ScriptableObject
{
    [SerializeField] private float m_currentPerception;
    [SerializeField] private float m_defaultPerception;

    [SerializeField] private int m_maxFleeCount;
    [SerializeField] private bool m_knownFleeCount = false;
    [SerializeField] private bool m_knownFleeCountThisRound = false;

    [SerializeField] private List<Region> m_preferredRegions;
    [SerializeField] private bool[] m_knownPreferredRegions;
    [SerializeField] private bool[] m_knownPreferredRegionsThisRound;

    [SerializeField] private List<Photograph> evidencePhotos;

    public float CurrentPerception { get => m_currentPerception; set => m_currentPerception = value; }
    public float DefaultPerception { get => m_defaultPerception; set => m_defaultPerception = value; }
    public int MaxFleeCount { get => m_maxFleeCount; set => m_maxFleeCount = value; }
    public bool KnownFleeCount { get => m_knownFleeCount; set => m_knownFleeCount = value; }
    public bool KnownFleeCountThisRound { get => m_knownFleeCountThisRound; set => m_knownFleeCountThisRound = value; }
    public List<Region> PreferredRegions { get => m_preferredRegions; set => m_preferredRegions = value; }
    public bool[] KnownPreferredRegions { get => m_knownPreferredRegions; set => m_knownPreferredRegions = value; }
    public bool[] KnownPreferredRegionsThisRound { get => m_knownPreferredRegionsThisRound; set => m_knownPreferredRegionsThisRound = value; }
    public List<Photograph> EvidencePhotos { get => evidencePhotos; set => evidencePhotos = value; }
}
