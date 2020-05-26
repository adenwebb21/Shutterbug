using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum cryptid { mosstop, red, blue };

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager s_instance;

    public static GameManager Instance { get => s_instance; set => s_instance = value; }
    #endregion

    public int currentBuildIndex;

    public GameObject cryptidPrefab;
    public GameObject currentCryptid;
    public cryptid cryptidEnum;

    public List<Cryptid> cryptidStats;

    public List<Photograph> currentPhotographs;

    public List<GameObject> proofsInWorld;

    public Photograph bestPhoto;
    public int bestPhotoScore;

    public int photoCap = 10;
    private int m_currentPhotoCount = 0;
    private int m_currentProofCount = 0;

    public LevelLoader levelLoader;

    public GameEvent enoughPhotos;

    private float m_spawnChance = 0f;
    private bool m_cryptidSpawned = false;

    public GameObject initialSpawner;

    public string currentPlayerArea;

    void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            s_instance = this;
        }
    }

    public void AssignProofs()
    {
        GameObject[] _temp = GameObject.FindGameObjectsWithTag("Proof");

        for (int i = 0; i < _temp.Length; i++)
        {
            proofsInWorld.Add(_temp[i]);
        }
    }

    public void AssignCryptid(GameObject _chosenCryptid)
    {
        cryptidPrefab = _chosenCryptid;
        cryptidEnum = cryptid.mosstop;

    }

    public void TakePhoto()
    {
        if (m_currentPhotoCount < photoCap)
        {
            m_currentPhotoCount++;
            UIManager.Instance.UpdatePictureCount(m_currentPhotoCount);

            if(m_currentPhotoCount == photoCap)
            {
                enoughPhotos.Raise();
            }
        }
        else
        {
            Debug.Log("Out of film");
        }
    }

    public void UpdateProofs()
    {
        m_currentProofCount++;
        UIManager.Instance.UpdateProofCount(m_currentProofCount);

        float _check = Random.Range(0f, 100f);
        if(_check < m_spawnChance && !m_cryptidSpawned)
        {
            currentCryptid.GetComponent<LocationSwitcher>().Respawn();
            UIManager.Instance.EntryPrompt();
            m_cryptidSpawned = true;
        }

        m_spawnChance = Mathf.Clamp(m_spawnChance + 25, 0f, 100f);
    }

    public void ResetPhotos()
    {
        m_currentPhotoCount = 0;
        m_currentProofCount = 0;
        m_spawnChance = 0f;
        m_cryptidSpawned = false;
        currentPhotographs.Clear();
        proofsInWorld.Clear();
    }

    private void Update()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentBuildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (currentBuildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            levelLoader.LoadHandInScreen();
        }
    }
}

