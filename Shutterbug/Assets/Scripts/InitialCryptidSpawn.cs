using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCryptidSpawn : MonoBehaviour
{
    public Transform initialSpawn;

    void Start()
    {
        GameManager.Instance.currentCryptid = Instantiate(GameManager.Instance.cryptidPrefab, initialSpawn.position, initialSpawn.rotation);
    }
}
