using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitcher : MonoBehaviour
{
    public List<SpawnPoint> spawnLocations;
    private GameObject[] m_spawns;

    private SpawnPoint m_currentSpawnPoint;

    private void Start()
    {
        // do specialised finding here

        m_spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach(GameObject _spawn in m_spawns)
        {
            spawnLocations.Add(_spawn.GetComponent<SpawnPoint>());
        }
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
        // Choose point based on preference
        int _randomIndex = Random.Range(0, spawnLocations.Count);
        m_currentSpawnPoint = spawnLocations[_randomIndex];

        m_currentSpawnPoint.Spawn(gameObject);
    }

    [ContextMenu("Leave")]
    public void Leave()
    {
        m_currentSpawnPoint.Leave(gameObject);
    }
}
