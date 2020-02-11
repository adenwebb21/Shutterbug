using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptidProperties : MonoBehaviour
{
    public Transform[] bodyParts;

    public float perception = 55f;

    public enum cryptidState {MOVING, SEARCHING};

    public cryptidState currentState;

    private void Start()
    {
        currentState = cryptidState.SEARCHING;
    }
}
