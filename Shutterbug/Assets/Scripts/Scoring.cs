using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{
    public List<GameObject> chosenProofs;
    public GameObject chosenSighting;
    public GameObject currentPhotoObject;

    private GameObject m_bestPhoto;

    public GameObject scoreValue, modifier1, modifier2, modifier3, nextPhoto, finishButton;

    private int m_currentPhotoIndex = -1;
    public int baseScore = 50;
    public int inFrameScore = 50;

    private int m_currentScore;

    private string[] m_randomAdditions;
    private int[] m_scores = new int[3];

    private void Start()
    {
        m_randomAdditions = new string[] { "Ghost in frame: ", "Faced camera in the right direction: ", "Nice exposure: ", "Nailed the focus: "};
    }

    public void ReturnToMenu()
    {
        int _highestScore = 0;

        for (int i = 0; i < m_scores.Length; i++)
        {
            if (m_scores[i] > _highestScore)
            {
                _highestScore = m_scores[i];
                m_bestPhoto = chosenProofs[i];
            }
        }

        GameManager.Instance.bestPhotoScore = _highestScore;
        GameManager.Instance.bestPhoto = m_bestPhoto.GetComponent<PhotoData>().photoData;
        GameManager.Instance.levelLoader.LoadMainMenu();     
    }

    public void ResetValues()
    {
        scoreValue.SetActive(false);
        modifier1.SetActive(false);
        modifier2.SetActive(false);
        modifier3.SetActive(false);
        finishButton.SetActive(false);
        nextPhoto.SetActive(false);

        m_scores[m_currentPhotoIndex] = m_currentScore;
        m_currentScore = 0;
    }

    public void AnimatePhotoOut()
    {
        currentPhotoObject.GetComponent<Animator>().Play("photo_out");

        if(m_currentPhotoIndex != 2)
        {
            Invoke("ScoreNextPhoto", 1f);
        }
        
    }

    public void ScoreNextPhoto()
    {
        m_currentPhotoIndex++;

        currentPhotoObject.GetComponent<Image>().sprite = chosenProofs[m_currentPhotoIndex].GetComponent<PhotoData>().photoData.Image;
        currentPhotoObject.GetComponent<Animator>().Play("photo_in");
        Invoke("InitialScore", 1f);             
    }

    private void InitialScore()
    {
        scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + baseScore.ToString();
        m_currentScore = baseScore;
        scoreValue.SetActive(true);

        Invoke("Mod1", 1f);
    }

    private void Mod1()
    {
        if(chosenProofs[m_currentPhotoIndex].GetComponent<PhotoData>().photoData.CryptidInPicture)
        {
            modifier1.GetComponent<TextMeshProUGUI>().text = "Cryptid was in the picture: " + inFrameScore.ToString() + " pts";
            m_currentScore += inFrameScore;
            scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
            modifier1.SetActive(true);
        }

        Invoke("Mod2", 1f);
    }

    private void Mod2()
    {
        int _temp = ((int)(Random.Range(30, 70) / 10)) * 10;
        m_currentScore += _temp;
        scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
        modifier2.GetComponent<TextMeshProUGUI>().text = m_randomAdditions[Random.Range(0, 2)] + _temp.ToString() + " pts";
        modifier2.SetActive(true);

        Invoke("Mod3", 1f);
    }

    private void Mod3()
    {
        int _temp = ((int)(Random.Range(30, 70) / 10)) * 10;
        m_currentScore += _temp;
        scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
        modifier3.GetComponent<TextMeshProUGUI>().text = m_randomAdditions[Random.Range(2, 4)] + _temp.ToString() + " pts";
        modifier3.SetActive(true);

        Invoke("NextButton", 1f);
    }

    private void NextButton()
    {
        if(m_currentPhotoIndex == chosenProofs.Count - 1)
        {
            finishButton.SetActive(true);
        }
        else
        {
            nextPhoto.SetActive(true);
        }     
    }
}
