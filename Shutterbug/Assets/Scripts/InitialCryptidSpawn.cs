using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCryptidSpawn : MonoBehaviour
{
    Transform _firstSpawn;

    void Start()
    {
        _firstSpawn = transform.GetChild(0);
        GameManager.Instance.currentCryptid = Instantiate(GameManager.Instance.cryptidPrefab, _firstSpawn.position, _firstSpawn.rotation);
    }
}
