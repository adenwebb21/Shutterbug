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

    public GameObject proofLayoutGroup;
    public GameObject scoreLayoutGroup;

    public GameObject proofImagePrefab;
    public GameObject scoreTextPrefab;

    private int m_currentPhotoIndex = -1;
    public int baseScore = 50;
    public int inFrameScore = 50;

    private int m_currentScore;

    private string[] m_randomAdditions;

    private bool m_showingProofs = true;
    private bool m_scoringSighting = true;

    private void Start()
    {
        m_randomAdditions = new string[] { "Ghost in frame: ", "Faced camera in the right direction: ", "Nice exposure: ", "Nailed the focus: "};
    }

    public void ReturnToMenu()
    {
        //int _highestScore = 0;

        //for (int i = 0; i < m_scores.Length; i++)
        //{
        //    if (m_scores[i] > _highestScore)
        //    {
        //        _highestScore = m_scores[i];
        //        m_bestPhoto = chosenProofs[i];
        //    }
        //}

        if(GameManager.Instance.bestPhotoScore < m_currentScore)
        {
            m_bestPhoto = chosenSighting;
            GameManager.Instance.bestPhotoScore = m_currentScore;
            GameManager.Instance.bestPhoto = m_bestPhoto.GetComponent<PhotoData>().photoData;           
        }

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
    }

    public void AnimatePhotoOut()
    {
        currentPhotoObject.GetComponent<Animator>().Play("photo_out");

        if(m_currentPhotoIndex != 2)
        {
            Invoke("ScoreNextPhoto", 1f);
        }
        
    }

    //public void ScoreNextPhoto()
    //{
    //    m_currentPhotoIndex++;

    //    currentPhotoObject.GetComponent<Image>().sprite = chosenProofs[m_currentPhotoIndex].GetComponent<PhotoData>().photoData.Image;
    //    currentPhotoObject.GetComponent<Animator>().Play("photo_in");
    //    Invoke("InitialScore", 1f);             
    //}

    public void ViewSighting()
    {
        currentPhotoObject.GetComponent<Image>().sprite = chosenSighting.GetComponent<PhotoData>().photoData.Image;
        currentPhotoObject.GetComponent<Animator>().Play("photo_in");

        Invoke("StartScoringSighting", 1f);
    }

    private void DelayedProof(bool _valid)
    {
        StartCoroutine(Proof(_valid));
    }

    IEnumerator Proof(bool _valid)
    {
        int _proofCount = 0;
        List<string> _seenProofs = new List<string>();

        while(m_showingProofs && _proofCount < chosenProofs.Count)
        {
            // animate proof photo in
            GameObject _proofImage = Instantiate(proofImagePrefab, proofLayoutGroup.transform);
            _proofImage.GetComponent<Image>().sprite = chosenProofs[_proofCount].GetComponent<PhotoData>().photoData.Image;

            int _timesProofSeen = 0;

            foreach(string _name in _seenProofs)
            {
                if(_name == chosenProofs[_proofCount].GetComponent<PhotoData>().photoData.ProofName)
                {
                    _timesProofSeen++;
                }
            }

            // add money + comments
            if(chosenProofs[_proofCount].GetComponent<PhotoData>().photoData.ProofInPicture && chosenProofs[_proofCount].GetComponent<PhotoData>().photoData.ProofCryptid == GameManager.Instance.cryptidEnum && _valid && _timesProofSeen == 0)
            {
                GameObject _scoreText = Instantiate(scoreTextPrefab, scoreLayoutGroup.transform);
                _scoreText.GetComponent<TextMeshProUGUI>().SetText("Relevant proof: 1.5x");
                m_currentScore = Mathf.RoundToInt(m_currentScore * 1.5f);
                scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();

                _seenProofs.Add(chosenProofs[_proofCount].GetComponent<PhotoData>().photoData.ProofName);
            }

            _proofCount++;
            yield return new WaitForSeconds(1f);
        }

        NextButton();
    }

    IEnumerator Sighting()
    {
        int _check = 0;
        while (m_scoringSighting)
        {
            GameObject _scoreText = Instantiate(scoreTextPrefab, scoreLayoutGroup.transform);

            switch (_check)
            {
                case 0:
                    if (chosenSighting.GetComponent<PhotoData>().photoData.CryptidInPicture)
                    {
                        _scoreText.GetComponent<TextMeshProUGUI>().SetText("Cryptid was in the picture: " + inFrameScore.ToString() + " pts");
                        m_currentScore += inFrameScore;
                        scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
                    }
                    else
                    {
                        Destroy(_scoreText);
                    }                       
                    break;

                case 1:
                    int _temp = ((int)(Random.Range(30, 70) / 10)) * 10;
                    m_currentScore += _temp;
                    scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
                    _scoreText.GetComponent<TextMeshProUGUI>().text = m_randomAdditions[Random.Range(0, 2)] + _temp.ToString() + " pts";
                    break;

                case 2:
                    _temp = ((int)(Random.Range(30, 70) / 10)) * 10;
                    m_currentScore += _temp;
                    scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
                    _scoreText.GetComponent<TextMeshProUGUI>().text = m_randomAdditions[Random.Range(2, 4)] + _temp.ToString() + " pts";
                    break;

                default:
                    m_scoringSighting = false;
                    Destroy(_scoreText);
                    break;
            }

            _check++;
            yield return new WaitForSeconds(1f);
        }

        if (chosenSighting.GetComponent<PhotoData>().photoData.CryptidInPicture)
            DelayedProof(true);
        else
            DelayedProof(false);
    }

    private void StartScoringSighting()
    {
        StartCoroutine(Sighting());
    }

    //private void InitialScore()
    //{
    //    scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + baseScore.ToString();
    //    m_currentScore = baseScore;
    //    scoreValue.SetActive(true);

    //    Invoke("Mod1", 1f);
    //}

    //private void Mod1()
    //{
    //    if(chosenProofs[m_currentPhotoIndex].GetComponent<PhotoData>().photoData.CryptidInPicture)
    //    {
    //        modifier1.GetComponent<TextMeshProUGUI>().text = "Cryptid was in the picture: " + inFrameScore.ToString() + " pts";
    //        m_currentScore += inFrameScore;
    //        scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
    //        modifier1.SetActive(true);
    //    }

    //    Invoke("Mod2", 1f);
    //}

    //private void Mod2()
    //{
    //    int _temp = ((int)(Random.Range(30, 70) / 10)) * 10;
    //    m_currentScore += _temp;
    //    scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
    //    modifier2.GetComponent<TextMeshProUGUI>().text = m_randomAdditions[Random.Range(0, 2)] + _temp.ToString() + " pts";
    //    modifier2.SetActive(true);

    //    Invoke("Mod3", 1f);
    //}

    //private void Mod3()
    //{
    //    int _temp = ((int)(Random.Range(30, 70) / 10)) * 10;
    //    m_currentScore += _temp;
    //    scoreValue.GetComponent<TextMeshProUGUI>().text = "Score: " + m_currentScore.ToString();
    //    modifier3.GetComponent<TextMeshProUGUI>().text = m_randomAdditions[Random.Range(2, 4)] + _temp.ToString() + " pts";
    //    modifier3.SetActive(true);

    //    Invoke("NextButton", 1f);
    //}

    private void NextButton()
    {
        finishButton.SetActive(true);
    }
}
