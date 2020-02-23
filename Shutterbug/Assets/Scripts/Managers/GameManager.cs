using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager s_instance;

    public static GameManager Instance { get => s_instance; set => s_instance = value; }
    #endregion

    public GameObject cryptidPrefab;
    public GameObject currentCryptid;

    public List<Photograph> currentPhotographs;

    public Photograph bestPhoto;
    public int bestPhotoScore;

    public int photoCap = 5;
    private int m_currentPhotoCount = 0;

    public LevelLoader levelLoader;

    public GameEvent enoughPhotos;

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

    public void AssignCryptid(GameObject _chosenCryptid)
    {
        cryptidPrefab = _chosenCryptid;
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

    public void ResetPhotos()
    {
        m_currentPhotoCount = 0;
        currentPhotographs.Clear();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
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

