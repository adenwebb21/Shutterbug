using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptidProperties : MonoBehaviour
{
    public Transform[] bodyParts;

    public Cryptid stats;

    public enum cryptidState {MOVING, SEARCHING};

    public cryptidState currentState;

    public List<string> relevantIdentifiers;
    public List<GameObject> relevantProof;

    private void Start()
    {
        currentState = cryptidState.SEARCHING;

        GameObject[] _tempEvidence = GameObject.FindGameObjectsWithTag("Proof");

        for (int i = 0; i < _tempEvidence.Length; i++)
        {
            if(relevantIdentifiers.Contains(_tempEvidence[i].GetComponent<ProofData>().identifier))
            {
                relevantProof.Add(_tempEvidence[i]);
            }
        }

    }
}
