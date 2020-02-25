using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public LocationSwitcher locationSwitcher;

    public void Respawn()
    {
        locationSwitcher.Respawn();
    }
}
