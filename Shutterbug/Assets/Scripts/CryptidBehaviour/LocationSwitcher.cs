using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitcher : MonoBehaviour
{
    public List<SpawnPoint> spawnLocations;
    private GameObject[] m_spawns;
    private GameObject m_initialSpawn;
    private CryptidProperties m_propertyBlock;
    private SpawnPoint m_currentSpawnPoint;

    private int m_currentIndex = 0;

    private int m_moveCount = -1;

    public SpawnPoint CurrentSpawnPoint { get => m_currentSpawnPoint; set => m_currentSpawnPoint = value; }

    private void Start()
    {
        m_moveCount = -1;

        m_propertyBlock = gameObject.GetComponent<CryptidProperties>();

        m_spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
        m_initialSpawn = GameObject.FindGameObjectWithTag("initialSpawn");

        foreach (GameObject _spawn in m_spawns)
        {
            spawnLocations.Add(_spawn.GetComponent<SpawnPoint>());
        }

        GameManager.Instance.AssignProofs();
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
        m_moveCount++;

        if(m_moveCount > 0)
            UIManager.Instance.AddTimeFled();

        if(m_moveCount >= m_propertyBlock.stats.MaxFleeCount)
        {
            if (!m_propertyBlock.stats.KnownFleeCount)
            {
                m_propertyBlock.stats.KnownFleeCount = true;
                m_propertyBlock.stats.KnownFleeCountThisRound = true;
            }

            LeavePermanent();
        }
        else
        {
            m_propertyBlock.currentState = CryptidProperties.cryptidState.MOVING;

            // Choose point based on preference
            int _randomIndex = 0;

            while (GameManager.Instance.currentPlayerArea == spawnLocations[_randomIndex].spawnRegion.ToString().ToLower() || _randomIndex == m_currentIndex || /*spawnLocations[_randomIndex].spawnRegion == spawnLocations[m_currentIndex].spawnRegion ||*/ !m_propertyBlock.stats.PreferredRegions.Contains(spawnLocations[_randomIndex].spawnRegion))
            {
                //
                _randomIndex = Random.Range(0, spawnLocations.Count);
            }

            m_currentIndex = _randomIndex;

            m_currentSpawnPoint = spawnLocations[m_currentIndex];
            m_currentSpawnPoint.Spawn(gameObject);
            m_propertyBlock.currentState = CryptidProperties.cryptidState.SEARCHING;
        }      
    }

    public void Leave()
    {
        m_propertyBlock.currentState = CryptidProperties.cryptidState.MOVING;

        m_currentSpawnPoint.Leave(gameObject);
        Invoke("Respawn", 1.5f);
    }

    private void LeavePermanent()
    {
        UIManager.Instance.ExitPrompt();
        gameObject.transform.position = m_initialSpawn.transform.position;
    }
}
