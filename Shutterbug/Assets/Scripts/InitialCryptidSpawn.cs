using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCryptidSpawn : MonoBehaviour
{
    public Transform _firstSpawn;

    void Start()
    {
        GameManager.Instance.currentCryptid = Instantiate(GameManager.Instance.cryptidPrefab, _firstSpawn.position, _firstSpawn.rotation);
        GameManager.Instance.ResetPhotos();
    }
}
