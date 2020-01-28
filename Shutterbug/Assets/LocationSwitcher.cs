using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitcher : MonoBehaviour
{
    public SpawnPoint[] spawnLocations;

    [ContextMenu("Respawn")]
    public void Respawn()
    {
        spawnLocations[0].Spawn(gameObject);
    }

    [ContextMenu("Despawn")]
    public void Despawn()
    {
        spawnLocations[0].Spawn(gameObject);
    }
}
