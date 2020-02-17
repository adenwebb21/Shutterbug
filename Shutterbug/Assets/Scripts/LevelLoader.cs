using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    private void Start()
    {
        GameManager.Instance.levelLoader = this.GetComponent<LevelLoader>();
    }

    public void LoadNextLevel()
    {
        int _buildIndex;

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }    
    }
    public void LoadHandInScreen()
    {
        int _buildIndex;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadLevel(2));
        }
    }

    IEnumerator LoadLevel(int _levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_levelIndex);
    }

    
}
