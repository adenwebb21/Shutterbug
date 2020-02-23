using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitcher : MonoBehaviour
{
    public List<SpawnPoint> spawnLocations;
    private GameObject[] m_spawns;
    private CryptidProperties m_propertyBlock;
    private SpawnPoint m_currentSpawnPoint;

    private int m_currentIndex = 0;

    private void Start()
    {
        // do specialised finding here
        m_propertyBlock = gameObject.GetComponent<CryptidProperties>();

        m_spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach(GameObject _spawn in m_spawns)
        {
            spawnLocations.Add(_spawn.GetComponent<SpawnPoint>());
        }

        Respawn();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            Leave();
        }
    }

    [ContextMenu("Respawn")]
    public void Respawn()
    {
        m_propertyBlock.currentState = CryptidProperties.cryptidState.MOVING;

        // Choose point based on preference
        int _randomIndex = 0; 

        while(_randomIndex == m_currentIndex)
        {
            _randomIndex = Random.Range(0, spawnLocations.Count);       
        }

        m_currentIndex = _randomIndex;

        m_currentSpawnPoint = spawnLocations[m_currentIndex];
        m_currentSpawnPoint.Spawn(gameObject);
        m_propertyBlock.currentState = CryptidProperties.cryptidState.SEARCHING;
    }

    [ContextMenu("Leave")]
    public void Leave()
    {
        m_propertyBlock.currentState = CryptidProperties.cryptidState.MOVING;

        m_currentSpawnPoint.Leave(gameObject);
        Invoke("Respawn", 1.5f);
    }
}
