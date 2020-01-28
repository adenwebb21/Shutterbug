using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitcher : MonoBehaviour
{
    public SpawnPoint[] spawnLocations;

    private SpawnPoint m_currentSpawnPoint;

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
        int _randomIndex = Random.Range(0, spawnLocations.Length);
        m_currentSpawnPoint = spawnLocations[_randomIndex];

        m_currentSpawnPoint.Spawn(gameObject);
    }

    [ContextMenu("Leave")]
    public void Leave()
    {
        m_currentSpawnPoint.Leave(gameObject);
    }
}
